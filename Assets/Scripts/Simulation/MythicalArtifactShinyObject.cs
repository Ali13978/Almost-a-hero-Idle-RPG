using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactShinyObject : MythicalArtifactEffect
	{
		public override void PreApply(UniversalTotalBonus totBonus)
		{
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.SHINY_OBJECT;
		}

		public override void Apply(UniversalTotalBonus totBonus)
		{
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactShinyObject mythicalArtifactShinyObject = new MythicalArtifactShinyObject();
			mythicalArtifactShinyObject.SetRank(base.GetRank());
			return mythicalArtifactShinyObject;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_QUALITY_POINT");
		}

		public override string GetName()
		{
			return MythicalArtifactShinyObject.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_QUALITY_POINT");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.GetQuality();
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank / 10) * (0.85 + (double)(rank % 10 + 1) * 0.015);
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactShinyObject.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			return "ARTIFACT_EFFECT_QUALITY_POINT".Loc();
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactShinyObject.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactShinyObject.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.ShinyObject;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactShinyObject.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactShinyObject.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactShinyObject.MAX_RANK, rank);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 2221;
	}
}
