using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillEnhancerBaseInstincts : SkillPassiveDataBase
	{
		public SkillEnhancerBaseInstincts()
		{
			this.nameKey = "SKILL_NAME_INSTINCTS";
			this.descKey = "SKILL_DESC_INSTINCTS";
			this.requiredHeroLevel = 6;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> skillEnhancers)
		{
			data.passiveBuff = new BuffDataInstincts
			{
				id = 109,
				cooldownReductionAmount = this.GetDuration(level),
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(LM.Get("SKILL_NAME_ROAR")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(LM.Get("SKILL_NAME_ROAR")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillEnhancerBaseInstincts.DUR_LEVEL) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(LM.Get("SKILL_NAME_ROAR")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return SkillEnhancerBaseInstincts.DUR_BASE + (float)level * SkillEnhancerBaseInstincts.DUR_LEVEL;
		}

		public static float DUR_BASE = 8f;

		public static float DUR_LEVEL = 3f;
	}
}
