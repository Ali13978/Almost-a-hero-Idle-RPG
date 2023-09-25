using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseXxl : SkillPassiveDataBase
	{
		public SkillDataBaseXxl()
		{
			this.nameKey = "SKILL_NAME_XXL";
			this.descKey = "SKILL_DESC_XXL";
			this.requiredHeroLevel = 3;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataHealthMax buffDataHealthMax = new BuffDataHealthMax();
			buffDataHealthMax.id = 94;
			data.passiveBuff = buffDataHealthMax;
			buffDataHealthMax.isPermenant = true;
			buffDataHealthMax.isStackable = true;
			buffDataHealthMax.healthMaxAdd = this.GetMaxHealthAdd(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthAdd(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMaxHealthAdd(data.level), false)));
		}

		public double GetMaxHealthAdd(int level)
		{
			return 0.35 + (double)level * 0.15;
		}

		private const double INITIAL_MAX_HEALTH = 0.35;

		private const double LEVEL_MAX_HEALTH = 0.15;
	}
}
