using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseToughness : SkillPassiveDataBase
	{
		public SkillDataBaseToughness()
		{
			this.nameKey = "SKILL_NAME_TOUGHNESS";
			this.descKey = "SKILL_DESC_TOUGHNESS";
			this.requiredHeroLevel = 5;
			this.maxLevel = 14;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataToughness buffDataToughness = new BuffDataToughness();
			buffDataToughness.id = 186;
			data.passiveBuff = buffDataToughness;
			buffDataToughness.isPermenant = true;
			buffDataToughness.healthRatioReqMax = this.GetHealthRatio(level);
			buffDataToughness.damageTakenFactor = 1.0 - this.GetDef(level);
		}

		public double GetDef(int level)
		{
			return 0.32 + (double)level * 0.02;
		}

		public double GetHealthRatio(int level)
		{
			return 0.72 + (double)level * 0.02;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDef(0), false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDef(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDef(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatio(data.level), false)));
		}

		private const double INITIAL_DAMAGE_RED = 0.32;

		private const double LEVEL_DAMAGE_RED = 0.02;

		private const double HEALTH_RATIO = 0.72;

		private const double LEVEL_HEALTH_RATIO = 0.02;
	}
}
