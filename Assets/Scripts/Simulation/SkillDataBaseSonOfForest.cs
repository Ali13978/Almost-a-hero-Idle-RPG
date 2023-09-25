using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSonOfForest : SkillPassiveDataBase
	{
		public SkillDataBaseSonOfForest()
		{
			this.nameKey = "SKILL_NAME_FOREST_SON";
			this.descKey = "SKILL_DESC_FOREST_SON";
			this.requiredHeroLevel = 5;
			this.maxLevel = 16;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDamageTotem buffDataDamageTotem = new BuffDataDamageTotem();
			buffDataDamageTotem.id = 48;
			data.passiveBuff = buffDataDamageTotem;
			buffDataDamageTotem.isPermenant = true;
			buffDataDamageTotem.damageAdd = this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)));
		}

		public double GetDamage(int level)
		{
			return 0.18 + 0.02 * (double)level;
		}

		private const double INITIAL_DAMAGE_BONUS = 0.18;

		private const double DAMAGE_BONUS_EACH_LEVEL = 0.02;
	}
}
