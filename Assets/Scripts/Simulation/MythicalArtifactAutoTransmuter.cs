using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactAutoTransmuter : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.goldIdleFactor += this.bonusRatio;
			totBonus.idleBonusAfter += this.duration;
			totBonus.goldBagAdDragonFactor *= 1.0 + this.bonusRatio * 0.95;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.AUTO_TRANSMUTER;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactAutoTransmuter mythicalArtifactAutoTransmuter = new MythicalArtifactAutoTransmuter();
			mythicalArtifactAutoTransmuter.SetRank(base.GetRank());
			return mythicalArtifactAutoTransmuter;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_IDLE_GOLD_GAIN");
		}

		public override string GetName()
		{
			return MythicalArtifactAutoTransmuter.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_IDLE_GOLD_GAIN");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.bonusRatio;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank) * 0.1;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactAutoTransmuter.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactAutoTransmuter.GetDuration(rank);
			double num2 = MythicalArtifactAutoTransmuter.GetBonusRatio(rank);
			string s = string.Empty;
			string s2 = string.Empty;
			if (rank < MythicalArtifactAutoTransmuter.MAX_RANK)
			{
				float num3 = num - MythicalArtifactAutoTransmuter.GetDuration(rank + levelDiff);
				if (num3 > 0f)
				{
					s = " (-" + GameMath.GetTimeInMilliSecondsString(num3) + ")";
				}
				double num4 = MythicalArtifactAutoTransmuter.GetBonusRatio(rank + levelDiff) - num2;
				if (num4 > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num4, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_IDLE_GOLD_GAIN"), GameMath.GetTimeInMilliSecondsString(num) + AM.csart(s), GameMath.GetPercentString(num2, false) + AM.csart(s2));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactAutoTransmuter.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactAutoTransmuter.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.IdleGoldGain;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactAutoTransmuter.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactAutoTransmuter.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactAutoTransmuter.MAX_RANK, rank);
			this.duration = MythicalArtifactAutoTransmuter.GetDuration(rank);
			this.bonusRatio = MythicalArtifactAutoTransmuter.GetBonusRatio(rank);
		}

		private static double GetBonusRatio(int rank)
		{
			return GameMath.PowInt(1.02517613, rank + 1) - 0.75;
		}

		private static float GetDuration(int rank)
		{
			return GameMath.GetMaxFloat(0f, 10f - 0.1f * (float)(rank + 1));
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public float duration;

		public double bonusRatio;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 249;
	}
}
