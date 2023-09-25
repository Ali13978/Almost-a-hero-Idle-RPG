using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseWhatDoesNotKillYou : SkillPassiveDataBase
	{
		public SkillDataBaseWhatDoesNotKillYou()
		{
			this.nameKey = "SKILL_NAME_BUT_MAYBE";
			this.descKey = "SKILL_DESC_BUT_MAYBE";
			this.requiredHeroLevel = 6;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> skillEnhancers)
		{
			BuffDataImmunityAfterSelfDestruct buffDataImmunityAfterSelfDestruct = new BuffDataImmunityAfterSelfDestruct(SkillDataBaseWhatDoesNotKillYou.INIT_DUR, this.GetCooldown(level));
			buffDataImmunityAfterSelfDestruct.id = 362;
			data.passiveBuff = buffDataImmunityAfterSelfDestruct;
			buffDataImmunityAfterSelfDestruct.isPermenant = true;
		}

		private float GetCooldown(int level)
		{
			return SkillDataBaseWhatDoesNotKillYou.INIT_COOLDOWN - (float)level * SkillDataBaseWhatDoesNotKillYou.LEVEL_COOLDOWN;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseWhatDoesNotKillYou.INIT_DUR)), AM.csa(LM.Get("SKILL_NAME_KAMIKAZE"))) + AM.GetCooldownText(this.GetCooldown(0), -1f);
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseWhatDoesNotKillYou.INIT_DUR)), AM.csa(LM.Get("SKILL_NAME_KAMIKAZE"))) + AM.GetCooldownText(this.GetCooldown(data.level), SkillDataBaseWhatDoesNotKillYou.LEVEL_COOLDOWN);
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseWhatDoesNotKillYou.INIT_DUR)), AM.csa(LM.Get("SKILL_NAME_KAMIKAZE"))) + AM.GetCooldownText(this.GetCooldown(data.level), -1f);
		}

		public static float INIT_COOLDOWN = 150f;

		public static float LEVEL_COOLDOWN = 10f;

		public static float INIT_DUR = 20f;
	}
}
