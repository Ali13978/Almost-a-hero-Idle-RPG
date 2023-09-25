using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBasePreparation : SkillPassiveDataBase
	{
		public SkillDataBasePreparation()
		{
			this.nameKey = "SKILL_NAME_PREPERATION";
			this.descKey = "SKILL_DESC_PREPERATION";
			this.requiredHeroLevel = 7;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataPreparation buffDataPreparation = new BuffDataPreparation(this.GetDuration(level), 1.0 - this.GetDamageTakenFactor(level));
			buffDataPreparation.id = 142;
			data.passiveBuff = buffDataPreparation;
			buffDataPreparation.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))), AM.csa(GameMath.GetPercentString(this.GetDamageTakenFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(SkillDataBasePreparation.DUR_LEVEL) + ")"), AM.csa(GameMath.GetPercentString(this.GetDamageTakenFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))), AM.csa(GameMath.GetPercentString(this.GetDamageTakenFactor(data.level), false)));
		}

		public float GetDuration(int level)
		{
			return SkillDataBasePreparation.DUR_BASE + (float)level * SkillDataBasePreparation.DUR_LEVEL;
		}

		public double GetDamageTakenFactor(int level)
		{
			return 0.2 + (double)level * 0.05;
		}

		public static float DUR_BASE = 6f;

		public static float DUR_LEVEL = 1f;

		private const double DMG_FACTOR_BASE = 0.2;

		private const double DMG_FACTOR_LEVEL = 0.05;
	}
}
