using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseMultiShot : SkillPassiveDataBase
	{
		public SkillDataBaseMultiShot()
		{
			this.nameKey = "SKILL_NAME_MULTI_SHOT";
			this.descKey = "SKILL_DESC_MULTI_SHOT";
			this.requiredHeroLevel = 9;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataMultiShot buffDataMultiShot = new BuffDataMultiShot();
			buffDataMultiShot.id = 133;
			data.passiveBuff = buffDataMultiShot;
			buffDataMultiShot.isPermenant = true;
			buffDataMultiShot.chance = this.GetChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseMultiShot.CHANCE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)));
		}

		public float GetChance(int level)
		{
			return SkillDataBaseMultiShot.CHANCE_INIT + (float)level * SkillDataBaseMultiShot.CHANCE_LEVEL;
		}

		public static float CHANCE_INIT = 0.1f;

		public static float CHANCE_LEVEL = 0.05f;
	}
}
