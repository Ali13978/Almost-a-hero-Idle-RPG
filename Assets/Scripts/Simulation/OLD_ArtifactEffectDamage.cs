using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectDamage : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.damageFactor += this.calculatedAmount;
		}

		public override void CalculateAdditionalBonuses(UniversalTotalBonus totBonus, int artifactRarity)
		{
			if (artifactRarity == ArtifactEffect.LEVEL_BASIC)
			{
				this.calculatedAmount = this.amount * totBonus.commonArtifactFactor;
			}
			else
			{
				this.calculatedAmount = this.amount;
			}
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectDamage
			{
				amount = this.amount
			};
		}

		public override bool IsLimited()
		{
			return false;
		}

		public override double GetReqMinQuality()
		{
			return -1.0;
		}

		public override float GetChanceWeight()
		{
			return 100f;
		}

		public override double GetAmountMin()
		{
			return -1.0;
		}

		public override double GetAmountMax()
		{
			return double.PositiveInfinity;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			return 0.0;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber(quality / OLD_ArtifactEffectDamage.QUALITY_PER_AMOUNT, 10.0);
		}

		public override double GetQuality(double amount)
		{
			return amount * OLD_ArtifactEffectDamage.QUALITY_PER_AMOUNT;
		}

		public override double GetAmount()
		{
			return this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectDamage.GetAmountString(this.calculatedAmount);
		}

		public static string GetAmountString(double amount)
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(amount, true));
		}

		public override double GetQuality()
		{
			return this.GetQuality(this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectDamage.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_DAMAGE");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectDamage.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectDamage.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.Damage;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectDamage.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_BASIC;
		}

		public double amount;

		public double calculatedAmount;

		public const double amountAllowed = double.PositiveInfinity;

		private static double QUALITY_PER_AMOUNT = 40.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.ENERGY;
	}
}
