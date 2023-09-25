using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactChampionsBounty : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.bountyIncreasePerDamageTakenFromHero += this.goldBonusPerPecentage / MythicalArtifactChampionsBounty.PER_PERCENT_DROP * 0.01;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CHAMPIONS_BOUNTY;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactChampionsBounty mythicalArtifactChampionsBounty = new MythicalArtifactChampionsBounty();
			mythicalArtifactChampionsBounty.SetRank(base.GetRank());
			return mythicalArtifactChampionsBounty;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_CHAMPIONS_BOUNTY");
		}

		public override string GetName()
		{
			return MythicalArtifactChampionsBounty.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_CHAMPIONS_BOUNTY");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.goldBonusPerPecentage;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactChampionsBounty.GetString(this.rank, levelDiff);
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank * 2 + 30);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double goldBonusPerPercentage = MythicalArtifactChampionsBounty.GetGoldBonusPerPercentage(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactChampionsBounty.MAX_RANK)
			{
				double num = MythicalArtifactChampionsBounty.GetGoldBonusPerPercentage(rank + levelDiff) - goldBonusPerPercentage;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_CHAMPIONS_BOUNTY"), GameMath.GetPercentString(MythicalArtifactChampionsBounty.PER_PERCENT_DROP, false), GameMath.GetPercentString(goldBonusPerPercentage, false) + AM.csart(s));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactChampionsBounty.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactChampionsBounty.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.ChampionsBounty;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactChampionsBounty.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactChampionsBounty.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactChampionsBounty.MAX_RANK, rank);
			this.goldBonusPerPecentage = MythicalArtifactChampionsBounty.GetGoldBonusPerPercentage(rank);
		}

		private static double GetGoldBonusPerPercentage(int rank)
		{
			return 0.01 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public static double PER_PERCENT_DROP = 0.1;

		private double goldBonusPerPecentage;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 99;
	}
}
