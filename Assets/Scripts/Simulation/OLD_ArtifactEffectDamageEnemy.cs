using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectDamageEnemy : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.damageEnemyFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectDamageEnemy
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
			return 2500.0;
		}

		public override float GetChanceWeight()
		{
			return 10f;
		}

		public override double GetAmountMin()
		{
			return 0.01;
		}

		public override double GetAmountMax()
		{
			return 0.3;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			double num = 0.0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectDamageEnemy)
					{
						num += ((OLD_ArtifactEffectDamageEnemy)artifactEffect).amount;
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
			return OLD_ArtifactEffectDamageEnemy.GetAmountString(-this.amount);
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
			return OLD_ArtifactEffectDamageEnemy.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_DAMAGE_ENEMY");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectDamageEnemy.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectDamageEnemy.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.DamageEnemy;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectDamageEnemy.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_EPIC;
		}

		public double amount;

		public const double amountAllowed = 0.9;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.HEALTH;
	}
}
