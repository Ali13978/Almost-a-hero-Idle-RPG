using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCityThief : SkillPassiveDataBase
	{
		public SkillDataBaseCityThief()
		{
			this.nameKey = "SKILL_NAME_CITY_THIEF";
			this.descKey = "SKILL_DESC_CITY_THIEF";
			this.requiredHeroLevel = 7;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCityThief buffDataCityThief = new BuffDataCityThief();
			data.passiveBuff = buffDataCityThief;
			buffDataCityThief.isPermenant = true;
			buffDataCityThief.goldAdd = this.GetGold(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.075, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false)));
		}

		public double GetGold(int level)
		{
			return 0.25 + (double)level * 0.075;
		}

		private const double INITIAL_GOLD = 0.25;

		private const double LEVEL_GOLD = 0.075;
	}
}
