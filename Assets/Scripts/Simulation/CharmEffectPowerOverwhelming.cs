using System;

namespace Simulation
{
	public class CharmEffectPowerOverwhelming : CharmEffectData
	{
		public CharmEffectPowerOverwhelming()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 103,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_POWER_OVERWHELMING_NAME",
				conditionKey = "CHARM_CONDITION_RIFT_SECONDS",
				descKey = "CHARM_POWER_OVERWHELMING_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffPowerOverwhelming
			{
				enchantmentData = this,
				damageIncrease = this.GetDamageInc(this.level),
				dur = this.GetEffectDur(this.level),
				pic = 0.06666667f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetDamageInc(this.level + 1) - this.GetDamageInc(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetEffectDur(this.level + 1) - this.GetEffectDur(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageInc(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetEffectDur(this.level))) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(15.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public double GetDamageInc(int lev)
		{
			return (double)(0.4f + 0.1f * (float)(lev - (lev + 1) / 5));
		}

		public float GetEffectDur(int lev)
		{
			return 8f + 2f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const float BASE_DAMAGE_INC = 0.4f;

		public const float PER_LEVEL_DAMAGE_INC = 0.1f;

		public const float BASE_DUR = 8f;

		public const float PER_5_LEVEL_DUR = 2f;

		public const int BASE_ACTIVATE_THRESHOLD = 15;
	}
}
