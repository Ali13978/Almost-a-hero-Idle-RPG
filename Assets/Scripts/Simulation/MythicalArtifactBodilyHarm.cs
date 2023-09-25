using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactBodilyHarm : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.extraDamageTakenFromHeroesFactor += this.bonusRatio;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.BODILY_HARM;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactBodilyHarm mythicalArtifactBodilyHarm = new MythicalArtifactBodilyHarm();
			mythicalArtifactBodilyHarm.SetRank(base.GetRank());
			return mythicalArtifactBodilyHarm;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_BODILY_HARM");
		}

		public override string GetName()
		{
			return MythicalArtifactBodilyHarm.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_BODILY_HARM");
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
			return this.bonusRatio;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactBodilyHarm.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double num = MythicalArtifactBodilyHarm.GetBonusRatio(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactBodilyHarm.MAX_RANK)
			{
				double num2 = MythicalArtifactBodilyHarm.GetBonusRatio(rank + levelDiff) - num;
				if (num2 > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num2, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_BODILY_HARM"), GameMath.GetPercentString(num, false) + AM.csart(s));
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 65);
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactBodilyHarm.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactBodilyHarm.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.BodilyHarm;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactBodilyHarm.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactBodilyHarm.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactBodilyHarm.MAX_RANK, rank);
			this.bonusRatio = MythicalArtifactBodilyHarm.GetBonusRatio(rank);
		}

		private static double GetBonusRatio(int rank)
		{
			return (GameMath.PowInt(1.033995, rank + 1) - 1.0) * 0.2;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double bonusRatio;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
