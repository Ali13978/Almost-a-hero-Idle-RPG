using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseHardTraining : SkillPassiveDataBase
	{
		public SkillDataBaseHardTraining()
		{
			this.nameKey = "SKILL_NAME_HARD_TRAINING";
			this.descKey = "SKILL_DESC_HARD_TRAINING";
			this.requiredHeroLevel = 3;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataHardTraining buffDataHardTraining = new BuffDataHardTraining();
			buffDataHardTraining.id = 93;
			data.passiveBuff = buffDataHardTraining;
			buffDataHardTraining.isPermenant = true;
			buffDataHardTraining.isStackable = true;
			buffDataHardTraining.cooldownDecrease = this.GetCooldownDec(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDec(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDec(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.1f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHOCK_WAVE")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDec(data.level))));
		}

		public float GetCooldownDec(int level)
		{
			return 0.3f + 0.1f * (float)level;
		}

		private const float INITIAL_CD_DEC = 0.3f;

		private const float LEVEL_CD_DEC = 0.1f;
	}
}
