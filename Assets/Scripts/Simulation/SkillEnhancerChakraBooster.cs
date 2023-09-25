using System;

namespace Simulation
{
	public class SkillEnhancerChakraBooster : SkillEnhancerBase
	{
		public SkillEnhancerChakraBooster()
		{
			this.nameKey = "SKILL_NAME_CHAKRA_BOOSTER";
			this.descKey = "SKILL_DESC_CHAKRA_BOOSTER";
			this.requiredHeroLevel = 7;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(this.GetCount(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(this.GetCount(data.level).ToString()) + AM.csl(" (+" + SkillEnhancerChakraBooster.LEVEL_TIMES.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(this.GetCount(data.level).ToString()));
		}

		public int GetCount(int level)
		{
			return SkillEnhancerChakraBooster.INITIAL_TIMES + SkillEnhancerChakraBooster.LEVEL_TIMES * level;
		}

		public static int INITIAL_TIMES = 1;

		public static int LEVEL_TIMES = 1;
	}
}
