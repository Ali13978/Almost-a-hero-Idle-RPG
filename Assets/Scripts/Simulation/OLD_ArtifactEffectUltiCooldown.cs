using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectUltiCooldown : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.ultiCoolDownMaxFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectUltiCooldown
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
			return 100.0;
		}

		public override float GetChanceWeight()
		{
			return 20f;
		}

		public override double GetAmountMin()
		{
			return 0.01;
		}

		public override double GetAmountMax()
		{
			return 1.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectUltiCooldown)
					{
						num += ((OLD_ArtifactEffectUltiCooldown)artifactEffect).amount;
					}
				}
			}
			return 0.5 - (double)num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber((float)(this.GetAmountMax() * (quality / Artifact.OLDER_ARTIFACT_MAX)), 1000f);
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
			return OLD_ArtifactEffectUltiCooldown.GetAmountString(-this.amount);
		}

		public static string GetAmountString(float amount)
		{
			return GameMath.GetPercentString(amount, true);
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectUltiCooldown.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_ULTI_COOLDOWN");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectUltiCooldown.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectUltiCooldown.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.UltiCooldown;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectUltiCooldown.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_LEGENDARY;
		}

		public float amount;

		public const double amountAllowed = 0.5;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.ENERGY;
	}
}
