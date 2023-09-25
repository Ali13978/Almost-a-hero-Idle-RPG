using System;

namespace Simulation
{
	public class CharmEffectEmergencyFlute : CharmEffectData
	{
		public CharmEffectEmergencyFlute()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 308,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_EMERGENCY_FLUTE_NAME",
				conditionKey = "CHARM_CONDITION_KILL_ENEMIES",
				descKey = "CHARM_EMERGENCY_FLUTE_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift rift)
		{
			rift.AddCharmBuff(new CharmBuffEmergencyFlute
			{
				enchantmentData = this,
				reduction = this.GetCooldown(this.level),
				dur = this.GetDuration(this.level),
				pic = 0.2f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetCooldown(this.level + 1) - this.GetCooldown(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetDuration(this.level + 1) - this.GetDuration(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCooldown(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetDuration(this.level))) + str2);
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

		public float GetCooldown(int lev)
		{
			return 2.8f + 0.2f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDuration(int lev)
		{
			return 5f + 1f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const float BASE_DECREMENTER = 2.8f;

		public const float PER_LEVEL_DECREMENTER = 0.2f;

		public const int BASE_ACTIVATE_THRESHOLD = 5;

		public const float BASE_DURATION = 5f;

		public const float PER_5_LEVEL_DURATION = 1f;
	}
}
