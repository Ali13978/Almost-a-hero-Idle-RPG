using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class Artifact
	{
		static Artifact()
		{
			Artifact.limitedEffects = new List<ArtifactEffect>();
			Artifact.unlimitedEffects = new List<ArtifactEffect>();
			foreach (ArtifactEffect artifactEffect in Artifact.allEffects)
			{
				if (artifactEffect.IsLimited())
				{
					Artifact.limitedEffects.Add(artifactEffect);
				}
				else
				{
					Artifact.unlimitedEffects.Add(artifactEffect);
				}
			}
		}

		public Artifact()
		{
			this.effects = new List<ArtifactEffect>();
		}

		public static bool IsEffectLimited(ArtifactEffectType type)
		{
			foreach (ArtifactEffect artifactEffect in Artifact.unlimitedEffects)
			{
				if (artifactEffect.GetEffectTypeSelf() == type)
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsArtifactEffectIsLegendary(ArtifactEffectType type)
		{
			foreach (ArtifactEffect artifactEffect in Artifact.legendaryPlusEffects)
			{
				if (artifactEffect.GetEffectTypeSelf() == type)
				{
					return true;
				}
			}
			return false;
		}

		private static ArtifactEffect GetRandomLimitedEffect(double effectQuality)
		{
			return Artifact.GetRandomEffectFrom(Artifact.limitedEffects, effectQuality);
		}

		private static ArtifactEffect GetRandomUnlimitedEffect(double curTotQuality)
		{
			return Artifact.GetRandomEffectFrom(Artifact.unlimitedEffects, curTotQuality);
		}

		private static ArtifactEffect GetRandomEffectFrom(List<ArtifactEffect> effects, double effectQuality)
		{
			float num = 0f;
			foreach (ArtifactEffect artifactEffect in effects)
			{
				if (effectQuality >= artifactEffect.GetReqMinQuality())
				{
					num += artifactEffect.GetChanceWeight();
				}
			}
			if (num <= 0f)
			{
				return null;
			}
			float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.Artifact);
			for (int i = effects.Count - 1; i >= 0; i--)
			{
				ArtifactEffect artifactEffect2 = effects[i];
				if (effectQuality >= artifactEffect2.GetReqMinQuality())
				{
					num2 -= artifactEffect2.GetChanceWeight();
					if (num2 <= 0f)
					{
						return artifactEffect2.GetCopy();
					}
				}
			}
			int num3 = -1;
			for (int j = effects.Count - 1; j >= 0; j--)
			{
				ArtifactEffect artifactEffect3 = effects[j];
				if (effectQuality >= artifactEffect3.GetReqMinQuality())
				{
					if (num3 < 0 || effects[num3].GetChanceWeight() < artifactEffect3.GetChanceWeight())
					{
						num3 = j;
					}
				}
			}
			return effects[num3].GetCopy();
		}

		public static int GetLegendaryEffectsTotalCount()
		{
			return Artifact.legendaryPlusEffects.Count;
		}

		public static Artifact CreateMythical(double cost, List<Artifact> artifacts, int numMythicalArtifacts)
		{
			Artifact artifact = new Artifact();
			List<MythicalArtifactEffect> list = new List<MythicalArtifactEffect>();
			foreach (ArtifactEffect artifactEffect in Artifact.legendaryPlusEffects)
			{
				MythicalArtifactEffect mythicalArtifactEffect = (MythicalArtifactEffect)artifactEffect;
				if (numMythicalArtifacts >= mythicalArtifactEffect.GetMinRequiredMythical())
				{
					bool flag = false;
					foreach (Artifact artifact2 in artifacts)
					{
						foreach (ArtifactEffect artifactEffect2 in artifact2.effects)
						{
							if (artifactEffect2.GetEffectTypeSelf() == mythicalArtifactEffect.GetEffectTypeSelf())
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						list.Add(mythicalArtifactEffect);
					}
				}
			}
			bool flag2 = false;
			foreach (Artifact artifact3 in artifacts)
			{
				foreach (ArtifactEffect artifactEffect3 in artifact3.effects)
				{
					if (artifactEffect3.GetEffectTypeSelf() == ArtifactEffectType.GearMultiplier)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					break;
				}
			}
			MythicalArtifactEffect mythicalArtifactEffect2;
			if (numMythicalArtifacts >= 8 && !flag2)
			{
				mythicalArtifactEffect2 = new MythicalArtifactCustomTailor();
			}
			else
			{
				mythicalArtifactEffect2 = list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.Artifact)];
			}
			mythicalArtifactEffect2.SetRank(0);
			artifact.effects.Add(mythicalArtifactEffect2);
			return artifact;
		}

		public static Artifact CreateRandom(double expectedQuality, List<Artifact> otherArtifacts, UniversalTotalBonus universalTotalBonus)
		{
			Artifact artifact = new Artifact();
			double num = 0.0;
			foreach (Artifact artifact2 in otherArtifacts)
			{
				num += artifact2.GetQuality();
			}
			double num2 = 10.0 + expectedQuality;
			double max = 15.0 + num2 * 1.25;
			double minDouble = GameMath.GetMinDouble(Artifact.ARTIFACT_QP_MAX + universalTotalBonus.qpIncrease, GameMath.GetRandomDouble(num2, max, GameMath.RandType.Artifact));
			float randomFloat = GameMath.GetRandomFloat(0f, 1f, GameMath.RandType.Artifact);
			int num3;
			if (expectedQuality > 50000.0 && randomFloat < 0.6f)
			{
				num3 = 6;
			}
			else if (expectedQuality > 20000.0 && randomFloat < 0.4f)
			{
				num3 = 5;
			}
			else if (randomFloat < 0.2f)
			{
				num3 = 4;
			}
			else if (randomFloat < 0.52f)
			{
				num3 = 3;
			}
			else if (randomFloat < 0.85f)
			{
				num3 = 2;
			}
			else
			{
				num3 = 1;
			}
			double num4 = minDouble / (double)num3;
			double num5 = 0.0;
			int num6 = num3;
			if (num < 5000.0)
			{
				num6--;
				num5 = num4;
			}
			int num7 = 0;
			while (num6-- > 0)
			{
				if (GameMath.GetProbabilityOutcome(0.31f, GameMath.RandType.Artifact))
				{
					num5 += num4 * (0.9 + GameMath.GetRandomDouble(0.0, 0.1, GameMath.RandType.Artifact));
				}
				else
				{
					ArtifactEffect effect = Artifact.GetRandomLimitedEffect(num4);
					if (effect == null)
					{
						num5 += num4 * (0.9 + GameMath.GetRandomDouble(0.0, 0.1, GameMath.RandType.Artifact));
					}
					else if (artifact.effects.Exists((ArtifactEffect x) => x.GetType() == effect.GetType()))
					{
						if (num7 > 10)
						{
							num5 += num4 * (0.9 + GameMath.GetRandomDouble(0.0, 0.1, GameMath.RandType.Artifact));
						}
						else
						{
							num7++;
							num6++;
						}
					}
					else
					{
						effect.SetQuality(num4);
						double amountAllowed = effect.GetAmountAllowed(otherArtifacts);
						if (amountAllowed > effect.GetAmountMin() && effect.GetAmount() > effect.GetAmountMin())
						{
							if (effect.GetAmount() > amountAllowed)
							{
								double quality = effect.GetQuality(amountAllowed);
								effect.SetQuality(quality);
								num5 += (num4 - quality) * (0.9 + GameMath.GetRandomDouble(0.0, 0.1, GameMath.RandType.Artifact));
							}
							artifact.effects.Add(effect);
						}
						else if (num7 > 10)
						{
							num5 += num4 * (0.9 + GameMath.GetRandomDouble(0.0, 0.1, GameMath.RandType.Artifact));
						}
						else
						{
							num7++;
							num6++;
						}
					}
				}
			}
			if (num5 > 0.0)
			{
				if (num == 0.0)
				{
					ArtifactEffect randomUnlimitedEffect = Artifact.GetRandomUnlimitedEffect(0.0);
					randomUnlimitedEffect.SetQuality(num5);
					artifact.effects.Add(randomUnlimitedEffect);
				}
				else
				{
					int num8 = 3 - artifact.effects.Count;
					if (expectedQuality > 50000.0)
					{
						num8 = 5 - artifact.effects.Count;
					}
					int num9 = 1;
					float randomFloat2 = GameMath.GetRandomFloat(0f, 1f, GameMath.RandType.Artifact);
					if (num8 > num9 && randomFloat2 > 0.9f)
					{
						num9++;
					}
					if (num8 > num9 && randomFloat2 > 0.75f)
					{
						num9++;
					}
					if (num8 > num9 && randomFloat2 > 0.6f)
					{
						num9++;
					}
					if (num8 > num9 && randomFloat2 > 0.4f)
					{
						num9++;
					}
					num4 = num5 / (double)num9;
					if (num4 >= 2.0)
					{
						while (num9-- > 0)
						{
							ArtifactEffect unlimitedEffect = Artifact.GetRandomUnlimitedEffect(num4);
							if (artifact.effects.Exists((ArtifactEffect x) => x.GetType() == unlimitedEffect.GetType()))
							{
								num9++;
							}
							else
							{
								unlimitedEffect.SetQuality(num4);
								artifact.effects.Add(unlimitedEffect);
							}
						}
					}
				}
			}
			artifact.CalculateAdditionalBonuses(universalTotalBonus);
			return artifact;
		}

		public void PreApply(UniversalTotalBonus totBonus)
		{
			foreach (ArtifactEffect artifactEffect in this.effects)
			{
				artifactEffect.PreApply(totBonus);
			}
		}

		public void Apply(UniversalTotalBonus totBonus, int totalArtifactsLevel)
		{
			foreach (ArtifactEffect artifactEffect in this.effects)
			{
				artifactEffect.Apply(totBonus, totalArtifactsLevel);
			}
		}

		public void CalculateAdditionalBonuses(UniversalTotalBonus totBonus)
		{
			int level = this.GetLevel();
			foreach (ArtifactEffect artifactEffect in this.effects)
			{
				artifactEffect.CalculateAdditionalBonuses(totBonus, level);
			}
		}

		public string GetName()
		{
			ArtifactEffectCategory categoryType = this.GetCategoryType();
			if (categoryType == ArtifactEffectCategory.ENERGY)
			{
				return LM.Get("ARTIFACT_ENERGY");
			}
			if (categoryType == ArtifactEffectCategory.GOLD)
			{
				return LM.Get("ARTIFACT_GOLD");
			}
			if (categoryType == ArtifactEffectCategory.HEALTH)
			{
				return LM.Get("ARTIFACT_HEALTH");
			}
			if (categoryType == ArtifactEffectCategory.HERO)
			{
				return LM.Get("ARTIFACT_HERO");
			}
			if (categoryType == ArtifactEffectCategory.RING)
			{
				return LM.Get("ARTIFACT_RING");
			}
			if (categoryType == ArtifactEffectCategory.UTILITY)
			{
				return LM.Get("ARTIFACT_UTILITY");
			}
			if (categoryType == ArtifactEffectCategory.MYTH)
			{
				return (this.effects[0] as MythicalArtifactEffect).GetName();
			}
			throw new NotImplementedException();
		}

		public string GetNameEN()
		{
			ArtifactEffectCategory categoryType = this.GetCategoryType();
			if (categoryType == ArtifactEffectCategory.ENERGY)
			{
				return LM.GetFromEN("ARTIFACT_ENERGY");
			}
			if (categoryType == ArtifactEffectCategory.GOLD)
			{
				return LM.GetFromEN("ARTIFACT_GOLD");
			}
			if (categoryType == ArtifactEffectCategory.HEALTH)
			{
				return LM.GetFromEN("ARTIFACT_HEALTH");
			}
			if (categoryType == ArtifactEffectCategory.HERO)
			{
				return LM.GetFromEN("ARTIFACT_HERO");
			}
			if (categoryType == ArtifactEffectCategory.RING)
			{
				return LM.GetFromEN("ARTIFACT_RING");
			}
			if (categoryType == ArtifactEffectCategory.UTILITY)
			{
				return LM.GetFromEN("ARTIFACT_UTILITY");
			}
			if (categoryType == ArtifactEffectCategory.MYTH)
			{
				return (this.effects[0] as MythicalArtifactEffect).GetNameEN();
			}
			throw new NotImplementedException();
		}

		public string GetMythicalLevelStringSimple()
		{
			return StringExtension.Concat((this.GetLegendaryPlusRank() + 1).ToString(), "/", (this.GetLegendaryPlusMaxRank() + 1).ToString());
		}

		public string GetMythicalLevelString()
		{
			return LM.Get("UI_HEROES_LV") + " " + this.GetMythicalLevelStringSimple();
		}

		public int GetLevel()
		{
			int num = 0;
			for (int i = this.effects.Count - 1; i >= 0; i--)
			{
				num = GameMath.GetMaxInt(num, this.effects[i].GetLevel());
			}
			return num;
		}

		public int GetMythicalLevel()
		{
			int result = 0;
			if (this.effects.Count == 0)
			{
				return 0;
			}
			if (this.effects[0] is MythicalArtifactEffect)
			{
				MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
				result = mythicalArtifactEffect.GetRank();
			}
			return result;
		}

		public double GetQuality()
		{
			double num = 0.0;
			for (int i = this.effects.Count - 1; i >= 0; i--)
			{
				num += this.effects[i].GetQuality();
			}
			return num;
		}

		public double GetRerollCost()
		{
			double quality = this.GetQuality();
			if (quality > Artifact.OLDER_ARTIFACT_MAX)
			{
				double num = quality - Artifact.OLDER_ARTIFACT_MAX;
				return GameMath.GetMinDouble(Artifact.MAX_REROLL_COST, Artifact.OLDER_ARTIFACT_MAX * 0.5 + num * 0.5 * GameMath.PowInt(1.0 + num * 0.00012, 2));
			}
			return this.GetQuality() * 0.5;
		}

		public ArtifactEffectCategory GetCategoryType()
		{
			if (!this.artifactTypeCalculated)
			{
				this.artifactTypeCalculated = true;
				List<double> list = new List<double>();
				int length = Enum.GetValues(typeof(ArtifactEffectCategory)).Length;
				for (int i = 0; i < length; i++)
				{
					list.Add(0.0);
				}
				foreach (ArtifactEffect artifactEffect in this.effects)
				{
					List<double> list2;
					int categorySelf;
					(list2 = list)[categorySelf = (int)artifactEffect.GetCategorySelf()] = list2[categorySelf] + artifactEffect.GetQuality();
				}
				int num = -1;
				double num2 = -1.0;
				for (int j = 0; j < length; j++)
				{
					if (list[j] > num2)
					{
						num2 = list[j];
						num = j;
					}
				}
				this.effectCategory = (ArtifactEffectCategory)num;
			}
			return this.effectCategory;
		}

		public double GetUpgradeCost()
		{
			if (this.effects.Count != 1)
			{
				throw new FormatException();
			}
			if (!(this.effects[0] is MythicalArtifactEffect))
			{
				throw new FormatException();
			}
			MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
			return mythicalArtifactEffect.GetUpgradeCost(mythicalArtifactEffect.GetRank());
		}

		public double GetUpgradeCost(int levelShiftCount)
		{
			MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
			double num = 0.0;
			int rank = mythicalArtifactEffect.GetRank();
			for (int i = 0; i < levelShiftCount; i++)
			{
				num += mythicalArtifactEffect.GetUpgradeCost(rank + i);
			}
			return num;
		}

		public int GetAffordableLevelJumpCount(double bid)
		{
			int num = 0;
			MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
			int rank = mythicalArtifactEffect.GetRank();
			int maxRank = mythicalArtifactEffect.GetMaxRank();
			for (;;)
			{
				double upgradeCost = mythicalArtifactEffect.GetUpgradeCost(rank + num);
				if ((upgradeCost > bid && !Cheats.allFree) || num > maxRank)
				{
					break;
				}
				bid -= upgradeCost;
				num++;
			}
			return num;
		}

		public bool CanUpgrade()
		{
			if (this.effects.Count != 1)
			{
				return false;
			}
			if (!(this.effects[0] is MythicalArtifactEffect))
			{
				return false;
			}
			MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
			return mythicalArtifactEffect.GetRank() < mythicalArtifactEffect.GetMaxRank();
		}

		public bool CanUpgrade(int jump)
		{
			if (this.effects.Count != 1)
			{
				return false;
			}
			if (!(this.effects[0] is MythicalArtifactEffect))
			{
				return false;
			}
			if (jump <= 0)
			{
				return false;
			}
			MythicalArtifactEffect mythicalArtifactEffect = this.effects[0] as MythicalArtifactEffect;
			return mythicalArtifactEffect.GetRank() + jump - 1 < mythicalArtifactEffect.GetMaxRank();
		}

		public void Upgrade()
		{
			foreach (ArtifactEffect artifactEffect in this.effects)
			{
				if (artifactEffect is MythicalArtifactEffect)
				{
					(artifactEffect as MythicalArtifactEffect).IncreaseRank();
				}
			}
		}

		public void Upgrade(int amount)
		{
			foreach (ArtifactEffect artifactEffect in this.effects)
			{
				if (artifactEffect is MythicalArtifactEffect)
				{
					(artifactEffect as MythicalArtifactEffect).IncreaseRank(amount);
				}
			}
		}

		public bool IsLegendaryPlus()
		{
			return this.effects.Count == 1 && this.effects[0] is MythicalArtifactEffect;
		}

		public int GetLegendaryPlusRank()
		{
			if (!this.IsLegendaryPlus())
			{
				return 0;
			}
			return (this.effects[0] as MythicalArtifactEffect).GetRank();
		}

		public int GetLegendaryPlusMaxRank()
		{
			if (!this.IsLegendaryPlus())
			{
				return 0;
			}
			return (this.effects[0] as MythicalArtifactEffect).GetMaxRank();
		}

		public bool IsLegendaryPlusMaxRanked()
		{
			return this.IsLegendaryPlus() && (this.effects[0] as MythicalArtifactEffect).IsMaxRanked();
		}

		public bool IsEnabled()
		{
			return !this.IsLegendaryPlus() || !(this.effects[0] as MythicalArtifactEffect).forcedDisable;
		}

		public bool CanBeDisabled()
		{
			return this.IsLegendaryPlus() && (this.effects[0] as MythicalArtifactEffect).CanBeDisabled();
		}

		public void TryEnable()
		{
			if (!this.IsLegendaryPlus() || !this.CanBeDisabled())
			{
				return;
			}
			(this.effects[0] as MythicalArtifactEffect).forcedDisable = false;
		}

		public void TryDisable()
		{
			if (!this.IsLegendaryPlus() || !this.CanBeDisabled())
			{
				return;
			}
			(this.effects[0] as MythicalArtifactEffect).forcedDisable = true;
		}

		public static double ARTIFACT_QP_MAX = 15000.0;

		public static double OLDER_ARTIFACT_MAX = 10000.0;

		public static double MAX_REROLL_COST = 50000000.0;

		private const double NEW_ARTIFACT_REROLL_COST_CONSTANT = 0.00012;

		private static List<ArtifactEffect> allEffects = new List<ArtifactEffect>
		{
			new OLD_ArtifactEffectDamage(),
			new OLD_ArtifactEffectGold(),
			new OLD_ArtifactEffectDamageHero(),
			new OLD_ArtifactEffectHealthHero(),
			new OLD_ArtifactEffectDamageTotem(),
			new OLD_ArtifactEffectBossTime(),
			new OLD_ArtifactEffectChestChance(),
			new OLD_ArtifactEffectCostHeroUpgrade(),
			new OLD_ArtifactEffectCostTotemUpgrade(),
			new OLD_ArtifactEffectCritChanceHero(),
			new OLD_ArtifactEffectCritChanceTotem(),
			new OLD_ArtifactEffectCritFactorHero(),
			new OLD_ArtifactEffectCritFactorTotem(),
			new OLD_ArtifactEffectDamageHeroSkill(),
			new OLD_ArtifactEffectDamageHeroNonSkill(),
			new OLD_ArtifactEffectDroneSpawnRate(),
			new OLD_ArtifactEffectEpicBossDropMythstone(),
			new OLD_ArtifactEffectFreePackCooldown(),
			new OLD_ArtifactEffectGoldBoss(),
			new OLD_ArtifactEffectGoldChest(),
			new OLD_ArtifactEffectGoldMultTenChance(),
			new OLD_ArtifactEffectGoldOffline(),
			new OLD_ArtifactEffectHealthBoss(),
			new OLD_ArtifactEffectHeroLevelReqForSkill(),
			new OLD_ArtifactEffectReviveTime(),
			new OLD_ArtifactEffectUltiCooldown(),
			new OLD_ArtifactEffectWaveSkipChance(),
			new OLD_ArtifactEffectPrestigeMyth(),
			new OLD_ArtifactEffectAutoTapTime(),
			new OLD_ArtifactEffectAutoTapCount(),
			new OLD_ArtifactEffectGoldBagCount(),
			new OLD_ArtifactEffectGoldBagValue(),
			new OLD_ArtifactEffectTimeWarpCount(),
			new OLD_ArtifactEffectTimeWarpSpeed(),
			new OLD_ArtifactEffectTimeWarpDuration(),
			new OLD_ArtifactEffectQuickWaveAfterBoss(),
			new OLD_ArtifactEffectFastSpawn(),
			new OLD_ArtifactEffectHealthEnemy(),
			new OLD_ArtifactEffectDamageEnemy(),
			new OLD_ArtifactEffectDamageBoss(),
			new OLD_ArtifactEffectShieldCount(),
			new OLD_ArtifactEffectShieldDuration(),
			new OLD_ArtifactEffectHorseshoeCount(),
			new OLD_ArtifactEffectHorseshoeDuration(),
			new OLD_ArtifactEffectHorseshoeValue(),
			new OLD_ArtifactEffectDestructionCount()
		};

		private static List<ArtifactEffect> limitedEffects;

		private static List<ArtifactEffect> unlimitedEffects;

		private static List<ArtifactEffect> legendaryPlusEffects = new List<ArtifactEffect>
		{
			new MythicalArtifactBrokenTeleporter(),
			new MythicalArtifactLifeBoat(),
			new MythicalArtifactGoblinLure(),
			new MythicalArtifactPerfectQuasi(),
			new MythicalArtifactCustomTailor(),
			new MythicalArtifactHalfRing(),
			new MythicalArtifactDPSMatter(),
			new MythicalArtifactFreeExploiter(),
			new MythicalArtifactOldCrucible(),
			new MythicalArtifactAutoTransmuter(),
			new MythicalArtifactLazyFinger(),
			new MythicalArtifactShinyObject(),
			new MythicalArtifactBluntRelic(),
			new MythicalArtifactImpatientRelic(),
			new MythicalArtifactBandAidRelic(),
			new MythicalArtifactBodilyHarm(),
			new MythicalArtifactChampionsBounty(),
			new MythicalArtifactCorpusImperium(),
			new MythicalArtifactCrestOfViloence(),
			new MythicalArtifactCrestOfSturdiness(),
			new MythicalArtifactCrestOfUsefulness()
		};

		public List<ArtifactEffect> effects;

		private bool artifactTypeCalculated;

		private ArtifactEffectCategory effectCategory;

		public bool fakePinned;
	}
}
