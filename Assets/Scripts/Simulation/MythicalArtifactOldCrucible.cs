using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactOldCrucible : MythicalArtifactEffect
	{
		public override void PreApply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.commonArtifactFactor += this.bonusRatio;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.OLD_CRUCIBLE;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(60 + rank / 2) * (0.85 + (double)(rank % 2 + 1) * 0.075) * (double)(rank + 10);
		}

		public override void Apply(UniversalTotalBonus totBonus)
		{
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactOldCrucible mythicalArtifactOldCrucible = new MythicalArtifactOldCrucible();
			mythicalArtifactOldCrucible.SetRank(base.GetRank());
			return mythicalArtifactOldCrucible;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_ARTIFACT_EFFICIENCY");
		}

		public override string GetName()
		{
			return MythicalArtifactOldCrucible.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_ARTIFACT_EFFICIENCY");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.bonusRatio;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactOldCrucible.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double num = MythicalArtifactOldCrucible.GetBonusRatio(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactOldCrucible.MAX_RANK)
			{
				double num2 = MythicalArtifactOldCrucible.GetBonusRatio(rank + levelDiff) - num;
				if (num2 > 0.0)
				{
					s = " (+" + GameMath.GetDetailedNumberString(num2) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_ARTIFACT_EFFICIENCY"), GameMath.GetDetailedNumberString(1.0 + num) + AM.csart(s));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactOldCrucible.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactOldCrucible.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.OldCrucible;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactOldCrucible.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactOldCrucible.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactOldCrucible.MAX_RANK, rank);
			this.bonusRatio = MythicalArtifactOldCrucible.GetBonusRatio(rank);
		}

		private static double GetBonusRatio(int rank)
		{
			return (GameMath.PowInt(1.015522, rank) - 0.8) * 0.5;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double bonusRatio;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 299;
	}
}
