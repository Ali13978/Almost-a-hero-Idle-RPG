using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFullStomach : SkillPassiveDataBase
	{
		public SkillDataBaseFullStomach()
		{
			this.nameKey = "SKILL_NAME_FULL_STOMACH";
			this.descKey = "SKILL_DESC_FULL_STOMACH";
			this.requiredHeroLevel = 3;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFullStomach buffDataFullStomach = new BuffDataFullStomach();
			buffDataFullStomach.id = 90;
			data.passiveBuff = buffDataFullStomach;
			buffDataFullStomach.isPermenant = true;
			buffDataFullStomach.minHealthRatio = 0.8;
			buffDataFullStomach.damageAdd = AM.LinearEquationDouble((double)level, 0.11, 0.33);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.11, false) + ")"), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public double GetDamage(int level)
		{
			return 0.33 + 0.11 * (double)level;
		}

		private const double INITIAL_DAMAGE_BONUS = 0.33;

		private const double LEVEL_DAMAGE_BONUS = 0.11;

		private const double MIN_HEALTH_RATIO = 0.8;
	}
}
