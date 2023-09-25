using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFuelThemUp : SkillPassiveDataBase
	{
		public SkillDataBaseFuelThemUp()
		{
			this.nameKey = "SKILL_NAME_FUEL_THEM_UP";
			this.descKey = "SKILL_DESC_FUEL_THEM_UP";
			this.requiredHeroLevel = 11;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFuelThemUp buffDataFuelThemUp = new BuffDataFuelThemUp();
			buffDataFuelThemUp.id = 89;
			data.passiveBuff = buffDataFuelThemUp;
			buffDataFuelThemUp.isPermenant = true;
			buffDataFuelThemUp.isStackable = true;
			buffDataFuelThemUp.allyCooldownDecrease = this.GetDuration(level);
			buffDataFuelThemUp.selfCooldownDecrease = this.GetDuration(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(2f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))));
		}

		public float GetDuration(int level)
		{
			return 12f + (float)level * 2f;
		}

		private const float INITIAL_DUR = 12f;

		private const float LEVEL_DUR = 2f;
	}
}
