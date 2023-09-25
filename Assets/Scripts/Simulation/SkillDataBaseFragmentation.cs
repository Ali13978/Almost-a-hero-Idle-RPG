using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFragmentation : SkillPassiveDataBase
	{
		public SkillDataBaseFragmentation()
		{
			this.nameKey = "SKILL_NAME_FRAGMENTATION";
			this.descKey = "SKILL_DESC_FRAGMENTATION";
			this.requiredHeroLevel = 3;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCritFactor buffDataCritFactor = new BuffDataCritFactor();
			buffDataCritFactor.id = 39;
			data.passiveBuff = buffDataCritFactor;
			buffDataCritFactor.isPermenant = true;
			buffDataCritFactor.isStackable = true;
			buffDataCritFactor.critFactorAdd = this.GetCritDamage(level);
		}

		public override string GetDescZero()
		{
			double critDamage = this.GetCritDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(critDamage, false)));
		}

		public override string GetDesc(SkillData data)
		{
			double critDamage = this.GetCritDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(critDamage, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.2, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			double critDamage = this.GetCritDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(critDamage, false)));
		}

		public double GetCritDamage(int level)
		{
			return 0.4 + 0.2 * (double)level;
		}

		private const double INIT_CRIT_DAMAGE = 0.4;

		private const double LEVEL_CRIT_DAMAGE = 0.2;
	}
}
