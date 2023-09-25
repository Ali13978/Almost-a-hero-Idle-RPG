using System;

namespace Simulation
{
	public class SkillEnhancerBaseDepression : SkillEnhancerBase
	{
		public SkillEnhancerBaseDepression()
		{
			this.nameKey = "SKILL_NAME_DEPRESSION";
			this.descKey = "SKILL_DESC_DEPRESSION";
			this.requiredHeroLevel = 3;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDepression.GetSlow(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDepression.GetSlow(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseDepression.SPEED_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_WEEPING_SONG")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDepression.GetSlow(data.level), false)));
		}

		public static float GetSlow(int enhanceLevelSpeed)
		{
			return SkillEnhancerBaseDepression.SPEED_BASE + SkillEnhancerBaseDepression.SPEED_LEVEL * (float)enhanceLevelSpeed;
		}

		private static float SPEED_BASE = 0.12f;

		private static float SPEED_LEVEL = 0.06f;
	}
}
