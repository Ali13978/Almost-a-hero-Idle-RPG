using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectFreePackCooldown : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.freePackCooldownFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectFreePackCooldown
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
			return 300.0;
		}

		public override float GetChanceWeight()
		{
			return 10f;
		}

		public override double GetAmountMin()
		{
			return 0.05;
		}

		public override double GetAmountMax()
		{
			return 1.5;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectFreePackCooldown)
					{
						num += ((OLD_ArtifactEffectFreePackCooldown)artifactEffect).amount;
					}
				}
			}
			return 0.75 - (double)num;
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
			return OLD_ArtifactEffectFreePackCooldown.GetAmountString(-this.amount);
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
			return OLD_ArtifactEffectFreePackCooldown.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_FREE_PACK_COOLDOWN");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectFreePackCooldown.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectFreePackCooldown.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.FreePackCooldown;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectFreePackCooldown.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_LEGENDARY;
		}

		public float amount;

		public const double amountAllowed = 0.75;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
