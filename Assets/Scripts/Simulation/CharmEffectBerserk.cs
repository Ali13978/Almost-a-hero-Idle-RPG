using System;

namespace Simulation
{
	public class CharmEffectBerserk : CharmEffectData
	{
		public CharmEffectBerserk()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 105,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_BERSERK_NAME",
				conditionKey = "CHARM_CONDITION_ENEMY_ATTACKS",
				descKey = "CHARM_BERSERK_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffBerserk
			{
				enchantmentData = this,
				attackSpeedIncrease = this.GetSpeedInc(this.level),
				dur = this.GetEffectDur(this.level),
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetSpeedInc(this.level + 1) - this.GetSpeedInc(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetEffectDur(this.level + 1) - this.GetEffectDur(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetSpeedInc(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetEffectDur(this.level))) + str2);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetActivationReq(this.level) - this.GetActivationReq(this.level + 1);
				if (num > 0)
				{
					str = AM.cau(" (-" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivationReq(this.level).ToString()) + str);
		}

		public float GetSpeedInc(int lev)
		{
			return 1f;
		}

		public float GetEffectDur(int lev)
		{
			return 4f + 1f * (float)((lev + 1) / 5);
		}

		public int GetActivationReq(int lev)
		{
			return 31 - (lev - (lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 1;
		}

		public const float BASE_INCREMENTER = 1f;

		public const float PER_LEVEL_INCREMENTER = 0.1f;

		public const float BASE_DUR = 4f;

		public const float PER_5_LEVEL_DUR = 1f;

		public const int BASE_ACTIVATE_THRESHOLD = 31;

		public const int PER_LEVEL_ACTIVATE_THRESHOLD = 1;
	}
}
