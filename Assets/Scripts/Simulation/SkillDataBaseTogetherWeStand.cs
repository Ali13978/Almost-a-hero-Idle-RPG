using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTogetherWeStand : SkillPassiveDataBase
	{
		public SkillDataBaseTogetherWeStand()
		{
			this.nameKey = "SKILL_NAME_TOGETHER_WE_STAND";
			this.descKey = "SKILL_DESC_TOGETHER_WE_STAND";
			this.requiredHeroLevel = 12;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTogetherWeStand buffDataTogetherWeStand = new BuffDataTogetherWeStand();
			buffDataTogetherWeStand.id = 185;
			data.passiveBuff = buffDataTogetherWeStand;
			buffDataTogetherWeStand.isPermenant = true;
			buffDataTogetherWeStand.damageTakenFactor = 1.0 - (double)this.GetReduction(level);
		}

		private float GetReduction(int level)
		{
			return 0.04f + (float)level * 0.02f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)));
		}

		private const float REDUCTION_BASE = 0.04f;

		private const float REDUCTION_LEVEL = 0.02f;
	}
}
