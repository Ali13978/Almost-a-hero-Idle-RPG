using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactCrestOfViloence : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.damageHeroFactorWithOneAttacker += this.damageBonusOneAttacker;
			totBonus.damageHeroFactorWithTwoAttackers += this.damageBonusTwoAttackers;
			totBonus.damageHeroFactorWithSeveralAttackers += this.damageBonusSeveralAttackers;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.CREST_OF_VIOLENCE;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactCrestOfViloence mythicalArtifactCrestOfViloence = new MythicalArtifactCrestOfViloence();
			mythicalArtifactCrestOfViloence.SetRank(base.GetRank());
			return mythicalArtifactCrestOfViloence;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_HERO_DAMAGE_PER_ATTACKER");
		}

		public override string GetName()
		{
			return MythicalArtifactCrestOfViloence.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_HERO_DAMAGE_PER_ATTACKER");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return this.damageBonusOneAttacker;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 120);
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactCrestOfViloence.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			double damageBonusPercentForOneAttacker = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForOneAttacker(rank);
			string s = string.Empty;
			double damageBonusPercentForTwoAttackers = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForTwoAttackers(rank);
			string s2 = string.Empty;
			double damageBonusPercentForSeveralAttackers = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForSeveralAttackers(rank);
			string s3 = string.Empty;
			if (rank < MythicalArtifactCrestOfViloence.MAX_RANK)
			{
				double num = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForOneAttacker(rank + levelDiff) - damageBonusPercentForOneAttacker;
				if (num > 0.0)
				{
					s = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForTwoAttackers(rank + levelDiff) - damageBonusPercentForTwoAttackers;
				if (num > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
				num = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForSeveralAttackers(rank + levelDiff) - damageBonusPercentForSeveralAttackers;
				if (num > 0.0)
				{
					s3 = " (+" + GameMath.GetPercentString(num, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_HERO_DAMAGE_PER_ATTACKER"), GameMath.GetPercentString(damageBonusPercentForOneAttacker, false) + AM.csart(s), GameMath.GetPercentString(damageBonusPercentForTwoAttackers, false) + AM.csart(s2), GameMath.GetPercentString(damageBonusPercentForSeveralAttackers, false) + AM.csart(s3));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactCrestOfViloence.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactCrestOfViloence.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.HeroDamagePerAttacker;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactCrestOfViloence.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactCrestOfViloence.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactCrestOfViloence.MAX_RANK, rank);
			this.damageBonusOneAttacker = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForOneAttacker(rank);
			this.damageBonusTwoAttackers = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForTwoAttackers(rank);
			this.damageBonusSeveralAttackers = MythicalArtifactCrestOfViloence.GetDamageBonusPercentForSeveralAttackers(rank);
		}

		private static double GetDamageBonusPercentForOneAttacker(int rank)
		{
			return 0.01 * (double)(rank + 1);
		}

		private static double GetDamageBonusPercentForTwoAttackers(int rank)
		{
			return 0.015 * (double)(rank + 1);
		}

		private static double GetDamageBonusPercentForSeveralAttackers(int rank)
		{
			return 0.02 * (double)(rank + 1);
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public double damageBonusOneAttacker;

		public double damageBonusTwoAttackers;

		public double damageBonusSeveralAttackers;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 149;
	}
}
