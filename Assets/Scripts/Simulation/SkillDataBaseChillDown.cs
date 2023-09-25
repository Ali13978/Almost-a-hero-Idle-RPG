using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseChillDown : SkillPassiveDataBase
	{
		public SkillDataBaseChillDown()
		{
			this.nameKey = "SKILL_NAME_CHILL_DOWN";
			this.descKey = "SKILL_DESC_CHILL_DOWN";
			this.requiredHeroLevel = 8;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataChillDown buffDataChillDown = new BuffDataChillDown();
			buffDataChillDown.id = 25;
			data.passiveBuff = buffDataChillDown;
			buffDataChillDown.isPermenant = true;
			buffDataChillDown.coolRatio = this.GetCoolRatio(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCoolRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCoolRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCoolRatio(data.level), false)));
		}

		public float GetCoolRatio(int level)
		{
			return 0.1f + (float)level * 0.1f;
		}

		private const float INITIAL_COOLING = 0.1f;

		private const float LEVEL_COOLING = 0.1f;
	}
}
