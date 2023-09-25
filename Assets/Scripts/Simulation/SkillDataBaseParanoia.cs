using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseParanoia : SkillPassiveDataBase
	{
		public SkillDataBaseParanoia()
		{
			this.nameKey = "SKILL_NAME_PARANOIA";
			this.descKey = "SKILL_DESC_PARANOIA";
			this.requiredHeroLevel = 8;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataParanoia buffDataParanoia = new BuffDataParanoia();
			buffDataParanoia.id = 139;
			data.passiveBuff = buffDataParanoia;
			buffDataParanoia.isPermenant = true;
			buffDataParanoia.coolDownDecrease = this.GetCooldownDecrease(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_I_HAVE_THE_POWER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDecrease(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_I_HAVE_THE_POWER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDecrease(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(2.5f)) + ")");
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_I_HAVE_THE_POWER")), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetCooldownDecrease(data.level))));
		}

		public float GetCooldownDecrease(int level)
		{
			return 5f + 2.5f * (float)level;
		}

		private const float INITIAL_REDUCTION = 5f;

		private const float REDUCTION_PER_LEVEL = 2.5f;
	}
}
