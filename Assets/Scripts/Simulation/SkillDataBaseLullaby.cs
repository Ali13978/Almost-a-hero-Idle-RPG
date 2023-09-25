using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLullaby : SkillPassiveDataBase
	{
		public SkillDataBaseLullaby()
		{
			this.nameKey = "SKILL_NAME_LULLABY";
			this.descKey = "SKILL_DESC_LULLABY";
			this.requiredHeroLevel = 4;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataLullaby buffDataLullaby = new BuffDataLullaby();
			buffDataLullaby.id = 119;
			data.passiveBuff = buffDataLullaby;
			buffDataLullaby.isPermenant = true;
			buffDataLullaby.cooldownReductionAmount = this.GetReduction(level);
		}

		private float GetReduction(int level)
		{
			return 1.4f + (float)level * 0.4f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.4f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReduction(data.level))));
		}

		private const float REDUCTION_BASE = 1.4f;

		private const float REDUCTION_LEVEL = 0.4f;
	}
}
