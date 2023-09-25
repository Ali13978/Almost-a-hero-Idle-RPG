using System;

namespace Simulation
{
	public class SkillEnhancerCurly : SkillEnhancerBase
	{
		public SkillEnhancerCurly()
		{
			this.nameKey = "SKILL_NAME_CURLY";
			this.descKey = "SKILL_DESC_CURLY";
			this.requiredHeroLevel = 5;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerCurly.GetCriticalDamageChanceAdd(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerCurly.GetCriticalDamageChanceAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerCurly.GetCriticalDamageChanceAdd(data.level), false)));
		}

		public static float GetCriticalDamageChanceAdd(int level)
		{
			return 0.08f + 0.02f * (float)level;
		}

		public const float CRIT_CHANCE_INIT = 0.08f;

		public const float CRIT_CHANCE_PER_LEVEL = 0.02f;
	}
}
