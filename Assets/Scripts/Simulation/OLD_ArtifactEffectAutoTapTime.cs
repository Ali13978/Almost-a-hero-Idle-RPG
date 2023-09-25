using System;
using System.Collections.Generic;
using System.Text;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectAutoTapTime : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.autoTapDurationAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectAutoTapTime
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
			return 25f;
		}

		public override double GetAmountMin()
		{
			return 15.0;
		}

		public override double GetAmountMax()
		{
			return 2000.0;
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
			return 1000.0 - (double)num;
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
			return OLD_ArtifactEffectAutoTapTime.GetAmountString(this.amount);
		}

		public static string GetAmountString(float amount)
		{
			StringBuilder stringBuilder = StringExtension.StringBuilder;
			stringBuilder.Append("+");
			return GameMath.GetTimeLessDetailedString((double)amount, stringBuilder, false).ToString();
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectAutoTapTime.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_AUTO_TAP_TIME");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectAutoTapTime.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectAutoTapTime.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.AutoTapTime;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectAutoTapTime.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public float amount;

		public const double amountAllowed = 1000.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
