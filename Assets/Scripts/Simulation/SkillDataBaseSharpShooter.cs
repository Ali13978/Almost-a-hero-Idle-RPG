using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSharpShooter : SkillPassiveDataBase
	{
		public SkillDataBaseSharpShooter()
		{
			this.nameKey = "SKILL_NAME_SHARP_SHOOTER";
			this.descKey = "SKILL_DESC_SHARP_SHOOTER";
			this.requiredHeroLevel = 3;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCritFactor buffDataCritFactor = new BuffDataCritFactor();
			buffDataCritFactor.id = 30;
			data.passiveBuff = buffDataCritFactor;
			buffDataCritFactor.isPermenant = true;
			buffDataCritFactor.critFactorAdd = this.GetCritDamage(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCritDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCritDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCritDamage(data.level), false)));
		}

		public double GetCritDamage(int level)
		{
			return 0.5 + 0.25 * (double)level;
		}

		private const double INITIAL_CRIT_DAMAGE = 0.5;

		private const double LEVEL_CRIT_DAMAGE = 0.25;
	}
}
