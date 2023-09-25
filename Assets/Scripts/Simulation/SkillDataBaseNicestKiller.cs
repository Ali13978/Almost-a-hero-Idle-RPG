using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseNicestKiller : SkillPassiveDataBase
	{
		public SkillDataBaseNicestKiller()
		{
			this.nameKey = "SKILL_NAME_NICEST_KILLER";
			this.descKey = "SKILL_DESC_NICEST_KILLER";
			this.requiredHeroLevel = 11;
			this.maxLevel = 14;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataNicestKiller buffDataNicestKiller = new BuffDataNicestKiller();
			buffDataNicestKiller.numCritsRequired = 7;
			buffDataNicestKiller.id = 134;
			data.passiveBuff = buffDataNicestKiller;
			buffDataNicestKiller.isPermenant = true;
			buffDataNicestKiller.effectDuration = 14f;
			buffDataNicestKiller.effectDamageAdd = this.GetDamageBonus(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(7.ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(0), false)), AM.csa(GameMath.GetTimeInSecondsString(14f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(7.ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(14f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(7.ToString()), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(14f)));
		}

		public double GetDamageBonus(int level)
		{
			return 0.08 + (double)level * 0.03;
		}

		private const float DURATION = 14f;

		private const double INITIAL_DAMAGE_BONUS = 0.08;

		private const double LEVEL_DAMAGE_BONUS = 0.03;

		private const int NUM_CRITS_REQUIRED = 7;
	}
}
