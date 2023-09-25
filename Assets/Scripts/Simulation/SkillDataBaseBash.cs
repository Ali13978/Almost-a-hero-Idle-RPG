using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBash : SkillPassiveDataBase
	{
		public SkillDataBaseBash()
		{
			this.nameKey = "SKILL_NAME_BASH";
			this.descKey = "SKILL_DESC_BASH";
			this.requiredHeroLevel = 3;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataBash buffDataBash = new BuffDataBash();
			buffDataBash.id = 13;
			data.passiveBuff = buffDataBash;
			buffDataBash.isPermenant = true;
			buffDataBash.chance = this.GetStun(level);
			buffDataBash.damageFactor = 1.0 + this.GetDamage(level);
			BuffDataStun buffDataStun = new BuffDataStun();
			buffDataStun.id = 173;
			buffDataBash.effect = buffDataStun;
			buffDataStun.dur = 3f;
			buffDataStun.visuals |= 512;
			buffDataBash.effect = buffDataStun;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStun(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStun(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.01f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetStun(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		private double GetDamage(int level)
		{
			return 0.75 + (double)level * 0.25;
		}

		public float GetStun(int level)
		{
			return 0.06f + 0.01f * (float)level;
		}

		private const float INITIAL_STUN = 0.06f;

		private const float LEVEL_STUN = 0.01f;

		private const float DURATION = 3f;

		private const double INIT_DAMAGE_BONUS = 0.75;

		private const double LEVEL_DAMAGE_BONUS = 0.25;
	}
}
