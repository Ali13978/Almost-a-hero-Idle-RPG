using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseWeakPoint : SkillPassiveDataBase
	{
		public SkillDataBaseWeakPoint()
		{
			this.nameKey = "SKILL_NAME_WEAK_POINT";
			this.descKey = "SKILL_DESC_WEAK_POINT";
			this.requiredHeroLevel = 11;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataWeakPoint buffDataWeakPoint = new BuffDataWeakPoint();
			buffDataWeakPoint.id = 198;
			data.passiveBuff = buffDataWeakPoint;
			buffDataWeakPoint.isPermenant = true;
			buffDataWeakPoint.isStackable = true;
			buffDataWeakPoint.damageFactor = this.GetDamageFactor(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)));
		}

		public double GetDamageFactor(int level)
		{
			return 0.7 + (double)level * 0.15;
		}

		private const double INITIAL_DAMAGE_FACTOR = 0.7;

		private const double LEVEL_DAMAGE_FACTOR = 0.15;
	}
}
