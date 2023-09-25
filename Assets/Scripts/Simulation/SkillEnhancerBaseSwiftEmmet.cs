using System;

namespace Simulation
{
	public class SkillEnhancerBaseSwiftEmmet : SkillEnhancerBase
	{
		public SkillEnhancerBaseSwiftEmmet()
		{
			this.nameKey = "SKILL_NAME_SWIFT_EMMET";
			this.descKey = "SKILL_DESC_SWIFT_EMMET";
			this.requiredHeroLevel = 5;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_RUN_EMMET_RUN")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_RUN_EMMET_RUN")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(SkillEnhancerBaseSwiftEmmet.LEVEL_RED) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_RUN_EMMET_RUN")), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return SkillEnhancerBaseSwiftEmmet.INITIAL_RED + (float)level * SkillEnhancerBaseSwiftEmmet.LEVEL_RED;
		}

		public static float INITIAL_RED = 15f;

		public static float LEVEL_RED = 15f;
	}
}
