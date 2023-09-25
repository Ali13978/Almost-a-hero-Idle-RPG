using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSoulSacrifice : SkillPassiveDataBase
	{
		public SkillDataBaseSoulSacrifice()
		{
			this.descKey = "SKILL_DESC_SOULSACRIFACE";
			this.nameKey = "SKILL_NAME_SOULSACRIFACE";
			this.requiredHeroLevel = 3;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataReduceReviveOnDeath buffDataReduceReviveOnDeath = new BuffDataReduceReviveOnDeath
			{
				reduction = this.GetReduction(level)
			};
			buffDataReduceReviveOnDeath.id = 276;
			data.passiveBuff = buffDataReduceReviveOnDeath;
			buffDataReduceReviveOnDeath.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetReduction(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetReduction(data.level)) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(1f) + ")")));
		}

		private float GetReduction(int level)
		{
			return 5f + (float)level * 1f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetReduction(data.level))));
		}

		public const float REVIVE_TIME_REDUCTION_BASE = 5f;

		public const float REVIVE_TIME_REDUCTION_PER_LEVEL = 1f;

		public const int LEVEL_MAX = 9;
	}
}
