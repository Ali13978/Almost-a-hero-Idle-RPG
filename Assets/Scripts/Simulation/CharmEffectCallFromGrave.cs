using System;

namespace Simulation
{
	public class CharmEffectCallFromGrave : CharmEffectData
	{
		public CharmEffectCallFromGrave()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 201,
				maxLevel = 20,
				dropWeight = 16f,
				descKey = "CHARM_CALL_FROM_GRAVE_DESC",
				conditionKey = "CHARM_CONDITION_HERO_DEATHS",
				nameKey = "CHARM_CALL_FROM_GRAVE_NAME"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift challenge)
		{
			challenge.AddCharmBuff(new CharmBuffCallFromGrave
			{
				enchantmentData = this,
				dur = this.GetDuration(this.level),
				reduction = this.GetRevive(this.level),
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetRevive(this.level + 1) - this.GetRevive(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetDuration(this.level + 1) - this.GetDuration(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + num2.ToString() + LM.Get("TIME_SEC_SHORT") + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetRevive(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetDuration(this.level))) + str2);
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

		public int GetActivationReq(int lev)
		{
			return 1;
		}

		public float GetRevive(int lev)
		{
			return 2f + 0.2f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDuration(int lev)
		{
			return 3f + 0.5f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 25;
		}

		public const float BASE_DECREMENTER = 2f;

		public const float PER_LEVEL_DECREMENTER = 0.2f;

		public const float BASE_DURATION = 3f;

		public const float PER_5_LEVEL_DURATION = 0.5f;

		public const int BASE_ACTIVATE_THRESHOLD = 1;
	}
}
