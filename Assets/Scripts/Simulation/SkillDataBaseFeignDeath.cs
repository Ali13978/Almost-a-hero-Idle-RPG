using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFeignDeath : SkillPassiveDataBase
	{
		public SkillDataBaseFeignDeath()
		{
			this.nameKey = "SKILL_NAME_FEIGN_DEATH";
			this.descKey = "SKILL_DESC_FEIGN_DEATH";
			this.requiredHeroLevel = 13;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFeignDeath buffDataFeignDeath = new BuffDataFeignDeath(0.2, this.GetImmunityTime(level), this.GetImmunityPeriod(level));
			buffDataFeignDeath.id = 82;
			data.passiveBuff = buffDataFeignDeath;
			buffDataFeignDeath.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.2, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetImmunityTime(0))), AM.csa(GameMath.GetTimeInMilliSecondsString(125f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.2, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetImmunityTime(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetImmunityPeriod(data.level))) + AM.csl(" (-" + GameMath.GetTimeInMilliSecondsString(5f) + ")"));
		}

		private float GetImmunityPeriod(int level)
		{
			return 125f - (float)level * 5f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.2, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetImmunityTime(data.level))), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetImmunityPeriod(data.level))));
		}

		public float GetImmunityTime(int level)
		{
			return 11f + (float)level * 1f;
		}

		public const double HEALTH_RATIO_BASE = 0.2;

		private const float IMMUNITY_TIME_BASE = 11f;

		private const float IMMUNITY_TIME_LEVEL = 1f;

		public const float IMMUNITY_PERIOD_BASE = 125f;

		public const float IMMUNITY_PERIOD_LEVEL = 5f;
	}
}
