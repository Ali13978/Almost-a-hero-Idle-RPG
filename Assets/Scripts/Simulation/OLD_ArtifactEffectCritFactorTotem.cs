using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectCritFactorTotem : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.critFactorTotemAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectCritFactorTotem
			{
				amount = this.amount
			};
		}

		public override bool IsLimited()
		{
			return true;
		}

		public override double GetReqMinQuality()
		{
			return 25.0;
		}

		public override float GetChanceWeight()
		{
			return 50f;
		}

		public override double GetAmountMin()
		{
			return 0.2;
		}

		public override double GetAmountMax()
		{
			return 10.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			double num = 0.0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectCritFactorTotem)
					{
						num += ((OLD_ArtifactEffectCritFactorTotem)artifactEffect).amount;
					}
				}
			}
			return 6.0 - num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber(this.GetAmountMax() * (quality / Artifact.OLDER_ARTIFACT_MAX), 1000.0);
		}

		public override double GetQuality(double amount)
		{
			return amount / this.GetAmountMax() * Artifact.OLDER_ARTIFACT_MAX;
		}

		public override double GetAmount()
		{
			return this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectCritFactorTotem.GetAmountString(this.amount);
		}

		public static string GetAmountString(double amount)
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(amount, true));
		}

		public override double GetQuality()
		{
			return this.GetQuality(this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectCritFactorTotem.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_CRIT_FACTOR_TOTEM");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectCritFactorTotem.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectCritFactorTotem.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.CritFactorTotem;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectCritFactorTotem.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public double amount;

		public const double amountAllowed = 6.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.RING;
	}
}
