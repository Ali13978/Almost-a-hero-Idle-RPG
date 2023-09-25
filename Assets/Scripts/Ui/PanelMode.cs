using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelMode : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.GoToMerchantItemsTab();
			this.mercanInfoParent.gameObject.SetActive(false);
			this.isMercanDealInfoShowing = false;
			this.panelMerchantParent.SetAnchorPosY(-544f);
			this.ribbonMerchantParent.SetAnchorPosY(-484.5f);
			this.mercanInfoParent.transform.localScale = new Vector3(1f, 0f, 1f);
			this.merchantItemsTabButton.onClick = new GameButton.VoidFunc(this.OnMerchantItemsTabButtonClicked);
			this.eventMerchantItemsTabButton.onClick = new GameButton.VoidFunc(this.OnEventMerchantItemsTabButtonClicked);
		}

		public void OnPanelOpened(bool merchantUnlocked, bool hasEventMerchantItems)
		{
			this.ribbonMerchantParent.SetAnchorPosY(-484.5f - ((!merchantUnlocked || !hasEventMerchantItems) ? 0f : 120f));
			this.panelMerchantParent.SetAnchorPosY(((!this.isMercanDealInfoShowing) ? -544f : -642f) - ((!merchantUnlocked || !hasEventMerchantItems) ? 0f : 120f));
		}

		public void Button_ToggleInfo(bool hasEventMerchantItems)
		{
			this.isMercanDealInfoShowing = !this.isMercanDealInfoShowing;
			if (this.isMercanDealInfoShowing)
			{
				this.mercanInfoParent.gameObject.SetActive(true);
				if (this.infoAnim != null && this.infoAnim.isPlaying)
				{
					this.infoAnim.Kill(false);
				}
				this.infoAnim = this.mercanInfoParent.transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack);
				this.panelMerchantParent.DOAnchorPosY(-642f - ((!hasEventMerchantItems) ? 0f : 120f), 0.2f, false).SetEase(Ease.OutBack);
			}
			else
			{
				if (this.infoAnim != null && this.infoAnim.isPlaying)
				{
					this.infoAnim.Kill(false);
				}
				this.infoAnim = this.mercanInfoParent.transform.DOScaleY(0f, 0.2f).SetEase(Ease.OutCirc).OnComplete(delegate
				{
					this.mercanInfoParent.gameObject.SetActive(false);
				});
				this.panelMerchantParent.DOAnchorPosY(-544f - ((!hasEventMerchantItems) ? 0f : 120f), 0.2f, false).SetEase(Ease.OutCirc);
			}
		}

		public void DisableInfo(bool hasEventMerchantItems)
		{
			if (this.isMercanDealInfoShowing)
			{
				this.isMercanDealInfoShowing = false;
				this.mercanInfoParent.gameObject.SetActive(true);
				if (this.infoAnim != null && this.infoAnim.isPlaying)
				{
					this.infoAnim.Kill(false);
				}
				this.mercanInfoParent.transform.SetScaleY(0f);
				this.panelMerchantParent.SetAnchorPosY(-544f - ((!hasEventMerchantItems) ? 0f : 120f));
			}
		}

		public void InitStrings()
		{
			this.buttonSeeAllUnlocks.text.text = LM.Get("UI_MODE_SEE_ALL");
			this.textAllUnlocksCollected.text = LM.Get("UI_MODE_ALL_COLLECTED");
			this.buttonAbandonChallenge.text.text = LM.Get("UI_MODE_ABANDON");
			this.textMerchantClosed.text = LM.Get("UI_MERCHANT_CLOSED");
			this.textMerchant.text = LM.Get("UI_MERCHANT_DEALS");
			this.buttonPrestige.text.text = LM.Get("UI_PRESTIGE");
			this.merchantItemsTabButton.text.text = LM.Get("MERCHANT_ITEM_TAB");
			this.eventMerchantItemsTabButton.text.text = LM.Get("EVENT_MERCHANT_ITEM_TAB");
			this.textMerchantInfo.text = ((!this.lookingEventMerchantItems) ? LM.Get("UI_MERCHANT_INFO") : LM.Get("CHRISTMAS_EVENT_MERCHANT_INFO"));
		}

		public void GoToMerchantItemsTab()
		{
			this.merchantItemsTabButton.fakeDisabled = true;
			this.eventMerchantItemsTabButton.fakeDisabled = false;
			this.lookingEventMerchantItems = false;
			this.textMerchantInfo.text = LM.Get("UI_MERCHANT_INFO");
		}

		private void OnMerchantItemsTabButtonClicked()
		{
			if (this.lookingEventMerchantItems)
			{
				this.GoToMerchantItemsTab();
				UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
			}
			else
			{
				UiManager.AddUiSound(SoundArchieve.inst.uiDefaultFailClick);
			}
			UiManager.stateJustChanged = true;
		}

		public void DisableShareButton()
		{
			this.shareScreenshotButton.gameObject.SetActive(false);
			this.buttonAbandonChallenge.rectTransform.SetAnchorPosX(0f);
			this.buttonAbandonChallenge.rectTransform.SetSizeDeltaX(500f);
			this.buttonPrestige.rectTransform.SetAnchorPosX(0f);
			this.buttonPrestige.rectTransform.SetSizeDeltaX(500f);
		}

		public void EnableShareButton()
		{
			this.shareScreenshotButton.gameObject.SetActive(true);
			this.buttonAbandonChallenge.rectTransform.SetAnchorPosX(73f);
			this.buttonAbandonChallenge.rectTransform.SetSizeDeltaX(405f);
			this.buttonPrestige.rectTransform.SetAnchorPosX(73f);
			this.buttonPrestige.rectTransform.SetSizeDeltaX(405f);
		}

		private void OnEventMerchantItemsTabButtonClicked()
		{
			if (this.lookingEventMerchantItems)
			{
				UiManager.AddUiSound(SoundArchieve.inst.uiDefaultFailClick);
			}
			else
			{
				this.lookingEventMerchantItems = true;
				this.merchantItemsTabButton.fakeDisabled = false;
				this.eventMerchantItemsTabButton.fakeDisabled = true;
				this.textMerchantInfo.text = LM.Get("CHRISTMAS_EVENT_MERCHANT_INFO");
				UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
			}
			UiManager.stateJustChanged = true;
		}

		public GameObject presrigeButtonParent;

		public GameButton buttonPrestige;

		public Text prestigeLabel;

		public Image imagePrestigeBg;

		public Image imagePrestigeBgDecor;

		public Image imageAllUnlocksCollected;

		public Text textAllUnlocksCollected;

		public RectTransform unlockWidgetParent;

		public PanelUnlock panelNextUnlock;

		public RectTransform panelMerchantParent;

		public GameObject panelMerchant;

		public GameObject panelMerchantClosed;

		public Text textMerchantClosed;

		public Text textMerchant;

		public GameObject mercanInfoParent;

		public Text textMerchantInfo;

		public Text textMerchantBubble;

		public MerchantItem[] merchantItems;

		public Image[] merchantItemLockeds;

		public GameButton buttonSeeAllUnlocks;

		public Text textNextUnlock;

		public GameButton buttonAbandonChallenge;

		public Sprite spriteAdventureEndModeBg;

		public Sprite spriteChallengeEndModeBg;

		public GameButton merchantItemsTabButton;

		public GameButton eventMerchantItemsTabButton;

		public GameObject merchantItemTabsParent;

		public RectTransform ribbonMerchantParent;

		public GameButton toggleInfo;

		public GameButton shareScreenshotButton;

		public Text merchantItemsMessage;

		[NonSerialized]
		public bool isMercanDealInfoShowing;

		private const float PanelMerchantDownPos = -642f;

		private const float PanelMerchantUpPos = -544f;

		private const float MerchanttRibbonPosWithoutEventItems = -484.5f;

		private const float PanelMerchantDiffPosWithEventItems = 120f;

		public static Color spriteAdventureEndModeDecorColor = Utility.HexColor("00947B");

		public static Color spriteChallengeEndModeDecorColor = Utility.HexColor("207088");

		[NonSerialized]
		public bool lookingEventMerchantItems;

		private Tweener infoAnim;
	}
}
