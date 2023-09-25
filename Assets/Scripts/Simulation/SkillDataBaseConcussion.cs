using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseConcussion : SkillPassiveDataBase
	{
		public SkillDataBaseConcussion()
		{
			this.nameKey = "SKILL_NAME_CONCUSSION";
			this.descKey = "SKILL_DESC_CONCUSSION";
			this.requiredHeroLevel = 3;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataConcussion buffDataConcussion = new BuffDataConcussion();
			buffDataConcussion.id = 28;
			data.passiveBuff = buffDataConcussion;
			buffDataConcussion.isPermenant = true;
			buffDataConcussion.isStackable = true;
			buffDataConcussion.critChance = this.GetChance(level);
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 174;
			buffDataConcussion.effect = buffDataStun;
			buffDataConcussion.effect.visuals |= 512;
			buffDataStun.dur = this.GetDuration(level);
		}

		public override string GetDescZero()
		{
			float chance = this.GetChance(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(chance, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			float chance = this.GetChance(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(chance, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public override string GetDescFull(SkillData data)
		{
			float chance = this.GetChance(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(chance, false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetChance(int level)
		{
			return 0.02f + 0.02f * (float)level;
		}

		public float GetDuration(int level)
		{
			return 0.6f;
		}

		private const float INITIAL_CHANCE = 0.02f;

		private const float LEVEL_CHANCE = 0.02f;

		private const float INIT_DURATION = 0.6f;
	}
}
