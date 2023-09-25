using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseHoldYourGround : SkillPassiveDataBase
	{
		public SkillDataBaseHoldYourGround()
		{
			this.nameKey = "SKILL_NAME_ARROGANT";
			this.descKey = "SKILL_DESC_ARROGANT";
			this.requiredHeroLevel = 9;
			this.maxLevel = 7;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTauntWhenHealthy buffDataTauntWhenHealthy = new BuffDataTauntWhenHealthy();
			buffDataTauntWhenHealthy.id = 183;
			data.passiveBuff = buffDataTauntWhenHealthy;
			buffDataTauntWhenHealthy.isPermenant = true;
			buffDataTauntWhenHealthy.minHealthRatio = 0.3;
			buffDataTauntWhenHealthy.tauntAdd = this.GetTaunt(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(0), false)), AM.csa(GameMath.GetPercentString(0.3, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5f, false) + ")"), AM.csa(GameMath.GetPercentString(0.3, false)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(data.level), false)), AM.csa(GameMath.GetPercentString(0.3, false)));
		}

		public float GetTaunt(int level)
		{
			return 0.5f + 0.5f * (float)level;
		}

		private const double HEALTH_RATIO = 0.3;

		private const float INIT_TAUNT = 0.5f;

		private const float LEVEL_TAUNT = 0.5f;
	}
}
