using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseVillageGirl : SkillPassiveDataBase
	{
		public SkillDataBaseVillageGirl()
		{
			this.nameKey = "SKILL_NAME_VILLAGE_GIRL";
			this.descKey = "SKILL_DESC_VILLAGE_GIRL";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataVillageGirl buffDataVillageGirl = new BuffDataVillageGirl();
			buffDataVillageGirl.id = 191;
			data.passiveBuff = buffDataVillageGirl;
			buffDataVillageGirl.isPermenant = true;
			buffDataVillageGirl.heatMaxAdd = this.GetHeatMaxAdd(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHeatMaxAdd(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHeatMaxAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHeatMaxAdd(data.level), false)));
		}

		public float GetHeatMaxAdd(int level)
		{
			return 0.25f + 0.15f * (float)level;
		}

		private const float INITIAL_HEAT_MAX_ADD = 0.25f;

		private const float LEVEL_HEAT_MAX_ADD = 0.15f;
	}
}
