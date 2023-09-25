using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBandage : SkillPassiveDataBase
	{
		public SkillDataBaseBandage()
		{
			this.nameKey = "SKILL_NAME_BANDAGE";
			this.descKey = "SKILL_DESC_BANDAGE";
			this.requiredHeroLevel = 8;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataBandage buffDataBandage = new BuffDataBandage(this.GetHealPeriod(level), this.GetHealRatio(level));
			buffDataBandage.id = 12;
			data.passiveBuff = buffDataBandage;
			buffDataBandage.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetHealPeriod(0))), AM.csa(GameMath.GetPercentString(this.GetHealRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetHealPeriod(data.level))) + AM.csl(" (" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseBandage.HEAL_PERIOD_LEVEL) + ")"), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetHealPeriod(data.level))), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)));
		}

		public float GetHealPeriod(int level)
		{
			return SkillDataBaseBandage.HEAL_PERIOD_BASE + (float)level * SkillDataBaseBandage.HEAL_PERIOD_LEVEL;
		}

		public double GetHealRatio(int level)
		{
			return 0.3 + (double)level * 0.03;
		}

		public static float HEAL_PERIOD_BASE = 70f;

		public static float HEAL_PERIOD_LEVEL = -4f;

		private const double HEAL_RATIO_BASE = 0.3;

		private const double HEAL_RATIO_LEVEL = 0.03;
	}
}
