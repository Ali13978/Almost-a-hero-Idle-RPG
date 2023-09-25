using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactCrestOfUsefulness : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.goldFactorWithOneSupporter += this.goldBonusOneSupporter;
			totBonus.goldFactorWithTwoSupporters += this.goldBonusTwoSupporters;
			totBonus.goldFactorWithSeveralSupporters += this.goldBonusSeveralSupporters;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CREST_OF_USEFULNESS;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactCrestOfUsefulness mythicalArtifactCrestOfUsefulness = new MythicalArtifactCrestOfUsefulness();
			mythicalArtifactCrestOfUsefulness.SetRank(base.GetRank());
			return mythicalArtifactCrestOfUsefulness;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_GOLD_BONUS_PER_SUPPORTER");
		}

		public override string GetName()
		{
			return MythicalArtifactCrestOfUsefulness.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_GOLD_BONUS_PER_SUPPORTER");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.goldBonusOneSupporter;
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
			return MythicalArtifactCrestOfUsefulness.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double goldBonusPercentForOneSupporter = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForOneSupporter(rank);
			string s = string.Empty;
			double goldBonusPercentForTwoSupporters = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForTwoSupporters(rank);
			string s2 = string.Empty;
			double goldBonusPercentForSeveralSupporters = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForSeveralSupporters(rank);
			string s3 = string.Empty;
			if (rank < MythicalArtifactCrestOfUsefulness.MAX_RANK)
			{
				double num = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForOneSupporter(rank + levelDiff) - goldBonusPercentForOneSupporter;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForTwoSupporters(rank + levelDiff) - goldBonusPercentForTwoSupporters;
				if (num > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForSeveralSupporters(rank + levelDiff) - goldBonusPercentForSeveralSupporters;
				if (num > 0.0)
				{
					s3 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_GOLD_BONUS_PER_SUPPORTER"), GameMath.GetPercentString(goldBonusPercentForOneSupporter, false) + AM.csart(s), GameMath.GetPercentString(goldBonusPercentForTwoSupporters, false) + AM.csart(s2), GameMath.GetPercentString(goldBonusPercentForSeveralSupporters, false) + AM.csart(s3));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactCrestOfUsefulness.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactCrestOfUsefulness.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.GoldBonusPerSupporter;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactCrestOfUsefulness.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactCrestOfUsefulness.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactCrestOfUsefulness.MAX_RANK, rank);
			this.goldBonusOneSupporter = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForOneSupporter(rank);
			this.goldBonusTwoSupporters = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForTwoSupporters(rank);
			this.goldBonusSeveralSupporters = MythicalArtifactCrestOfUsefulness.GetGoldBonusPercentForSeveralSupporters(rank);
		}

		private static double GetGoldBonusPercentForOneSupporter(int rank)
		{
			return 0.01 * (double)(rank + 1);
		}

		private static double GetGoldBonusPercentForTwoSupporters(int rank)
		{
			return 0.015 * (double)(rank + 1);
		}

		private static double GetGoldBonusPercentForSeveralSupporters(int rank)
		{
			return 0.02 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double goldBonusOneSupporter;

		public double goldBonusTwoSupporters;

		public double goldBonusSeveralSupporters;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 149;
	}
}
