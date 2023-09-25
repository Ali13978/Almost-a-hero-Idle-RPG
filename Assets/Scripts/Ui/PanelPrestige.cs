using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelPrestige : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.posBgPrestige = this.bgPrestige.anchoredPosition;
			this.InitStrings();
			this.buttonMoreInfo.onClick = new GameButton.VoidFunc(this.OpenBigInfo);
			this.buttonLessInfo.onClick = new GameButton.VoidFunc(this.CloseBigInfo);
			this.textAmountPrestige.rectTransform.SetSizeDeltaX(140f);
			this.textAmountMegaPrestige.rectTransform.SetSizeDeltaX(140f);
			this.SetSmallInfo();
		}

		public override void AahUpdate(float dt)
		{
			this.timer += dt;
			float num = this.warningButtonCurve.Evaluate(this.timer / 3f);
			float num2 = this.warningButtonCurve2.Evaluate(this.timer / 3f);
			this.buttonMoreInfo.transform.localScale = Vector3.one + new Vector3(num, num, 1f);
			this.buttonMoreInfo.transform.eulerAngles = new Vector3(0f, 0f, -num2 * 20f);
		}

		public void InitStrings()
		{
			this.textTitle.text = LM.Get("UI_PRESTIGE_HEADER");
			this.textTimePlayed.text = LM.Get("UI_PRESTIGE_TIME_PLAYED");
			this.textStage.text = LM.Get("UI_PRESTIGE_STAGE");
			this.buttonPrestige.text.text = LM.Get("UI_PRESTIGE");
			this.textMythstoneInfos[0].text = LM.Get("UI_PRESTIGE_REWARD");
			this.textMythstoneInfos[1].text = LM.Get("UI_PRESTIGE_ARTIFACT");
			this.buttonMegaPrestige.textUp.text = GameMath.GetDoubleString(100.0);
			this.buttonMegaPrestige.textDown.text = LM.Get("UI_MEGA_PRESTIGE");
			this.textWhenPrestigeYouLL.text = LM.Get("UI_PRESTIGE_SHORT_DESC");
			this.textRestart.text = LM.Get("UI_PRESTIGE_YOU_WILL_RESTART");
			this.textGetMythstones.text = LM.Get("UI_PRESTIGE_YOU_GET_MYTH");
			this.prestigeInfoHeader.text = LM.Get("UI_PRESTIGE_INFO_HEADER");
			this.prestigeInfoYouKeepEverything.text = LM.Get("UI_PRESTIGE_YOU_KEEP");
			this.prestigeInfoButYouReset.text = LM.Get("UI_PRESTIGE_YOU_RESTART");
			this.prestigeInfoResetElemets.text = string.Concat(new string[]
			{
				LM.Get("UI_PRESTIGE_STAGE_PROGRESS"),
				"\n",
				LM.Get("UI_PRESTIGE_HERO_LEVELS"),
				"\n",
				LM.Get("UI_PRESTIGE_GOLD")
			});
			this.prestigeInfoWhyPrestige.text = LM.Get("UI_PRESTIGE_WHY");
			this.prestigeInfoYouWillGetMyth.text = LM.Get("UI_PRESTIGE_YOU_WILL_GET_MYTH");
			this.prestigeInfoLastInfo.text = LM.Get("UI_PRESTIGE_INFO_DESC");
			this.lastRunHeader.text = LM.Get("UI_PRESTIGE_LASTRUN_HEADER");
			this.lastRunTimePlayed.text = LM.Get("UI_PRESTIGE_TIME_PLAYED");
			this.lastRunStage.text = LM.Get("UNLOCK_REQ_LEAST_STAGE");
			this.lastRunMythstoneEarned.text = LM.Get("UI_PRESTIGE_MYTHSTONE_EARNED");
		}

		private void OpenBigInfo()
		{
			this.buttonMoreInfo.gameObject.SetActive(false);
			Sequence sequence = DOTween.Sequence();
			this.bigCanvasGroup.gameObject.SetActive(true);
			this.bgWarningParent.gameObject.SetActive(true);
			sequence.Append(this.smallCanvasGroup.DOFade(0f, 0.2f));
			sequence.Join(this.bgWarningParent.DOSizeDelta(new Vector2(695f, 1120f), 0.2f, false).SetEase(Ease.InOutCirc));
			sequence.Join(this.popupRect.DOSizeDelta(new Vector2(this.popupRect.sizeDelta.x, this.bigHeight), 0.2f, false));
			sequence.Join(this.bgWarningParent.DOAnchorPosY(-105f, 0.2f, false));
			sequence.InsertCallback(0.1f, delegate
			{
				this.header1.gameObject.SetActive(false);
				this.header2.gameObject.SetActive(true);
			});
			sequence.Insert(0.1f, this.bigCanvasGroup.DOFade(1f, 0.2f));
			sequence.OnComplete(delegate
			{
				this.smallCanvasGroup.gameObject.SetActive(false);
			});
			sequence.Play<Sequence>();
		}

		private void CloseBigInfo()
		{
			Sequence sequence = DOTween.Sequence();
			if (!this.isShowingLastRunInfo || this.isShowingSmallWarningInfo)
			{
				this.smallCanvasGroup.gameObject.SetActive(true);
			}
			sequence.Append(this.bigCanvasGroup.DOFade(0f, 0.1f));
			if (this.isShowingLastRunInfo && !this.isShowingSmallWarningInfo)
			{
				sequence.Join(this.bgWarningParent.DOSizeDelta(new Vector2(755f, 0f), 0.2f, false).SetEase(Ease.OutSine));
				sequence.Join(this.popupRect.DOSizeDelta(new Vector2(this.popupRect.sizeDelta.x, this.smallHeight), 0.2f, false));
			}
			else
			{
				if (this.isShowingSmallWarningInfo && this.isShowingLastRunInfo)
				{
					sequence.Join(this.bgWarningParent.DOAnchorPosY(6f, 0.2f, false));
					sequence.Join(this.popupRect.DOSizeDelta(new Vector2(this.popupRect.sizeDelta.x, this.evenBigHeight), 0.2f, false));
				}
				else
				{
					sequence.Join(this.bgWarningParent.DOAnchorPosY(-105f, 0.2f, false));
					sequence.Join(this.popupRect.DOSizeDelta(new Vector2(this.popupRect.sizeDelta.x, this.smallWithoutHeight), 0.2f, false));
				}
				sequence.Join(this.bgWarningParent.DOSizeDelta(new Vector2(755f, 399f), 0.2f, false).SetEase(Ease.OutBack));
			}
			sequence.InsertCallback(0.1f, delegate
			{
				this.header1.gameObject.SetActive(true);
				this.header2.gameObject.SetActive(false);
			});
			sequence.Insert(0.1f, this.smallCanvasGroup.DOFade(1f, 0.2f));
			sequence.OnComplete(delegate
			{
				this.bigCanvasGroup.gameObject.SetActive(false);
				if (this.isShowingLastRunInfo && !this.isShowingSmallWarningInfo)
				{
					this.bgWarningParent.gameObject.SetActive(false);
				}
				this.buttonMoreInfo.gameObject.SetActive(true);
			});
			sequence.Play<Sequence>();
		}

		public void SetBigInfo()
		{
			this.buttonMoreInfo.gameObject.SetActive(false);
			this.header1.gameObject.SetActive(false);
			this.header2.gameObject.SetActive(true);
			this.bgWarningParent.SetSizeDeltaY(1120f);
			this.bgWarningParent.SetSizeDeltaX(695f);
			this.bigCanvasGroup.gameObject.SetActive(true);
			this.bigCanvasGroup.alpha = 1f;
			this.smallCanvasGroup.gameObject.SetActive(false);
			this.smallCanvasGroup.alpha = 0f;
		}

		public void SetSmallInfo()
		{
			this.buttonMoreInfo.gameObject.SetActive(true);
			this.header1.gameObject.SetActive(true);
			this.header2.gameObject.SetActive(false);
			this.bgWarningParent.SetSizeDeltaY(399f);
			this.bgWarningParent.SetSizeDeltaX(755f);
			this.bigCanvasGroup.gameObject.SetActive(false);
			this.bigCanvasGroup.alpha = 0f;
			this.smallCanvasGroup.gameObject.SetActive(true);
			this.smallCanvasGroup.alpha = 1f;
		}

		private void Update()
		{
			if (this.setLastRun)
			{
				this.setLastRun = false;
				this.SetWithLastRunInfo();
			}
			if (this.openBigInfo)
			{
				this.openBigInfo = false;
				this.OpenBigInfo();
			}
			if (this.closeBigInfo)
			{
				this.closeBigInfo = false;
				this.CloseBigInfo();
			}
		}

		public void SetPreLastRunInfo()
		{
			this.isShowingSmallWarningInfo = true;
			this.isShowingLastRunInfo = true;
			this.popupRect.SetSizeDeltaY(this.evenBigHeight);
			this.buttonsParent.SetAnchorPosY(this.buttonsYPosSmall);
			this.paperBackground.SetBottomDelta(this.paperSmallBottomDelta);
			this.smallCanvasGroup.gameObject.SetActive(true);
			this.bgWarningParent.gameObject.SetActive(true);
			this.lastRunParent.gameObject.SetActive(true);
			this.bgWarningParent.SetAnchorPosY(6f);
		}

		public void SetWithLastRunInfo()
		{
			this.isShowingSmallWarningInfo = false;
			this.isShowingLastRunInfo = true;
			this.popupRect.SetSizeDeltaY(this.smallHeight);
			this.buttonsParent.SetAnchorPosY(this.buttonsYPosSmall);
			this.paperBackground.SetBottomDelta(this.paperSmallBottomDelta);
			this.smallCanvasGroup.gameObject.SetActive(false);
			this.bgWarningParent.gameObject.SetActive(false);
			this.lastRunParent.gameObject.SetActive(true);
			this.bgWarningParent.SetAnchorPosY(-150f);
		}

		public void SetWithoutLastRunInfo()
		{
			this.isShowingSmallWarningInfo = true;
			this.isShowingLastRunInfo = false;
			this.popupRect.SetSizeDeltaY(this.smallWithoutHeight);
			this.buttonsParent.SetAnchorPosY(this.buttonsYPosBig);
			this.paperBackground.SetBottomDelta(this.paperBigBottomDelta);
			this.smallCanvasGroup.gameObject.SetActive(true);
			this.bgWarningParent.gameObject.SetActive(true);
			this.lastRunParent.gameObject.SetActive(false);
			this.bgWarningParent.SetAnchorPosY(-110f);
		}

		public GameButton buttonPrestige;

		public GameButton backgroundButton;

		public GameButton buttonCancel;

		public GameButton buttonMoreInfo;

		public GameButton buttonLessInfo;

		public ButtonUpgradeAnim buttonMegaPrestige;

		public Text textTitle;

		public Text[] textMythstoneInfos;

		public Text[] textMythstoneAmounts;

		public Text textTimePlayed;

		public Text textTimePlayedAmount;

		public Text textStage;

		public Text textStageAmount;

		public Text textWhenPrestigeYouLL;

		public Text textRestart;

		public Text textGetMythstones;

		public Text textAmountPrestige;

		public Text textAmountMegaPrestige;

		public RectTransform bgPrestige;

		[HideInInspector]
		public Vector2 posBgPrestige;

		public RectTransform bgMegaPrestige;

		public RectTransform bgWarningParent;

		public CanvasGroup smallCanvasGroup;

		public RectTransform header1;

		public RectTransform header2;

		public CanvasGroup bigCanvasGroup;

		public Text prestigeInfoHeader;

		public Text prestigeInfoYouKeepEverything;

		public Text prestigeInfoButYouReset;

		public Text prestigeInfoResetElemets;

		public Text prestigeInfoWhyPrestige;

		public Text prestigeInfoYouWillGetMyth;

		public Text prestigeInfoLastInfo;

		public Text prestigeInfoMythAmount;

		private float timer;

		public AnimationCurve warningButtonCurve;

		public AnimationCurve warningButtonCurve2;

		public MenuShowCurrency mythStoneCurrencyWidget;

		public MenuShowCurrency gemStoneCurrencyWidget;

		public RectTransform popupRect;

		public RectTransform buttonsParent;

		public RectTransform paperBackground;

		public RectTransform lastRunParent;

		public Text lastRunHeader;

		public Text lastRunTimePlayed;

		public Text lastRunTimePlayedAmount;

		public Text lastRunStage;

		public Text lastRunStageAmount;

		public Text lastRunMythstoneEarned;

		public Text lastRunMythstoneEarnedAmount;

		private bool isShowingLastRunInfo;

		private bool isShowingSmallWarningInfo;

		private float bigHeight = 1125f;

		private float evenBigHeight = 1225f;

		private float smallHeight = 925f;

		private float smallWithoutHeight = 1060f;

		private float buttonsYPosBig = 167f;

		private float buttonsYPosSmall = 380f;

		private float paperBigBottomDelta = 25f;

		private float paperSmallBottomDelta = 246f;

		public bool setLastRun;

		public bool openBigInfo;

		public bool closeBigInfo;
	}
}
