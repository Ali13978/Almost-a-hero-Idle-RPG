using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactDPSMatter : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
		}

		public override void Apply(UniversalTotalBonus totBonus, int totalArtifactsLevel)
		{
			if (this.forcedDisable)
			{
				return;
			}
			MythicalArtifactDPSMatter.totalArtifactsLevel = (double)totalArtifactsLevel;
			totBonus.commonArtifactDamageFactor += this.damageBonus * (double)(totalArtifactsLevel / MythicalArtifactDPSMatter.PER_X_TAL);
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.DPS_MATTER;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactDPSMatter mythicalArtifactDPSMatter = new MythicalArtifactDPSMatter();
			mythicalArtifactDPSMatter.SetRank(base.GetRank());
			return mythicalArtifactDPSMatter;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_ALL_DAMAGE_FROM_QP");
		}

		public override string GetName()
		{
			return MythicalArtifactDPSMatter.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_ALL_DAMAGE_FROM_QP");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.damageBonus * 0.01;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactDPSMatter.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double damageBonusPercent = MythicalArtifactDPSMatter.GetDamageBonusPercent(rank);
			string s = string.Empty;
			if (rank < MythicalArtifactDPSMatter.MAX_RANK)
			{
				double num = MythicalArtifactDPSMatter.GetDamageBonusPercent(rank + levelDiff) - damageBonusPercent;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_ALL_DAMAGE_FROM_QP"), GameMath.GetPercentString(damageBonusPercent, false) + AM.csart(s), MythicalArtifactDPSMatter.PER_X_TAL.ToString(), GameMath.GetPercentString(damageBonusPercent * (double)((int)(MythicalArtifactDPSMatter.totalArtifactsLevel / (double)MythicalArtifactDPSMatter.PER_X_TAL)), false));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactDPSMatter.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactDPSMatter.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.DpsMaster;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactDPSMatter.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank);
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactDPSMatter.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactDPSMatter.MAX_RANK, rank);
			this.damageBonus = MythicalArtifactDPSMatter.GetDamageBonusPercent(rank);
		}

		private static double GetDamageBonusPercent(int rank)
		{
			return 0.01 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public static int PER_X_TAL = 500;

		public static double totalArtifactsLevel;

		public double damageBonus;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
