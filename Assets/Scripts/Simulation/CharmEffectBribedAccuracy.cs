using System;

namespace Simulation
{
	public class CharmEffectBribedAccuracy : CharmEffectData
	{
		public CharmEffectBribedAccuracy()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 106,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_BRIBED_ACCURACY_NAME",
				conditionKey = "CHARM_CONDITION_COLLECT_GOLD_PIECES",
				descKey = "CHARM_BRIBED_ACCURACY_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffBribedAccuracy
			{
				enchantmentData = this,
				critChanceIncrease = this.GetCrit(this.level),
				dur = this.GetDur(this.level),
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetCrit(this.level + 1) - this.GetCrit(this.level);
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
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCrit(this.level), false) + str), AM.cds(GameMath.GetTimeInSecondsString(this.GetDur(this.level)) + str2));
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

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		private int GetActivationReq(int lev)
		{
			return 60;
		}

		public float GetCrit(int lev)
		{
			return 0.27f + 0.03f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDur(int lev)
		{
			return 5f + 1f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 25;
		}

		public const float BASE_CRIT = 0.27f;

		public const float PER_LEVEL_CRIT = 0.03f;

		public const float BASE_DUR = 5f;

		public const float PER_5_LEVEL_DUR = 1f;

		public const int BASE_ACTIVATE_THRESHOLD = 60;
	}
}
