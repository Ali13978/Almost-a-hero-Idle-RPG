using System;

namespace Simulation
{
	public class SkillEnhancerLongWinded : SkillEnhancerBase
	{
		public SkillEnhancerLongWinded()
		{
			this.nameKey = "SKILL_NAME_LONG_WINDED";
			this.descKey = "SKILL_DESC_LONG_WINDED";
			this.requiredHeroLevel = 11;
			this.maxLevel = 2;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_TAKE_ONE_FOR_THE_TEAM")), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillEnhancerLongWinded.GetIncreaseDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_TAKE_ONE_FOR_THE_TEAM")), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillEnhancerLongWinded.GetIncreaseDuration(data.level))) + AM.csl(" (+" + SkillEnhancerLongWinded.DURATION_PER_LEVEL.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_TAKE_ONE_FOR_THE_TEAM")), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillEnhancerLongWinded.GetIncreaseDuration(data.level))));
		}

		public static float GetIncreaseDuration(int level)
		{
			return SkillEnhancerLongWinded.DURATION_INIT + SkillEnhancerLongWinded.DURATION_PER_LEVEL * (float)level;
		}

		public static float DURATION_INIT = 2f;

		public static float DURATION_PER_LEVEL = 2f;
	}
}
