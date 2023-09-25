using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseWisdom : SkillPassiveDataBase
	{
		public SkillDataBaseWisdom()
		{
			this.nameKey = "SKILL_NAME_WISDOM";
			this.descKey = "SKILL_DESC_WISDOM";
			this.requiredHeroLevel = 6;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDamageUndamaged buffDataDamageUndamaged = new BuffDataDamageUndamaged();
			buffDataDamageUndamaged.id = 51;
			data.passiveBuff = buffDataDamageUndamaged;
			buffDataDamageUndamaged.isPermenant = true;
			buffDataDamageUndamaged.healthFactor = 0.8;
			buffDataDamageUndamaged.damageFactor = this.GetDamageFactor(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.3, false) + ")"), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)), AM.csa(GameMath.GetPercentString(0.8, false)));
		}

		public double GetDamageFactor(int level)
		{
			return 0.6 + 0.3 * (double)level;
		}

		private const double INITIAL_DAMAGE_FACTOR = 0.6;

		private const double LEVEL_DAMAGE_ADD = 0.3;

		private const double HEALTH_FACTOR = 0.8;
	}
}
