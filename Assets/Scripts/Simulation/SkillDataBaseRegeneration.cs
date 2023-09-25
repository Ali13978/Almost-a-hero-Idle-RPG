using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRegeneration : SkillPassiveDataBase
	{
		public SkillDataBaseRegeneration()
		{
			this.nameKey = "SKILL_NAME_REGENERATION";
			this.descKey = "SKILL_DESC_REGENERATION";
			this.requiredHeroLevel = 10;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataMaxHealthAndRegen buffDataMaxHealthAndRegen = new BuffDataMaxHealthAndRegen();
			buffDataMaxHealthAndRegen.id = 128;
			data.passiveBuff = buffDataMaxHealthAndRegen;
			buffDataMaxHealthAndRegen.isPermenant = true;
			buffDataMaxHealthAndRegen.healthRegenAdd = this.GetReg(level);
			buffDataMaxHealthAndRegen.healthBonus = this.GetHealthBonus(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthBonus(0), false)), AM.csa(GameMath.GetPercentString(this.GetReg(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.07, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetReg(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.001, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthBonus(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetReg(data.level), false)));
		}

		private double GetHealthBonus(int level)
		{
			return 0.37 + (double)level * 0.07;
		}

		public double GetReg(int level)
		{
			return 0.001 + 0.001 * (double)level;
		}

		private const double INITIAL_HEALTH_REGEN = 0.001;

		private const double LEVEL_HEALTH_REGEN = 0.001;

		public const double INITIAL_HEALTH = 0.37;

		public const double LEVEL_HEALTH = 0.07;
	}
}
