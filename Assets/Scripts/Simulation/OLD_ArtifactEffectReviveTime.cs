using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectReviveTime : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.reviveTimeFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectReviveTime
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
			return 120.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override double GetAmountMin()
		{
			return 0.029999999329447746;
		}

		public override double GetAmountMax()
		{
			return 2.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectReviveTime)
					{
						num += ((OLD_ArtifactEffectReviveTime)artifactEffect).amount;
					}
				}
			}
			return 0.65 - (double)num;
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
			return OLD_ArtifactEffectReviveTime.GetAmountString(-this.amount);
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
			return OLD_ArtifactEffectReviveTime.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_REVIVE_TIME");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectReviveTime.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectReviveTime.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.ReviveTime;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectReviveTime.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_RARE;
		}

		public float amount;

		public const double amountAllowed = 0.65;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.HEALTH;
	}
}
