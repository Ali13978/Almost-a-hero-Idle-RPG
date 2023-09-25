using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseMoraleBooster : SkillPassiveDataBase
	{
		public SkillDataBaseMoraleBooster()
		{
			this.nameKey = "SKILL_NAME_MORALE_BOOSTER";
			this.descKey = "SKILL_DESC_MORALE_BOOSTER";
			this.requiredHeroLevel = 5;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataMoraleBooster((double)(1f + this.GetDamageIncreaseRatio(level)))
			{
				id = 339,
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncreaseRatio(data.level), false)));
		}

		public float GetDamageIncreaseRatio(int level)
		{
			return 0.3f + (float)level * 0.1f;
		}

		public const float INCREASE_RATIO_INIT = 0.3f;

		public const float INCREASE_RATIO_PER_LEVEL = 0.1f;
	}
}
