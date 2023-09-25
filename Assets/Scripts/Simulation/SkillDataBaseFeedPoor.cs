using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFeedPoor : SkillPassiveDataBase
	{
		public SkillDataBaseFeedPoor()
		{
			this.nameKey = "SKILL_NAME_FEED_THE_POOR";
			this.descKey = "SKILL_DESC_FEED_THE_POOR";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataHealOnConsHit buffDataHealOnConsHit = new BuffDataHealOnConsHit();
			buffDataHealOnConsHit.id = 81;
			buffDataHealOnConsHit.consCount = SkillDataBaseFeedPoor.CONS_COUNT;
			data.passiveBuff = buffDataHealOnConsHit;
			buffDataHealOnConsHit.isPermenant = true;
			buffDataHealOnConsHit.healRatio = this.GetHeal(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetCons(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetHeal(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetCons(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetHeal(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseFeedPoor.HEAL_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetCons(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetHeal(data.level), false)));
		}

		public double GetHeal(int level)
		{
			return SkillDataBaseFeedPoor.HEAL_INIT + SkillDataBaseFeedPoor.HEAL_LEVEL * (double)level;
		}

		public int GetCons(int level)
		{
			return SkillDataBaseFeedPoor.CONS_COUNT;
		}

		public static double HEAL_INIT = 0.05;

		public static double HEAL_LEVEL = 0.01;

		public static int CONS_COUNT = 5;
	}
}
