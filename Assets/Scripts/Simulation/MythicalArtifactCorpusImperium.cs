using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactCorpusImperium : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.healthHeroFactorMuliplier += this.totalHealthBonusIncreaser;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CORPUS_IMPERIUM;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactCorpusImperium mythicalArtifactCorpusImperium = new MythicalArtifactCorpusImperium();
			mythicalArtifactCorpusImperium.SetRank(base.GetRank());
			return mythicalArtifactCorpusImperium;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_CORPUS_IMPERIUM");
		}

		public override string GetName()
		{
			return MythicalArtifactCorpusImperium.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_CORPUS_IMPERIUM");
		}

		public static string GetAmountString(double amount)
		{
			throw new NotImplementedException();
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", this.GetAmount().ToString());
		}

		public override double GetAmount()
		{
			return this.totalHealthBonusIncreaser;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactCorpusImperium.GetString(this.rank, levelDiff);
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 35);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double bonusRatio = MythicalArtifactCorpusImperium.GetBonusRatio(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactCorpusImperium.MAX_RANK)
			{
				double num = MythicalArtifactCorpusImperium.GetBonusRatio(rank + levelDiff) - bonusRatio;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_CORPUS_IMPERIUM"), GameMath.GetPercentString(bonusRatio, false) + AM.csart(s));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactCorpusImperium.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactCorpusImperium.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.CorpusImperium;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactCorpusImperium.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactCorpusImperium.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactCorpusImperium.MAX_RANK, rank);
			this.totalHealthBonusIncreaser = MythicalArtifactCorpusImperium.GetBonusRatio(rank);
		}

		private static double GetBonusRatio(int rank)
		{
			return GameMath.PowInt(1.0309408, rank + 1) * 2.0;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double totalHealthBonusIncreaser;

		public static double addedAmount;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 239;
	}
}
