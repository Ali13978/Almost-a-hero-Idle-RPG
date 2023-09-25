using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRiftSelect : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			RiftOptionWidget.lockedBackgroundColor = this.lockedBackgroundColor;
			RiftOptionWidget.normalBackgroundColor = this.normalBackgroundColor;
			RiftOptionWidget.selectedBackgroundColor = this.selectedBackgroundColor;
			RiftOptionWidget.lockedNameColor = this.lockedNameColor;
			RiftOptionWidget.normalNameColor = this.normalNameColor;
			this.selectedRiftIndex = int.MinValue;
			this.buttonTabCursed.onClick = delegate()
			{
				this.isCurseMode = true;
				this.Button_OnModeChange(true);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
			};
			this.buttonTabNormal.onClick = delegate()
			{
				this.isCurseMode = false;
				this.Button_OnModeChange(false);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
			};
			this.riftOptionWidgets = new List<RiftOptionWidget>();
			float num = 0f;
			for (int i = 0; i < 6; i++)
			{
				RiftOptionWidget riftOptionWidget = UnityEngine.Object.Instantiate<RiftOptionWidget>(this.riftOptionWidgetPrefab, this.scrollContent);
				riftOptionWidget.rectTransform.anchoredPosition = this.GetElementPosition(i);
				riftOptionWidget.rectTransform.SetRightDelta(10f);
				riftOptionWidget.rectTransform.SetLeftDelta(10f);
				riftOptionWidget.riftIndex = i;
				RiftOptionWidget riftOptionWidget2 = riftOptionWidget;
				riftOptionWidget2.onClicked = (Action<int>)Delegate.Combine(riftOptionWidget2.onClicked, new Action<int>(this.Button_OnClickedRiftSelected));
				num += riftOptionWidget.rectTransform.sizeDelta.y;
				this.riftOptionWidgets.Add(riftOptionWidget);
			}
			this.buttonToggleInfo.onClick = delegate()
			{
				this.ToggleRestBonusInfo();
			};
		}

		private void Button_OnModeChange(bool v)
		{
			this.OnModeChange(v);
		}

		public void OnModeChange(bool v)
		{
			this.isCurseMode = v;
			this.SetTheme(this.isCurseMode);
			int activeChallengeIndex = this.uiManager.sim.GetWorld(GameMode.RIFT).GetActiveChallengeIndex();
			bool flag = this.uiManager.sim.GetWorld(GameMode.RIFT).IsActiveChallengeCursed();
			if (this.isCurseMode)
			{
				if (flag)
				{
					this.SetUnhiddenChallengeCount(this.uiManager.sim.cursedRiftSlots.GetMaxSlotCount());
					if (this.currentCurseSelection != -1)
					{
						activeChallengeIndex = this.currentCurseSelection;
					}
					this.OnSelecRift(activeChallengeIndex);
					this.CalculateScrollElementActivity();
					this.SetScrollPosition(activeChallengeIndex);
					RiftOptionWidget buttonFromRiftIndex = this.GetButtonFromRiftIndex(activeChallengeIndex);
					buttonFromRiftIndex.SetSelected();
				}
				else
				{
					this.SetUnhiddenChallengeCount(this.uiManager.sim.cursedRiftSlots.GetMaxSlotCount());
					this.OnSelecRift(this.currentCurseSelection);
					this.CalculateScrollElementActivity();
					this.SetScrollPosition(this.currentCurseSelection);
				}
				this.buttonSelect.interactable = (flag || this.currentCurseSelection != -1);
			}
			else
			{
				if (!flag)
				{
					this.SetUnhiddenChallengeCount(this.uiManager.sim.GetDiscoveredRiftCount());
					if (this.currentNormalSelection != -1 && this.currentNormalSelection <= this.uiManager.sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex())
					{
						activeChallengeIndex = this.currentNormalSelection;
					}
					this.OnSelecRift(activeChallengeIndex);
					this.CalculateScrollElementActivity();
					this.SetScrollPosition(activeChallengeIndex);
					RiftOptionWidget buttonFromRiftIndex2 = this.GetButtonFromRiftIndex(activeChallengeIndex);
					buttonFromRiftIndex2.SetSelected();
				}
				else
				{
					this.SetUnhiddenChallengeCount(this.uiManager.sim.GetDiscoveredRiftCount());
					this.OnSelecRift(this.currentNormalSelection);
					this.CalculateScrollElementActivity();
					this.SetScrollPosition(this.currentNormalSelection);
					RiftOptionWidget buttonFromRiftIndex3 = this.GetButtonFromRiftIndex(this.currentNormalSelection);
					buttonFromRiftIndex3.SetSelected();
				}
				this.buttonSelect.interactable = true;
			}
		}

		public void SetUnhiddenChallengeCount(int count)
		{
			if (count == this.riftChallengeCount)
			{
				return;
			}
			this.riftChallengeCount = count;
			this.scrollContent.SetSizeDeltaY((float)this.riftChallengeCount * 180f + 10f);
		}

		private Vector2 GetElementPosition(int index)
		{
			return new Vector2(0f, -180f * (float)index - 10f);
		}

		public void OpenDiscover()
		{
			this.riftDiscoverBonuses.SetAlpha(1f);
			this.riftDiscoverBonuses.transform.SetScale(1f);
			this.warningCanvasGroup.alpha = 1f;
			this.warningCanvasGroup.transform.SetScale(1f);
			this.riftDiscover.SetAlpha(1f);
			this.riftDiscover.transform.SetScale(1f);
			this.bookRect.SetAnchorPosY(240f);
			this.headerText.text = LM.Get("UI_DISCOVER");
			this.buttonDiscover.text.text = LM.Get("UI_PRESTIGE_LETS_GO");
			this.isOnDiscoverMode = true;
			this.buttonDiscover.gameObject.SetActive(true);
			Sequence sequence = DOTween.Sequence();
			this.discoverCanvasGroup.gameObject.SetActive(true);
			sequence.Append(this.mainCanvasGroup.DOFade(0f, 0.2f)).Join(this.discoverCanvasGroup.DOFade(1f, 0.2f)).AppendCallback(delegate
			{
				this.mainCanvasGroup.gameObject.SetActive(false);
			});
			sequence.Play<Sequence>();
		}

		public void DoLevelUp(Simulator sim)
		{
			this.isAnimatingLevelUp = true;
			this.buttonClose.gameObject.SetActive(false);
			this.buttonDiscover.gameObject.SetActive(false);
			Sequence s = DOTween.Sequence();
			s.Append(this.riftDiscoverBonuses.DOFade(0f, 0.2f)).Join(this.riftDiscoverBonuses.transform.DOScale(1.2f, 0.2f)).Append(this.warningCanvasGroup.DOFade(0f, 0.2f)).Join(this.warningCanvasGroup.transform.DOScale(1.2f, 0.2f)).Append(this.bookRect.DOAnchorPosY(0f, 0.3f, false).SetEase(Ease.InOutQuad)).Join(this.bookRect.DOScale(0.95f, 0.3f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine))).Join(this.riftDiscover.DOFade(0f, 0.2f)).Join(this.riftDiscover.transform.DOScale(1.2f, 0.2f)).AppendInterval(0.3f).Append(this.DoFlame()).AppendCallback(delegate
			{
				this.CloseDiscover();
				this.hardUpdate = true;
				this.targetIndexToFocus = sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex();
				this.buttonClose.gameObject.SetActive(true);
			}).Append(this.DoTextsAnimUp(sim)).OnComplete(delegate
			{
				this.isAnimatingLevelUp = false;
			}).Play<Sequence>();
		}

		public Tween DoFlame()
		{
			Sequence s = DOTween.Sequence();
			this.fireAnimation1.gameObject.SetActive(true);
			this.fireAnimation2.gameObject.SetActive(true);
			this.fireAnimation1.SetScale(0f);
			this.fireAnimation2.SetScale(0f);
			return s.Append(this.fireAnimation1.DOScale(1f, 0.4f).SetEase(Ease.OutBack)).Join(this.fireAnimation2.DOScale(1f, 0.4f).SetEase(Ease.OutBack)).AppendInterval(1.5f).Play<Sequence>();
		}

		public void CloseDiscover()
		{
			this.headerText.text = LM.Get("UI_SELECT_RIFT");
			this.buttonDiscover.text.text = LM.Get("UI_DISCOVER");
			this.buttonDiscoverPrimal.text.text = LM.Get("UI_DISCOVER");
			this.buttonDiscover.gameObject.SetActive(false);
			Sequence sequence = DOTween.Sequence();
			this.mainCanvasGroup.gameObject.SetActive(true);
			sequence.Append(this.discoverCanvasGroup.DOFade(0f, 0.2f)).Join(this.mainCanvasGroup.DOFade(1f, 0.2f)).AppendCallback(delegate
			{
				this.discoverCanvasGroup.gameObject.SetActive(false);
				this.isOnDiscoverMode = false;
				this.fireAnimation1.gameObject.SetActive(false);
				this.fireAnimation2.gameObject.SetActive(false);
			});
			sequence.Play<Sequence>();
		}

		public Sequence DoTextsAnimUp(Simulator sim)
		{
			Sequence s = DOTween.Sequence();
			return s.Append(this.DoRiftSliderLeveledUp(sim)).Append(this.DoAeonRewardLevelup(sim)).Join(this.rewardText.transform.DOScale(1.3f, 0.5f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine))).Append(this.DoAeonBonusRewardLevelup(sim)).Join(this.rewardBonusText.transform.DOScale(1.3f, 0.5f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine)));
		}

		public Tweener DoRiftSliderLeveledUp(Simulator sim)
		{
			double target = sim.GetRiftQuestDustRequired();
			double riftQuestDustRequired = sim.GetRiftQuestDustRequired(sim.riftDiscoveryIndex + 1);
			double currentScore = sim.riftQuestDustCollected;
			return DOTween.To(() => target, delegate(double x)
			{
				target = x;
			}, riftQuestDustRequired, 0.5f).OnUpdate(delegate
			{
				this.riftScoreProgresBar.SetSlider(currentScore, target, false);
			});
		}

		public Tweener DoAeonRewardLevelup(Simulator sim)
		{
			double target = sim.GetCurrentRiftQuestStandardReward();
			double riftQuestStandardReward = sim.GetRiftQuestStandardReward(sim.riftDiscoveryIndex + 1);
			return DOTween.To(() => target, delegate(double x)
			{
				target = x;
			}, riftQuestStandardReward, 0.5f).OnUpdate(delegate
			{
				this.rewardText.text = GameMath.GetDoubleString(target);
			});
		}

		public Tweener DoAeonBonusRewardLevelup(Simulator sim)
		{
			double target = sim.GetCurrentRiftQuestRestReward();
			double riftQuestRestReward = sim.GetRiftQuestRestReward(sim.riftDiscoveryIndex + 1);
			return DOTween.To(() => target, delegate(double x)
			{
				target = x;
			}, riftQuestRestReward, 0.5f).OnUpdate(delegate
			{
				this.rewardBonusText.text = "+" + GameMath.GetDoubleString(target);
			});
		}

		public void UpdateScroll()
		{
			float y = this.scrollRect.content.anchoredPosition.y;
			float yMax = this.scrollRect.viewport.rect.yMax;
			float yMin = this.scrollRect.viewport.rect.yMin;
			float f = y - this.lastVerticalPos;
			if (Mathf.Abs(f) > 20f)
			{
				this.CalculateScrollElementActivity();
			}
		}

		private void CalculateScrollElementActivity()
		{
			this.lastVerticalPos = this.scrollRect.content.anchoredPosition.y;
			int value = GameMath.FloorToInt(this.lastVerticalPos / 180f);
			this.firstIndexOfEnabledItem = GameMath.Clamp(value, 0, this.riftChallengeCount - 1);
			int elementCountToUpdate = this.GetElementCountToUpdate();
			for (int i = 0; i < 6; i++)
			{
				RiftOptionWidget riftOptionWidget = this.riftOptionWidgets[i];
				if (i >= elementCountToUpdate)
				{
					riftOptionWidget.gameObject.SetActive(false);
				}
				else
				{
					riftOptionWidget.rectTransform.anchoredPosition = this.GetElementPosition(this.firstIndexOfEnabledItem + i);
					if (this.firstIndexOfEnabledItem + i == this.selectedRiftIndex)
					{
						riftOptionWidget.SetSelected();
					}
					else
					{
						riftOptionWidget.SetUnlselected();
					}
					riftOptionWidget.gameObject.SetActive(true);
				}
			}
			this.hardUpdate = true;
		}

		public void AnimateScrollPosition(int buttonIndex)
		{
			float scrollPosition = this.GetScrollPosition(buttonIndex);
			this.scrollRect.content.DOAnchorPosY(-scrollPosition, 0.3f, false).SetEase(Ease.InOutSine).OnUpdate(delegate
			{
				this.CalculateScrollElementActivity();
			});
		}

		public void SetScrollPosition(int buttonIndex)
		{
			float scrollPosition = this.GetScrollPosition(buttonIndex);
			this.scrollRect.content.SetAnchorPosY(-scrollPosition);
			this.CalculateScrollElementActivity();
			this.scrollRect.StopMovement();
		}

		private float GetScrollPosition(int buttonIndex)
		{
			float height = this.scrollRect.viewport.rect.height;
			float height2 = this.scrollRect.content.rect.height;
			float num = height2 - height;
			float num2 = GameMath.GetMaxFloat(-num, this.GetElementPosition(buttonIndex).y + 85f);
			if (num2 > 0f)
			{
				num2 = 0f;
			}
			return num2;
		}

		public int GetElementCountToUpdate()
		{
			return GameMath.GetMinInt(this.riftChallengeCount - this.firstIndexOfEnabledItem, 6);
		}

		public void SetButtonSelected(int index)
		{
			foreach (RiftOptionWidget riftOptionWidget in this.riftOptionWidgets)
			{
				riftOptionWidget.SetUnlselected();
			}
			RiftOptionWidget buttonFromRiftIndex = this.GetButtonFromRiftIndex(index);
			buttonFromRiftIndex.SetSelected();
		}

		public RiftOptionWidget GetButtonFromRiftIndex(int index)
		{
			return this.riftOptionWidgets[index - this.firstIndexOfEnabledItem];
		}

		private void Button_OnClickedRiftSelected(int buttonIndex)
		{
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiOpenTabClick, 1f));
			if (this.selectedRiftIndex != buttonIndex + this.firstIndexOfEnabledItem)
			{
				this.OnSelecRift(buttonIndex + this.firstIndexOfEnabledItem);
				if (this.isCurseMode)
				{
					this.buttonSelect.interactable = true;
					this.currentCurseSelection = buttonIndex + this.firstIndexOfEnabledItem;
				}
				else
				{
					this.currentNormalSelection = buttonIndex + this.firstIndexOfEnabledItem;
					Main.instance.GetSim().lastSelectedRegularGateIndex = this.currentNormalSelection;
				}
				RiftOptionWidget riftOptionWidget = this.riftOptionWidgets[buttonIndex];
				riftOptionWidget.DoBoingAnimation(this.isCurseMode);
				riftOptionWidget.SetSelected();
			}
		}

		public void OnSelecRift(int index)
		{
			this.selectedRiftIndex = index;
			foreach (RiftOptionWidget riftOptionWidget in this.riftOptionWidgets)
			{
				riftOptionWidget.SetUnlselected();
			}
		}

		public void InitStrings()
		{
			this.headerText.text = LM.Get("UI_SELECT_RIFT");
			this.buttonDiscover.text.text = LM.Get("UI_DISCOVER");
			this.buttonDiscoverPrimal.text.text = LM.Get("UI_DISCOVER");
			this.buttonSelect.text.text = LM.Get("UI_SELECT");
			this.remainingPointsToNextReardHint.text = LM.Get("RIFT_AEON_DAILY_BONUS");
			this.riftDiscoverCost.text = LM.Get("UI_DISCOVER_D_1");
			this.currentCursesLabel.text = LM.Get("CURRENT_CURSES") + ":";
			this.buttonTabCursed.text.text = LM.Get("RIFT_TAB_CURSED");
			this.buttonTabNormal.text.text = LM.Get("RIFT_TAB_NORMAL");
			foreach (RiftOptionWidget riftOptionWidget in this.riftOptionWidgets)
			{
				riftOptionWidget.firstTimeRewardLabel.text = LM.Get("RIFT_FIRST_TIME_REWARD");
			}
			this.infoMaxRestBonusLabel.text = LM.Get("REST_BONUS_INFO_MAX_BONUS");
			this.infoMaxRestBonusTimeLabel.text = LM.Get("REST_BONUS_INFO_TIME_TO_MAX");
		}

		public int GetSelectedRiftIndex()
		{
			return this.selectedRiftIndex + this.firstIndexOfEnabledItem;
		}

		public void EnableDiscoverButton()
		{
			this.discoverButtonParent.gameObject.SetActive(true);
			this.scrollRect.viewport.SetBottomDelta(315f);
		}

		public void DisableDiscoverButton()
		{
			this.discoverButtonParent.gameObject.SetActive(false);
			this.scrollRect.viewport.SetBottomDelta(0f);
		}

		public void ToggleRestBonusInfo()
		{
			this.isRestBonusInfoShowing = !this.isRestBonusInfoShowing;
			if (this.restBonusInfoAnimation != null && this.restBonusInfoAnimation.IsPlaying())
			{
				this.restBonusInfoAnimation.Kill(false);
			}
			if (this.isRestBonusInfoShowing)
			{
				this.infoRestBonusParent.gameObject.SetActive(true);
				this.restBonusInfoAnimation = this.infoRestBonusParent.DOScaleY(1f, 5f).SetSpeedBased<Tweener>().SetEase(Ease.OutBack);
			}
			else
			{
				this.restBonusInfoAnimation = this.infoRestBonusParent.DOScaleY(0f, 5f).SetSpeedBased<Tweener>().SetEase(Ease.OutCubic).OnComplete(delegate
				{
					this.infoRestBonusParent.gameObject.SetActive(false);
				});
			}
		}

		public void SetPositioning(bool hasCurseTabs, bool isInCurseTab)
		{
			if (hasCurseTabs)
			{
				this.mainRect.SetTopDelta(192f);
			}
			else
			{
				this.mainRect.SetTopDelta(88f);
			}
			float num;
			if (!isInCurseTab)
			{
				num = 170f;
				if (this.isRestBonusInfoShowing)
				{
					num += 125f;
				}
			}
			else
			{
				num = 170f;
			}
			if (!this.lastDelta.SigmaEqual(num) || this.lastTabCursed != isInCurseTab)
			{
				if (this.scrollAnimation != null && this.scrollAnimation.isPlaying)
				{
					this.scrollAnimation.Complete(true);
				}
				this.scrollAnimation = DOTween.Sequence();
				Ease ease = (num - this.lastDelta <= 0f) ? Ease.OutCubic : Ease.OutBack;
				this.scrollAnimation.Append(this.scrollSelfRect.DoTopDelta(num, 0.2f, false).SetEase(ease));
				if (isInCurseTab)
				{
					this.currentCursesCanvasGroup.gameObject.SetActive(true);
					this.scrollAnimation.Join(this.currentCursesCanvasGroup.DOFade(1f, 0.2f));
				}
				else
				{
					this.scrollAnimation.Join(this.currentCursesCanvasGroup.DOFade(0f, 0.1f));
					this.scrollAnimation.AppendCallback(delegate
					{
						this.currentCursesCanvasGroup.gameObject.SetActive(false);
					});
				}
				this.scrollAnimation.Play<Sequence>();
				this.lastDelta = num;
				this.lastTabCursed = isInCurseTab;
			}
		}

		public void SetTheme(bool isCursed)
		{
			if (isCursed)
			{
				this.background1.DOColor(this.background1CursedColor, 0.2f);
				this.background2.DOColor(this.background2CursedColor, 0.2f);
				this.background3.DOColor(this.background3CursedColor, 0.2f);
				this.curseOrnament.gameObject.SetActive(true);
				RiftOptionWidget.SetTheme(this.normalBackgroundColor_cursed, this.lockedBackgroundColor_cursed, this.selectedBackgroundColor_cursed, this.outloneColor_cursed, this.lockColor_cursed);
			}
			else
			{
				this.background1.DOColor(this.background1Color, 0.2f);
				this.background2.DOColor(this.background2Color, 0.2f);
				this.background3.DOColor(this.background3Color, 0.2f);
				this.curseOrnament.gameObject.SetActive(false);
				RiftOptionWidget.SetTheme(this.normalBackgroundColor, this.lockedBackgroundColor, this.selectedBackgroundColor, this.outloneColor, this.lockColor);
			}
		}

		public void SetCurseEffectIcons()
		{
			if (this.uiManager.sim.currentCurses == null)
			{
				return;
			}
			int count = this.uiManager.sim.currentCurses.Count;
			if (count != this.curseEffectImages.Count)
			{
				Utility.FillUiElementList<Image>(UiData.inst.imagePrafab, this.currentCursesIconParent, count, this.curseEffectImages);
			}
			for (int i = 0; i < count; i++)
			{
				Image image = this.curseEffectImages[i];
				image.sprite = this.uiManager.spritesCurseEffectIcon[this.uiManager.sim.currentCurses[i]];
				image.color = ((this.uiManager.sim.currentCurses[i] != 1019) ? new Color(0.7058824f, 0.1843137f, 0.1215686f, 1f) : new Color(0.549019635f, 0.309803933f, 0.75686276f));
				image.rectTransform.sizeDelta = new Vector2(80f, 80f);
			}
		}

		public RiftOptionWidget riftOptionWidgetPrefab;

		[NonSerialized]
		public List<RiftOptionWidget> riftOptionWidgets;

		public GameButton buttonSelect;

		public GameButton buttonDiscoverPrimal;

		public GameButton buttonDiscover;

		public GameButton buttonClose;

		public GameButton buttonTabNormal;

		public GameButton buttonTabCursed;

		public GameButton buttonToggleInfo;

		public GameButton buttonCurseInfo;

		public ScrollRect scrollRect;

		public RectTransform scrollContent;

		public RectTransform scrollSelfRect;

		public RectTransform discoverButtonParent;

		public RectTransform tabButtonsParent;

		public RectTransform aeonDustParent;

		public RectTransform infoRestBonusParent;

		public RectTransform mainRect;

		public RectTransform curseOrnament;

		public RectTransform currentCursesParent;

		public RectTransform currentCursesIconParent;

		public RectTransform aeonDustMissionParent;

		public RectTransform popupRect;

		public Text headerText;

		public Text remainingPointsToNextReardHint;

		public Text rewardText;

		public Text rewardBonusText;

		public Text discoverFive;

		public Text infoMaxRestBonusLabel;

		public Text infoMaxRestBonusAmount;

		public Text infoMaxRestBonusTimeLabel;

		public Text infoMaxRestBonusTimeAmount;

		public Text currentCursesLabel;

		public CanvasGroup mainCanvasGroup;

		public CanvasGroup discoverCanvasGroup;

		public CanvasGroup currentCursesCanvasGroup;

		public Text riftDiscover;

		public Text riftDiscoverCost;

		public Text riftDiscoverBonuses;

		public int selectedRiftIndex;

		public UiManager uiManager;

		public int firstIndexOfEnabledItem;

		public const int maxItemToShow = 6;

		public Color lockedBackgroundColor;

		public Color normalBackgroundColor;

		public Color selectedBackgroundColor;

		public Color outloneColor;

		public Color lockColor;

		public Color lockedNameColor;

		public Color normalNameColor;

		public Color lockedBackgroundColor_cursed;

		public Color normalBackgroundColor_cursed;

		public Color selectedBackgroundColor_cursed;

		public Color outloneColor_cursed;

		public Color lockColor_cursed;

		public RiftQuestSlider riftScoreProgresBar;

		public Scaler referenceSlider;

		public RectTransform fireAnimation1;

		public RectTransform fireAnimation2;

		private float lastVerticalPos = float.MaxValue;

		private const float startOffest = 10f;

		private const float padding = 10f;

		private const float distance = 170f;

		private int riftChallengeCount;

		private RiftOptionWidget lastSelectedButton;

		public CanvasGroup warningCanvasGroup;

		public bool hardUpdate;

		public bool isOnDiscoverMode;

		public bool isAnimatingLevelUp;

		public bool isCurseMode;

		public RectTransform bookRect;

		public Outline background1;

		public Image background2;

		public Image background3;

		public List<Image> curseEffectImages;

		public Color background1Color;

		public Color background2Color;

		public Color background3Color;

		public Color background1CursedColor;

		public Color background2CursedColor;

		public Color background3CursedColor;

		public int targetIndexToFocus = -1;

		public bool isRestBonusInfoShowing;

		private Tweener restBonusInfoAnimation;

		private Sequence scrollAnimation;

		[NonSerialized]
		public int currentCurseSelection = -1;

		[NonSerialized]
		public int currentNormalSelection = -1;

		private float lastDelta;

		private bool lastTabCursed;
	}
}
