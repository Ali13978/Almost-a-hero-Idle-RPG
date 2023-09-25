using System;

namespace Simulation
{
	public class SkillEnhancerBaseDeathFromAbove : SkillEnhancerBase
	{
		public SkillEnhancerBaseDeathFromAbove()
		{
			this.nameKey = "SKILL_NAME_DEATH_FROM_ABOVE";
			this.descKey = "SKILL_DESC_DEATH_FROM_ABOVE";
			this.requiredHeroLevel = 9;
			this.maxLevel = 9;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseDeathFromAbove.DAMAGE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)));
		}

		public double GetDamage(int level)
		{
			return SkillEnhancerBaseDeathFromAbove.DAMAGE_BASE + (double)level * SkillEnhancerBaseDeathFromAbove.DAMAGE_LEVEL;
		}

		public static double DAMAGE_BASE = 9.0;

		public static double DAMAGE_LEVEL = 3.0;
	}
}
