using System;

namespace Simulation
{
	public class SkillEnhancerBaseNotSoFast : SkillEnhancerBase
	{
		public SkillEnhancerBaseNotSoFast()
		{
			this.nameKey = "SKILL_NAME_NOT_SO_FAST";
			this.descKey = "SKILL_DESC_NOT_SO_FAST";
			this.requiredHeroLevel = 6;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseNotSoFast.GetGold(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseNotSoFast.GetGold(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseNotSoFast.GOLD_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseNotSoFast.GetGold(data.level), false)));
		}

		public static float GetGold(int enhanceLevelGold)
		{
			return SkillEnhancerBaseNotSoFast.GOLD_BASE + SkillEnhancerBaseNotSoFast.GOLD_LEVEL * (float)enhanceLevelGold;
		}

		private static float GOLD_BASE = 0.25f;

		private static float GOLD_LEVEL = 0.15f;
	}
}
