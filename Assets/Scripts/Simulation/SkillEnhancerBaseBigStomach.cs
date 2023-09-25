using System;

namespace Simulation
{
	public class SkillEnhancerBaseBigStomach : SkillEnhancerBase
	{
		public SkillEnhancerBaseBigStomach()
		{
			this.nameKey = "SKILL_NAME_BIG_STOMACH";
			this.descKey = "SKILL_DESC_BIG_STOMACH";
			this.requiredHeroLevel = 8;
			this.maxLevel = 5;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_EAT_AN_APPLE")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_EAT_AN_APPLE")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(2f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_EAT_AN_APPLE")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return 4f + 2f * (float)level;
		}

		private const float INITIAL_DURATION = 4f;

		private const float LEVEL_DURATION = 2f;
	}
}
