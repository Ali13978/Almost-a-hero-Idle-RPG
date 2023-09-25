using System;

namespace Simulation
{
	public class SkillEnhancerLetThemCome : SkillEnhancerBase
	{
		public SkillEnhancerLetThemCome()
		{
			this.nameKey = "SKILL_NAME_LET_THEM_COME";
			this.descKey = "SKILL_DESC_LET_THEM_COME";
			this.requiredHeroLevel = 13;
			this.maxLevel = 8;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_REVENGE")), AM.csa(GameMath.GetPercentString(SkillEnhancerLetThemCome.GetDamageAdd(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_REVENGE")), AM.csa(GameMath.GetPercentString(SkillEnhancerLetThemCome.GetDamageAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerLetThemCome.LEVEL_DAMAGE_ADD, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_REVENGE")), AM.csa(GameMath.GetPercentString(SkillEnhancerLetThemCome.GetDamageAdd(data.level), false)));
		}

		public static double GetDamageAdd(int level)
		{
			return SkillEnhancerLetThemCome.INIT_DAMAGE_ADD + SkillEnhancerLetThemCome.LEVEL_DAMAGE_ADD * (double)level;
		}

		private static double INIT_DAMAGE_ADD = 0.8;

		private static double LEVEL_DAMAGE_ADD = 0.4;
	}
}
