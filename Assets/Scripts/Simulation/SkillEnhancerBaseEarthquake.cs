using System;

namespace Simulation
{
	public class SkillEnhancerBaseEarthquake : SkillEnhancerBase
	{
		public SkillEnhancerBaseEarthquake()
		{
			this.nameKey = "SKILL_NAME_EARTHQUAKE";
			this.descKey = "SKILL_DESC_EARTHQUAKE";
			this.requiredHeroLevel = 11;
			this.maxLevel = 9;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(this.GetAttackTimes(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(this.GetAttackTimes(data.level).ToString()) + AM.csl(" (+" + SkillEnhancerBaseEarthquake.LEVEL_NUM.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(this.GetAttackTimes(data.level).ToString()));
		}

		public int GetAttackTimes(int level)
		{
			return SkillEnhancerBaseEarthquake.INITIAL_NUM + SkillEnhancerBaseEarthquake.LEVEL_NUM * level;
		}

		public static int INITIAL_NUM = 1;

		public static int LEVEL_NUM = 1;
	}
}
