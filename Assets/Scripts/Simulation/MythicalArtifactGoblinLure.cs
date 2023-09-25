using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactGoblinLure : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.treasureRaidChance += this.chance;
			totBonus.goldChestRaidFactor += this.bonusRatio;
			totBonus.goldBagAdDragonFactor *= 1.0 + this.bonusRatio * 0.05;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.GOBLIN_LURE;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactGoblinLure mythicalArtifactGoblinLure = new MythicalArtifactGoblinLure();
			mythicalArtifactGoblinLure.SetRank(base.GetRank());
			return mythicalArtifactGoblinLure;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_TREASURE_RAID");
		}

		public override string GetName()
		{
			return MythicalArtifactGoblinLure.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_TREASURE_RAID");
		}

		public static string GetAmountString(double amount)
		{
			throw new NotImplementedException();
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return (double)this.chance;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactGoblinLure.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactGoblinLure.GetChance(rank);
			double num2 = MythicalArtifactGoblinLure.GetBonusRatio(rank);
			string s = string.Empty;
			string s2 = string.Empty;
			if (rank < MythicalArtifactGoblinLure.MAX_RANK)
			{
				float num3 = MythicalArtifactGoblinLure.GetChance(rank + levelDiff) - num;
				if (num3 > 0f)
				{
					s = " (+" + GameMath.GetPercentString(num3, false) + ")";
				}
				double num4 = MythicalArtifactGoblinLure.GetBonusRatio(rank + levelDiff) - num2;
				if (num4 > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num4, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_TREASURE_RAID"), GameMath.GetPercentString(num, false) + AM.csart(s), GameMath.GetPercentString(num2, false) + AM.csart(s2));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactGoblinLure.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactGoblinLure.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.RaidTreasureGoblinChance;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactGoblinLure.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactGoblinLure.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactGoblinLure.MAX_RANK, rank);
			this.chance = MythicalArtifactGoblinLure.GetChance(rank);
			this.bonusRatio = MythicalArtifactGoblinLure.GetBonusRatio(rank);
		}

		private static double GetBonusRatio(int rank)
		{
			return (double)(rank + 1) * 0.01;
		}

		private static float GetChance(int rank)
		{
			return GameMath.Clamp01(0.5f + (float)rank * 0.01f);
		}

		public override bool CanBeDisabled()
		{
			return true;
		}

		public float chance;

		public double bonusRatio;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
