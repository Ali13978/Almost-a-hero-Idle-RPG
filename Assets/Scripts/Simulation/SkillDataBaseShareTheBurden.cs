using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseShareTheBurden : SkillPassiveDataBase
	{
		public SkillDataBaseShareTheBurden()
		{
			this.nameKey = "SKILL_NAME_SHARE_THE_BURDEN";
			this.descKey = "SKILL_DESC_SHARE_THE_BURDEN";
			this.requiredHeroLevel = 9;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataReduceAllyUpgradeCostOnHit buffDataReduceAllyUpgradeCostOnHit = new BuffDataReduceAllyUpgradeCostOnHit
			{
				id = 332,
				maxReduceCost = this.GetMaxCostReduction(level),
				reduceCost = this.GetCostReductionPerHit(level)
			};
			data.passiveBuff = buffDataReduceAllyUpgradeCostOnHit;
			buffDataReduceAllyUpgradeCostOnHit.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostReductionPerHit(0), false)), AM.csa(GameMath.GetPercentString(this.GetMaxCostReduction(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostReductionPerHit(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.004f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetMaxCostReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostReductionPerHit(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetMaxCostReduction(data.level), false)));
		}

		public float GetCostReductionPerHit(int level)
		{
			return 0.01f + (float)level * 0.004f;
		}

		public float GetMaxCostReduction(int level)
		{
			return 0.2f + (float)level * 0.02f;
		}

		public const float COST_REDUCTION_INIT = 0.01f;

		public const float COST_REDUCTION_PER_LEVEL = 0.004f;

		public const float MAX_COST_REDUCTION_INIT = 0.2f;

		public const float MAX_COST_REDUCTION_PER_LEVEL = 0.02f;
	}
}
