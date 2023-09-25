using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseImpulsive : SkillPassiveDataBase
	{
		public SkillDataBaseImpulsive()
		{
			this.nameKey = "SKILL_NAME_IMPULSIVE";
			this.descKey = "SKILL_DESC_IMPULSIVE";
			this.requiredHeroLevel = 13;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataIncreaseAttackWhenHealthLost passiveBuff = new BuffDataIncreaseAttackWhenHealthLost
			{
				damageFactor = this.GetDamageFactor(level),
				healthLostRatio = this.GetHealthLostRatioNeeded(level),
				isPermenant = true
			};
			data.passiveBuff = passiveBuff;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthLostRatioNeeded(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthLostRatioNeeded(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetHealthLostRatioNeeded(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)));
		}

		public float GetDamageFactor(int level)
		{
			return 2f + (float)level * 0.5f;
		}

		public double GetHealthLostRatioNeeded(int level)
		{
			return 0.25 + (double)level * 0.0;
		}

		public const float DAMAGE_FACTOR_INIT = 2f;

		public const float DAMAGE_FACTOR_PER_LEVEL = 0.5f;

		public const double HEALTH_LOST_RATIO_NEEDED_INIT = 0.25;

		public const double HEALTH_LOST_RATIO_NEEDED_PER_LEVEL = 0.0;
	}
}
