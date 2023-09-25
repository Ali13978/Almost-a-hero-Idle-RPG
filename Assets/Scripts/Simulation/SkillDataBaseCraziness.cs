using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCraziness : SkillPassiveDataBase
	{
		public SkillDataBaseCraziness()
		{
			this.nameKey = "SKILL_NAME_CRAZINESS";
			this.descKey = "SKILL_DESC_CRAZINESS";
			this.requiredHeroLevel = 8;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCraziness buffDataCraziness = new BuffDataCraziness();
			buffDataCraziness.id = 31;
			data.passiveBuff = buffDataCraziness;
			buffDataCraziness.isPermenant = true;
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 35;
			buffDataCraziness.effect = buffDataCritChance;
			buffDataCraziness.visuals |= 4;
			buffDataCritChance.isStackable = true;
			buffDataCritChance.dur = 15f;
			buffDataCritChance.critChanceAdd = this.GetChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(GameMath.GetTimeInSecondsString(15f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(15f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(15f)));
		}

		public float GetChance(int level)
		{
			return 0.06f + 0.03f * (float)level;
		}

		private const float INITIAL_CHANCE = 0.06f;

		private const float LEVEL_CHANCE = 0.03f;

		private const float DURATION = 15f;
	}
}
