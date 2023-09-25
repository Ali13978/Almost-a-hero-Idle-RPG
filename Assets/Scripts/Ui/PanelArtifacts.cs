using System;
using System.Collections.Generic;
using Simulation;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifacts : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonRectTransforms = new RectTransform[this.buttons.Length];
			for (int i = 0; i < this.buttons.Length; i++)
			{
				int ic = i;
				this.buttons[ic].gameButton.onClick = delegate()
				{
					this.OnClickedButtonArtifact(ic);
				};
				this.buttonRectTransforms[ic] = this.buttons[ic].GetComponent<RectTransform>();
			}
			this.BUTTON_MAX_X = this.buttonRectTransforms[4].anchoredPosition.x;
			this.BUTTON_MIN_Y = this.buttonRectTransforms[0].anchoredPosition.y;
			this.BUTTON_PER_ROW_Y = this.buttonRectTransforms[5].anchoredPosition.y - this.buttonRectTransforms[0].anchoredPosition.y;
			this.totalQualityFontSize = (float)this.textTotalQualityPoints.fontSize;
			this.totalQualityTimer = 0f;
			this.posButtonNormal = this.buttonReroll.GetComponent<RectTransform>().anchoredPosition;
			this.csfArtifactBonuses = this.textArtifactBonuses.GetComponent<LimittedContentSizeFitter>();
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textCraftDescription.text = LM.Get("UI_ARTIFACTS_CRAFT_DESC");
			this.buttonCraft.textDown.text = LM.Get("UI_ARTIFACTS_CRAFT");
			this.buttonUnlockSlot.textDown.text = LM.Get("UI_ARTIFACTS_UNLOCK_SLOT");
			this.textTotalQualityPointsDesc.text = LM.Get("UI_ARTIFACTS_QP") + "    " + LM.Get("UI_ARTIFACTS_TOTAL_QP");
			this.buttonTabMythical.text.text = LM.Get("UI_ARTIFACTS_MYTHICAL");
			this.buttonTabRegular.text.text = LM.Get("UI_ARTIFACTS_REGULAR");
			this.qpPerArtifactLabel.text = LM.Get("UI_ARTIFACTS_QP") + "    " + LM.Get("UI_ARTIFACTS_QP_PER_ARTIFACT");
		}

		public void InitButtonOnClicks(GameButton.VoidFunc craftOnClick, GameButton.VoidFunc rerollOnClick, GameButton.VoidFunc infoOnClick, GameButton.VoidFunc unlockSlotOnClick, GameButton.VoidFunc enableDisableOnClick)
		{
			this.buttonCraft.gameButton.onClick = craftOnClick;
			this.buttonReroll.gameButton.onClick = rerollOnClick;
			this.buttonInfo.onClick = infoOnClick;
			this.buttonUnlockSlot.gameButton.onClick = unlockSlotOnClick;
			this.buttonEnableDisable.gameButton.onClick = enableDisableOnClick;
		}

		public void SetPanelSelectedArtifactSize()
		{
			if (this.selected >= 0 && this.artifacts[this.selected].IsLegendaryPlus())
			{
				float y = this.textArtifactBonuses.rectTransform.sizeDelta.y + 250f;
				this.textArtifactBonuses.rectTransform.SetSizeDeltaX(670f);
				this.panelSelectedArtifact.sizeDelta = new Vector2(this.panelSelectedArtifact.sizeDelta.x, y);
			}
			else
			{
				float y2 = this.textArtifactPercent.rectTransform.sizeDelta.y + 250f;
				this.textArtifactBonuses.rectTransform.sizeDelta = new Vector2(this.textArtifactBonuses.rectTransform.sizeDelta.x, this.textArtifactPercent.rectTransform.sizeDelta.y + 10f);
				this.panelSelectedArtifact.sizeDelta = new Vector2(this.panelSelectedArtifact.sizeDelta.x, y2);
			}
		}

		public void SetRerollButtonNormal()
		{
			this.buttonReroll.rectTransform.anchorMax = new Vector2(0.5f, 0f);
			this.buttonReroll.rectTransform.anchorMin = new Vector2(0.5f, 0f);
			this.buttonReroll.rectTransform.anchoredPosition = new Vector2(0f, 78f);
		}

		public void SetRerollButtonMythical()
		{
			this.buttonReroll.rectTransform.anchorMax = new Vector2(1f, 1f);
			this.buttonReroll.rectTransform.anchorMin = new Vector2(1f, 1f);
			this.buttonReroll.rectTransform.anchoredPosition = new Vector2(0f, 78f);
		}

		private void OnClickedButtonArtifact(int index)
		{
			if (this.selected != index)
			{
				this.selected = index;
				if (!TutorialManager.IsThereTutorialCurrently())
				{
					this.uiManager.SetScrollViewContentY(this.buttons[index].rectTransform.anchoredPosition.y - 500f);
				}
			}
			else
			{
				this.selected = -1;
			}
			this.unlockButtonWaitingForConfirm = false;
			UiManager.stateJustChanged = true;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotSelect, 1f));
		}

		private void PlaceButton(RectTransform buttonRectTransform, int i, int selectedUiIndex)
		{
			if (buttonRectTransform.parent != this.parentArtifactSelect)
			{
				return;
			}
			float x = -this.BUTTON_MAX_X + this.BUTTON_MAX_X * 2f / 4f * (float)(i % 5);
			int num = i / 5;
			float num2 = this.BUTTON_MIN_Y + this.BUTTON_PER_ROW_Y * (float)num;
			if (selectedUiIndex != -1 && selectedUiIndex / 5 < num)
			{
				num2 -= this.panelSelectedArtifact.sizeDelta.y + 80f;
			}
			buttonRectTransform.anchoredPosition = new Vector2(x, num2);
		}

		public void SetCraftButton(string costString, bool canCraft, bool canAffordCraft)
		{
			if (UiManager.stateJustChanged)
			{
				this.buttonCraft.textUp.text = costString;
			}
			this.buttonCraft.openWarningPopup = !canCraft;
			this.buttonCraft.textUpCantAffordColorChangeForced = !canAffordCraft;
			this.buttonCraft.textDownCantAffordColorChangeForced = !canCraft;
		}

		public void SetRerollButton(string costString, bool interactable)
		{
			if (UiManager.stateJustChanged)
			{
				this.buttonReroll.textUp.text = costString;
			}
			this.buttonReroll.gameButton.interactable = interactable;
		}

		public string GetAttributeString(int i, bool isAmount, out double amount, out bool maxedOut)
		{
			switch (i)
			{
			case 0:
				amount = (double)this.universalTotalBonus.bossTimeAdd;
				maxedOut = ((double)this.universalTotalBonus.bossTimeAdd >= 89.55);
				if (isAmount)
				{
					return OLD_ArtifactEffectBossTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectBossTime.GetString();
			case 1:
				amount = (double)(this.universalTotalBonus.chestChanceFactor - 1f);
				maxedOut = ((double)this.universalTotalBonus.chestChanceFactor >= 3.98);
				if (isAmount)
				{
					return OLD_ArtifactEffectChestChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectChestChance.GetString();
			case 2:
				amount = this.universalTotalBonus.costHeroUpgradeFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.costHeroUpgradeFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectCostHeroUpgrade.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCostHeroUpgrade.GetString();
			case 3:
				amount = this.universalTotalBonus.costTotemUpgradeFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.costTotemUpgradeFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectCostTotemUpgrade.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCostTotemUpgrade.GetString();
			case 4:
				amount = (double)this.universalTotalBonus.critChanceHeroAdd;
				maxedOut = ((double)this.universalTotalBonus.critChanceHeroAdd >= 0.398);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritChanceHero.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectCritChanceHero.GetString();
			case 5:
				amount = (double)this.universalTotalBonus.critChanceTotemAdd;
				maxedOut = ((double)this.universalTotalBonus.critChanceTotemAdd >= 0.398);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritChanceTotem.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectCritChanceTotem.GetString();
			case 6:
				amount = this.universalTotalBonus.critFactorHeroAdd;
				maxedOut = (this.universalTotalBonus.critFactorHeroAdd >= 5.97);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritFactorHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCritFactorHero.GetString();
			case 7:
				amount = this.universalTotalBonus.critFactorTotemAdd;
				maxedOut = (this.universalTotalBonus.critFactorTotemAdd >= 5.97);
				if (isAmount)
				{
					return OLD_ArtifactEffectCritFactorTotem.GetAmountString(amount);
				}
				return OLD_ArtifactEffectCritFactorTotem.GetString();
			case 8:
				amount = this.universalTotalBonus.damageFactor - 1.0;
				maxedOut = (this.universalTotalBonus.damageFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamage.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamage.GetString();
			case 9:
				amount = this.universalTotalBonus.damageHeroFactor - 1.0;
				maxedOut = (this.universalTotalBonus.damageHeroFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHero.GetString();
			case 10:
				amount = this.universalTotalBonus.damageHeroSkillFactor - 1.0;
				maxedOut = (this.universalTotalBonus.damageHeroSkillFactor >= 2.985);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHeroSkill.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHeroSkill.GetString();
			case 11:
				amount = this.universalTotalBonus.damageTotemFactor - 1.0;
				maxedOut = (this.universalTotalBonus.damageTotemFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageTotem.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageTotem.GetString();
			case 12:
				amount = (double)(this.universalTotalBonus.dragonSpawnRateFactor - 1f);
				maxedOut = ((double)(this.universalTotalBonus.dragonSpawnRateFactor - 1f) >= 1.99);
				if (isAmount)
				{
					return OLD_ArtifactEffectDroneSpawnRate.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectDroneSpawnRate.GetString();
			case 13:
				amount = (double)this.universalTotalBonus.epicBossDropMythstonesAdd;
				maxedOut = ((double)this.universalTotalBonus.epicBossDropMythstonesAdd >= 300.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectEpicBossDropMythstone.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectEpicBossDropMythstone.GetString();
			case 14:
				amount = (double)(this.universalTotalBonus.freePackCooldownFactor - 1f);
				maxedOut = ((double)(1f - this.universalTotalBonus.freePackCooldownFactor) >= 0.74625);
				if (isAmount)
				{
					return OLD_ArtifactEffectFreePackCooldown.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectFreePackCooldown.GetString();
			case 15:
				amount = this.universalTotalBonus.goldFactor - 1.0;
				maxedOut = (this.universalTotalBonus.goldFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectGold.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGold.GetString();
			case 16:
				amount = this.universalTotalBonus.goldBossFactor - 1.0;
				maxedOut = (this.universalTotalBonus.goldBossFactor - 1.0 >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldBoss.GetString();
			case 17:
				amount = this.universalTotalBonus.goldChestFactor - 1.0;
				maxedOut = (this.universalTotalBonus.goldChestFactor - 1.0 >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldChest.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldChest.GetString();
			case 18:
				amount = (double)this.universalTotalBonus.goldMultTenChanceAdd;
				maxedOut = ((double)this.universalTotalBonus.goldMultTenChanceAdd >= 0.995);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldMultTenChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectGoldMultTenChance.GetString();
			case 19:
				amount = this.universalTotalBonus.goldOfflineFactor - 1.0;
				maxedOut = (this.universalTotalBonus.goldOfflineFactor >= 7.96);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldOffline.GetAmountString(amount);
				}
				return OLD_ArtifactEffectGoldOffline.GetString();
			case 20:
				amount = this.universalTotalBonus.healthBossFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.healthBossFactor >= 0.8955);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthBoss.GetString();
			case 21:
				amount = this.universalTotalBonus.healthHeroFactor - 1.0;
				maxedOut = (this.universalTotalBonus.healthHeroFactor >= double.PositiveInfinity);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthHero.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthHero.GetString();
			case 22:
				amount = (double)this.universalTotalBonus.heroLevelRequiredForSkillDecrease;
				maxedOut = ((double)this.universalTotalBonus.heroLevelRequiredForSkillDecrease >= 8.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHeroLevelReqForSkill.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectHeroLevelReqForSkill.GetString();
			case 23:
				amount = (double)(this.universalTotalBonus.reviveTimeFactor - 1f);
				maxedOut = ((double)(1f - this.universalTotalBonus.reviveTimeFactor) >= 0.64675);
				if (isAmount)
				{
					return OLD_ArtifactEffectReviveTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectReviveTime.GetString();
			case 24:
				amount = (double)(this.universalTotalBonus.ultiCoolDownMaxFactor - 1f);
				maxedOut = ((double)(1f - this.universalTotalBonus.ultiCoolDownMaxFactor) >= 0.4975);
				if (isAmount)
				{
					return OLD_ArtifactEffectUltiCooldown.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectUltiCooldown.GetString();
			case 25:
				amount = (double)this.universalTotalBonus.waveSkipChanceAdd;
				maxedOut = ((double)this.universalTotalBonus.waveSkipChanceAdd >= 0.94524999999999992);
				if (isAmount)
				{
					return OLD_ArtifactEffectWaveSkipChance.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectWaveSkipChance.GetString();
			case 26:
				amount = this.universalTotalBonus.prestigeMythFactor - 1.0;
				maxedOut = (this.universalTotalBonus.prestigeMythFactor - 1.0 >= 14.925);
				if (isAmount)
				{
					return OLD_ArtifactEffectPrestigeMyth.GetAmountString(amount);
				}
				return OLD_ArtifactEffectPrestigeMyth.GetString();
			case 27:
				amount = (double)this.universalTotalBonus.autoTapDurationAdd;
				maxedOut = ((double)this.universalTotalBonus.autoTapDurationAdd >= 995.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectAutoTapTime.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectAutoTapTime.GetString();
			case 28:
				amount = (double)this.universalTotalBonus.autoTapCountAdd;
				maxedOut = ((double)this.universalTotalBonus.autoTapCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectAutoTapCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectAutoTapCount.GetString();
			case 29:
				amount = (double)this.universalTotalBonus.goldBagCountAdd;
				maxedOut = ((double)this.universalTotalBonus.goldBagCountAdd >= 8.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBagCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectGoldBagCount.GetString();
			case 30:
				amount = (double)this.universalTotalBonus.timeWarpCountAdd;
				maxedOut = ((double)this.universalTotalBonus.timeWarpCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectTimeWarpCount.GetString();
			case 31:
				amount = (double)(this.universalTotalBonus.goldBagValueFactor - 1f);
				maxedOut = ((double)(this.universalTotalBonus.goldBagValueFactor - 1f) >= 2.985);
				if (isAmount)
				{
					return OLD_ArtifactEffectGoldBagValue.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectGoldBagValue.GetString();
			case 32:
				amount = (double)(this.universalTotalBonus.timeWarpSpeedFactor - 1f);
				maxedOut = ((double)(this.universalTotalBonus.timeWarpSpeedFactor - 1f) >= 1.99);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpSpeed.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectTimeWarpSpeed.GetString();
			case 33:
				amount = (double)this.universalTotalBonus.timeWarpDurationAdd;
				maxedOut = ((double)this.universalTotalBonus.timeWarpDurationAdd >= 298.5);
				if (isAmount)
				{
					return OLD_ArtifactEffectTimeWarpDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectTimeWarpDuration.GetString();
			case 34:
				amount = (double)(this.universalTotalBonus.afterBossDurationFactor - 1f);
				maxedOut = ((double)(this.universalTotalBonus.afterBossDurationFactor - 1f) >= 1.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectQuickWaveAfterBoss.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectQuickWaveAfterBoss.GetString();
			case 35:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactHalfRing));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactHalfRing));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactHalfRing.GetNameStatic();
			}
			case 36:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBrokenTeleporter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactBrokenTeleporter));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBrokenTeleporter.GetNameStatic();
			}
			case 37:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactLifeBoat));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactLifeBoat));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactLifeBoat.GetNameStatic();
			}
			case 38:
				amount = (double)this.universalTotalBonus.fastEnemySpawnBelow;
				maxedOut = ((double)this.universalTotalBonus.fastEnemySpawnBelow >= 900.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectFastSpawn.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectFastSpawn.GetString();
			case 39:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactGoblinLure));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactGoblinLure));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactGoblinLure.GetNameStatic();
			}
			case 40:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactPerfectQuasi));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactPerfectQuasi));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactPerfectQuasi.GetNameStatic();
			}
			case 41:
				amount = this.universalTotalBonus.healthEnemyFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.healthEnemyFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectHealthEnemy.GetAmountString(amount);
				}
				return OLD_ArtifactEffectHealthEnemy.GetString();
			case 42:
				amount = this.universalTotalBonus.damageEnemyFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.damageEnemyFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageEnemy.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageEnemy.GetString();
			case 43:
				amount = this.universalTotalBonus.damageBossFactor - 1.0;
				maxedOut = (1.0 - this.universalTotalBonus.damageBossFactor >= 0.895);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageBoss.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageBoss.GetString();
			case 44:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactCustomTailor));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactCustomTailor));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactCustomTailor.GetNameStatic();
			}
			case 45:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactDPSMatter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactDPSMatter));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactDPSMatter.GetNameStatic();
			}
			case 46:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactFreeExploiter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactFreeExploiter));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactFreeExploiter.GetNameStatic();
			}
			case 47:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactOldCrucible));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactOldCrucible));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactOldCrucible.GetNameStatic();
			}
			case 48:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactAutoTransmuter));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactAutoTransmuter));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactAutoTransmuter.GetNameStatic();
			}
			case 49:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactLazyFinger));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactLazyFinger));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactLazyFinger.GetNameStatic();
			}
			case 50:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactShinyObject));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactShinyObject));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactShinyObject.GetNameStatic();
			}
			case 51:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBluntRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactBluntRelic));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBluntRelic.GetNameStatic();
			}
			case 52:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactImpatientRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactImpatientRelic));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactImpatientRelic.GetNameStatic();
			}
			case 53:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBandAidRelic));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactBandAidRelic));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBandAidRelic.GetNameStatic();
			}
			case 54:
				amount = this.universalTotalBonus.damageHeroNonSkillFactor - 1.0;
				maxedOut = (this.universalTotalBonus.damageHeroNonSkillFactor >= 4.975);
				if (isAmount)
				{
					return OLD_ArtifactEffectDamageHeroNonSkill.GetAmountString(amount);
				}
				return OLD_ArtifactEffectDamageHeroNonSkill.GetString();
			case 55:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactBodilyHarm));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactBodilyHarm));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactBodilyHarm.GetNameStatic();
			}
			case 56:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactChampionsBounty));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactChampionsBounty));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactChampionsBounty.GetNameStatic();
			}
			case 57:
			{
				Artifact artifactMythical = this.GetArtifactMythical(typeof(MythicalArtifactCorpusImperium));
				MythicalArtifactEffect artifactEffectMythical = this.GetArtifactEffectMythical(typeof(MythicalArtifactCorpusImperium));
				amount = ((artifactEffectMythical == null) ? 0.0 : artifactEffectMythical.GetAmount());
				maxedOut = (artifactEffectMythical != null && artifactEffectMythical.GetRank() == artifactEffectMythical.GetMaxRank());
				if (isAmount)
				{
					return (artifactMythical == null) ? "0" : artifactMythical.GetMythicalLevelStringSimple();
				}
				return MythicalArtifactCorpusImperium.GetNameStatic();
			}
			case 58:
				amount = (double)this.universalTotalBonus.shieldCountAdd;
				maxedOut = ((double)this.universalTotalBonus.shieldCountAdd >= 3.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectShieldCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectShieldCount.GetString();
			case 59:
				amount = (double)this.universalTotalBonus.shieldDurationAdd;
				maxedOut = ((double)this.universalTotalBonus.shieldDurationAdd >= 119.4);
				if (isAmount)
				{
					return OLD_ArtifactEffectShieldDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectShieldDuration.GetString();
			case 60:
				amount = (double)this.universalTotalBonus.horseshoeCountAdd;
				maxedOut = ((double)this.universalTotalBonus.horseshoeCountAdd >= 6.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectHorseshoeCount.GetString();
			case 61:
				amount = (double)this.universalTotalBonus.horseshoeDurationAdd;
				maxedOut = ((double)this.universalTotalBonus.horseshoeDurationAdd >= 597.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeDuration.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectHorseshoeDuration.GetString();
			case 62:
				amount = (double)(this.universalTotalBonus.horseshoeValueFactor - 1f);
				maxedOut = ((double)(this.universalTotalBonus.horseshoeValueFactor - 1f) >= 3.98);
				if (isAmount)
				{
					return OLD_ArtifactEffectHorseshoeValue.GetAmountString((float)amount);
				}
				return OLD_ArtifactEffectHorseshoeValue.GetString();
			case 63:
				amount = (double)this.universalTotalBonus.destructionCountAdd;
				maxedOut = ((double)this.universalTotalBonus.destructionCountAdd >= 4.0);
				if (isAmount)
				{
					return OLD_ArtifactEffectDestructionCount.GetAmountString((int)amount);
				}
				return OLD_ArtifactEffectDestructionCount.GetString();
			default:
				throw new NotImplementedException();
			}
		}

		private MythicalArtifactEffect GetArtifactEffectMythical(Type t)
		{
			int i = 0;
			int count = this.artifactsMythical.Count;
			while (i < count)
			{
				if (this.artifactsMythical[i].effects[0].GetType() == t)
				{
					return this.artifactsMythical[i].effects[0] as MythicalArtifactEffect;
				}
				i++;
			}
			return null;
		}

		private Artifact GetArtifactMythical(Type t)
		{
			int i = 0;
			int count = this.artifactsMythical.Count;
			while (i < count)
			{
				if (this.artifactsMythical[i].effects[0].GetType() == t)
				{
					return this.artifactsMythical[i];
				}
				i++;
			}
			return null;
		}

		public void SetArtifactsDetails()
		{
			int i = 0;
			int count = this.artifacts.Count;
			while (i < count)
			{
				ButtonArtifact buttonArtifact = this.buttons[i];
				buttonArtifact.artifactStone.imageIcon.sprite = this.GetEffectTypeSprite(this.artifacts[i].GetCategoryType(), this.artifacts[i].effects[0].GetType());
				Graphic imageIcon = buttonArtifact.artifactStone.imageIcon;
				Color color = new Color(1f, 1f, 1f, (!this.artifacts[i].IsEnabled()) ? 0.5f : 1f);
				buttonArtifact.artifactStone.imageStone.color = color;
				imageIcon.color = color;
				buttonArtifact.artifactStone.imageIcon.SetNativeSize();
				buttonArtifact.qualityPointString = GameMath.GetDoubleString(this.artifacts[i].GetQuality());
				buttonArtifact.name = this.artifacts[i].GetName();
				buttonArtifact.visualLevel = this.artifacts[i].GetLevel();
				buttonArtifact.maxed = this.artifacts[i].IsLegendaryPlusMaxRanked();
				if (this.selected == i)
				{
					if (this.artifacts[i].IsLegendaryPlus())
					{
						this.textArtifactBonuses.text = this.artifacts[i].effects[0].GetStringSelf(1);
						this.csfArtifactBonuses.horizontalFit = LimittedContentSizeFitter.FitMode.Unconstrained;
						this.csfArtifactBonuses.verticalFit = LimittedContentSizeFitter.FitMode.PreferredSize;
						this.textArtifactPercent.enabled = false;
						this.buttonReroll.GetComponent<RectTransform>().anchoredPosition = this.posButtonNormal;
					}
					else
					{
						this.csfArtifactBonuses.horizontalFit = LimittedContentSizeFitter.FitMode.PreferredSize;
						this.csfArtifactBonuses.verticalFit = LimittedContentSizeFitter.FitMode.Unconstrained;
						this.textArtifactPercent.enabled = true;
						string text = string.Empty;
						string text2 = string.Empty;
						int j = 0;
						int count2 = this.artifacts[i].effects.Count;
						while (j < count2)
						{
							string stringSelf = this.artifacts[i].effects[j].GetStringSelf(1);
							text += stringSelf;
							text2 += this.artifacts[i].effects[j].GetAmountString();
							if (j < count2 - 1)
							{
								text += "\n";
								text2 += "\n";
							}
							j++;
						}
						this.textArtifactBonuses.text = text;
						this.textArtifactPercent.text = text2;
					}
				}
				i++;
			}
		}

		public Sprite GetEffectTypeSprite(ArtifactEffectCategory aet, Type type)
		{
			switch (aet)
			{
			case ArtifactEffectCategory.HERO:
				return this.spriteIconHero;
			case ArtifactEffectCategory.RING:
				return this.spriteIconTotem;
			case ArtifactEffectCategory.UTILITY:
				return this.spriteIconUtility;
			case ArtifactEffectCategory.GOLD:
				return this.spriteIconGold;
			case ArtifactEffectCategory.ENERGY:
				return this.spriteIconEnergy;
			case ArtifactEffectCategory.HEALTH:
				return this.spriteIconHealth;
			case ArtifactEffectCategory.MYTH:
				if (type == typeof(MythicalArtifactDPSMatter))
				{
					return this.spriteIconMythDpsMatter;
				}
				if (type == typeof(MythicalArtifactOldCrucible))
				{
					return this.spriteIconMythOldCrucible;
				}
				if (type == typeof(MythicalArtifactFreeExploiter))
				{
					return this.spriteIconMythFreeExploiter;
				}
				if (type == typeof(MythicalArtifactCustomTailor))
				{
					return this.spriteIconMythCustomTailor;
				}
				if (type == typeof(MythicalArtifactLifeBoat))
				{
					return this.spriteIconMythLifeBoat;
				}
				if (type == typeof(MythicalArtifactAutoTransmuter))
				{
					return this.spriteIconMythAutoTransmuter;
				}
				if (type == typeof(MythicalArtifactPerfectQuasi))
				{
					return this.spriteIconMythPerfectQuasi;
				}
				if (type == typeof(MythicalArtifactBrokenTeleporter))
				{
					return this.spriteIconMythBrokenTeleporter;
				}
				if (type == typeof(MythicalArtifactHalfRing))
				{
					return this.spriteIconMythHalfRing;
				}
				if (type == typeof(MythicalArtifactGoblinLure))
				{
					return this.spriteIconMythGoblinLure;
				}
				if (type == typeof(MythicalArtifactLazyFinger))
				{
					return this.spriteIconMythLazyFinger;
				}
				if (type == typeof(MythicalArtifactShinyObject))
				{
					return this.spriteIconMythShinyObject;
				}
				if (type == typeof(MythicalArtifactBluntRelic))
				{
					return this.spriteIconMythPowerupCritChance;
				}
				if (type == typeof(MythicalArtifactImpatientRelic))
				{
					return this.spriteIconMythPowerupCooldown;
				}
				if (type == typeof(MythicalArtifactBandAidRelic))
				{
					return this.spriteIconMythPowerupRevive;
				}
				if (type == typeof(MythicalArtifactBodilyHarm))
				{
					return this.spriteIconMythBodilyHarm;
				}
				if (type == typeof(MythicalArtifactChampionsBounty))
				{
					return this.spriteIconMythChampionsBounty;
				}
				if (type == typeof(MythicalArtifactCorpusImperium))
				{
					return this.spriteIconMythCorpusImperium;
				}
				return null;
			default:
				throw new NotImplementedException();
			}
		}

		public void SetButtonUnlockSlot(string unlockSlotCost, bool canUnlockSlot)
		{
			if (UiManager.stateJustChanged)
			{
				this.buttonUnlockSlot.textUp.text = unlockSlotCost;
			}
			this.buttonUnlockSlot.gameButton.interactable = canUnlockSlot;
		}

		public Vector2 GetSelectedArtifactPosition()
		{
			return this.buttons[this.selected].artifactStone.transform.position;
		}

		public RectTransform GetSelectedArtifactWidget()
		{
			return this.buttons[this.selected].rectTransform;
		}

		public RectTransform GetSelectedArtifactStone()
		{
			return this.buttons[this.selected].artifactStone.rectTransform;
		}

		public Vector2 GetSelectedArtifactAnchoredPosition()
		{
			return this.buttons[this.selected].rectTransform.anchoredPosition;
		}

		public ButtonArtifact GetSelectedArtifactButton()
		{
			return this.buttons[this.selected];
		}

		public void OnClickUpgrade()
		{
			if (this.spineSkillUpgrade.AnimationState != null)
			{
				this.spineSkillUpgrade.transform.localScale = new Vector3(2f, 2f, 1f);
				this.spineSkillUpgrade.AnimationState.SetAnimation(0, this.spineAnim, false);
				this.spineSkillUpgrade.rectTransform.position = this.buttons[this.selected].rectTransform.position + Vector3.up * this.spinePosYOffset;
			}
		}

		public Sprite spriteIconHero;

		public Sprite spriteIconTotem;

		public Sprite spriteIconUtility;

		public Sprite spriteIconGold;

		public Sprite spriteIconEnergy;

		public Sprite spriteIconHealth;

		public Sprite spriteIconMythAutoTransmuter;

		public Sprite spriteIconMythBrokenTeleporter;

		public Sprite spriteIconMythCustomTailor;

		public Sprite spriteIconMythDpsMatter;

		public Sprite spriteIconMythFreeExploiter;

		public Sprite spriteIconMythGoblinLure;

		public Sprite spriteIconMythHalfRing;

		public Sprite spriteIconMythLazyFinger;

		public Sprite spriteIconMythLifeBoat;

		public Sprite spriteIconMythOldCrucible;

		public Sprite spriteIconMythPerfectQuasi;

		public Sprite spriteIconMythShinyObject;

		public Sprite spriteIconMythPowerupCritChance;

		public Sprite spriteIconMythPowerupCooldown;

		public Sprite spriteIconMythPowerupRevive;

		public Sprite spriteIconMythBodilyHarm;

		public Sprite spriteIconMythChampionsBounty;

		public Sprite spriteIconMythCorpusImperium;

		public Text textCraftDescription;

		public ButtonUpgradeAnim buttonCraft;

		public ButtonUpgradeAnim buttonReroll;

		public ButtonOnOff buttonEnableDisable;

		public ButtonUpgradeAnim buttonUnlockSlot;

		public Text textTotalQualityPoints;

		public Text textTotalQualityPointsDesc;

		public GameButton buttonInfo;

		public ButtonArtifact[] buttons;

		public int selected = -1;

		public int numAvailableSlots = 5;

		public List<Artifact> artifacts;

		public List<Artifact> artifactsRegular;

		public List<Artifact> artifactsMythical;

		public RectTransform panelSelectedArtifact;

		private Vector2 posButtonNormal;

		public Vector2 posButtonEdge;

		public Text textArtifactName;

		public Text textArtifactLevel;

		public Text textArtifactPercent;

		public Text textArtifactBonuses;

		private LimittedContentSizeFitter csfArtifactBonuses;

		public Text qpPerArtifactAmount;

		public Text qpPerArtifactLabel;

		public RectTransform parentArtifactSelect;

		private RectTransform[] buttonRectTransforms;

		private const int BUTTONS_PER_ROW = 5;

		private const float PANEL_SELECTED_ARTIFACT_Y = 80f;

		private float BUTTON_MAX_X;

		private float BUTTON_MIN_Y;

		private float BUTTON_PER_ROW_Y;

		private const float MIN_LEGENDARY_PLUS_PANEL_SIZE = 340f;

		public Vector2 MIN_LEGENDARY_PLUS_TEXT_SIZE;

		public UniversalTotalBonus universalTotalBonus;

		public bool unlockButtonWaitingForConfirm;

		public float totalQualityTimer;

		public float totalQualityPeriod = 1f;

		public double totalQualityOld;

		public float totalQualityFontSize;

		public float totalQualityTextMaxScale = 1.5f;

		public UiManager uiManager;

		public Color colorTextArtifactName;

		public GameObject maxedOut;

		public SkeletonGraphic spineSkillUpgrade;

		[SpineAnimation("", "", true, false)]
		private string spineAnim = "upgrade";

		[SerializeField]
		private float spinePosYOffset;

		[SerializeField]
		private RectTransform rectTabButtons;

		public GameButton buttonTabRegular;

		[SerializeField]
		private RectTransform rectTabBg;

		public GameButton buttonTabMythical;

		public bool isLookingAtMythical;

		public bool canLookAtMythical;

		public GameObject parentTabButtons;

		public GameObject allParent;

		public GameObject mythicalTabLockIcon;

		public GameObject noMythicalParent;

		public Text hintText;

		[SerializeField]
		private Image imageAltar;

		[SerializeField]
		private Sprite spriteAltarMythical;

		public GameButton buttonPin;

		public GameObject pinned;
	}
}
