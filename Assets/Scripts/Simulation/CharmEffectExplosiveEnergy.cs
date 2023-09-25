using System;

namespace Simulation
{
	public class CharmEffectExplosiveEnergy : CharmEffectData
	{
		public CharmEffectExplosiveEnergy()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 110,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_EXPLOSIVE_ENERGY_NAME",
				conditionKey = "CHARM_CONDITION_SHIELD_HEROES",
				descKey = "CHARM_EXPLOSIVE_ENERGY_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffExplosiveEnergy
			{
				enchantmentData = this,
				damagePerToDeal = 0.1,
				attackSpeedIncrease = this.GetAttackSpeed(this.level),
				dur = this.GetDur(this.level),
				pic = 0.2f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetAttackSpeed(this.level + 1) - this.GetAttackSpeed(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetDur(this.level + 1) - this.GetDur(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(0.1, false)), AM.cds(GameMath.GetPercentString(this.GetAttackSpeed(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetDur(this.level))) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(5.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetAttackSpeed(int lev)
		{
			return 1.4f + 0.1f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDur(int lev)
		{
			return 4f + 1f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 10;
		}

		public const double DAMAGE_PER_TO_DEAL = 0.1;

		public const float BASE_ATT_SPEED_INC = 1.4f;

		public const float PER_LEVEL_ATT_SPEED_INC = 0.1f;

		public const float BASE_DUR = 4f;

		public const float PER_5_LEVEL_DUR = 1f;

		public const int BASE_ACTIVATE_THRESHOLD = 5;
	}
}
