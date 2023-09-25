using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Simulation.ArtifactSystem;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifactPopup : AahMonoBehaviour
	{
		public void InitStrings()
		{
			this.textHeader.text = "UI_ARTIFACT".Loc();
			this.textStatsHeader.text = "UI_UNIQUE_STATS".Loc();
			this.buttonEvolveFillerText.text = "UI_EVOLVE".Loc();
			this.buttonEvolve.text.text = "UI_EVOLVE".Loc();
			this.buttonUpgradeMaxedText.text = "UI_UPGRADED".Loc();
			this.buttonEvolveMaxedText.text = "UI_EVOLVED".Loc();
			for (int i = 0; i < this.uniqueStatWidgets.Count; i++)
			{
				ArtifactUniqueStatWidget artifactUniqueStatWidget = this.uniqueStatWidgets[i];
				artifactUniqueStatWidget.button.textDown.text = "UI_ARTIFACTS_REROLL".Loc();
			}
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonClose.onClick = (this.buttonBg.onClick = delegate()
			{
				this.manager.panelArtifactScroller.PopLastSelectedArtifact();
				this.manager.state = this.stateToReturn;
				this.manager.panelArtifactScroller.SetScrollPositionOnArtifactIndex(this.selectedArtifactIndex);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
			});
			this.buttonFiveX.onClick = new GameButton.VoidFunc(this.Button_OnUpgradeFiveClicked);
			this.buttonFiveX.onSelectionChange = new GameButton.BoolFunc(this.Button_OnFiveButtonSelectionChange);
			this.buttonRight.onClick = new GameButton.VoidFunc(this.Button_NavigateRight);
			this.buttonLeft.onClick = new GameButton.VoidFunc(this.Button_NavigateLeft);
			this.buttonEvolve.onClick = new GameButton.VoidFunc(this.Button_OnEvolveClicked);
			this.buttonPossibleEffects.onClick = new GameButton.VoidFunc(this.Button_OnPossibleEffectsClicked);
			this.tooltipParent.SetScaleY(0f);
			this.tooltipParent.gameObject.SetActive(false);
			this.buttonClaudronGlow.SetAlpha(0f);
		}

		public void OnClose()
		{
			this.buttonClaudronParticle.AnimationState.ClearTracks();
			this.buttonClaudronParticle.Skeleton.SetToSetupPose();
		}

		private void Button_OnFiveButtonSelectionChange(bool state)
		{
			if (DOTween.IsTweening(5990580, false))
			{
				DOTween.Complete(5990580, true);
			}
			if (state)
			{
				this.tooltipParent.gameObject.SetActive(true);
				this.tooltipParent.DOScaleY(1f, 0.3f).SetEase(Ease.OutBack).SetId(5990580);
			}
			else
			{
				this.tooltipParent.DOScaleY(0f, 0.2f).SetEase(Ease.OutExpo).OnComplete(delegate
				{
					this.tooltipParent.gameObject.SetActive(false);
				}).SetId(5990580);
			}
		}

		private void Button_NavigateLeft()
		{
			this.NavigateArtifacts(true);
		}

		private void Button_NavigateRight()
		{
			this.NavigateArtifacts(false);
		}

		private void Button_OnPossibleEffectsClicked()
		{
			this.manager.state = ((this.manager.sim.artifactsManager.GetUniqueEffectsStockCount() <= 0) ? UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP : UiState.POSSIBLE_ARTIFACT_EFFECTS_POPUP);
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupAppear, 1f));
		}

		private void NavigateArtifacts(bool isLeft)
		{
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			int count = artifactsManager.Artifacts.Count;
			if (isLeft)
			{
				this.selectedArtifactIndex--;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
				if (this.selectedArtifactIndex < 0)
				{
					this.selectedArtifactIndex = count - 1;
				}
			}
			else
			{
				this.selectedArtifactIndex++;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
				if (this.selectedArtifactIndex >= count)
				{
					this.selectedArtifactIndex = 0;
				}
			}
			this.manager.panelArtifactScroller.selectedArtifactIndex = this.selectedArtifactIndex;
			this.SetArtifact(false);
		}

		private void Button_OnUpgradeClicked()
		{
			UpgradeSift upgradeSift = this.multipleBuySteps[this.multipleBuyIndex];
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			Simulation.ArtifactSystem.Artifact selectedArtifact = this.GetSelectedArtifact();
			MultiJumpResult effectiveJumpCount = this.GetEffectiveJumpCount(selectedArtifact, artifactsManager, upgradeSift);
			if (effectiveJumpCount.jumpCount == 0)
			{
				this.manager.panelCurrencyWarning.SetCurrency(PanelCurrencyWarning.GetPopupTypeForCurrency(CurrencyType.MYTHSTONE), this.manager, string.Empty, 0, null);
				this.manager.state = UiState.CURRENCY_WARNING;
			}
			else
			{
				this.manager.SetCommand(new UiCommandArtifactLevelUp
				{
					artifact = selectedArtifact,
					jumpCount = effectiveJumpCount.jumpCount
				});
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiWorldUpgrade, 1f));
			}
			this.forceUpdate = true;
		}

		private MultiJumpResult GetEffectiveJumpCount(Simulation.ArtifactSystem.Artifact af, ArtifactsManager am, UpgradeSift upgradeSift)
		{
			int value = 0;
			int min = 1;
			MultiJumpResult result = default(MultiJumpResult);
			double num = 0.0;
			switch (upgradeSift)
			{
			case UpgradeSift.One:
				value = 1;
				break;
			case UpgradeSift.Five:
				value = 5;
				break;
			case UpgradeSift.TenPerCent:
				min = 0;
				num = this.manager.sim.GetMythstones().GetAmount() * 0.1;
				value = am.GetLevelCountForPrice(af, num, this.manager.sim.GetUniversalBonusAll());
				break;
			case UpgradeSift.TwentyFivePerCent:
				num = this.manager.sim.GetMythstones().GetAmount() * 0.25;
				value = am.GetLevelCountForPrice(af, num, this.manager.sim.GetUniversalBonusAll());
				min = 0;
				break;
			case UpgradeSift.Max:
				value = int.MaxValue;
				break;
			}
			int levelCountForPrice = am.GetLevelCountForPrice(af, this.manager.sim.GetMythstones().GetAmount(), this.manager.sim.GetUniversalBonusAll());
			int maxLevelJump = am.GetMaxLevelJump(af);
			int num2 = GameMath.Clamp(value, min, maxLevelJump);
			num2 = GameMath.Clamp(levelCountForPrice, min, num2);
			result.jumpCount = num2;
			if (num2 == 0)
			{
				result.amountToConsume = num;
			}
			else
			{
				result.amountToConsume = am.GetUpgradeCostOf(af, num2, this.manager.sim.GetUniversalBonusAll());
			}
			return result;
		}

		private int GetUnclampedJumpCount(Simulation.ArtifactSystem.Artifact af, ArtifactsManager am, UpgradeSift upgradeSift)
		{
			int value = 0;
			switch (upgradeSift)
			{
			case UpgradeSift.One:
				value = 1;
				break;
			case UpgradeSift.Five:
				value = 5;
				break;
			case UpgradeSift.TenPerCent:
				value = am.GetLevelCountForPrice(af, this.manager.sim.GetMythstones().GetAmount() * 0.1, this.manager.sim.GetUniversalBonusAll());
				break;
			case UpgradeSift.TwentyFivePerCent:
				value = am.GetLevelCountForPrice(af, this.manager.sim.GetMythstones().GetAmount() * 0.25, this.manager.sim.GetUniversalBonusAll());
				break;
			case UpgradeSift.Max:
				value = int.MaxValue;
				break;
			}
			int maxLevelJump = am.GetMaxLevelJump(af);
			return GameMath.Clamp(value, 1, maxLevelJump);
		}

		public static string GetFiveXString(UpgradeSift upgradeSift)
		{
			switch (upgradeSift)
			{
			case UpgradeSift.One:
				return "x1";
			case UpgradeSift.Five:
				return "x5";
			case UpgradeSift.TenPerCent:
				return GameMath.GetPercentString(0.1, false);
			case UpgradeSift.TwentyFivePerCent:
				return GameMath.GetPercentString(0.25, false);
			case UpgradeSift.Max:
				return "UI_MAX".Loc();
			default:
				throw new Exception("Invalit shift: " + upgradeSift);
			}
		}

		private string GetTooltipString(UpgradeSift upgradeSift)
		{
			switch (upgradeSift)
			{
			case UpgradeSift.One:
				return "MULTI_UPGRADE_TOOLTIP_DESC_COUNT".LocFormat(1);
			case UpgradeSift.Five:
				return "MULTI_UPGRADE_TOOLTIP_DESC_COUNT".LocFormat(5);
			case UpgradeSift.TenPerCent:
				return "MULTI_UPGRADE_TOOLTIP_DESC_PERCENT".LocFormat(GameMath.GetPercentString(0.1, false));
			case UpgradeSift.TwentyFivePerCent:
				return "MULTI_UPGRADE_TOOLTIP_DESC_PERCENT".LocFormat(GameMath.GetPercentString(0.25, false));
			case UpgradeSift.Max:
				return "MULTI_UPGRADE_TOOLTIP_DESC_MAX".Loc();
			default:
				throw new Exception("Invalit shift: " + upgradeSift);
			}
		}

		private Simulation.ArtifactSystem.Artifact GetSelectedArtifact()
		{
			int index = this.selectedArtifactIndex;
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			return artifactsManager.Artifacts[index];
		}

		private void Button_OnUpgradeFiveClicked()
		{
			this.multipleBuyIndex++;
			if (this.multipleBuyIndex >= this.multipleBuySteps.Length)
			{
				this.multipleBuyIndex = 0;
			}
			this.manager.SetCommand(new CommandToggleArtifactUpgradeMultiplier
			{
				value = this.multipleBuyIndex
			});
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			this.SetArtifact(true);
		}

		public void UpdateButtons()
		{
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			Simulation.ArtifactSystem.Artifact selectedArtifact = this.GetSelectedArtifact();
			UniversalTotalBonus universalBonusAll = this.manager.sim.GetUniversalBonusAll();
			UpgradeSift upgradeSift = this.multipleBuySteps[this.multipleBuyIndex];
			if (artifactsManager.IsLevelMaxed(selectedArtifact))
			{
				this.buttonUpgradeMaxedParent.gameObject.SetActive(true);
				this.buttonUpgrade.gameObject.SetActive(false);
			}
			else
			{
				MultiJumpResult effectiveJumpCount = this.GetEffectiveJumpCount(selectedArtifact, artifactsManager, upgradeSift);
				this.buttonUpgradeMaxedParent.gameObject.SetActive(false);
				this.buttonUpgrade.gameObject.SetActive(true);
				this.SetUpgradeButton(selectedArtifact, artifactsManager, universalBonusAll, this.manager.sim, effectiveJumpCount.amountToConsume, effectiveJumpCount.jumpCount);
			}
			for (int i = 0; i < selectedArtifact.Rarity; i++)
			{
				ArtifactUniqueStatWidget artifactUniqueStatWidget = this.uniqueStatWidgets[i];
				ButtonUpgradeAnim button = artifactUniqueStatWidget.button;
				double rerollCostOf = artifactsManager.GetRerollCostOf(selectedArtifact, i);
				this.SetButtonState(selectedArtifact, artifactsManager, i, button, rerollCostOf);
			}
		}

		public void DoTransitionAnimation(Simulation.ArtifactSystem.Artifact af, string commonStatDescription)
		{
			this.isAnimatingNavigation = true;
			if (DOTween.IsTweening("ArtifactTransition", false))
			{
				DOTween.Complete("ArtifactTransition", true);
			}
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.buttonArtifact.artifactStone.transform.DOScale(0f, 0.1f).SetEase(Ease.OutQuint)).Join(this.textArtifactStatNameCanvas.DOFade(0f, 0.1f));
			int count = af.UniqueEffectsIds.Count;
			for (int i = 0; i < 4; i++)
			{
				ArtifactUniqueStatWidget artifactUniqueStatWidget = this.uniqueStatWidgets[i];
				RectTransform buttonParent = artifactUniqueStatWidget.buttonParent;
				if (i >= count && buttonParent.gameObject.activeSelf)
				{
					sequence.Join(buttonParent.DOScale(0f, 0.1f));
				}
				sequence.Join(artifactUniqueStatWidget.textCanvasGroup.DOFade(0f, 0.1f));
				sequence.Join(artifactUniqueStatWidget.textCanvasRect.DOAnchorPosX(10f, 0.1f, false));
			}
			sequence.AppendCallback(delegate
			{
				this.buttonArtifact.maxed = this.manager.sim.artifactsManager.IsLevelMaxed(af);
				this.buttonArtifact.SetButton(ButtonArtifact.State.FULL, af.Level, af.Rarity);
				this.buttonArtifact.artifactStone.imageIcon.sprite = this.manager.GetEffectSprite(af.CommonEffectId);
				this.textArtifactStatName.text = commonStatDescription;
				this.SetUniqueStats(af, false);
			}).Append(this.buttonArtifact.artifactStone.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
			sequence.Join(this.textArtifactStatNameCanvas.DOFade(1f, 0.2f));
			for (int j = 0; j < 4; j++)
			{
				ArtifactUniqueStatWidget artifactUniqueStatWidget2 = this.uniqueStatWidgets[j];
				RectTransform buttonParent2 = artifactUniqueStatWidget2.buttonParent;
				sequence.Insert(0.3f + (float)j * 0.05f, artifactUniqueStatWidget2.textCanvasGroup.DOFade(1f, 0.2f));
				sequence.Insert(0.3f + (float)j * 0.05f, artifactUniqueStatWidget2.textCanvasRect.DOAnchorPosX(0f, 0.2f, false));
				if (j < count)
				{
					sequence.Insert(0.3f + (float)j * 0.05f, buttonParent2.DOScale(1f, 0.2f));
				}
			}
			sequence.SetId("ArtifactTransition").OnComplete(delegate
			{
				this.isAnimatingNavigation = false;
			}).Play<Sequence>();
		}

		public void DoRerollAnimation(int rolledStatIndex)
		{
			ArtifactUniqueStatWidget w = this.uniqueStatWidgets[rolledStatIndex];
			Simulation.ArtifactSystem.Artifact af = this.GetSelectedArtifact();
			Sequence s = DOTween.Sequence();
			this.rerollingIndex = rolledStatIndex;
			this.buttonPossibleEffects.rectTransform.parent.DOScale(1.1f, 0.5f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine));
			s.Append(w.textCanvasGroup.DOFade(0f, 0.1f)).Join(w.textCanvasRect.DOAnchorPosX(100f, 0.1f, false)).AppendCallback(delegate
			{
				this.rerollingIndex = -1;
				this.SetUniqueStats(af, true);
				w.textCanvasRect.SetAnchorPosX(-100f);
				this.buttonClaudronParticle.AnimationState.SetAnimation(0, "animation", false);
			}).AppendInterval(0.1f).Append(w.textCanvasGroup.DOFade(1f, 0.2f)).Join(w.textCanvasRect.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutBack)).Insert(0f, this.buttonClaudronGlow.DOFade(1f, 0.25f).SetEase(Ease.InOutQuad)).Insert(0.25f, this.buttonClaudronGlow.DOFade(0f, 0.25f).SetEase(Ease.InOutQuad)).Play<Sequence>();
		}

		public void PlayNewTALMilestoneReachedAnim()
		{
			this.buttonPossibleEffects.rectTransform.parent.DOScale(1.1f, 0.5f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine));
			DOTween.Sequence().AppendInterval(0.3f).AppendCallback(delegate
			{
				this.buttonClaudronParticle.AnimationState.SetAnimation(0, "animation", false);
				UiManager.AddUiSound(SoundArchieve.inst.uiNewTALMilestoneReached);
			}).Insert(0f, this.buttonClaudronGlow.DOFade(1f, 0.25f).SetEase(Ease.InOutQuad)).Insert(0.25f, this.buttonClaudronGlow.DOFade(0f, 0.25f).SetEase(Ease.InOutQuad)).Play<Sequence>();
		}

		public void SetArtifact(bool skipAnimation = true)
		{
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			Simulation.ArtifactSystem.Artifact selectedArtifact = this.GetSelectedArtifact();
			UniversalTotalBonus universalBonusAll = this.manager.sim.GetUniversalBonusAll();
			this.textHeader.text = "UI_ARTIFACT".Loc() + " " + (this.selectedArtifactIndex + 1).ToString("D2");
			Simulation.ArtifactSystem.ArtifactEffect artifactEffect = EffectsDatabase.Common[selectedArtifact.CommonEffectId];
			UpgradeSift upgradeSift = this.multipleBuySteps[this.multipleBuyIndex];
			string text;
			if (artifactsManager.IsLevelMaxed(selectedArtifact))
			{
				text = artifactEffect.GetDescriptionWithValue(selectedArtifact.Level, this.manager.sim.GetUniversalBonusAll());
				this.buttonUpgradeMaxedParent.gameObject.SetActive(true);
				this.buttonUpgrade.gameObject.SetActive(false);
			}
			else
			{
				MultiJumpResult effectiveJumpCount = this.GetEffectiveJumpCount(selectedArtifact, artifactsManager, upgradeSift);
				this.buttonUpgradeMaxedParent.gameObject.SetActive(false);
				this.buttonUpgrade.gameObject.SetActive(true);
				if (effectiveJumpCount.jumpCount == 0)
				{
					text = artifactEffect.GetDescriptionWithValue(selectedArtifact.Level, this.manager.sim.GetUniversalBonusAll());
				}
				else
				{
					string signedJumpString = artifactEffect.GetSignedJumpString(selectedArtifact.Level, effectiveJumpCount.jumpCount, this.manager.sim.GetUniversalBonusAll());
					text = artifactEffect.GetDescriptionWithValue(selectedArtifact.Level, this.manager.sim.GetUniversalBonusAll()) + AM.csg(" (" + signedJumpString + ")");
				}
				this.SetUpgradeButton(selectedArtifact, artifactsManager, universalBonusAll, this.manager.sim, effectiveJumpCount.amountToConsume, effectiveJumpCount.jumpCount);
			}
			if (artifactsManager.IsMultipleUpgradeUnlocked())
			{
				this.buttonFiveX.text.text = PanelArtifactPopup.GetFiveXString(upgradeSift);
				this.textTooltip.text = this.GetTooltipString(upgradeSift);
				this.buttonFiveX.gameObject.SetActive(true);
			}
			else
			{
				this.buttonFiveX.gameObject.SetActive(false);
			}
			if (skipAnimation)
			{
				this.buttonArtifact.maxed = this.manager.sim.artifactsManager.IsLevelMaxed(selectedArtifact);
				this.buttonArtifact.SetButton(ButtonArtifact.State.FULL, selectedArtifact.Level, selectedArtifact.Rarity);
				this.buttonArtifact.artifactStone.imageIcon.sprite = this.manager.GetEffectSprite(selectedArtifact.CommonEffectId);
				this.textArtifactStatName.text = text;
				this.SetUniqueStats(selectedArtifact, skipAnimation);
			}
			else
			{
				this.DoTransitionAnimation(selectedArtifact, text);
			}
			if (artifactsManager.IsUniqueStatCountMaxed(selectedArtifact))
			{
				this.buttonEvolveMaxedParent.gameObject.SetActive(true);
				this.buttonEvolve.gameObject.SetActive(false);
				this.buttonEvolveFillerParent.gameObject.SetActive(false);
			}
			else
			{
				this.buttonEvolveMaxedParent.gameObject.SetActive(false);
				this.buttonEvolve.gameObject.SetActive(true);
				this.SetEvolveButton(selectedArtifact, artifactsManager, skipAnimation);
			}
		}

		private void SetUniqueStats(Simulation.ArtifactSystem.Artifact af, bool skipAnimation)
		{
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			int count = af.UniqueEffectsIds.Count;
			float width = this.statsParent.rect.width;
			for (int i = 0; i < 4; i++)
			{
				ArtifactUniqueStatWidget artifactUniqueStatWidget = this.uniqueStatWidgets[i];
				RectTransform buttonParent = artifactUniqueStatWidget.buttonParent;
				ButtonUpgradeAnim button = artifactUniqueStatWidget.button;
				if (skipAnimation)
				{
					buttonParent.SetScale(1f);
				}
				if (i < count)
				{
					double rerollCostOf = artifactsManager.GetRerollCostOf(af, i);
					int key = af.UniqueEffectsIds[i];
					int statIndex = i;
					EffectsDatabase.UniqueEffectInfo uniqueEffectInfo = EffectsDatabase.Unique[key];
					artifactUniqueStatWidget.imageLock.gameObject.SetActive(false);
					artifactUniqueStatWidget.textStat.gameObject.SetActive(true);
					button.gameObject.SetActive(true);
					artifactUniqueStatWidget.readyToEvolveText.enabled = false;
					this.SetButtonState(af, artifactsManager, i, button, rerollCostOf);
					button.gameButton.onClick = delegate()
					{
						this.Button_OnRerollUniqueStatClicked(statIndex);
					};
					if (this.rerollingIndex == -1)
					{
						artifactUniqueStatWidget.textStat.text = uniqueEffectInfo.Effect.GetDescriptionWithValue(1, this.manager.sim.GetUniversalBonusAll());
					}
					artifactUniqueStatWidget.textStat.color = this.colorDescNormal;
				}
				else if (i == count)
				{
					button.gameObject.SetActive(false);
					if (artifactsManager.ReachedMinLevelForNextRarity(af))
					{
						artifactUniqueStatWidget.textStat.gameObject.SetActive(false);
						artifactUniqueStatWidget.imageLock.gameObject.SetActive(false);
						artifactUniqueStatWidget.readyToEvolveText.enabled = true;
						artifactUniqueStatWidget.readyToEvolveText.text = "UI_READY_TO_EVOLVE".Loc();
					}
					else
					{
						artifactUniqueStatWidget.textStat.gameObject.SetActive(true);
						artifactUniqueStatWidget.imageLock.gameObject.SetActive(true);
						artifactUniqueStatWidget.readyToEvolveText.enabled = false;
						artifactUniqueStatWidget.textStat.text = "ARTIFACT_EVOLVE_NEXT_DESC".LocFormat(AM.cs(artifactsManager.GetRequiredLevelToEvolve(af, af.Rarity).ToString(), this.colorDescLockedLevelHighlight));
						artifactUniqueStatWidget.textStat.color = this.colorDescLocked;
					}
				}
				else
				{
					artifactUniqueStatWidget.imageLock.gameObject.SetActive(false);
					artifactUniqueStatWidget.textStat.gameObject.SetActive(false);
					button.gameObject.SetActive(false);
					artifactUniqueStatWidget.readyToEvolveText.enabled = false;
				}
			}
		}

		private void SetButtonState(Simulation.ArtifactSystem.Artifact af, ArtifactsManager am, int effectIndex, ButtonUpgradeAnim button, double rerollCost)
		{
			button.textUp.text = GameMath.GetDoubleString(rerollCost);
			bool flag = am.CanRerollWithoutAffording(af, effectIndex, this.manager.sim);
			bool flag2 = am.CanAffordRerrolOf(af, effectIndex, this.manager.sim);
			button.fakeDisabled = !flag;
			button.gameButton.fakeDisabled = !flag;
			button.gameButton.interactable = flag2;
			button.textUpDisabledColor = ((!flag2) ? this.colorCannotAfford : ((!flag) ? this.colorCannotReroll : Color.white));
			button.textDownDisabledColor = ((!flag) ? this.colorCannotReroll : Color.white);
			if (!flag2)
			{
				button.textUpDisabledOffset = -10f;
				button.textDownDisabledOffset = -10f;
				button.spriteNotInteractable = this.buttonRerollCannotAffordDisabled;
				button.textDownDisabledColor = this.colorTextDownCannotAfford;
			}
			else
			{
				button.textUpDisabledOffset = 0.01f;
				button.textDownDisabledOffset = 0.01f;
				button.spriteNotInteractable = this.buttonRerollCannotNormalDisabled;
				button.textDownDisabledColor = ((!flag) ? this.colorCannotReroll : Color.white);
			}
		}

		private void SetUpgradeButton(Simulation.ArtifactSystem.Artifact af, ArtifactsManager am, UniversalTotalBonus ub, Simulator sim, double cost, int jump)
		{
			this.buttonUpgrade.gameButton.onClick = new GameButton.VoidFunc(this.Button_OnUpgradeClicked);
			this.buttonUpgrade.textDown.text = "UI_UPGRADE".Loc();
			bool flag = am.IsMultipleUpgradeUnlocked();
			bool interactable;
			if (flag)
			{
				Text textDown = this.buttonUpgrade.textDown;
				string text = textDown.text;
				textDown.text = string.Concat(new object[]
				{
					text,
					" (",
					jump,
					")"
				});
				interactable = am.CanUpgrade(af, jump, sim);
			}
			else
			{
				this.multipleBuyIndex = 0;
				cost = am.GetUpgradeCostOf(af, this.manager.sim.GetUniversalBonusAll());
				interactable = am.CanUpgrade(af, sim);
			}
			this.buttonUpgrade.textUp.text = GameMath.GetDoubleString(cost);
			this.buttonUpgrade.gameButton.interactable = interactable;
		}

		private void SetEvolveButton(Simulation.ArtifactSystem.Artifact af, ArtifactsManager am, bool animateFill)
		{
			bool flag = am.ReachedMinLevelForNextRarity(af);
			if (flag)
			{
				bool flag2 = am.CanEvolve(af, this.manager.sim);
				this.buttonEvolve.gameObject.SetActive(true);
				this.buttonEvolveFillerParent.gameObject.SetActive(false);
				this.buttonEvolve.fakeDisabled = !flag2;
				if (flag2 && !this.imageEvolveAvailableGlow.enabled)
				{
					this.imageEvolveAvailableGlow.enabled = true;
					this.imageEvolveAvailableGlow.SetAlpha(0f);
					this.imageEvolveAvailableGlow.DOFade(1f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
				}
				else if (!flag2 && this.imageEvolveAvailableGlow.enabled)
				{
					this.imageEvolveAvailableGlow.enabled = false;
					DOTween.Kill(this.imageEvolveAvailableGlow, true);
				}
			}
			else
			{
				float evolveProgress = am.GetEvolveProgress(af);
				this.buttonEvolve.gameObject.SetActive(false);
				this.buttonEvolveFillerParent.gameObject.SetActive(true);
				if (this.imageEvolveAvailableGlow.enabled)
				{
					this.imageEvolveAvailableGlow.enabled = false;
					DOTween.Kill(this.imageEvolveAvailableGlow, true);
				}
				if (this.fillAmountFlag != evolveProgress)
				{
					this.buttonEvolveFillerFill.fillAmount = evolveProgress;
					this.fillAmountFlag = evolveProgress;
				}
			}
		}

		private void Button_OnRerollUniqueStatClicked(int statIndex)
		{
			Simulation.ArtifactSystem.Artifact selectedArtifact = this.GetSelectedArtifact();
			if (this.manager.sim.artifactsManager.CanReroll(selectedArtifact, statIndex, this.manager.sim))
			{
				this.manager.SetCommand(new UiCommandArtifactRerollUniqueEffect
				{
					artifact = selectedArtifact,
					effectIndex = statIndex
				});
				this.DoRerollAnimation(statIndex);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactReroll, 1f));
			}
			else
			{
				this.manager.state = UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			}
		}

		private void Button_OnEvolveClicked()
		{
			Simulation.ArtifactSystem.Artifact selectedArtifact = this.GetSelectedArtifact();
			if (this.manager.sim.artifactsManager.CanEvolve(selectedArtifact, this.manager.sim))
			{
				this.manager.SetCommand(new UiCommandArtifactEvolve
				{
					artifact = selectedArtifact
				});
				this.manager.state = UiState.ARTIFACT_EVOLVE;
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactEvolve, 1f));
			}
			else
			{
				this.manager.state = UiState.CAN_NOT_EVOLVE_ARTIFACT_POPUP;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupAppear);
			}
			this.forceUpdate = true;
		}

		public Text textHeader;

		public Text textStatsHeader;

		public Text textArtifactStatName;

		public CanvasGroup textArtifactStatNameCanvas;

		public GameButton buttonClose;

		public GameButton buttonBg;

		public GameButton buttonPossibleEffects;

		public GameButton buttonRight;

		public GameButton buttonLeft;

		public RectTransform tooltipParent;

		public SkeletonGraphic buttonClaudronParticle;

		public Image buttonClaudronGlow;

		public Text textTooltip;

		public RectTransform buttonUpgradeParent;

		public RectTransform buttonUpgradeMaxedParent;

		public Text buttonUpgradeMaxedText;

		public ButtonUpgradeAnim buttonUpgrade;

		public RectTransform buttonEvolveParent;

		public RectTransform buttonEvolveMaxedParent;

		public Text buttonEvolveMaxedText;

		public GameButton buttonEvolve;

		public Image imageEvolveAvailableGlow;

		public RectTransform buttonEvolveFillerParent;

		public Image buttonEvolveFillerFill;

		public Text buttonEvolveFillerText;

		public ButtonArtifact buttonArtifact;

		public GameButton buttonFiveX;

		public List<ArtifactUniqueStatWidget> uniqueStatWidgets;

		public RectTransform popupRect;

		public RectTransform statsParent;

		public float basePopupHeight = 640f;

		public float uniqueStatWidgetHeight = 126f;

		public int selectedArtifactIndex;

		public int lastSelectedArtifactIndex;

		public UiState stateToReturn;

		public Image maxedImage;

		public Color colorCannotAfford;

		public Color colorCannotReroll;

		public Color colorDescNormal;

		public Color colorDescLocked;

		public Color colorDescLockedLevelHighlight;

		public Color colorTextDownCannotAfford;

		public Sprite buttonRerollNormal;

		public Sprite buttonEvolveNormal;

		public Sprite buttonUpgradeNormal;

		public Sprite buttonRerollCannotAffordDisabled;

		public Sprite buttonRerollCannotNormalDisabled;

		public AnimationCurve buttonKickCurve;

		public MenuShowCurrency mythstoneCurrency;

		[NonSerialized]
		public UiManager manager;

		public int multipleBuyIndex;

		public bool isAnimatingNavigation;

		private UpgradeSift[] multipleBuySteps = new UpgradeSift[]
		{
			UpgradeSift.One,
			UpgradeSift.Five,
			UpgradeSift.TenPerCent,
			UpgradeSift.TwentyFivePerCent,
			UpgradeSift.Max
		};

		private float fillAmountFlag = -1f;

		private int rerollingIndex = -1;

		public bool forceUpdate;
	}
}
