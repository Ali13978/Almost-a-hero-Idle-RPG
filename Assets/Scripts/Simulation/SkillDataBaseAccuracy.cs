using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseAccuracy : SkillPassiveDataBase
	{
		public SkillDataBaseAccuracy()
		{
			this.nameKey = "SKILL_NAME_ACCURACY";
			this.descKey = "SKILL_DESC_ACCURACY";
			this.requiredHeroLevel = 3;
			this.maxLevel = 14;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataAccuracy buffDataAccuracy = new BuffDataAccuracy();
			buffDataAccuracy.id = 0;
			data.passiveBuff = buffDataAccuracy;
			buffDataAccuracy.isPermenant = true;
			buffDataAccuracy.damageFactorAdd = this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseAccuracy.DAMAGE_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)));
		}

		public double GetDamage(int level)
		{
			return SkillDataBaseAccuracy.DAMAGE_INIT + (double)level * SkillDataBaseAccuracy.DAMAGE_LEVEL;
		}

		public static double DAMAGE_INIT = 0.4;

		public static double DAMAGE_LEVEL = 0.15;
	}
}
