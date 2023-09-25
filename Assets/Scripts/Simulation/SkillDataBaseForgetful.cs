using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseForgetful : SkillPassiveDataBase
	{
		public SkillDataBaseForgetful()
		{
			this.nameKey = "SKILL_NAME_FORGETFUL";
			this.descKey = "SKILL_DESC_FORGETFUL";
			this.requiredHeroLevel = 6;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataForgetful buffDataForgetful = new BuffDataForgetful();
			buffDataForgetful.id = 86;
			data.passiveBuff = buffDataForgetful;
			buffDataForgetful.isPermenant = true;
			buffDataForgetful.chance = this.GetChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(LM.Get("SKILL_NAME_OUT_OF_CONTROL")));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.025f, false) + ")"), AM.csa(LM.Get("SKILL_NAME_OUT_OF_CONTROL")));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(LM.Get("SKILL_NAME_OUT_OF_CONTROL")));
		}

		public float GetChance(int level)
		{
			return 0.05f + 0.025f * (float)level;
		}

		private const float INITIAL_CHANCE = 0.05f;

		private const float LEVEL_CHANCE = 0.025f;
	}
}
