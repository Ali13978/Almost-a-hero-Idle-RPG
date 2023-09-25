using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactImpatientRelic : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.powerupCooldownDuration += this.duration;
			totBonus.powerupCooldownDropChance += this.dropChance;
			totBonus.powerupCooldownUltiAdd += this.ultiCooldownAdd;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.IMPATIENT_RELIC;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 20);
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactImpatientRelic mythicalArtifactImpatientRelic = new MythicalArtifactImpatientRelic();
			mythicalArtifactImpatientRelic.SetRank(base.GetRank());
			return mythicalArtifactImpatientRelic;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_POWERUP_COOLDOWN");
		}

		public override string GetName()
		{
			return MythicalArtifactImpatientRelic.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_POWERUP_COOLDOWN");
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
			return MythicalArtifactImpatientRelic.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactImpatientRelic.GetDropChance(rank);
			string s = string.Empty;
			float num2 = MythicalArtifactImpatientRelic.GetUltiCooldownAdd(rank);
			string s2 = string.Empty;
			if (rank < MythicalArtifactImpatientRelic.MAX_RANK)
			{
				double num3 = (double)(MythicalArtifactImpatientRelic.GetDropChance(rank + levelDiff) - num);
				if (num3 > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num3, false) + ")";
				}
				float num4 = MythicalArtifactImpatientRelic.GetUltiCooldownAdd(rank + levelDiff) - num2;
				if (num4 > 0f)
				{
					s2 = " (+" + GameMath.GetPercentString(num4, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_POWERUP_COOLDOWN"), GameMath.GetPercentString(num, false) + AM.csart(s), GameMath.GetPercentString(num2, false) + AM.csart(s2), GameMath.GetTimeInMilliSecondsString(60f));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactImpatientRelic.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactImpatientRelic.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.PowerupCooldown;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactImpatientRelic.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactImpatientRelic.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactImpatientRelic.MAX_RANK, rank);
			this.duration = 60f;
			this.ultiCooldownAdd = MythicalArtifactImpatientRelic.GetUltiCooldownAdd(rank);
			this.dropChance = MythicalArtifactImpatientRelic.GetDropChance(rank);
		}

		private static float GetUltiCooldownAdd(int rank)
		{
			return GameMath.Clamp01(0.2f + (float)(rank + 1) * 0.004f);
		}

		private static float GetDropChance(int rank)
		{
			return GameMath.Clamp(0.15f + (float)(rank + 1) * 0.01f, 0f, 0.3f);
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		public const float DURATION = 60f;

		public float dropChance;

		public float duration;

		public float ultiCooldownAdd;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
