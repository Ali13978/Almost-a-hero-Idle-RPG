using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseDoubleMissile : SkillPassiveDataBase
	{
		public SkillDataBaseDoubleMissile()
		{
			this.nameKey = "SKILL_NAME_DOUBLE_MISSILE";
			this.descKey = "SKILL_DESC_DOUBLE_MISSILE";
			this.requiredHeroLevel = 10;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDoubleMissile buffDataDoubleMissile = new BuffDataDoubleMissile();
			buffDataDoubleMissile.id = 67;
			data.passiveBuff = buffDataDoubleMissile;
			buffDataDoubleMissile.isPermenant = true;
			buffDataDoubleMissile.chance = this.GetChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)));
		}

		public float GetChance(int level)
		{
			return 0.4f + 0.1f * (float)level;
		}

		private const float INITIAL_CHANCE = 0.4f;

		private const float LEVEL_CHANCE = 0.1f;
	}
}
