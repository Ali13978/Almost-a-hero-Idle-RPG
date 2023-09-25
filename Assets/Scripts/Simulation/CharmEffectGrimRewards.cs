using System;

namespace Simulation
{
	public class CharmEffectGrimRewards : CharmEffectData
	{
		public CharmEffectGrimRewards()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 301,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_GRIM_REWARDS_NAME",
				conditionKey = "CHARM_CONDITION_HERO_DEATHS",
				descKey = "CHARM_GRIM_REWARDS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffGrimRewards
			{
				enchantmentData = this,
				costReductionMin = (double)this.GetCostReductionAmountMin(this.level),
				costReductionMax = (double)this.GetCostReductionAmountMax(this.level),
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetCostReductionAmountMin(this.level + 1) - this.GetCostReductionAmountMin(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetCostReductionAmountMax(this.level + 1) - this.GetCostReductionAmountMax(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCostReductionAmountMin(this.level), false)) + str, AM.cds(GameMath.GetPercentString(this.GetCostReductionAmountMax(this.level), false)) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivationReq(this.level).ToString()));
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
			return 2;
		}

		public float GetCostReductionAmountMin(int lev)
		{
			return 0.04f + 0.01f * (float)(lev - (lev + 1) / 5);
		}

		public float GetCostReductionAmountMax(int lev)
		{
			return 0.15f + 0.05f * (float)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 70;
		}

		public const float BASE_COST_REDUCTION_MIN = 0.04f;

		public const float PER_LEVEL_COST_REDUCTION_MIN = 0.01f;

		public const float BASE_COST_REDUCTION_MAX = 0.15f;

		public const float PER_5_LEVEL_COST_REDUCTION_MAX = 0.05f;

		public const int BASE_ACTIVATE_THRESHOLD = 2;
	}
}
