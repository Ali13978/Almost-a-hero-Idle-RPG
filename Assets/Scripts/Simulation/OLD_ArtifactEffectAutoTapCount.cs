using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectAutoTapCount : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.autoTapCountAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectAutoTapCount
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
			return 2.0;
		}

		public override double GetAmountMax()
		{
			return 12.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			int num = 0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectAutoTapCount)
					{
						num += ((OLD_ArtifactEffectAutoTapCount)artifactEffect).amount;
					}
				}
			}
			return 6.0 - (double)num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = (int)(this.GetAmountMax() * (quality / Artifact.OLDER_ARTIFACT_MAX));
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
			return OLD_ArtifactEffectAutoTapCount.GetAmountString(this.amount);
		}

		public static string GetAmountString(int amount)
		{
			return StringExtension.Concat("+", amount.ToString());
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectAutoTapCount.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_AUTO_TAP_COUNT");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectAutoTapCount.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectAutoTapCount.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.AutoTapCount;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectAutoTapCount.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public int amount;

		public const double amountAllowed = 6.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
