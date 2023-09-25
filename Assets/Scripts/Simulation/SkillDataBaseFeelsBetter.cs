using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFeelsBetter : SkillPassiveDataBase
	{
		public SkillDataBaseFeelsBetter()
		{
			this.nameKey = "SKILL_NAME_FEELSBETTER";
			this.descKey = "SKILL_DESC_FEELSBETTER";
			this.requiredHeroLevel = 8;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFeelsBetter buffDataFeelsBetter = new BuffDataFeelsBetter
			{
				shieldPercentage = (double)this.GetShieldPrecentage(data.level)
			};
			buffDataFeelsBetter.id = 275;
			buffDataFeelsBetter.isPermenant = true;
			data.passiveBuff = buffDataFeelsBetter;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.15f, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShieldPrecentage(data.level), false) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")")));
		}

		private float GetShieldPrecentage(int level)
		{
			return 0.15f + (float)level * 0.05f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetShieldPrecentage(data.level), false)));
		}

		public const float SHIELD_GAIN_PERCENTAGE_BASE = 0.15f;

		public const float SHIELD_GAIN_PERCENTAGE_PER_LEVEL = 0.05f;
	}
}
