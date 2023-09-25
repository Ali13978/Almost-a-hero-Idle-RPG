using System;

namespace Simulation
{
	public class SkillEnhancerBaseConstitution : SkillEnhancerBase
	{
		public SkillEnhancerBaseConstitution()
		{
			this.nameKey = "SKILL_NAME_CONSTITUTION";
			this.descKey = "SKILL_DESC_CONSTITUTION";
			this.requiredHeroLevel = 7;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_MINI_CANNON")), AM.csa(this.GetCount(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_MINI_CANNON")), AM.csa(this.GetCount(data.level).ToString()) + AM.csl(" (+" + SkillEnhancerBaseConstitution.LEVEL_TIMES.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_MINI_CANNON")), AM.csa(this.GetCount(data.level).ToString()));
		}

		public int GetCount(int level)
		{
			return SkillEnhancerBaseConstitution.INITIAL_TIMES + SkillEnhancerBaseConstitution.LEVEL_TIMES * level;
		}

		public static int INITIAL_TIMES = 4;

		public static int LEVEL_TIMES = 2;
	}
}
