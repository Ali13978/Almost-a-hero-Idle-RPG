using System;
using Static;

namespace Simulation
{
	public class MythicalArtifactHalfRing : MythicalArtifactEffect
	{
		public override void Apply(UniversalTotalBonus totBonus)
		{
			if (this.forcedDisable)
			{
				return;
			}
			totBonus.ringUltraCritCd += this.critCooldown;
			totBonus.ringUltraCritChance += this.critChance;
			totBonus.ringUltraCritFactor += this.critFactor;
			totBonus.extraDamageTakenFromRingFactor += this.damageFactor;
		}

		public override int GetMinRequiredMythical()
		{
			return MythicalArtifactLevelRequirements.HALF_RING;
		}

		public override ArtifactEffect GetCopy()
		{
			MythicalArtifactHalfRing mythicalArtifactHalfRing = new MythicalArtifactHalfRing();
			mythicalArtifactHalfRing.SetRank(base.GetRank());
			return mythicalArtifactHalfRing;
		}

		public override string GetNameEN()
		{
			return LM.GetFromEN("ARTIFACT_NAME_TOTEM_ULTRA_CRIT");
		}

		public override string GetName()
		{
			return MythicalArtifactHalfRing.GetNameStatic();
		}

		public static string GetNameStatic()
		{
			return LM.Get("ARTIFACT_NAME_TOTEM_ULTRA_CRIT");
		}

		public override string GetAmountString()
		{
			return StringExtension.Concat("+", GameMath.GetPercentString(this.GetAmount(), false));
		}

		public override double GetAmount()
		{
			return (double)this.critChance;
		}

		public override double GetUpgradeCost(int rank)
		{
			return base.GetUpgradeCost(rank + 25) * 2.0;
		}

		public override float GetChanceWeight()
		{
			return 30f;
		}

		public override string GetStringSelf(int levelDiff)
		{
			return MythicalArtifactHalfRing.GetString(this.rank, levelDiff);
		}

		public static string GetString(int rank, int levelDiff)
		{
			float num = MythicalArtifactHalfRing.GetCritChance(rank);
			double num2 = MythicalArtifactHalfRing.GetDamageFactor(rank);
			double num3 = MythicalArtifactHalfRing.GetCritFactor(rank);
			string s = string.Empty;
			string s2 = string.Empty;
			string s3 = string.Empty;
			if (rank < MythicalArtifactHalfRing.MAX_RANK)
			{
				float num4 = MythicalArtifactHalfRing.GetCritChance(rank + levelDiff) - num;
				if (num4 > 0f)
				{
					s = " (+" + GameMath.GetPercentString(num4, false) + ")";
				}
				double num5 = MythicalArtifactHalfRing.GetCritFactor(rank + levelDiff) - num3;
				if (num5 > 0.0)
				{
					s3 = " (+" + GameMath.GetPercentString(num5, false) + ")";
				}
				double num6 = MythicalArtifactHalfRing.GetDamageFactor(rank + levelDiff) - num2;
				if (num6 > 0.0)
				{
					s2 = " (+" + GameMath.GetPercentString(num6, false) + ")";
				}
			}
			return string.Format(LM.Get("ARTIFACT_EFFECT_TOTEM_ULTRA_CRIT"), GameMath.GetPercentString(num2, false) + AM.csart(s2), GameMath.GetPercentString(num, false) + AM.csart(s), GameMath.GetPercentString(num3, false) + AM.csart(s3));
		}

		public override ArtifactEffectCategory GetCategorySelf()
		{
			return MythicalArtifactHalfRing.effectCategory;
		}

		public static ArtifactEffectCategory GetCategoryType()
		{
			return MythicalArtifactHalfRing.effectCategory;
		}

		public static ArtifactEffectType GetEffectType()
		{
			return ArtifactEffectType.TotemUltraCrit;
		}

		public override ArtifactEffectType GetEffectTypeSelf()
		{
			return MythicalArtifactHalfRing.GetEffectType();
		}

		public override int GetLevel()
		{
			return ArtifactEffect.LEVEL_MYTHICAL;
		}

		public override int GetMaxRank()
		{
			return MythicalArtifactHalfRing.MAX_RANK;
		}

		public override void SetRank(int rank)
		{
			this.rank = GameMath.GetMinInt(MythicalArtifactHalfRing.MAX_RANK, rank);
			this.critChance = MythicalArtifactHalfRing.GetCritChance(rank);
			this.critFactor = MythicalArtifactHalfRing.GetCritFactor(rank);
			this.critCooldown = MythicalArtifactHalfRing.GetCritCooldown(rank);
			this.damageFactor = MythicalArtifactHalfRing.GetDamageFactor(rank);
		}

		private static float GetCritCooldown(int rank)
		{
			return 0.5f;
		}

		private static double GetDamageFactor(int rank)
		{
			return GameMath.PowInt(1.038736, rank + 1) * 0.25;
		}

		private static double GetCritFactor(int rank)
		{
			return 10.0 + (double)(rank + 1) * 0.2;
		}

		private static float GetCritChance(int rank)
		{
			int num = (rank + 1) / 10;
			return 0.05f + 0.01f * (float)num;
		}

		public override bool CanBeDisabled()
		{
			return false;
		}

		public float critCooldown;

		public float critChance;

		public double critFactor;

		public double damageFactor;

		private static ArtifactEffectCategory effectCategory = ArtifactEffectCategory.MYTH;

		public static int MAX_RANK = 199;
	}
}
