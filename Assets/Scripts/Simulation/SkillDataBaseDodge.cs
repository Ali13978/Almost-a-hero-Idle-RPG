using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseDodge : SkillPassiveDataBase
	{
		public SkillDataBaseDodge()
		{
			this.nameKey = "SKILL_NAME_DODGE";
			this.descKey = "SKILL_DESC_DODGE";
			this.requiredHeroLevel = 3;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDodge buffDataDodge = new BuffDataDodge();
			buffDataDodge.id = 66;
			data.passiveBuff = buffDataDodge;
			buffDataDodge.isPermenant = true;
			buffDataDodge.dodgeChanceAdd = this.GetDodgeChance(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false)) + ")");
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDodgeChance(data.level), false)));
		}

		public float GetDodgeChance(int level)
		{
			return 0.06f + 0.03f * (float)level;
		}

		private const float INITIAL_DODGE = 0.06f;

		private const float UPGRADE_ADD = 0.03f;
	}
}
