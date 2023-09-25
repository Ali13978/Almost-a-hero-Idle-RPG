using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFormerFriends : SkillPassiveDataBase
	{
		public SkillDataBaseFormerFriends()
		{
			this.nameKey = "SKILL_NAME_FORMER_FRIENDS";
			this.descKey = "SKILL_DESC_FORMER_FRIENDS";
			this.requiredHeroLevel = 3;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFormerFriends buffDataFormerFriends = new BuffDataFormerFriends
			{
				effect = new BuffDataDamageAdd(),
				reduction = this.GetReduction(level),
				duration = this.GetDuration(level)
			};
			data.passiveBuff = buffDataFormerFriends;
			buffDataFormerFriends.isPermenant = true;
			buffDataFormerFriends.id = 283;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetReduction(int level)
		{
			return 0.1f + (float)level * 0.02f;
		}

		public float GetDuration(int level)
		{
			return 10f + (float)level * 0f;
		}

		private const float INITIAL_REDUCTION = 0.1f;

		private const float LEVEL_REDUCTION = 0.02f;

		private const float INITIAL_DURATION = 10f;

		private const float LEVEL_DURATION = 0f;
	}
}
