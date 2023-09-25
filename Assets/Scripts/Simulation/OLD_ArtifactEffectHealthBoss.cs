using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectHealthBoss : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.healthBossFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectHealthBoss
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
			return 1.2;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			double num = 0.0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectHealthBoss)
					{
						num += ((OLD_ArtifactEffectHealthBoss)artifactEffect).amount;
					}
				}
			}
			return 0.9 - num;
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
			return OLD_ArtifactEffectHealthBoss.GetAmountString(-this.amount);
		}

		public static string GetAmountString(double amount)
		{
			return GameMath.GetPercentString(amount, true);
		}

		public override double GetQuality()
		{
			return this.GetQuality(this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectHealthBoss.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_HEALTH_BOSS");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectHealthBoss.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectHealthBoss.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HealthBoss;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectHealthBoss.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public double amount;

		public const double amountAllowed = 0.9;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.HEALTH;
	}
}
