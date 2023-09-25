using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseEasyTargets : SkillPassiveDataBase
	{
		public SkillDataBaseEasyTargets()
		{
			this.nameKey = "SKILL_NAME_EASY_TARGETS";
			this.descKey = "SKILL_DESC_EASY_TARGETS";
			this.requiredHeroLevel = 6;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataEasyTargets buffDataEasyTargets = new BuffDataEasyTargets();
			buffDataEasyTargets.id = 338;
			data.passiveBuff = buffDataEasyTargets;
			buffDataEasyTargets.isPermenant = true;
			buffDataEasyTargets.isStackable = true;
			buffDataEasyTargets.damageFactor = 1.0 + this.GetDamageFactor(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)));
		}

		public double GetDamageFactor(int level)
		{
			return 0.6 + (double)level * 0.1;
		}

		private const double INITIAL_DAMAGE_FACTOR = 0.6;

		private const double LEVEL_DAMAGE_FACTOR = 0.1;
	}
}
