using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseGoForTheEyes : SkillPassiveDataBase
	{
		public SkillDataBaseGoForTheEyes()
		{
			this.nameKey = "SKILL_NAME_GO_FOR_THE_EYES";
			this.descKey = "SKILL_DESC_GO_FOR_THE_EYES";
			this.requiredHeroLevel = 9;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataBlind(this.GetBlindChance(level), this.GetBlindDuration(level), this.GetMissChanceAdd())
			{
				id = 252,
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBlindChance(0), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetBlindDuration(0))), AM.csa(GameMath.GetPercentString(this.GetMissChanceAdd(), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBlindChance(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetBlindDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(1f) + ")"), AM.csa(GameMath.GetPercentString(this.GetMissChanceAdd(), false)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBlindChance(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetBlindDuration(data.level))), AM.csa(GameMath.GetPercentString(this.GetMissChanceAdd(), false)));
		}

		public float GetBlindChance(int level)
		{
			return 0.3f + (float)level * 0f;
		}

		public float GetMissChanceAdd()
		{
			return 0.5f;
		}

		public float GetBlindDuration(int level)
		{
			return 2f + (float)level * 1f;
		}

		public const float BLIND_CHANCE_INIT = 0.3f;

		public const float BLIND_CHANCE_PER_LEVEL = 0f;

		public const float MISS_CHANCE = 0.5f;

		public const float BLIND_DUR_INIT = 2f;

		public const float BLIND_DUR_PER_LEVEL = 1f;
	}
}
