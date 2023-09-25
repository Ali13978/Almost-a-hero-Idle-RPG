using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectQuickWaveAfterBoss : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.afterBossDurationFactor -= this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectQuickWaveAfterBoss
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
			return 500.0;
		}

		public override float GetChanceWeight()
		{
			return 20f;
		}

		public override double GetAmountMin()
		{
			return 1.0;
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
					if (artifactEffect is OLD_ArtifactEffectAutoTapTime)
					{
						num += ((OLD_ArtifactEffectAutoTapTime)artifactEffect).amount;
					}
				}
			}
			if (num > 0f)
			{
				return 0.0;
			}
			return 1.0;
		}

		public override void SetQuality(double quality)
		{
			this.amount = 1f;
		}

		public override double GetQuality(double amount)
		{
			return 1000.0;
		}

		public override double GetAmount()
		{
			return (double)this.amount;
		}

		public override string GetAmountString()
		{
			return string.Empty;
		}

		public static string GetAmountString(float amount)
		{
			return string.Empty;
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectQuickWaveAfterBoss.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_QUICK_WAVE_AFTER_BOSS");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectQuickWaveAfterBoss.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectQuickWaveAfterBoss.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.QuickWaveAfterBoss;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectQuickWaveAfterBoss.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_LEGENDARY;
		}

		public float amount;

		public const double amountAllowed = 1.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
