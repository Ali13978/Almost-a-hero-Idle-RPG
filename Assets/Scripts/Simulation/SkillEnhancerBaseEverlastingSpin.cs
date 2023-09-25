using System;

namespace Simulation
{
	public class SkillEnhancerBaseEverlastingSpin : SkillEnhancerBase
	{
		public SkillEnhancerBaseEverlastingSpin()
		{
			this.nameKey = "SKILL_NAME_EVERLAST";
			this.descKey = "SKILL_DESC_EVERLAST";
			this.requiredHeroLevel = 9;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEADLY_TWIRL")), AM.csa(this.GetCount(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEADLY_TWIRL")), AM.csa(this.GetCount(data.level).ToString()) + AM.csl(" (+" + SkillEnhancerBaseEverlastingSpin.NUM_LEVEL.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEADLY_TWIRL")), AM.csa(this.GetCount(data.level).ToString()));
		}

		public int GetCount(int level)
		{
			return SkillEnhancerBaseEverlastingSpin.NUM_INITIAL + SkillEnhancerBaseEverlastingSpin.NUM_LEVEL * level;
		}

		public static int NUM_INITIAL = 2;

		public static int NUM_LEVEL = 1;
	}
}
