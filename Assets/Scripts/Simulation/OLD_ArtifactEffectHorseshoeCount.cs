using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectHorseshoeCount : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.horseshoeCountAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectHorseshoeCount
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
			return 20f;
		}

		public override double GetAmountMin()
		{
			return 1.0;
		}

		public override double GetAmountMax()
		{
			return 7.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			int num = 0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectHorseshoeCount)
					{
						num += ((OLD_ArtifactEffectHorseshoeCount)artifactEffect).amount;
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
			return OLD_ArtifactEffectHorseshoeCount.GetAmountString(this.amount);
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
			return OLD_ArtifactEffectHorseshoeCount.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_HORSESHOE_COUNT");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectHorseshoeCount.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectHorseshoeCount.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HorseshoeCount;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectHorseshoeCount.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_RARE;
		}

		public int amount;

		public const double amountAllowed = 6.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
