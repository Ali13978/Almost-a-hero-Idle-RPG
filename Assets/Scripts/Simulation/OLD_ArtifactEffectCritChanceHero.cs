using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectCritChanceHero : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.critChanceHeroAdd += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectCritChanceHero
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
			return 50.0;
		}

		public override float GetChanceWeight()
		{
			return 25f;
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
					if (artifactEffect is OLD_ArtifactEffectCritChanceHero)
					{
						num += ((OLD_ArtifactEffectCritChanceHero)artifactEffect).amount;
					}
				}
			}
			return 0.4 - (double)num;
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
			return OLD_ArtifactEffectCritChanceHero.GetAmountString(this.amount);
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
			return OLD_ArtifactEffectCritChanceHero.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_CRIT_CHANCE_HERO");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectCritChanceHero.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectCritChanceHero.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.CritChanceHero;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectCritChanceHero.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_UNCOMMON;
		}

		public float amount;

		public const double amountAllowed = 0.4;

		private static ArtifactEffectCategory effectCategory;
	}
}
