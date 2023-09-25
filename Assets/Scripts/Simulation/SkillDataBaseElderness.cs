using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseElderness : SkillPassiveDataBase
	{
		public SkillDataBaseElderness()
		{
			this.nameKey = "SKILL_NAME_ELDERNESS";
			this.descKey = "SKILL_DESC_ELDERNESS";
			this.requiredHeroLevel = 9;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataUpgradeCostReduce buffDataUpgradeCostReduce = new BuffDataUpgradeCostReduce();
			buffDataUpgradeCostReduce.id = 189;
			data.passiveBuff = buffDataUpgradeCostReduce;
			buffDataUpgradeCostReduce.isPermenant = true;
			buffDataUpgradeCostReduce.reductionRatio = this.GetReductionRatio(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReductionRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReductionRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReductionRatio(data.level), false)));
		}

		public double GetReductionRatio(int level)
		{
			return 0.1 + (double)level * 0.05;
		}

		private const double INITIAL_COST_REDUCTION = 0.1;

		private const double LEVEL_COST_REDUCTION = 0.05;
	}
}
