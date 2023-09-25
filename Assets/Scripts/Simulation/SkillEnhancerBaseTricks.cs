using System;

namespace Simulation
{
	public class SkillEnhancerBaseTricks : SkillEnhancerBase
	{
		public SkillEnhancerBaseTricks()
		{
			this.nameKey = "SKILL_NAME_TRICKS";
			this.descKey = "SKILL_DESC_TRICKS";
			this.requiredHeroLevel = 3;
			this.maxLevel = 9;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseTricks.LEVEL_BONUS, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(data.level), false)));
		}

		private double GetBonus(int level)
		{
			return SkillEnhancerBaseTricks.INITIAL_BONUS + SkillEnhancerBaseTricks.LEVEL_BONUS * (double)level;
		}

		public static double INITIAL_BONUS = 0.12;

		public static double LEVEL_BONUS = 0.06;
	}
}
