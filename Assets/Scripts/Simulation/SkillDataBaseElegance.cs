using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseElegance : SkillPassiveDataBase
	{
		public SkillDataBaseElegance()
		{
			this.nameKey = "SKILL_NAME_ELEGANCE";
			this.descKey = "SKILL_DESC_ELEGANCE";
			this.requiredHeroLevel = 5;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataElegance buffDataElegance = new BuffDataElegance();
			buffDataElegance.id = 71;
			data.passiveBuff = buffDataElegance;
			buffDataElegance.isPermenant = true;
			buffDataElegance.durAdd = this.GetDurationIncrease(level);
			buffDataElegance.durTotMax = this.GetDurationMax(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SWIFT_MOVES")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationIncrease(0))), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationMax(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SWIFT_MOVES")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationIncrease(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseElegance.INCREASE_LEVEL) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationMax(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseElegance.DURATION_LEVEL) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SWIFT_MOVES")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationIncrease(data.level))), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDurationMax(data.level))));
		}

		public float GetDurationIncrease(int level)
		{
			return SkillDataBaseElegance.INCREASE_INIT + SkillDataBaseElegance.INCREASE_LEVEL * (float)level;
		}

		public float GetDurationMax(int level)
		{
			return SkillDataBaseElegance.DURATION_INIT + SkillDataBaseElegance.DURATION_LEVEL * (float)level;
		}

		public static float DURATION_INIT = 8f;

		public static float DURATION_LEVEL = 2f;

		public static float INCREASE_INIT = 1f;

		public static float INCREASE_LEVEL = 0.5f;
	}
}
