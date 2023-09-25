using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectDroneSpawnRate : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.dragonSpawnRateFactor += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectDroneSpawnRate
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
			return 150.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override double GetAmountMin()
		{
			return 0.1;
		}

		public override double GetAmountMax()
		{
			return 5.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectDroneSpawnRate)
					{
						num += ((OLD_ArtifactEffectDroneSpawnRate)artifactEffect).amount;
					}
				}
			}
			return 2.0 - (double)num;
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
			return OLD_ArtifactEffectDroneSpawnRate.GetAmountString(this.amount);
		}

		public static string GetAmountString(float amount)
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(amount, true));
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectDroneSpawnRate.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_DRONE_SPAWN_RATE");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectDroneSpawnRate.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectDroneSpawnRate.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.DroneSpawnRate;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectDroneSpawnRate.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_EPIC;
		}

		public float amount;

		public const double amountAllowed = 2.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
