using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectDamageHeroNonSkill : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.damageHeroNonSkillFactor += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectDamageHeroNonSkill
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
			return 10000.0;
		}

		public override float GetChanceWeight()
		{
			return 20f;
		}

		public override double GetAmountMin()
		{
			return 0.1;
		}

		public override double GetAmountMax()
		{
			return 15.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			double num = 0.0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectDamageHeroNonSkill)
					{
						num += ((OLD_ArtifactEffectDamageHeroNonSkill)artifactEffect).amount;
					}
				}
			}
			return 5.0 - num;
		}

		public override void SetQuality(double quality)
		{
			this.amount = AM.RoundNumber(this.GetAmountMax() * (quality / Artifact.OLDER_ARTIFACT_MAX), 1000.0);
		}

		public override double GetQuality(double amount)
		{
			return amount / this.GetAmountMax() * Artifact.OLDER_ARTIFACT_MAX;
		}

		public override double GetAmount()
		{
			return this.amount;
		}

		public override string GetAmountString()
		{
			return OLD_ArtifactEffectDamageHeroNonSkill.GetAmountString(this.amount);
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
			return OLD_ArtifactEffectDamageHeroNonSkill.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_DAMAGE_HERO_NONSKILL");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectDamageHeroNonSkill.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectDamageHeroNonSkill.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.DamageHeroNonSkill;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectDamageHeroNonSkill.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_EPIC;
		}

		public double amount;

		public const double amountAllowed = 5.0;

		private static ArtifactEffectCategory effectCategory;
	}
}
