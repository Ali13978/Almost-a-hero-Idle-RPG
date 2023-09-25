using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseDividedWeFall : SkillPassiveDataBase
	{
		public SkillDataBaseDividedWeFall()
		{
			this.nameKey = "SKILL_NAME_DIVIDED_WE_FALL";
			this.descKey = "SKILL_DESC_DIVIDED_WE_FALL";
			this.requiredHeroLevel = 10;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDividedWeFall buffDataDividedWeFall = new BuffDataDividedWeFall();
			buffDataDividedWeFall.id = 65;
			data.passiveBuff = buffDataDividedWeFall;
			buffDataDividedWeFall.isPermenant = true;
			buffDataDividedWeFall.shieldRatio = (double)this.GetShield(level);
			buffDataDividedWeFall.shieldDur = 1000f;
		}

		private float GetShield(int level)
		{
			return SkillDataBaseDividedWeFall.SHIELD_BASE + (float)level * SkillDataBaseDividedWeFall.SHIELD_LEVEL;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseDividedWeFall.SHIELD_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShield(data.level), false)));
		}

		private static float SHIELD_BASE = 0.3f;

		private static float SHIELD_LEVEL = 0.12f;
	}
}
