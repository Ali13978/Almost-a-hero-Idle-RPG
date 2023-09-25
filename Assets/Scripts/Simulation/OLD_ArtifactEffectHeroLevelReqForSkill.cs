using System;
using System.Collections.Generic;
using Static;

namespace Simulation
{
	public class OLD_ArtifactEffectHeroLevelReqForSkill : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.heroLevelRequiredForSkillDecrease += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectHeroLevelReqForSkill
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
			return 300.0;
		}

		public override float GetChanceWeight()
		{
			return 15f;
		}

		public override double GetAmountMin()
		{
			return 1.0;
		}

		public override double GetAmountMax()
		{
			return 14.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			int num = 0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectHeroLevelReqForSkill)
					{
						num += ((OLD_ArtifactEffectHeroLevelReqForSkill)artifactEffect).amount;
					}
				}
			}
			return 8.0 - (double)num;
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
			return OLD_ArtifactEffectHeroLevelReqForSkill.GetAmountString(this.amount);
		}

		public static string GetAmountString(int amount)
		{
			return StringExtension.Concat("-", amount.ToString());
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectHeroLevelReqForSkill.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_HERO_LEVEL_REQ_FOR_SKILL");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectHeroLevelReqForSkill.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectHeroLevelReqForSkill.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HeroLevelReqForSkill;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectHeroLevelReqForSkill.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_LEGENDARY;
		}

		public int amount;

		public const double amountAllowed = 8.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.ENERGY;
	}
}
