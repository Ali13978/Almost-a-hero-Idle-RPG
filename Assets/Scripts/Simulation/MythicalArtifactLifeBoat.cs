using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactLifeBoat : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
		}

		public override void Apply(UniversalTotalBonus totBonus, int totalArtifactsLevel)
		{
			if (this.forcedDisable)
			{
				return;
			}
			MythicalArtifactLifeBoat.totalArtifactsLevel = totalArtifactsLevel;
			totBonus.commonArtifactHealthFactor += this.healthBonus * (double)(totalArtifactsLevel / MythicalArtifactLifeBoat.PER_X_TAL);
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.LIFE_BOAT;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactLifeBoat mythicalArtifactLifeBoat = new MythicalArtifactLifeBoat();
			mythicalArtifactLifeBoat.SetRank(base.GetRank());
			return mythicalArtifactLifeBoat;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_HERO_HEALTH_FROM_QP");
		}

		public override string GetName()
		{
			return MythicalArtifactLifeBoat.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_HERO_HEALTH_FROM_QP");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.healthBonus * 0.01;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactLifeBoat.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double healthBonusPercent = MythicalArtifactLifeBoat.GetHealthBonusPercent(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactLifeBoat.MAX_RANK)
			{
				double num = MythicalArtifactLifeBoat.GetHealthBonusPercent(rank + levelDiff) - healthBonusPercent;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_HERO_HEALTH_FROM_QP"), GameMath.GetPercentString(healthBonusPercent, false) + AM.csart(s), MythicalArtifactLifeBoat.PER_X_TAL.ToString(), GameMath.GetPercentString(healthBonusPercent * (double)(MythicalArtifactLifeBoat.totalArtifactsLevel / MythicalArtifactLifeBoat.PER_X_TAL), false));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactLifeBoat.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactLifeBoat.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.LifeBoat;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactLifeBoat.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactLifeBoat.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactLifeBoat.MAX_RANK, rank);
			this.healthBonus = MythicalArtifactLifeBoat.GetHealthBonusPercent(rank);
		}

		private static double GetHealthBonusPercent(int rank)
		{
			return 0.01 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public static int PER_X_TAL = 500;

		public static int totalArtifactsLevel;

		public double healthBonus;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
