using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseInnerWorth : SkillPassiveDataBase
	{
		public SkillDataBaseInnerWorth()
		{
			this.nameKey = "SKILL_NAME_INNER_WORTH";
			this.descKey = "SKILL_DESC_INNER_WORTH";
			this.requiredHeroLevel = 7;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataHealthMax
			{
				id = 94,
				healthMaxAdd = (double)this.GetMaxHealthIncrease(level),
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthIncrease(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthIncrease(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthIncrease(data.level), false)));
		}

		public float GetMaxHealthIncrease(int level)
		{
			return 0.6f + (float)level * 0.1f;
		}

		public const float HEALTH_MAX_INIT = 0.6f;

		public const float HEALTH_MAX_PER_LEVEL = 0.1f;
	}
}
