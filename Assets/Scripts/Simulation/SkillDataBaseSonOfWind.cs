using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSonOfWind : SkillPassiveDataBase
	{
		public SkillDataBaseSonOfWind()
		{
			this.nameKey = "SKILL_NAME_SON_OF_THE_WIND";
			this.descKey = "SKILL_DESC_SON_OF_THE_WIND";
			this.requiredHeroLevel = 5;
			this.maxLevel = 7;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 9;
			data.passiveBuff = buffDataAttackSpeed;
			buffDataAttackSpeed.isPermenant = true;
			buffDataAttackSpeed.attackSpeedAdd = this.GetBonus(level);
		}

		public float GetBonus(int level)
		{
			return 0.2f + (float)level * 0.1f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetBonus(data.level), false)));
		}

		private const float INITIAL_BONUS = 0.2f;

		private const float LEVEL_BONUS = 0.1f;
	}
}
