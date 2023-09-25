using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCallOfWild : SkillPassiveDataBase
	{
		public SkillDataBaseCallOfWild()
		{
			this.nameKey = "SKILL_NAME_CALL_OF_WILD";
			this.descKey = "SKILL_DESC_CALL_OF_WILD";
			this.requiredHeroLevel = 11;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCallOfWild buffDataCallOfWild = new BuffDataCallOfWild(this.GetDuration(level));
			buffDataCallOfWild.id = 23;
			data.passiveBuff = buffDataCallOfWild;
			buffDataCallOfWild.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseCallOfWild.DUR_LEVEL) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return SkillDataBaseCallOfWild.DUR_BASE + (float)level * SkillDataBaseCallOfWild.DUR_LEVEL;
		}

		public static float DUR_BASE = 0.1f;

		public static float DUR_LEVEL = 0.1f;
	}
}
