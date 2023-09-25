using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseToughLove : SkillPassiveDataBase
	{
		public SkillDataBaseToughLove()
		{
			this.nameKey = "SKILL_NAME_TOUGH_LOVE";
			this.descKey = "SKILL_DESC_TOUGH_LOVE";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataStunWhenHit
			{
				id = 326,
				stunChance = this.GetStunChance(level),
				stunDuration = this.GetStunDuration(level),
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStunChance(0), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetStunDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStunChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(this.GetStunDuration(data.level))));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStunChance(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetStunDuration(data.level))));
		}

		public float GetStunChance(int level)
		{
			return 0.1f + (float)level * 0.02f;
		}

		public float GetStunDuration(int level)
		{
			return 2f;
		}

		public const float STUN_DUR = 2f;

		public const float STUN_CHANCE_INIT = 0.1f;

		public const float STUN_CHANCE_PER_LEVEL = 0.02f;
	}
}
