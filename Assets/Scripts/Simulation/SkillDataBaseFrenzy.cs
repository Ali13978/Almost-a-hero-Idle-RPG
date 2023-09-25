using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFrenzy : SkillPassiveDataBase
	{
		public SkillDataBaseFrenzy()
		{
			this.nameKey = "SKILL_NAME_FRENZY";
			this.descKey = "SKILL_DESC_FRENZY";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFrenzy buffDataFrenzy = new BuffDataFrenzy(this.GetAttackSpeedAdd(level), this.GetDuration(level));
			buffDataFrenzy.id = 87;
			data.passiveBuff = buffDataFrenzy;
			buffDataFrenzy.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedAdd(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBaseFrenzy.DUR_LEVEL) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_CROW_ATTACK")), AM.csa(GameMath.GetPercentString(this.GetAttackSpeedAdd(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return SkillDataBaseFrenzy.DUR_BASE + (float)level * SkillDataBaseFrenzy.DUR_LEVEL;
		}

		public float GetAttackSpeedAdd(int level)
		{
			return 0.5f + (float)level * 0.25f;
		}

		private const float ATTACK_SPEED_BASE = 0.5f;

		private const float ATTACK_SPEED_LEVEL = 0.25f;

		public static float DUR_BASE = 10f;

		public static float DUR_LEVEL = 1f;
	}
}
