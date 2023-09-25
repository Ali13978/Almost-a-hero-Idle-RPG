using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBasePartyTime : SkillPassiveDataBase
	{
		public SkillDataBasePartyTime()
		{
			this.nameKey = "SKILL_NAME_PARTY_TIME";
			this.descKey = "SKILL_DESC_PARTY_TIME";
			this.requiredHeroLevel = 8;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataPartyTime buffDataPartyTime = new BuffDataPartyTime();
			buffDataPartyTime.id = 140;
			data.passiveBuff = buffDataPartyTime;
			buffDataPartyTime.isPermenant = true;
			buffDataPartyTime.effect = new BuffDataDamageAdd();
			buffDataPartyTime.effect.id = 34;
			buffDataPartyTime.effect.isPermenant = false;
			buffDataPartyTime.effect.visuals |= 8;
			buffDataPartyTime.effect.isStackable = true;
			buffDataPartyTime.effect.dur = SkillDataBasePartyTime.DURATION;
			buffDataPartyTime.effect.damageAdd = this.GetDamageAdd(level);
		}

		private double GetDamageAdd(int level)
		{
			return SkillDataBasePartyTime.DAMAGE_ADD_BASE + (double)level * SkillDataBasePartyTime.DAMAGE_ADD_LEVEL;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(0), false)), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBasePartyTime.DURATION)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBasePartyTime.DAMAGE_ADD_LEVEL, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBasePartyTime.DURATION)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(SkillDataBasePartyTime.DURATION)));
		}

		private static float DURATION = 12f;

		private static double DAMAGE_ADD_BASE = 0.25;

		private static double DAMAGE_ADD_LEVEL = 0.05;
	}
}
