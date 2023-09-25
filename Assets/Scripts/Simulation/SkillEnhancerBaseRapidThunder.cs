using System;

namespace Simulation
{
	public class SkillEnhancerBaseRapidThunder : SkillEnhancerBase
	{
		public SkillEnhancerBaseRapidThunder()
		{
			this.nameKey = "SKILL_NAME_RAPID_THUNDER";
			this.descKey = "SKILL_DESC_RAPID_THUNDER";
			this.requiredHeroLevel = 11;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_THUNDER_SOMETHING")), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_THUNDER_SOMETHING")), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseRapidThunder.LEVEL_REDUCTION, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_THUNDER_SOMETHING")), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)));
		}

		public float GetReduction(int level)
		{
			return SkillEnhancerBaseRapidThunder.INITIAL_REDUCTION + (float)level * SkillEnhancerBaseRapidThunder.LEVEL_REDUCTION;
		}

		public static float INITIAL_REDUCTION = 0.2f;

		public static float LEVEL_REDUCTION = 0.1f;
	}
}
