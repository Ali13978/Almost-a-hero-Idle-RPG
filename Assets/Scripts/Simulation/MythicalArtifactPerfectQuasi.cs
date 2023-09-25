using System;
using Simulation.ArtifactSystem;
using Static;

namespace Simulation
{
	public class MythicalArtifactPerfectQuasi : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.PERFECT_QUASI;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactPerfectQuasi mythicalArtifactPerfectQuasi = new MythicalArtifactPerfectQuasi();
			mythicalArtifactPerfectQuasi.SetRank(base.GetRank());
			return mythicalArtifactPerfectQuasi;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_QP_CAP_INCREASE");
		}

		public override string GetName()
		{
			return MythicalArtifactPerfectQuasi.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_QP_CAP_INCREASE");
		}

		public static string GetAmountString(double amount)
		{
			throw new NotImplementedException();
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetDoubleString(this.GetAmount()));
		}

		public override double GetAmount()
		{
			return (double)this.levelIncrease;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 25) * 2.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactPerfectQuasi.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			int num = MythicalArtifactPerfectQuasi.GetLevelIncrease(rank) + ArtifactsManager.BASE_LEVEL_CAP;
			string s = string.Empty;
			if (rank < MythicalArtifactPerfectQuasi.MAX_RANK)
			{
				int num2 = MythicalArtifactPerfectQuasi.GetLevelIncrease(rank + levelDiff) + ArtifactsManager.BASE_LEVEL_CAP - num;
				if (num2 > 0)
				{
					s = " (+" + num2.ToString() + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_QP_CAP_INCREASE"), GameMath.GetDoubleString((double)num) + AM.csart(s));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactPerfectQuasi.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactPerfectQuasi.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.PerfectQuasi;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactPerfectQuasi.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactPerfectQuasi.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactPerfectQuasi.MAX_RANK, rank);
			this.levelIncrease = MythicalArtifactPerfectQuasi.GetLevelIncrease(rank);
		}

		private static int GetLevelIncrease(int rank)
		{
			return rank + 1;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public int levelIncrease;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
