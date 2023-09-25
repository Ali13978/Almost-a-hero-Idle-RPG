using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseExpertLoveLore : SkillPassiveDataBase
	{
		public SkillDataBaseExpertLoveLore()
		{
			this.nameKey = "SKILL_NAME_EXPERT_LOVE_LORE";
			this.descKey = "SKILL_DESC_EXPERT_LOVE_LORE";
			this.requiredHeroLevel = 12;
			this.maxLevel = 15;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			data.passiveBuff = new BuffDataHealthMaxSelfAndAllies
			{
				id = 334,
				healthMaxAddSelf = (double)(-(double)this.GetSelfHealthMax(level)),
				healthMaxAddAllies = (double)this.GetAlliesHealthMax(level),
				isPermenant = true
			};
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetSelfHealthMax(0), false)), AM.csa(GameMath.GetPercentString(this.GetAlliesHealthMax(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetSelfHealthMax(data.level), false)) + AM.csl(" (-" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetAlliesHealthMax(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetSelfHealthMax(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetAlliesHealthMax(data.level), false)));
		}

		public float GetSelfHealthMax(int level)
		{
			return 0.5f - (float)level * 0.02f;
		}

		public float GetAlliesHealthMax(int level)
		{
			return 0.3f + (float)level * 0.02f;
		}

		public const float HEALTH_SELF_REDUCE = 0.5f;

		public const float HEALTH_SELF_REDUCE_PER_LEVEL = 0.02f;

		public const float HEALTH_ALLIES_INIT = 0.3f;

		public const float HEALTH_ALLIES_PER_LEVEL = 0.02f;
	}
}
