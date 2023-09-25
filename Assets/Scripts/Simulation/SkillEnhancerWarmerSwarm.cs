using System;

namespace Simulation
{
	public class SkillEnhancerWarmerSwarm : SkillEnhancerBase
	{
		public SkillEnhancerWarmerSwarm()
		{
			this.nameKey = "SKILL_NAME_WARMER_SWARM";
			this.descKey = "SKILL_DESC_WARMER_SWARM";
			this.requiredHeroLevel = 10;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(GameMath.GetPercentString(this.GetRed(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(GameMath.GetPercentString(this.GetRed(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.06f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DEMONIC_SWARM")), AM.csa(GameMath.GetPercentString(this.GetRed(data.level), false)));
		}

		public float GetRed(int level)
		{
			return 0.24f + 0.06f * (float)level;
		}

		public const float INITIAL_REDUCTION = 0.24f;

		public const float LEVEL_REDUCTION = 0.06f;
	}
}
