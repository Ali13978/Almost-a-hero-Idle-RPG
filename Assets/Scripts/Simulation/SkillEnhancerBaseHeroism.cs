using System;

namespace Simulation
{
	public class SkillEnhancerBaseHeroism : SkillEnhancerBase
	{
		public SkillEnhancerBaseHeroism()
		{
			this.nameKey = "SKILL_NAME_HEROISM";
			this.descKey = "SKILL_DESC_HEROISM";
			this.requiredHeroLevel = 6;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BATTLECRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseHeroism.GetBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BATTLECRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseHeroism.GetBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseHeroism.CRIT_DAMAGE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BATTLECRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseHeroism.GetBonus(data.level), false)));
		}

		public static double GetBonus(int enhanceLevel)
		{
			return SkillEnhancerBaseHeroism.CRIT_DAMAGE_BASE + (double)enhanceLevel * SkillEnhancerBaseHeroism.CRIT_DAMAGE_LEVEL;
		}

		private static double CRIT_DAMAGE_BASE = 0.5;

		private static double CRIT_DAMAGE_LEVEL = 0.1;
	}
}
