using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseManOfHill : SkillPassiveDataBase
	{
		public SkillDataBaseManOfHill()
		{
			this.nameKey = "SKILL_NAME_HILL_MAN";
			this.descKey = "SKILL_DESC_HILL_MAN";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataManOfHill buffDataManOfHill = new BuffDataManOfHill();
			buffDataManOfHill.id = 124;
			data.passiveBuff = buffDataManOfHill;
			buffDataManOfHill.isPermenant = true;
			BuffDataDropGold buffDataDropGold = new BuffDataDropGold();
			buffDataDropGold.id = 68;
			buffDataManOfHill.effect = buffDataDropGold;
			buffDataDropGold.dur = 20f;
			buffDataDropGold.isStackable = true;
			buffDataDropGold.dropGoldFactorAdd = this.GetGoldFactorAdd(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactorAdd(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(20f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactorAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(20f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactorAdd(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(20f)));
		}

		public double GetGoldFactorAdd(int level)
		{
			return 0.1 + 0.05 * (double)level;
		}

		private const double INIT_GOLD = 0.1;

		private const double LEVEL_GOLD = 0.05;

		private const float DURATION = 20f;
	}
}
