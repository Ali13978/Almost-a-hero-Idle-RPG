using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectGold : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.goldFactor += this.calculatedAmount;
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
			return new OLD_ArtifactEffectGold
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
			return 1.0;
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
			this.amount = AM.RoundNumber(quality / OLD_ArtifactEffectGold.QUALITY_PER_AMOUNT, 10.0);
		}

		public override double GetQuality(double amount)
		{
			return amount * OLD_ArtifactEffectGold.QUALITY_PER_AMOUNT;
		}

		public override double GetAmount()
		{
			return this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectGold.GetAmountString(this.calculatedAmount);
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
			return OLD_ArtifactEffectGold.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_GOLD");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectGold.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectGold.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.Gold;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectGold.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_BASIC;
		}

		public double amount;

		public double calculatedAmount;

		public const double amountAllowed = double.PositiveInfinity;

		private static double QUALITY_PER_AMOUNT = 40.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.GOLD;
	}
}
