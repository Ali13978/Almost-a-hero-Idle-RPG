using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTracker : SkillPassiveDataBase
	{
		public SkillDataBaseTracker()
		{
			this.nameKey = "SKILL_NAME_TRACKER";
			this.descKey = "SKILL_DESC_TRACKER";
			this.requiredHeroLevel = 7;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTracker buffDataTracker = new BuffDataTracker();
			buffDataTracker.id = 187;
			data.passiveBuff = buffDataTracker;
			buffDataTracker.isPermenant = true;
			buffDataTracker.chestChanceAdd = this.GetChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseTracker.CHANCE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)));
		}

		public float GetChance(int level)
		{
			return SkillDataBaseTracker.CHANCE_INIT + (float)level * SkillDataBaseTracker.CHANCE_LEVEL;
		}

		public static float CHANCE_INIT = 0.05f;

		public static float CHANCE_LEVEL = 0.04f;
	}
}
