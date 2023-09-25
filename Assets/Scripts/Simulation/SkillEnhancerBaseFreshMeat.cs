using System;

namespace Simulation
{
	public class SkillEnhancerBaseFreshMeat : SkillEnhancerBase
	{
		public SkillEnhancerBaseFreshMeat()
		{
			this.nameKey = "SKILL_NAME_FRESH_MEAT";
			this.descKey = "SKILL_DESC_FRESH_MEAT";
			this.requiredHeroLevel = 7;
			this.maxLevel = 9;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LUNCH_TIME")), AM.csa(GameMath.GetTimeInSecondsString(this.GetRed(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LUNCH_TIME")), AM.csa(GameMath.GetTimeInSecondsString(this.GetRed(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(10f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LUNCH_TIME")), AM.csa(GameMath.GetTimeInSecondsString(this.GetRed(data.level))));
		}

		public float GetRed(int level)
		{
			return 10f + 10f * (float)level;
		}

		public const float INITIAL_REDUCTION = 10f;

		public const float LEVEL_REDUCTION = 10f;
	}
}
