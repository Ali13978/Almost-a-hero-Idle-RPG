using System;

namespace Simulation
{
	public class CharmEffectExtremeImpatience : CharmEffectData
	{
		public CharmEffectExtremeImpatience()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 306,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_EXTREME_IMPATIENCE_NAME",
				conditionKey = "CHARM_CONDITION_DODGE_ATTACKS",
				descKey = "CHARM_EXTREME_IMPATIENCE_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift rift)
		{
			rift.AddCharmBuff(new CharmBuffExtremeImpatience
			{
				enchantmentData = this,
				reduction = this.GetCdRed(this.level),
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
				float num = this.GetCdRed(this.level + 1) - this.GetCdRed(this.level);
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
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCdRed(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetDuration(this.level))) + str2);
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

		public float GetCdRed(int lev)
		{
			return 1.8f + 0.2f * (float)(lev - (lev + 1) / 5);
		}

		public float GetDuration(int lev)
		{
			return 7f + 1f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 10;
		}

		public const float BASE_DECREMENTER = 1.8f;

		public const float PER_LEVEL_DECREMENTER = 0.2f;

		public const int BASE_ACTIVATE_THRESHOLD = 5;

		public const float BASE_DURATION = 7f;

		public const float PER_5_LEVEL_DURATION = 1f;
	}
}
