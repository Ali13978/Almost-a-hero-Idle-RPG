using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseExplosiveShots : SkillPassiveDataBase
	{
		public SkillDataBaseExplosiveShots()
		{
			this.nameKey = "SKILL_NAME_EXPLOSIVE_SHOTS";
			this.descKey = "SKILL_DESC_EXPLOSIVE_SHOTS";
			this.requiredHeroLevel = 11;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDamageArea buffDataDamageArea = new BuffDataDamageArea();
			buffDataDamageArea.id = 42;
			data.passiveBuff = buffDataDamageArea;
			buffDataDamageArea.isPermenant = true;
			buffDataDamageArea.isStackable = true;
			buffDataDamageArea.damageAdd = this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			double damage = this.GetDamage(0);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)));
		}

		public override string GetDesc(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)) + AM.csl(" (+" + GameMath.GetPercentString(0.25, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			double damage = this.GetDamage(data.level);
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(damage, false)));
		}

		public double GetDamage(int level)
		{
			return 0.25 + 0.25 * (double)level;
		}

		private const double INIT_DAMAGE = 0.25;

		private const double LEVEL_DAMAGE = 0.25;
	}
}
