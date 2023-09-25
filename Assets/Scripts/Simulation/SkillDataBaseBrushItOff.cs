using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBrushItOff : SkillPassiveDataBase
	{
		public SkillDataBaseBrushItOff()
		{
			this.nameKey = "SKILL_NAME_BRUSH_IT_OFF";
			this.descKey = "SKILL_DESC_BRUSH_IT_OFF";
			this.requiredHeroLevel = 4;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataHealOnDamageTaken
			{
				id = 330,
				attacksAmountTrigger = this.GetEveryAttackCount(level),
				healRatio = (double)this.GetHealRatio(level),
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetEveryAttackCount(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetEveryAttackCount(data.level).ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetEveryAttackCount(data.level).ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)));
		}

		public int GetEveryAttackCount(int level)
		{
			return 4 - level * 0;
		}

		public float GetHealRatio(int level)
		{
			return 0.06f + 0.03f * (float)level;
		}

		public const int HEAL_EVERY_ATTACKS_INIT = 4;

		public const int HEAL_EVERY_ATTACKS_PER_LEVEL = 0;

		public const float HEAL_RATIO_INIT = 0.06f;

		public const float HEAL_RATIO_PER_LEVEL = 0.03f;
	}
}
