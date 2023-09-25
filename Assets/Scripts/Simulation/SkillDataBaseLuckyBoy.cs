using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLuckyBoy : SkillPassiveDataBase
	{
		public SkillDataBaseLuckyBoy()
		{
			this.nameKey = "SKILL_NAME_LUCKY_BOY";
			this.descKey = "SKILL_DESC_LUCKY_BOY";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 33;
			data.passiveBuff = buffDataCritChance;
			buffDataCritChance.isPermenant = true;
			buffDataCritChance.critChanceAdd = this.GetChance(level);
		}

		public float GetChance(int level)
		{
			return 0.03f + 0.03f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)));
		}

		private const float INITIAL_INCREASE = 0.03f;

		private const float LEVEL_INCREASE = 0.03f;
	}
}
