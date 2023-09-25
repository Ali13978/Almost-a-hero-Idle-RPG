using System;

namespace Simulation
{
	public class SkillEnhancerBaseBargainer : SkillEnhancerBase
	{
		public SkillEnhancerBaseBargainer()
		{
			this.nameKey = "SKILL_NAME_BARGAINER";
			this.descKey = "SKILL_DESC_BARGAINER";
			this.requiredHeroLevel = 6;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_NEGOTIATE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_NEGOTIATE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(data.level))) + AM.csl(" (+" + SkillEnhancerBaseBargainer.LEVEL_REDUCTION.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_NEGOTIATE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(data.level))));
		}

		public float GetReduction(int level)
		{
			return SkillEnhancerBaseBargainer.INITIAL_REDUCTION + SkillEnhancerBaseBargainer.LEVEL_REDUCTION * (float)level;
		}

		public static float INITIAL_REDUCTION = 2f;

		public static float LEVEL_REDUCTION = 1.5f;
	}
}
