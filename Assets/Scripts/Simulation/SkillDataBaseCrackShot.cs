using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCrackShot : SkillPassiveDataBase
	{
		public SkillDataBaseCrackShot()
		{
			this.nameKey = "SKILL_NAME_CRACK_SHOT";
			this.descKey = "SKILL_DESC_CRACK_SHOT";
			this.requiredHeroLevel = 9;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCrackShot buffDataCrackShot = new BuffDataCrackShot();
			buffDataCrackShot.id = 30;
			data.passiveBuff = buffDataCrackShot;
			buffDataCrackShot.isPermenant = true;
			buffDataCrackShot.isStackable = true;
			buffDataCrackShot.damageFactor = 1.0 + this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)));
		}

		public override string GetDesc(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.4, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)));
		}

		public double GetDamage(int level)
		{
			return 0.8 + (double)level * 0.4;
		}

		private const double INIT_DAMAGE = 0.8;

		private const double LEVEL_DAMAGE = 0.4;
	}
}
