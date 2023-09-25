using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseAngerManagement : SkillPassiveDataBase
	{
		public SkillDataBaseAngerManagement()
		{
			this.nameKey = "SKILL_NAME_ANGER_MANAGEMENT";
			this.descKey = "SKILL_DESC_ANGER_MANAGEMENT";
			this.requiredHeroLevel = 9;
			this.maxLevel = 2;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataAngerManagement buffDataAngerManagement = new BuffDataAngerManagement();
			buffDataAngerManagement.id = 2;
			data.passiveBuff = buffDataAngerManagement;
			buffDataAngerManagement.isPermenant = true;
			buffDataAngerManagement.durInc = AM.LinearEquationFloat((float)level, 0.5f, 1f);
			buffDataAngerManagement.durMax = AM.LinearEquationFloat((float)level, 7.5f, 15f);
		}

		public float GetDur(int level)
		{
			return 1f + 0.5f * (float)level;
		}

		public float GetMax(int level)
		{
			return 15f + 7.5f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_ANGER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(0))), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetMax(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_ANGER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetMax(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(7.5f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_ANGER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetMax(data.level))));
		}

		private const float INITIAL_DUR_INC = 1f;

		private const float LEVEL_DUR_INC = 0.5f;

		private const float INITIAL_DUR_MAX = 15f;

		private const float LEVEL_DUR_MAX = 7.5f;
	}
}
