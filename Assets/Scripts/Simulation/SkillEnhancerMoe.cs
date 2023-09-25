using System;

namespace Simulation
{
	public class SkillEnhancerMoe : SkillEnhancerBase
	{
		public SkillEnhancerMoe()
		{
			this.nameKey = "SKILL_NAME_MOE";
			this.descKey = "SKILL_DESC_MOE";
			this.requiredHeroLevel = 8;
			this.maxLevel = 10;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerMoe.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerMoe.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.10000000149011612, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_LARRY")), AM.csa(GameMath.GetPercentString(SkillEnhancerMoe.GetDamage(data.level), false)));
		}

		public static double GetDamage(int level)
		{
			return 0.20000000298023224 + 0.10000000149011612 * (double)level;
		}

		public const double DAMAGE_INIT = 0.20000000298023224;

		public const double DAMAGE_PER_LEVEL = 0.10000000149011612;
	}
}
