using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseHearthSeeker : SkillPassiveDataBase
	{
		public SkillDataBaseHearthSeeker()
		{
			this.nameKey = "SKILL_NAME_HEART_SEEKER";
			this.descKey = "SKILL_DESC_HEART_SEEKER";
			this.requiredHeroLevel = 9;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataHeartSeeker buffDataHeartSeeker = new BuffDataHeartSeeker();
			buffDataHeartSeeker.id = 103;
			data.passiveBuff = buffDataHeartSeeker;
			buffDataHeartSeeker.isPermenant = true;
			buffDataHeartSeeker.healthRatioLimit = this.GetHealthRatioLimit(level);
			buffDataHeartSeeker.critChance = 0.4f;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatioLimit(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatioLimit(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.075, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.4f, false)), AM.csa(GameMath.GetPercentString(this.GetHealthRatioLimit(data.level), false)));
		}

		public double GetHealthRatioLimit(int level)
		{
			return 0.2 + (double)level * 0.075;
		}

		private const double INITIAL_HEALTH_RATIO = 0.2;

		private const double LEVEL_HEALTH_RATIO = 0.075;

		private const float CRIT_CHANCE = 0.4f;
	}
}
