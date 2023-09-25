using System;
using System.Collections.Generic;

namespace Simulation
{
	public class OLD_ArtifactEffectFastSpawn : ArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			totBonus.fastEnemySpawnBelow += this.amount;
		}

		public override ArtifactEffect GetCopy()
		{
			return new OLD_ArtifactEffectFastSpawn
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
			return 50f;
		}

		public override double GetAmountMin()
		{
			return 10.0;
		}

		public override double GetAmountMax()
		{
			return 100.0;
		}

		public override double GetAmountAllowed(List<Artifact> otherArtifacts)
		{
			int num = 0;
			foreach (Artifact artifact in otherArtifacts)
			{
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					if (artifactEffect is OLD_ArtifactEffectFastSpawn)
					{
						num += ((OLD_ArtifactEffectFastSpawn)artifactEffect).amount;
					}
				}
			}
			return 900.0 - (double)num;
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
			return OLD_ArtifactEffectFastSpawn.GetAmountString(this.amount);
		}

		public static string GetAmountString(int amount)
		{
			return string.Format(LM.Get("ARTIFACT_EFFECT_FAST_SPAWN_STAGE"), amount.ToString());
		}

		public override double GetQuality()
		{
			return this.GetQuality((double)this.amount);
		}

		public override string GetStringSelf(int levelJump)
		{
			return OLD_ArtifactEffectFastSpawn.GetString();
		}

		public static string GetString()
		{
			return LM.Get("ARTIFACT_EFFECT_FAST_SPAWN");
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return OLD_ArtifactEffectFastSpawn.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return OLD_ArtifactEffectFastSpawn.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.FastSpawn;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return OLD_ArtifactEffectFastSpawn.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_RARE;
		}

		public int amount;

		public const double amountAllowed = 900.0;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.UTILITY;
	}
}
