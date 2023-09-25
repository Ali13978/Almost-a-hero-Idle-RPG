using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactBandAidRelic : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.powerupReviveHealthRegen += this.healthRegen;
			totBonus.powerupReviveDuration += 60f;
			totBonus.powerupReviveSpeedAdd += this.reviveSpeedAdd;
			totBonus.powerupReviveDropChance += this.dropChance;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.BAND_AID_RELIC;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 20);
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactBandAidRelic mythicalArtifactBandAidRelic = new MythicalArtifactBandAidRelic();
			mythicalArtifactBandAidRelic.SetRank(base.GetRank());
			return mythicalArtifactBandAidRelic;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_POWERUP_REVIVE");
		}

		public override string GetName()
		{
			return MythicalArtifactBandAidRelic.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_POWERUP_REVIVE");
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
			return MythicalArtifactBandAidRelic.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactBandAidRelic.GetDropChance(rank);
			string s = string.Empty;
			double num2 = MythicalArtifactBandAidRelic.GetHealthRegen(rank);
			string s2 = string.Empty;
			float num3 = MythicalArtifactBandAidRelic.GetReviveSpeedAdd(rank);
			string s3 = string.Empty;
			if (rank < MythicalArtifactBandAidRelic.MAX_RANK)
			{
				float num4 = MythicalArtifactBandAidRelic.GetDropChance(rank + levelDiff) - num;
				if (num4 > 0f)
				{
					s = " (+" + GameMath.GetPercentString(num4, false) + ")";
				}
				double num5 = MythicalArtifactBandAidRelic.GetHealthRegen(rank + levelDiff) - num2;
				if (num5 > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num5, false) + ")";
				}
				float num6 = MythicalArtifactBandAidRelic.GetReviveSpeedAdd(rank + levelDiff) - num3;
				if (num6 > 0f)
				{
					s3 = " (+" + GameMath.GetPercentString(num6, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_POWERUP_REVIVE"), new object[]
			{
				GameMath.GetPercentString(num, false) + AM.csart(s),
				GameMath.GetPercentString(num2, false) + AM.csart(s2),
				GameMath.GetPercentString(num3, false) + AM.csart(s3),
				GameMath.GetTimeInMilliSecondsString(60f)
			});
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactBandAidRelic.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactBandAidRelic.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.PowerupRevive;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactBandAidRelic.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactBandAidRelic.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactBandAidRelic.MAX_RANK, rank);
			this.duration = 60f;
			this.healthRegen = MythicalArtifactBandAidRelic.GetHealthRegen(rank);
			this.dropChance = MythicalArtifactBandAidRelic.GetDropChance(rank);
			this.reviveSpeedAdd = MythicalArtifactBandAidRelic.GetReviveSpeedAdd(rank);
		}

		private static float GetReviveSpeedAdd(int rank)
		{
			return 0.25f + 0.005f * (float)(rank + 1);
		}

		private static double GetHealthRegen(int rank)
		{
			int num = (rank + 1) / 30;
			return 0.05 + (double)num * 0.01;
		}

		private static float GetDropChance(int rank)
		{
			return GameMath.Clamp(0.15f + (float)(rank + 1) * 0.01f, 0f, 0.3f);
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		public float dropChance;

		public float duration;

		public float reviveSpeedAdd;

		public double healthRegen;

		public const float DURATION = 60f;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
