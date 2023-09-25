using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSurvivalist : SkillPassiveDataBase
	{
		public SkillDataBaseSurvivalist()
		{
			this.nameKey = "SKILL_NAME_SURVIVALIST";
			this.descKey = "SKILL_DESC_SURVIVALIST";
			this.requiredHeroLevel = 7;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataSurvivalist buffDataSurvivalist = new BuffDataSurvivalist();
			buffDataSurvivalist.id = 178;
			data.passiveBuff = buffDataSurvivalist;
			buffDataSurvivalist.isPermenant = true;
			buffDataSurvivalist.missChance = this.GetMissChance(level);
			buffDataSurvivalist.damageReduction = this.GetDamageReduction(level);
			buffDataSurvivalist.addedBuffDur = SkillDataBaseSurvivalist.MISS_BUFF_DURATION;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(0), false)), AM.csa(GameMath.GetPercentString(this.GetMissChance(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseSurvivalist.MISS_BUFF_DURATION)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseSurvivalist.DAMAGE_REDUCTION_LEVEL, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseSurvivalist.MISS_CHANCE_LEVEL, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseSurvivalist.MISS_BUFF_DURATION)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(SkillDataBaseSurvivalist.MISS_BUFF_DURATION)));
		}

		private double GetDamageReduction(int level)
		{
			return SkillDataBaseSurvivalist.DAMAGE_REDUCTION_INIT + (double)level * SkillDataBaseSurvivalist.DAMAGE_REDUCTION_LEVEL;
		}

		public float GetMissChance(int level)
		{
			return SkillDataBaseSurvivalist.MISS_CHANCE_INIT + (float)level * SkillDataBaseSurvivalist.MISS_CHANCE_LEVEL;
		}

		public static float MISS_CHANCE_INIT = 0.01f;

		public static float MISS_CHANCE_LEVEL = 0.01f;

		public static double DAMAGE_REDUCTION_INIT = 0.15;

		public static double DAMAGE_REDUCTION_LEVEL = 0.05;

		public static float MISS_BUFF_DURATION = 4f;
	}
}
