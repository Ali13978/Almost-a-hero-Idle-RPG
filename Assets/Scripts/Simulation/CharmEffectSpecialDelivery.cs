using System;

namespace Simulation
{
	public class CharmEffectSpecialDelivery : CharmEffectData
	{
		public CharmEffectSpecialDelivery()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 305,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_SPECIAL_DELIVERY_NAME",
				conditionKey = "CHARM_CONDITION_HERO_HEALTH_RESTORED",
				descKey = "CHARM_SPECIAL_DELIVERY_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffSpecialDelivery
			{
				enchantmentData = this,
				goldPercentageAmount = this.GetGoldPercentage(this.level),
				pic = 1f / this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetGoldPercentage(this.level + 1) - this.GetGoldPercentage(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetGoldPercentage(this.level), false)) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetActivationReq(this.level) - this.GetActivationReq(this.level + 1);
				if (num > 0f)
				{
					str = AM.cau(" (-" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(GameMath.GetPercentString(this.GetActivationReq(this.level), false)) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetActivationReq(int lev)
		{
			return 0.9f - 0.15f * (float)((lev + 1) / 5);
		}

		public double GetGoldPercentage(int lev)
		{
			return 0.9 + 0.1 * (double)(lev - (lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const double BASE_GOLD_REWARD = 0.9;

		public const double PER_LEVEL_GOLD_REWARD = 0.1;

		public const float BASE_ACTIVATE_THRESHOLD = 0.9f;

		public const float PER_5_LEVEL_ACTIVATE_THRESHOLD = 0.15f;
	}
}
