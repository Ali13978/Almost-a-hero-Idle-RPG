using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRepel : SkillPassiveDataBase
	{
		public SkillDataBaseRepel()
		{
			this.nameKey = "SKILL_NAME_REPEL";
			this.descKey = "SKILL_DESC_REPEL";
			this.requiredHeroLevel = 5;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataRepel buffDataRepel = new BuffDataRepel();
			buffDataRepel.id = 149;
			data.passiveBuff = buffDataRepel;
			buffDataRepel.isPermenant = true;
			buffDataRepel.damageBuff = new BuffDataDamageMul();
			buffDataRepel.damageBuff.id = 355;
			buffDataRepel.damageBuff.dur = 7f;
			buffDataRepel.damageBuff.damageMul = 1.0 + this.GetDamage(level);
			buffDataRepel.damageBuff.isStackable = true;
			buffDataRepel.damageBuff.visuals |= 8;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(7f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.08, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(7f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(7f)));
		}

		public double GetDamage(int level)
		{
			return 0.28 + (double)level * 0.08;
		}

		private const double INIT_DAMAGE = 0.28;

		private const double LEVEL_DAMAGE = 0.08;

		private const float DURATION = 7f;
	}
}
