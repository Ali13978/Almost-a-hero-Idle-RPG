using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectGoldMultTenChance : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.goldMultTenChanceAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectGoldMultTenChance
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
			return 200.0;
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
			return 1.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			float num = 0f;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectGoldMultTenChance)
					{
						num += ((OLD_ArtifactEffectGoldMultTenChance)artifactEffect).amount;
					}
				}
			}
			return 1.0 - (double)num;
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
			return OLD_ArtifactEffectGoldMultTenChance.GetAmountString(this.amount);
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
			return OLD_ArtifactEffectGoldMultTenChance.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_GOLD_MULT_TEN_CHANCE");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectGoldMultTenChance.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectGoldMultTenChance.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.GoldMultTenChance;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectGoldMultTenChance.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_LEGENDARY;
		}

		public float amount;

		public const double amountAllowed = 1.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.GOLD;
	}
}
