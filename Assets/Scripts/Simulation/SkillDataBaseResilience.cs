using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseResilience : SkillPassiveDataBase
	{
		public SkillDataBaseResilience()
		{
			this.nameKey = "SKILL_NAME_RESILIENCE";
			this.descKey = "SKILL_DESC_RESILIENCE";
			this.requiredHeroLevel = 6;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataResilience buffDataResilience = new BuffDataResilience();
			buffDataResilience.id = 150;
			data.passiveBuff = buffDataResilience;
			buffDataResilience.isPermenant = true;
			buffDataResilience.costDecPerHit = this.GetCostDecPerHit(level);
			buffDataResilience.costDecMax = this.GetCostDecMax(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDecPerHit(0), false)), AM.csa(GameMath.GetPercentString(this.GetCostDecMax(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDecPerHit(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.001, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetCostDecMax(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDecPerHit(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetCostDecMax(data.level), false)));
		}

		private double GetCostDecPerHit(int level)
		{
			return AM.LinearEquationDouble((double)level, 0.001, 0.001);
		}

		private double GetCostDecMax(int level)
		{
			return AM.LinearEquationDouble((double)level, 0.03, 0.23);
		}

		private const double INITIAL_REDUCTION = 0.001;

		private const double LEVEL_REDUCTION = 0.001;

		private const double INITIAL_MAX = 0.23;

		private const double LEVEL_MAX = 0.03;
	}
}
