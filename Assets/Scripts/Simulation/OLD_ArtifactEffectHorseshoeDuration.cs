using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectHorseshoeDuration : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.horseshoeDurationAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectHorseshoeDuration
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
			return 40000.0;
		}

		public override float GetChanceWeight()
		{
			return 25f;
		}

		public override double GetAmountMin()
		{
			return 15.0;
		}

		public override double GetAmountMax()
		{
			return 650.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectHorseshoeDuration)
					{
						num += ((OLD_ArtifactEffectHorseshoeDuration)artifactEffect).amount;
					}
				}
			}
			return 600.0 - (double)num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber((float)(this.GetAmountMax() * (quality / Artifact.OLDER_ARTIFACT_MAX)), 10f);
		}

		public override double GetQuality(double amount)
		{
			return amount / this.GetAmountMax() * Artifact.OLDER_ARTIFACT_MAX;
		}

		public override double GetAmount()
		{
			return (double)this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectHorseshoeDuration.GetAmountString(this.amount);
		}

		public static string GetAmountString(float amount)
		{
			return StringExtension.Concat("+", GameMath.GetTimeLessDetailedString((double)amount, false));
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectHorseshoeDuration.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_HORSESHOE_DURATION");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectHorseshoeDuration.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectHorseshoeDuration.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HorseshoeDuration;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectHorseshoeDuration.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public float amount;

		public const double amountAllowed = 600.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
