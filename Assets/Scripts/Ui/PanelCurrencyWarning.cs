using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelCurrencyWarning : AahMonoBehaviour
	{
		public static PanelCurrencyWarning.PopupType GetPopupTypeForCurrency(CurrencyType currencyType)
		{
			switch (currencyType)
			{
			case CurrencyType.SCRAP:
				return PanelCurrencyWarning.PopupType.SCRAPS;
			case CurrencyType.MYTHSTONE:
				return PanelCurrencyWarning.PopupType.MYTHSTONES;
			case CurrencyType.GEM:
				return PanelCurrencyWarning.PopupType.GEMS;
			case CurrencyType.TOKEN:
				return PanelCurrencyWarning.PopupType.TOKENS;
			case CurrencyType.AEON:
				return PanelCurrencyWarning.PopupType.AEONS;
			case CurrencyType.CANDY:
				return PanelCurrencyWarning.PopupType.CANDIES;
			default:
				throw new NotImplementedException("Currency type not available in currency warning popup " + currencyType);
			}
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.messageBotDefaultPos = this.messageBottom.rectTransform.anchoredPosition.y;
		}

		public void InitStrings()
		{
			this.buttonOk.text.text = LM.Get("UI_CLOSE");
		}

		public void SetCurrency(PanelCurrencyWarning.PopupType pt, UiManager uiManager, string extraString = "", int spriteIndex = 0, PanelCurrencyWarning.RedirectInfo redirectInfo = null)
		{
			this.imageQuestionMark.gameObject.SetActive(pt == PanelCurrencyWarning.PopupType.EVOLVE);
			this.buttonClose.gameObject.SetActive(redirectInfo != null);
			this.buttonOk.text.text = LM.Get((redirectInfo != null) ? redirectInfo.buttonMessageKey : "UI_CLOSE");
			this.redirectButtonClickedCallback = ((redirectInfo != null) ? redirectInfo.callback : null);
			this.messageTop.enabled = false;
			this.imageReward.gameObject.SetActive(false);
			this.cartWidget.gameObject.SetActive(false);
			this.unlockArtifactSlot.SetActive(false);
			this.unlockMythicalArtifactSlot.SetActive(false);
			this.messageMidleParent.gameObject.SetActive(false);
			this.headerPlate.color = this.normalHeaderPlateColor;
			this.messageBottom.rectTransform.SetAnchorPosY(this.messageBotDefaultPos);
			if (pt == PanelCurrencyWarning.PopupType.MYTHSTONES)
			{
				this.textTitle.text = LM.Get("UI_WARNING_MYTHSTONES");
				this.messageBottom.text = LM.Get("UI_WARNING_MYTHSTONES_DESC");
				this.cartWidget.SetCurrency(CurrencyType.MYTHSTONE);
				this.cartWidget.gameObject.SetActive(true);
			}
			else if (pt == PanelCurrencyWarning.PopupType.NO_ARTIFACT_SLOTS)
			{
				this.textTitle.text = LM.Get("UI_NOSLOTS_HEADER");
				this.messageBottom.text = LM.Get("UI_NOSLOTS_BODY");
				this.unlockArtifactSlot.SetActive(true);
				this.unlockArtifactSlotCost.text = GameMath.GetDoubleString(uiManager.sim.artifactsManager.GetSlotCost());
				this.messageBottom.rectTransform.SetAnchorPosY(144f);
			}
			else if (pt == PanelCurrencyWarning.PopupType.NO_MYTHICAL_ARTIFACT_SLOTS)
			{
				this.textTitle.text = LM.Get("UI_NOSLOTS_HEADER");
				Unlock nextMythicalSlotUnlock = uiManager.sim.GetNextMythicalSlotUnlock();
				this.messageBottom.text = "UI_NOSLOTSMYTHICAL_BODY".LocFormat(nextMythicalSlotUnlock.GetReqInt().ToString());
				this.unlockMythicalArtifactSlot.SetActive(true);
				this.messageBottom.rectTransform.SetAnchorPosY(144f);
			}
			else if (pt == PanelCurrencyWarning.PopupType.SCRAPS)
			{
				this.textTitle.text = LM.Get("UI_WARNING_SCRAPS");
				this.messageBottom.text = LM.Get("UI_WARNING_SCRAPS_DESC");
				this.cartWidget.SetCurrency(CurrencyType.SCRAP);
				this.cartWidget.gameObject.SetActive(true);
			}
			else if (pt == PanelCurrencyWarning.PopupType.EVOLVE)
			{
				this.textTitle.text = LM.Get("UI_WARNING_EVOLVE");
				this.messageBottom.text = string.Format(LM.Get("UI_WARNING_EVOLVE_DESC"), extraString);
				this.imageReward.sprite = this.spritesRewardEvolve[spriteIndex];
				this.imageReward.gameObject.SetActive(true);
				this.imageReward.SetNativeSize();
			}
			else if (pt == PanelCurrencyWarning.PopupType.TOKENS)
			{
				this.textTitle.text = LM.Get("UI_WARNING_TOKENS");
				this.messageBottom.text = LM.Get("UI_WARNING_TOKENS_DESC");
				this.cartWidget.SetCurrency(CurrencyType.TOKEN);
				this.cartWidget.gameObject.SetActive(true);
			}
			else if (pt == PanelCurrencyWarning.PopupType.AEONS)
			{
				this.textTitle.text = LM.Get("UI_WARNING_AEON");
				this.messageBottom.text = LM.Get("UI_WARNING_AEON_DESC");
				this.cartWidget.SetCurrency(CurrencyType.AEON);
				this.cartWidget.gameObject.SetActive(true);
			}
			else if (pt == PanelCurrencyWarning.PopupType.GEMS)
			{
				this.textTitle.text = LM.Get("UI_WARNING_GEMS");
				this.messageBottom.text = LM.Get("UI_WARNING_GEMS_DESC");
				this.cartWidget.SetCurrency(CurrencyType.GEM);
				this.cartWidget.gameObject.SetActive(true);
			}
			else
			{
				if (pt != PanelCurrencyWarning.PopupType.CANDIES)
				{
					throw new NotImplementedException();
				}
				this.textTitle.text = LM.Get("UI_WARNING_CANDIES");
				this.messageBottom.text = LM.Get("UI_WARNING_CANDIES_DESC");
				this.imageReward.sprite = null;
				this.imageReward.gameObject.SetActive(true);
				this.imageReward.SetNativeSize();
			}
		}

		public GameButton buttonOk;

		public GameButton buttonClose;

		public GameButton buttonCloseBg;

		public Text textTitle;

		public Text messageBottom;

		public Text messageTop;

		public RectTransform messageMidleParent;

		public Text messageMidle;

		public Image messageMidleImage;

		public Image imageBg0;

		public Image imageReward;

		public Image imageQuestionMark;

		public CartWidget cartWidget;

		public Sprite[] spritesRewardEvolve;

		public GameObject unlockArtifactSlot;

		public Text unlockArtifactSlotCost;

		public GameObject unlockMythicalArtifactSlot;

		public Image headerPlate;

		public Color normalHeaderPlateColor;

		public Color artifactEvolvePlateColor;

		public Action redirectButtonClickedCallback;

		[NonSerialized]
		public UiState previousState;

		private float messageBotDefaultPos;

		public enum PopupType
		{
			MYTHSTONES,
			NO_ARTIFACT_SLOTS,
			SCRAPS,
			EVOLVE,
			TOKENS,
			AEONS,
			GEMS,
			CANDIES,
			NO_MYTHICAL_ARTIFACT_SLOTS
		}

		public class RedirectInfo
		{
			public Action callback;

			public string buttonMessageKey;
		}
	}
}
