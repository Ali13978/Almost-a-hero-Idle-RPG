using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseConfusingPresence : SkillPassiveDataBase
	{
		public SkillDataBaseConfusingPresence()
		{
			this.nameKey = "SKILL_NAME_CONFUSING_PRESENCE";
			this.descKey = "SKILL_DESC_CONFUSING_PRESENCE";
			this.requiredHeroLevel = 8;
			this.maxLevel = 12;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataConfusingPresence buffDataConfusingPresence = new BuffDataConfusingPresence
			{
				chance = this.GetChance(level),
				slow = this.GetSlow(level),
				duration = this.GetDuration(level)
			};
			data.passiveBuff = buffDataConfusingPresence;
			buffDataConfusingPresence.isPermenant = true;
			buffDataConfusingPresence.id = 282;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetSlow(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.01f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetSlow(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetSlow(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetSlow(int level)
		{
			return 0.05f + (float)level * 0.02f;
		}

		public float GetChance(int level)
		{
			return 0.03f + (float)level * 0.01f;
		}

		public float GetDuration(int level)
		{
			return 8f + (float)level * 0f;
		}

		private const float INITIAL_SLOW = 0.05f;

		private const float LEVEL_SLOW = 0.02f;

		private const float INITIAL_CHANCE = 0.03f;

		private const float LEVEL_CHANCE = 0.01f;

		private const float INITIAL_DURATION = 8f;

		private const float LEVEL_DURATION = 0f;
	}
}
