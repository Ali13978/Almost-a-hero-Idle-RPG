using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactCrestOfSturdiness : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.healthHeroFactorWithOneDefender += this.healthBonusOneDefender;
			totBonus.healthHeroFactorWithTwoDefenders += this.healthBonusTwoDefenders;
			totBonus.healthHeroFactorWithSeveralDefenders += this.healthBonusSeveralDefenders;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CREST_OF_STURDINESS;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactCrestOfSturdiness mythicalArtifactCrestOfSturdiness = new MythicalArtifactCrestOfSturdiness();
			mythicalArtifactCrestOfSturdiness.SetRank(base.GetRank());
			return mythicalArtifactCrestOfSturdiness;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_HERO_HEALTH_PER_DEFENDER");
		}

		public override string GetName()
		{
			return MythicalArtifactCrestOfSturdiness.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_HERO_HEALTH_PER_DEFENDER");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.healthBonusOneDefender;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 120);
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactCrestOfSturdiness.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double healthBonusPercentForOneDefender = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForOneDefender(rank);
			string s = string.Empty;
			double healthBonusPercentForTwoDefenders = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForTwoDefenders(rank);
			string s2 = string.Empty;
			double healthBonusPercentForSeveralDefenders = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForSeveralDefenders(rank);
			string s3 = string.Empty;
			if (rank < MythicalArtifactCrestOfSturdiness.MAX_RANK)
			{
				double num = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForOneDefender(rank + levelDiff) - healthBonusPercentForOneDefender;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForTwoDefenders(rank + levelDiff) - healthBonusPercentForTwoDefenders;
				if (num > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForSeveralDefenders(rank + levelDiff) - healthBonusPercentForSeveralDefenders;
				if (num > 0.0)
				{
					s3 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_HERO_HEALTH_PER_DEFENDER"), GameMath.GetPercentString(healthBonusPercentForOneDefender, false) + AM.csart(s), GameMath.GetPercentString(healthBonusPercentForTwoDefenders, false) + AM.csart(s2), GameMath.GetPercentString(healthBonusPercentForSeveralDefenders, false) + AM.csart(s3));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactCrestOfSturdiness.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactCrestOfSturdiness.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HeroHealthPerDefender;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactCrestOfSturdiness.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactCrestOfSturdiness.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactCrestOfSturdiness.MAX_RANK, rank);
			this.healthBonusOneDefender = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForOneDefender(rank);
			this.healthBonusTwoDefenders = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForTwoDefenders(rank);
			this.healthBonusSeveralDefenders = MythicalArtifactCrestOfSturdiness.GetHealthBonusPercentForSeveralDefenders(rank);
		}

		private static double GetHealthBonusPercentForOneDefender(int rank)
		{
			return 0.02 * (double)(rank + 1);
		}

		private static double GetHealthBonusPercentForTwoDefenders(int rank)
		{
			return 0.03 * (double)(rank + 1);
		}

		private static double GetHealthBonusPercentForSeveralDefenders(int rank)
		{
			return 0.04 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double healthBonusOneDefender;

		public double healthBonusTwoDefenders;

		public double healthBonusSeveralDefenders;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 149;
	}
}
