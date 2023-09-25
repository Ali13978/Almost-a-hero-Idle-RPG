using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBasePoisonedDaggers : SkillPassiveDataBase
	{
		public SkillDataBasePoisonedDaggers()
		{
			this.nameKey = "SKILL_NAME_POISONED_DAGGERS";
			this.descKey = "SKILL_DESC_POISONED_DAGGERS";
			this.requiredHeroLevel = 3;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataPoisonedDaggers buffDataPoisonedDaggers = new BuffDataPoisonedDaggers();
			buffDataPoisonedDaggers.id = 141;
			data.passiveBuff = buffDataPoisonedDaggers;
			buffDataPoisonedDaggers.isPermenant = true;
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 40;
			buffDataPoisonedDaggers.effect = buffDataDamageAdd;
			buffDataDamageAdd.dur = 5f;
			buffDataDamageAdd.isStackable = false;
			buffDataDamageAdd.damageAdd = -this.GetDamageReduction(level);
			buffDataDamageAdd.visuals |= 16;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(0), false)), AM.csa(GameMath.GetTimeInSecondsString(5f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(5f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageReduction(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(5f)));
		}

		public double GetDamageReduction(int level)
		{
			return 0.15 + 0.05 * (double)level;
		}

		private const double INITIAL_DAMAGE = 0.15;

		private const double LEVEL_DAMAGE = 0.05;

		private const float DURATION = 5f;
	}
}
