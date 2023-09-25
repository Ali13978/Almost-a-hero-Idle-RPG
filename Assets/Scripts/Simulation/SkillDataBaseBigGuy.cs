using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBigGuy : SkillPassiveDataBase
	{
		public SkillDataBaseBigGuy()
		{
			this.nameKey = "SKILL_NAME_BIG_GUY";
			this.descKey = "SKILL_DESC_BIG_GUY";
			this.requiredHeroLevel = 11;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataBigGuy buffDataBigGuy = new BuffDataBigGuy();
			buffDataBigGuy.id = 14;
			data.passiveBuff = buffDataBigGuy;
			buffDataBigGuy.isPermenant = true;
			buffDataBigGuy.reviveDecPerHit = this.GetReviveTimeRed(level);
		}

		private float GetReviveTimeRed(int level)
		{
			return 0.3f + (float)level * 0.3f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReviveTimeRed(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReviveTimeRed(data.level))) + AM.csl(" +(" + GameMath.GetTimeInMilliSecondsString(0.3f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetReviveTimeRed(data.level))));
		}

		private const float INITIAL_REVIVE_TIME_RED = 0.3f;

		private const float LEVEL_REVIVE_TIME_RED = 0.3f;
	}
}
