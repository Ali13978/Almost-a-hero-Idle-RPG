using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTimidFriends : SkillPassiveDataBase
	{
		public SkillDataBaseTimidFriends()
		{
			this.nameKey = "SKILL_NAME_TIMIDFRIENDS";
			this.descKey = "SKILL_DESC_TIMIDFRIENDS";
			this.requiredHeroLevel = 9;
			this.maxLevel = 12;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataAccelerateCommonAffinities
			{
				idleDur = SkillDataBaseTimidFriends.NO_TAP_DUR,
				id = 281,
				isPermenant = true,
				speedupPercentage = this.GetReduction(level)
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBaseTimidFriends.NO_TAP_DUR)), AM.csa(LM.Get("SKILL_NAME_COMMON_AFFINITIES")), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBaseTimidFriends.NO_TAP_DUR)), AM.csa(LM.Get("SKILL_NAME_COMMON_AFFINITIES")), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseTimidFriends.LEVEL_ACCELERATION, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBaseTimidFriends.NO_TAP_DUR)), AM.csa(LM.Get("SKILL_NAME_COMMON_AFFINITIES")), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)));
		}

		public float GetReduction(int level)
		{
			return SkillDataBaseTimidFriends.INITIAL_ACCELERATION + SkillDataBaseTimidFriends.LEVEL_ACCELERATION * (float)level;
		}

		public static float NO_TAP_DUR = 5f;

		public static float INITIAL_ACCELERATION = 0.14f;

		public static float LEVEL_ACCELERATION = 0.03f;
	}
}
