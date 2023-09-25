using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactCustomTailor : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.gearAddMultiplierFactor += this.gearMultiplierFactor;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CUSTOM_TAILOR;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactCustomTailor mythicalArtifactCustomTailor = new MythicalArtifactCustomTailor();
			mythicalArtifactCustomTailor.SetRank(base.GetRank());
			return mythicalArtifactCustomTailor;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_GEAR_MULTIPLIER");
		}

		public override string GetName()
		{
			return MythicalArtifactCustomTailor.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_GEAR_MULTIPLIER");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.gearMultiplierFactor;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactCustomTailor.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double multiFactor = MythicalArtifactCustomTailor.GetMultiFactor(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactCustomTailor.MAX_RANK)
			{
				double num = MythicalArtifactCustomTailor.GetMultiFactor(rank + levelDiff) - multiFactor;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetDetailedNumberString(num) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_GEAR_MULTIPLIER"), new object[]
			{
				GameMath.GetDetailedNumberString(1.0 + multiFactor) + AM.csart(s),
				GameMath.GetPercentString(MythicalArtifactCustomTailor.totalDamageBonus, false),
				GameMath.GetPercentString(MythicalArtifactCustomTailor.totalHealthBonus, false),
				GameMath.GetPercentString(MythicalArtifactCustomTailor.totalGoldBonus, false)
			});
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactCustomTailor.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactCustomTailor.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.GearMultiplier;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactCustomTailor.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactCustomTailor.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactCustomTailor.MAX_RANK, rank);
			this.gearMultiplierFactor = MythicalArtifactCustomTailor.GetMultiFactor(rank);
		}

		private static double GetMultiFactor(int rank)
		{
			return (GameMath.PowInt(1.015522, rank) - 0.8) * 0.5;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(20 + rank / 2) * (0.85 + (double)(rank % 2 + 1) * 0.075) * (GameMath.PowDouble((double)rank, 1.5) + 10.0);
		}

		public double gearMultiplierFactor;

		public static double totalDamageBonus;

		public static double totalHealthBonus;

		public static double totalGoldBonus;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 299;
	}
}
