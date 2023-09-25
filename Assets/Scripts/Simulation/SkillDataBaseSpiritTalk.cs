using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSpiritTalk : SkillPassiveDataBase
	{
		public SkillDataBaseSpiritTalk()
		{
			this.nameKey = "SKILL_NAME_SPIRIT_TALK";
			this.descKey = "SKILL_DESC_SPIRIT_TALK";
			this.requiredHeroLevel = 3;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDamageTotem buffDataDamageTotem = new BuffDataDamageTotem();
			buffDataDamageTotem.id = 49;
			data.passiveBuff = buffDataDamageTotem;
			buffDataDamageTotem.isPermenant = true;
			buffDataDamageTotem.damageAdd = this.GetDamageBonus(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)));
		}

		public double GetDamageBonus(int level)
		{
			return 0.18 + 0.02 * (double)level;
		}

		private const double INITIAL_DAMAGE = 0.18;

		private const double LEVEL_DAMAGE = 0.02;
	}
}
