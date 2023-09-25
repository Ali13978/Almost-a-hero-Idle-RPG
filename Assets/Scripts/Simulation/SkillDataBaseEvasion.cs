using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseEvasion : SkillPassiveDataBase
	{
		public SkillDataBaseEvasion()
		{
			this.nameKey = "SKILL_NAME_EVASION";
			this.descKey = "SKILL_DESC_EVASION";
			this.requiredHeroLevel = 7;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataEvasion buffDataEvasion = new BuffDataEvasion();
			buffDataEvasion.id = 79;
			data.passiveBuff = buffDataEvasion;
			buffDataEvasion.isPermenant = true;
			buffDataEvasion.healthRatioMax = this.GetHealthRatio(level);
			buffDataEvasion.dodgeChanceAdd = this.GetDodgeChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.04f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(data.level), false)));
		}

		public float GetDodgeChance(int level)
		{
			return 0.28f + (float)level * 0.04f;
		}

		public double GetHealthRatio(int level)
		{
			return 0.2 + (double)level * 0.05;
		}

		private const double HEALTH_RATIO_MAX = 0.2;

		private const double LEVEL_HEALTH_RATIO = 0.05;

		private const float INITIAL_DODGE_CHANCE = 0.28f;

		private const float LEVEL_DODGE_CHANCE = 0.04f;
	}
}
