using System;

namespace Simulation
{
	public class SkillEnhancerBaseMadness : SkillEnhancerBase
	{
		public SkillEnhancerBaseMadness()
		{
			this.nameKey = "SKILL_NAME_MADNESS";
			this.descKey = "SKILL_DESC_MADNESS";
			this.requiredHeroLevel = 9;
			this.maxLevel = 5;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumFireworks(0).ToString()), AM.csa(LM.Get("SKILL_NAME_FIREWORKS")));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumFireworks(data.level).ToString()) + AM.csl(" (+" + SkillEnhancerBaseMadness.LEVEL_NUM_FIRES.ToString() + ")"), AM.csa(LM.Get("SKILL_NAME_FIREWORKS")));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumFireworks(data.level).ToString()), AM.csa(LM.Get("SKILL_NAME_FIREWORKS")));
		}

		public int GetNumFireworks(int level)
		{
			return SkillEnhancerBaseMadness.INITIAL_NUM_FIRES + SkillEnhancerBaseMadness.LEVEL_NUM_FIRES * level;
		}

		public static int INITIAL_NUM_FIRES = 2;

		public static int LEVEL_NUM_FIRES = 1;
	}
}
