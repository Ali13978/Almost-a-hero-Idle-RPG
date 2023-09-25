using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseCollectScraps : SkillPassiveDataBase
	{
		public SkillDataBaseCollectScraps()
		{
			this.nameKey = "SKILL_NAME_COLLECT_SCRAPS";
			this.descKey = "SKILL_DESC_COLLECT_SCRAPS";
			this.requiredHeroLevel = 11;
			this.maxLevel = 7;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataCollectScraps buffDataCollectScraps = new BuffDataCollectScraps();
			buffDataCollectScraps.id = 27;
			data.passiveBuff = buffDataCollectScraps;
			buffDataCollectScraps.isPermenant = true;
			buffDataCollectScraps.costDecPerKill = this.GetCostDec(level);
			buffDataCollectScraps.costDecMax = this.GetCostCap(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDec(0), false)), AM.csa(GameMath.GetPercentString(this.GetCostCap(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDec(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.01f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetCostCap(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetCostDec(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetCostCap(data.level), false)));
		}

		public double GetCostCap(int level)
		{
			return (double)(0.45f + 0.05f * (float)level);
		}

		public double GetCostDec(int level)
		{
			return (double)(0.03f + 0.01f * (float)level);
		}

		private const float INITIAL_DEC = 0.03f;

		private const float LEVEL_DEC = 0.01f;

		private const float INITIAL_CAP = 0.45f;

		private const float LEVEL_CAP = 0.05f;
	}
}
