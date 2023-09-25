using System;

namespace Simulation
{
	public class SkillEnhancerStrengthInNumbers : SkillEnhancerBase
	{
		public SkillEnhancerStrengthInNumbers()
		{
			this.nameKey = "SKILL_NAME_STRENGTH_IN_NUMBERS";
			this.descKey = "SKILL_DESC_STRENGTH_IN_NUMBERS";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_STAMPEDE")), AM.csa(SkillEnhancerStrengthInNumbers.GetAnimalsCount(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_STAMPEDE")), AM.csa(SkillEnhancerStrengthInNumbers.GetAnimalsCount(data.level).ToString()) + AM.csl(" (+" + 1.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_STAMPEDE")), AM.csa(SkillEnhancerStrengthInNumbers.GetAnimalsCount(data.level).ToString()));
		}

		public static int GetAnimalsCount(int level)
		{
			return 1 + level;
		}

		public const int ANIMALS_COUNT_INIT = 1;

		public const int ANIMALS_COUNT_PER_LEVEL = 1;
	}
}
