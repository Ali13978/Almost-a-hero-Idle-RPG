using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHub : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_SELECT_GAME_MODE");
			this.buttonGameModeStandard.InitStrings();
			this.buttonGameModeCrusade.InitStrings();
			this.buttonGameModeRift.InitStrings();
			this.textAeonCollectReward.text = LM.Get("UI_COLLECT");
		}

		public void DoFillRiftDustBar(double currentDust, double targetDust)
		{
			this.isAnimatingRiftPoint = true;
			Sequence s = DOTween.Sequence();
			s.AppendInterval(0.3f).AppendCallback(new TweenCallback(this.PlayFillAeonDustBarSound)).AppendCallback(delegate
			{
				this.iconDust.transform.localScale = new Vector3(1.2f, 0.8f, 1f);
			}).Append(PanelHub.DOValue(this.riftQuestSlider, currentDust, targetDust, 1.5f, false).SetEase(Ease.Linear)).Join(this.GetDustIconJumAnimation(1.5f)).Append(this.iconDust.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f)).OnComplete(delegate
			{
				this.isAnimatingRiftPoint = false;
				if (currentDust >= targetDust)
				{
					UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiFillAeonDustBarComplete, 1f));
				}
			}).SetDelay(0.5f).Play<Sequence>();
			this.dustBarAnim = s;
			this.riftPointToAnimate = double.NegativeInfinity;
		}

		private Sequence GetDustIconJumAnimation(float duration)
		{
			int loops = Mathf.FloorToInt(duration / 0.12f);
			return DOTween.Sequence().Append(this.iconDust.transform.DOScale(new Vector3(0.8f, 1.2f, 1f), 0.05f).SetEase(Ease.InOutCubic)).Append(this.iconDust.transform.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.05f).SetEase(Ease.InOutCubic)).SetLoops(loops, LoopType.Restart).Insert(0.02f, this.iconDust.rectTransform.DOAnchorPosY(5f, 0.05f, false)).Insert(0.07f, this.iconDust.rectTransform.DOAnchorPosY(-16f, 0.05f, false));
		}

		private void PlayFillAeonDustBarSound()
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiFillAeonDustBarProgress, 1f));
		}

		public static Tweener DOValue(RiftQuestSlider target, double endValue, double maxValue, float duration, bool snapping = false)
		{
			return DOTween.To(() => target.GetSliderVal(maxValue), delegate(double x)
			{
				target.SetSlider(x, maxValue, false);
			}, endValue, duration).SetTarget(target);
		}

		public void DoRiftCollectButtonShow()
		{
			this.isShowingCollectButton = true;
			this.buttonCollectRiftReward.deltaScaleOnDown = 0f;
			this.buttonCollectRiftReward.gameObject.SetActive(true);
			this.buttonCollectRiftBackground.gameObject.SetActive(true);
			this.buttonCollectRiftReward.transform.localScale = Vector3.zero;
			this.aeonToCollectParent.SetAnchorPosY(35f);
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.buttonCollectRiftReward.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).Append(this.aeonToCollectParent.DOAnchorPosY(97.6f, 0.2f, false).SetEase(Ease.OutBack)).OnComplete(delegate
			{
				this.buttonCollectRiftReward.deltaScaleOnDown = -0.1f;
			});
			sequence.Play<Sequence>();
		}

		public void DoRiftCollectButtonHide()
		{
			this.isShowingCollectButton = false;
			this.buttonCollectRiftReward.enabled = false;
			this.buttonCollectRiftReward.deltaScaleOnDown = 0f;
			this.buttonCollectRiftReward.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(delegate
			{
				this.buttonCollectRiftReward.gameObject.SetActive(false);
				this.buttonCollectRiftBackground.gameObject.SetActive(false);
				this.buttonCollectRiftReward.deltaScaleOnDown = -0.1f;
			});
		}

		public void DoFadeIn(TweenCallback onComplede)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.topBar.DOAnchorPosY(-50f, 0.3f, false).SetEase(Ease.OutCubic)).Join(this.bottomBar.DOAnchorPosY(0f, 0.3f, false).SetEase(Ease.OutCubic));
			this.isFadeIn = true;
			for (int i = 0; i < this.modeButtonCanvases.Length; i++)
			{
				CanvasGroup canvasGroup = this.modeButtonCanvases[i];
				RectTransform component = canvasGroup.GetComponent<RectTransform>();
				component.SetAnchorPosY(-217f + -381f * (float)i + 50f);
				canvasGroup.alpha = 0f;
			}
			for (int j = 0; j < this.modeButtonCanvases.Length; j++)
			{
				CanvasGroup canvasGroup2 = this.modeButtonCanvases[j];
				SkeletonGraphic skeletonGraphic = this.modeLockSpine[j];
				RectTransform component2 = canvasGroup2.GetComponent<RectTransform>();
				float atPosition = (float)j * 0.1f;
				sequence.Insert(atPosition, canvasGroup2.DOFade(1f, 0.1f));
				if (skeletonGraphic != null)
				{
					skeletonGraphic.SetAlpha(1f);
				}
				sequence.Insert(atPosition, component2.DOAnchorPosY(-217f + -381f * (float)j, 0.1f, false));
			}
			sequence.OnComplete(delegate
			{
				this.isFadeIn = true;
				onComplede();
			}).Play<Sequence>();
		}

		public Sequence DoFadeOut(TweenCallback onComplede)
		{
			this.isFadeIn = false;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.topBar.DOAnchorPosY(150f, 0.3f, false).SetEase(Ease.OutCubic)).Join(this.bottomBar.DOAnchorPosY(-255f, 0.3f, false).SetEase(Ease.OutCubic));
			for (int i = 0; i < this.modeButtonCanvases.Length; i++)
			{
				CanvasGroup canvasGroup = this.modeButtonCanvases[i];
				SkeletonGraphic skeletonGraphic = this.modeLockSpine[i];
				RectTransform component = canvasGroup.GetComponent<RectTransform>();
				float num = (float)i * 0.1f;
				sequence.Insert(num, canvasGroup.DOFade(0f, 0.1f).SetEase(Ease.OutCubic));
				if (skeletonGraphic != null)
				{
					sequence.Insert(num + 0.05f, skeletonGraphic.DOFade(0f, 0.1f).SetEase(Ease.OutCubic));
				}
				sequence.Insert(num, component.DOAnchorPosY(-217f + -381f * (float)i + 50f, 0.1f, false).SetEase(Ease.OutCubic));
			}
			sequence.OnComplete(onComplede).Play<Sequence>();
			return sequence;
		}

		public ButtonGameMode buttonGameModeStandard;

		public ButtonGameMode buttonGameModeCrusade;

		public ButtonGameMode buttonGameModeRift;

		public RiftQuestSlider riftQuestSlider;

		public GameButton buttonHeroes;

		[FormerlySerializedAs("buttonRings")]
		public GameButton buttonTrinkets;

		public GameButton buttonArtifacts;

		public GameButton buttonCharms;

		public GameButton buttonShop;

		public GameButton buttonOptions;

		public PanelGlobalBonus panelGlobalBonus;

		public GameButton buttonAchievements;

		public Text achievementsUnlockedText;

		public NotificationBadge notificationAchievements;

		public NotificationBadge notificationCharms;

		public NotificationBadge notificationShop;

		public Text textHeader;

		public Sequence dustBarAnim;

		public bool isAnimatingRiftPoint;

		[NonSerialized]
		public double riftPointToAnimate = double.NegativeInfinity;

		public bool isShowingCollectButton;

		public Image iconDust;

		public GameButton buttonCollectRiftReward;

		public Text textAeontoCollect;

		public Text textAeontoCollectFull;

		public Text textAeonCollectReward;

		public RectTransform aeonToCollectParent;

		public GameObject buttonCollectRiftBackground;

		public HorizontalLayoutGroup tabBarLayout;

		public SkeletonGraphic[] modeLockSpine;

		public RectTransform topBar;

		public RectTransform bottomBar;

		public CanvasGroup[] modeButtonCanvases;

		public GameObject christmasEventParent;

		public GameButton openChristmasPopupButton;

		public Image christmasCandyColletedBar;

		public RectTransform gameModeButtonsParent;

		public bool isFadeIn = true;
	}
}
