using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactBluntRelic : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.powerupNonCritDamageFactorBonus += this.nonCritDamageBonus;
			totBonus.powerupNonCritDamageDuration += MythicalArtifactBluntRelic.DURATION;
			totBonus.powerupNonCritDamageDropChance += this.dropChance;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.BLUNT_RELIC;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 20);
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactBluntRelic mythicalArtifactBluntRelic = new MythicalArtifactBluntRelic();
			mythicalArtifactBluntRelic.SetRank(base.GetRank());
			return mythicalArtifactBluntRelic;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_POWERUP_NONCRIT_DAMAGE");
		}

		public override string GetName()
		{
			return MythicalArtifactBluntRelic.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_POWERUP_NONCRIT_DAMAGE");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return 0.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactBluntRelic.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactBluntRelic.GetDropChance(rank);
			string s = string.Empty;
			float duration = MythicalArtifactBluntRelic.DURATION;
			double nonCritDamageAdd = MythicalArtifactBluntRelic.GetNonCritDamageAdd(rank);
			string s2 = string.Empty;
			if (rank < MythicalArtifactBluntRelic.MAX_RANK)
			{
				double num2 = (double)(MythicalArtifactBluntRelic.GetDropChance(rank + levelDiff) - num);
				if (num2 > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num2, false) + ")";
				}
				double num3 = MythicalArtifactBluntRelic.GetNonCritDamageAdd(rank + levelDiff) - nonCritDamageAdd;
				if (num3 > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num3, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_POWERUP_NONCRIT_DAMAGE"), GameMath.GetPercentString(num, false) + AM.csart(s), GameMath.GetPercentString(nonCritDamageAdd, false) + AM.csart(s2), GameMath.GetTimeInMilliSecondsString(duration));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactBluntRelic.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactBluntRelic.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.PowerupCritChance;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactBluntRelic.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactBluntRelic.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactBluntRelic.MAX_RANK, rank);
			this.dropChance = MythicalArtifactBluntRelic.GetDropChance(rank);
			this.nonCritDamageBonus = MythicalArtifactBluntRelic.GetNonCritDamageAdd(rank);
		}

		private static float GetDropChance(int rank)
		{
			return GameMath.Clamp(0.15f + (float)(rank + 1) * 0.01f, 0f, 0.3f);
		}

		private static double GetNonCritDamageAdd(int rank)
		{
			return (double)(0.5f + 0.01f * (float)(rank + 1));
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		public static float DURATION = 60f;

		public float dropChance;

		public double nonCritDamageBonus;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
