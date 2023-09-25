using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactLazyFinger : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.autoUpgradeMaxCost += this.maxCost;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.LAZY_FINGER;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactLazyFinger mythicalArtifactLazyFinger = new MythicalArtifactLazyFinger();
			mythicalArtifactLazyFinger.SetRank(base.GetRank());
			return mythicalArtifactLazyFinger;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_AUTO_UPGRADE");
		}

		public override string GetName()
		{
			return MythicalArtifactLazyFinger.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_AUTO_UPGRADE");
		}

		public static string GetAmountString(double amount)
		{
			throw new NotImplementedException();
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.maxCost;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactLazyFinger.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double num = MythicalArtifactLazyFinger.GetMaxCost(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactLazyFinger.MAX_RANK)
			{
				double num2 = MythicalArtifactLazyFinger.GetMaxCost(rank + levelDiff) - num;
				if (num2 > 0.0)
				{
					s = " (+" + GameMath.GetDoubleString(num2) + ")";
				}
			}
			string arg = string.Empty;
			if (rank == 89)
			{
				arg = AM.csart(" (" + LM.Get("ARTIFACT_EFFECT_AUTO_UPGRADE_MILESTONE") + ")");
			}
			else if (rank >= 90)
			{
				arg = LM.Get("ARTIFACT_EFFECT_AUTO_UPGRADE_MILESTONE");
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_AUTO_UPGRADE"), GameMath.GetDoubleString(num) + AM.csart(s), arg);
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactLazyFinger.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactLazyFinger.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.AutoUpgrade;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactLazyFinger.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactLazyFinger.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactLazyFinger.MAX_RANK, rank);
			this.maxCost = MythicalArtifactLazyFinger.GetMaxCost(rank);
		}

		public static double GetMaxCost(int rank)
		{
			return 1E+21 * GameMath.PowInt(10.0, rank);
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank);
		}

		public double maxCost;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
