using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseMadGirl : SkillPassiveDataBase
	{
		public SkillDataBaseMadGirl()
		{
			this.nameKey = "SKILL_NAME_MAD_GIRL";
			this.descKey = "SKILL_DESC_MAD_GIRL";
			this.requiredHeroLevel = 6;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataMadGirl buffDataMadGirl = new BuffDataMadGirl();
			buffDataMadGirl.id = 122;
			data.passiveBuff = buffDataMadGirl;
			buffDataMadGirl.isPermenant = true;
			buffDataMadGirl.maxStack = 5;
			buffDataMadGirl.attackSpeedAdd = this.GetAttackSpeed(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(0), false)), AM.csa(5.ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15f, false) + ")"), AM.csa(5.ToString()));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetAttackSpeed(data.level), false)), AM.csa(5.ToString()));
		}

		public float GetAttackSpeed(int level)
		{
			return 0.3f + 0.15f * (float)level;
		}

		private const float INITIAL_ATT_SPEED = 0.3f;

		private const float LEVEL_ATT_SPEED = 0.15f;

		private const int MAX_STACK = 5;
	}
}
