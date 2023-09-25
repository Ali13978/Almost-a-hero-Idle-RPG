using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectGoldOffline : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.goldOfflineFactor += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectGoldOffline
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
			return 25.0;
		}

		public override float GetChanceWeight()
		{
			return 20f;
		}

		public override double GetAmountMin()
		{
			return 0.2;
		}

		public override double GetAmountMax()
		{
			return 8.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			double num = 0.0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectGoldOffline)
					{
						num += ((OLD_ArtifactEffectGoldOffline)artifactEffect).amount;
					}
				}
			}
			return 8.0 - num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber(quality / OLD_ArtifactEffectGoldOffline.QUALITY_PER_AMOUNT, 10.0);
		}

		public override double GetQuality(double amount)
		{
			return amount * OLD_ArtifactEffectGoldOffline.QUALITY_PER_AMOUNT;
		}

		public override double GetAmount()
		{
			return this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectGoldOffline.GetAmountString(this.amount);
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
			return OLD_ArtifactEffectGoldOffline.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_GOLD_OFFLINE");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectGoldOffline.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectGoldOffline.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.GoldOffline;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectGoldOffline.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_RARE;
		}

		public double amount;

		public const double amountAllowed = 8.0;

		private static double QUALITY_PER_AMOUNT = 250.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.GOLD;
	}
}
