using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseWellFed : SkillPassiveDataBase
	{
		public SkillDataBaseWellFed()
		{
			this.nameKey = "SKILL_NAME_WELL_FED";
			this.descKey = "SKILL_DESC_WELL_FED";
			this.requiredHeroLevel = 9;
			this.maxLevel = 14;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataWellFed buffDataWellFed = new BuffDataWellFed();
			buffDataWellFed.id = 194;
			data.passiveBuff = buffDataWellFed;
			buffDataWellFed.isPermenant = true;
			buffDataWellFed.minHealthRatio = 0.6;
			buffDataWellFed.damageAdd = this.GetDamageAdd(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(0), false)), AM.csa(GameMath.GetPercentString(0.6, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15, false) + ")"), AM.csa(GameMath.GetPercentString(0.6, false)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)), AM.csa(GameMath.GetPercentString(0.6, false)));
		}

		public double GetDamageAdd(int level)
		{
			return 0.4 + 0.15 * (double)level;
		}

		private const double HEALTH_RATIO = 0.6;

		private const double INITIAL_DAMAGE = 0.4;

		private const double LEVEL_DAMAGE = 0.15;
	}
}
