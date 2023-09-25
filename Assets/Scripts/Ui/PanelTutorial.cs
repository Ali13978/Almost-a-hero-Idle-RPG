using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTutorial : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.animTimer = this.animPeriod;
			this.animMoveTimer = this.animMovePeriod;
			base.gameObject.SetActive(false);
			this.isActive = false;
			this.positionRingFirstLocal = this.imageRing.transform.localPosition;
			this.positionRingFirst = this.imageRing.transform.position;
			this.imageOkayScale = this.imageOkayBg.rectTransform.localScale;
			this.animOkayTimer = 0.2f;
			this.isOkayActive = false;
			this.InitStrings();
			this.missionParent.gameObject.SetActive(false);
		}

		public void InitStrings()
		{
			this.buttonOkay.text.text = LM.Get("UI_OKAY");
			this.buttonRingOffer.text.text = LM.Get("UI_ACCEPT");
			this.claimMissionRewardButton.textDown.text = LM.Get("UI_COLLECT");
		}

		public override void AahUpdate(float dt)
		{
			if (this.animMoveTimer < this.animMovePeriod)
			{
				this.animMoveTimer += dt;
				float x = Easing.SineEaseOut(this.animMoveTimer, this.xBegin, this.xGoal - this.xBegin, this.animMovePeriod);
				float y = Easing.SineEaseOut(this.animMoveTimer, this.yBegin, this.yGoal - this.yBegin, this.animMovePeriod);
				this.rectTransform.anchoredPosition = new Vector3(x, y);
			}
			else
			{
				this.rectTransform.anchoredPosition = new Vector3(this.xGoal, this.yGoal);
			}
			if (this.animTimer <= this.animPeriod - dt)
			{
				this.animTimer += dt;
				float t = this.animTimer / this.animPeriod;
				float x2;
				float y2;
				if (this.isActive)
				{
					x2 = Easing.CubicEaseOut(t, 0f, 1f, 1f);
					y2 = Easing.BackEaseInOut(t, 0f, 1f, 1f);
				}
				else
				{
					x2 = Easing.CubicEaseOut(t, 1f, -0.5f, 1f);
					y2 = Easing.CubicEaseOut(t, 1f, -1f, 1f);
				}
				this.rectTransform.localScale = new Vector3(x2, y2, 1f);
			}
			else if (this.isActive)
			{
				this.rectTransform.localScale = new Vector3(1f, 1f, 1f);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
			if (this.animOkayTimer <= 0.2f - dt)
			{
				this.animOkayTimer += dt;
				float num = this.animOkayTimer / 0.2f;
				if (!this.isOkayActive)
				{
					num = 1f - num;
				}
				float num2 = Easing.Linear(num, 1.1f, -0.1f, 1f);
				float num3 = Easing.SineEaseOut(num, 0f, 1f, 1f);
				this.imageOkayBg.rectTransform.localScale = new Vector3(num2 * this.imageOkayScale.x, num3 * this.imageOkayScale.y, 1f);
			}
			else if (this.isOkayActive)
			{
				this.imageOkayBg.rectTransform.localScale = this.imageOkayScale;
			}
			else
			{
				this.imageOkayBg.gameObject.SetActive(false);
			}
		}

		public void OpenClose(bool isActive)
		{
			if (this.isActive != isActive)
			{
				this.animTimer = 0f;
				if (this.isActive)
				{
					this.animPeriod = 0.14f;
					this.rectTransform.localScale = new Vector3(1f, 1f, 1f);
				}
				else
				{
					this.animPeriod = 0.5f;
					this.rectTransform.localScale = new Vector3(0f, 0f, 0f);
					base.gameObject.SetActive(true);
				}
				this.isActive = isActive;
			}
		}

		public void OpenCloseOkay(bool isOkayActive)
		{
			if (this.isOkayActive != isOkayActive)
			{
				this.animOkayTimer = 0f;
				if (this.isOkayActive)
				{
					this.imageOkayBg.rectTransform.localScale = this.imageOkayScale;
				}
				else
				{
					this.imageOkayBg.rectTransform.localScale = new Vector3(0f, 0f, 0f);
					this.imageOkayBg.gameObject.SetActive(true);
				}
				this.isOkayActive = isOkayActive;
			}
		}

		public void Move(float y, float x = 64f, float bgWidth = 620f, float bgPos = 0f, float textWidth = 400f)
		{
			if (this.yGoal != y)
			{
				this.animMoveTimer = 0f;
				this.yBegin = this.rectTransform.anchoredPosition.y;
				this.yGoal = y;
			}
			if (this.xGoal != x)
			{
				this.animMoveTimer = 0f;
				this.yBegin = this.rectTransform.anchoredPosition.x;
				this.xGoal = x;
			}
			if (this.messageParent.anchoredPosition.x != bgPos)
			{
				this.messageParent.anchoredPosition = new Vector2(bgPos, this.messageParent.anchoredPosition.y);
			}
			if (this.messageParent.sizeDelta.x != bgWidth)
			{
				this.messageParent.sizeDelta = new Vector2(bgWidth, this.messageParent.sizeDelta.y);
			}
			if (this.text.rectTransform.sizeDelta.x != textWidth)
			{
				this.text.rectTransform.sizeDelta = new Vector2(textWidth, this.text.rectTransform.sizeDelta.y);
			}
		}

		public void CancelMissionAnimations()
		{
			if (this.currentMissionSequence != null)
			{
				this.currentMissionSequence.Kill(false);
			}
		}

		public void HideMission(bool forceHide = false, bool animated = true)
		{
			if (!this.missionTransitioning || forceHide)
			{
				if (animated)
				{
					this.missionProgressBlocked = true;
					this.missionTransitioning = true;
					this.currentMissionSequence = DOTween.Sequence().Append(this.missionParent.DOAnchorPosY(-170f, 0.4f, false).SetEase(Ease.InBack)).Insert(0.7f, this.missionParent.DOScaleX(0f, 0.2f).SetEase(Ease.InBack)).Insert(0.7f, this.missionCanvasGroup.DOFade(0f, 0.2f)).AppendInterval(0.5f).AppendCallback(new TweenCallback(this.OnMissionHidden)).Play<Sequence>();
				}
				else
				{
					this.OnMissionHidden();
				}
			}
		}

		public void UpdateMission(TutorialMission mission, int missionIndex, int missionsCount, bool isDoingPrestige)
		{
			if (!this.missionProgressBlocked)
			{
				bool flag = !this.missionParent.gameObject.activeSelf && !isDoingPrestige;
				if (flag)
				{
					this.missionsCount.text = string.Format(LM.Get("TUTORIAL_MISSIONS_COUNT"), missionIndex + 1, missionsCount);
					this.ShowMission(!mission.IsComplete);
				}
				this.SetMission(mission, flag, isDoingPrestige);
			}
		}

		public void ToggleMissionDescription()
		{
			if (this.currentMissionSequence != null)
			{
				this.currentMissionSequence.Kill(false);
			}
			if (this.missionDescParent.gameObject.activeSelf)
			{
				this.CollapseMission();
			}
			else
			{
				this.ExpandMission(true);
			}
		}

		public void ExpandMission(bool collapseAfterInterval)
		{
			if (this.missionDescParent.gameObject.activeSelf)
			{
				return;
			}
			this.missionTransitioning = true;
			this.missionDescParent.gameObject.SetActive(true);
			this.currentMissionSequence = DOTween.Sequence().Append(this.missionDescParent.DOScaleX(1f, 0.2f).SetEase(Ease.OutBack));
			if (collapseAfterInterval)
			{
				this.currentMissionSequence.AppendCallback(new TweenCallback(this.FinishMissionTransition)).AppendInterval(5f).AppendCallback(new TweenCallback(this.CollapseMission));
			}
			else
			{
				this.currentMissionSequence.AppendCallback(new TweenCallback(this.FinishMissionTransition));
			}
			this.currentMissionSequence.Play<Sequence>();
		}

		private void ShowMission(bool collapseAfterShow)
		{
			this.missionTransitioning = true;
			this.missionParent.gameObject.SetActive(true);
			this.missionParent.SetAnchorPosY(-170f);
			this.missionCanvasGroup.alpha = 0f;
			this.missionParent.SetScale(0f, 1f);
			this.missionDescParent.SetScale(1f);
			this.missionButtonParent.SetScale(1f, 1f);
			this.claimMissionRewardButtonParent.SetScale(1f, 1f);
			this.missionDescParent.gameObject.SetActive(true);
			this.missionButtonParent.gameObject.SetActive(collapseAfterShow);
			this.claimMissionRewardButtonParent.gameObject.SetActive(!collapseAfterShow);
			this.missionDescParent.SetAnchorPosX((float)((!collapseAfterShow) ? 35 : -113));
			this.missionButton.interactable = true;
			this.currentMissionSequence = DOTween.Sequence().AppendInterval(this.waitTimeToShow).Append(this.missionParent.DOAnchorPosY(410f, 0.5f, false).SetEase(Ease.OutBack)).Insert(this.waitTimeToShow + 0.1f, this.missionParent.DOScaleX(1f, 0.4f).SetEase(Ease.OutBack)).Insert(this.waitTimeToShow + 0.2f, this.missionCanvasGroup.DOFade(1f, 0.2f));
			if (collapseAfterShow)
			{
				this.currentMissionSequence.AppendInterval(2f).AppendCallback(new TweenCallback(this.CollapseMission));
			}
			else
			{
				this.currentMissionSequence.AppendCallback(new TweenCallback(this.FinishMissionTransition));
			}
			this.currentMissionSequence.Play<Sequence>();
			this.waitTimeToShow = 0f;
		}

		private void CollapseMission()
		{
			this.currentMissionSequence = DOTween.Sequence().Append(this.missionDescParent.DOScaleX(0f, 0.2f).SetEase(Ease.InBack)).AppendCallback(new TweenCallback(this.OnCollapseMissionAnimFinished)).Play<Sequence>();
		}

		private void OnCollapseMissionAnimFinished()
		{
			this.missionTransitioning = false;
			this.missionDescParent.gameObject.SetActive(false);
		}

		private void SetMission(TutorialMission mission, bool newValue, bool isDoingPrestige)
		{
			if (mission.IsComplete && !this.missionTransitioning && !this.claimMissionRewardButtonParent.gameObject.activeSelf)
			{
				this.MissionCompleteAnim(isDoingPrestige);
			}
			if (!this.missionTransitioning || newValue)
			{
				this.missionButtonCheck.color = ((!mission.IsComplete) ? this.missionInProgressCheckColor : this.missionCompleteCheckColor);
			}
			this.missionProgress.fillAmount = mission.Progress;
			if (newValue)
			{
				this.missionDesc.text = mission.GetDescription();
				this.claimMissionRewardButton.textUp.text = GameMath.GetDoubleString(mission.RewardAmount);
				switch (mission.RewardCurrency)
				{
				case CurrencyType.GOLD:
					this.claimMissionRewardButton.iconUpType = ButtonUpgradeAnim.IconType.GOLD;
					return;
				case CurrencyType.SCRAP:
					this.claimMissionRewardButton.iconUpType = ButtonUpgradeAnim.IconType.SCRAPS;
					return;
				case CurrencyType.GEM:
					this.claimMissionRewardButton.iconUpType = ButtonUpgradeAnim.IconType.CREDITS;
					return;
				}
				throw new NotImplementedException();
			}
		}

		private void MissionCompleteAnim(bool isDoingPrestige)
		{
			this.missionTransitioning = true;
			this.missionButton.interactable = false;
			this.currentMissionSequence = DOTween.Sequence().Append(this.missionButtonCheck.rectTransform.DOScale(1.2f, 0.3f).SetEase(Ease.InOutBack)).AppendCallback(delegate
			{
				this.missionButtonCheck.color = this.missionCompleteCheckColor;
			}).Append(this.missionButtonCheck.rectTransform.DOScale(1f, 0.3f)).AppendInterval(0.2f);
			if (isDoingPrestige)
			{
				this.currentMissionSequence.AppendCallback(delegate
				{
					this.HideMission(true, true);
				});
			}
			else
			{
				this.currentMissionSequence.Append(this.missionButtonParent.DOScaleX(0f, 0.4f).SetEase(Ease.InBack)).Insert(0.8f, this.missionDescParent.DOAnchorPosX(-210f, 0.4f, false).SetEase(Ease.InBack)).AppendCallback(new TweenCallback(this.InitClaimMissionRewardButton)).Append(this.claimMissionRewardButtonParent.DOScaleX(1f, 0.6f).SetEase(Ease.OutBack)).Insert(1.2f, this.missionDescParent.DOAnchorPosX(35f, 0.2f, false).SetEase(Ease.OutBack)).Insert(1.2f, this.missionDescParent.DOScaleX(1f, 0.6f).SetEase(Ease.OutBack)).Append(this.missionRewardParent.DOSizeDeltaY(132f, 0.2f, false).SetEase(Ease.OutBack)).AppendCallback(new TweenCallback(this.FinishMissionTransition));
			}
			this.currentMissionSequence.Play<Sequence>();
		}

		private void InitClaimMissionRewardButton()
		{
			this.claimMissionRewardButtonParent.SetScale(0f, 1f);
			this.missionRewardParent.SetSizeDeltaY(56f);
			this.missionButtonParent.gameObject.SetActive(false);
			this.missionDescParent.SetScaleX(0f);
			this.missionDescParent.gameObject.SetActive(true);
			this.claimMissionRewardButtonParent.gameObject.SetActive(true);
		}

		private void OnMissionHidden()
		{
			this.missionParent.gameObject.SetActive(false);
			this.missionTransitioning = false;
			this.missionProgressBlocked = false;
		}

		private void FinishMissionTransition()
		{
			this.missionTransitioning = false;
		}

		public RectTransform rectTransform;

		public Text text;

		public Image imageCharacter;

		public RectTransform messageParent;

		public Image imageRingBg;

		public Image imageRing;

		public CanvasGroup canvasGroupRingBg;

		public CanvasGroup canvasGroupRing;

		public GameButton buttonRingOffer;

		public Image imageArrow;

		public Image imageBlackCurtain;

		public RectTransform maskedElementsParent;

		private float animTimer;

		private const float animPeriodOpen = 0.5f;

		private const float animPeriodClose = 0.14f;

		private float animPeriod = 0.5f;

		private float animMoveTimer;

		private float animMovePeriod = 0.4f;

		private bool isActive;

		private bool isOkayActive;

		private float yBegin;

		private float xBegin;

		private float yGoal;

		private float xGoal;

		public Vector3 positionRingFirstLocal;

		public Vector3 positionRingFirst;

		public Sprite spriteTutoCharGreenMan;

		public Sprite spriteTutoCharAlchemist;

		public Sprite spriteTutoCharMerchant;

		public Sprite spriteTutoSerpent;

		public Image imageOkayBg;

		public GameButton buttonOkay;

		private Vector3 imageOkayScale;

		private float animOkayTimer;

		private const float animOkayPeriod = 0.2f;

		public SoundManager soundManager;

		public RectTransform missionParent;

		public RectTransform missionDescParent;

		public CanvasGroup missionCanvasGroup;

		public Text missionDesc;

		public Text missionsCount;

		public GameButton missionButton;

		public RectTransform missionButtonParent;

		public ButtonUpgradeAnim claimMissionRewardButton;

		public RectTransform claimMissionRewardButtonParent;

		public Image missionProgress;

		public Image missionButtonCheck;

		public Color missionInProgressCheckColor;

		public Color missionCompleteCheckColor;

		public RectTransform missionRewardParent;

		[NonSerialized]
		public bool missionTransitioning;

		[NonSerialized]
		public float waitTimeToShow;

		private bool missionProgressBlocked;

		private Sequence currentMissionSequence;

		private const float BlackCurtainAlphaValue = 0f;

		private const float DefaultBgWidth = 620f;

		private const float DefaultBgPos = 0f;

		private const float DefaultTextWidth = 400f;

		private const float DefaultXPosition = 64f;
	}
}
