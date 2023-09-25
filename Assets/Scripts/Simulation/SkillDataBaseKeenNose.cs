using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseKeenNose : SkillPassiveDataBase
	{
		public SkillDataBaseKeenNose()
		{
			this.nameKey = "SKILL_NAME_KEEN_NOSE";
			this.descKey = "SKILL_DESC_KEEN_NOSE";
			this.requiredHeroLevel = 5;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTracker buffDataTracker = new BuffDataTracker();
			buffDataTracker.id = 284;
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
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseKeenNose.CHANCE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)));
		}

		public float GetChance(int level)
		{
			return SkillDataBaseKeenNose.CHANCE_INIT + (float)level * SkillDataBaseKeenNose.CHANCE_LEVEL;
		}

		public static float CHANCE_INIT = 0.1f;

		public static float CHANCE_LEVEL = 0.1f;
	}
}
