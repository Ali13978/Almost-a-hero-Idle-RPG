using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRecycle : SkillPassiveDataBase
	{
		public SkillDataBaseRecycle()
		{
			this.nameKey = "SKILL_NAME_RECYCLE";
			this.descKey = "SKILL_DESC_RECYCLE";
			this.requiredHeroLevel = 5;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataRecycle buffDataRecycle = new BuffDataRecycle();
			buffDataRecycle.id = 145;
			data.passiveBuff = buffDataRecycle;
			buffDataRecycle.isPermenant = true;
			buffDataRecycle.goldFactor = this.GetGoldEarning(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldEarning(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldEarning(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.12, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldEarning(data.level), false)));
		}

		public double GetGoldEarning(int level)
		{
			return 0.24 + 0.12 * (double)level;
		}

		private const double INITIAL_GOLD_EARNING = 0.24;

		private const double LEVEL_GOLD_EARNING = 0.12;
	}
}
