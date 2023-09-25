using System;

namespace Simulation
{
	public class SkillEnhancerMyPittyTeam : SkillEnhancerBase
	{
		public SkillEnhancerMyPittyTeam()
		{
			this.nameKey = "SKILL_NAME_MY_PITTY_TEAM";
			this.descKey = "SKILL_DESC_MY_PITTY_TEAM";
			this.requiredHeroLevel = 11;
			this.maxLevel = 12;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DARK_RITUAL")), AM.csa(GameMath.GetPercentString(this.GetIncrease(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DARK_RITUAL")), AM.csa(GameMath.GetPercentString(this.GetIncrease(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.3f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_DARK_RITUAL")), AM.csa(GameMath.GetPercentString(this.GetIncrease(data.level), false)));
		}

		public float GetIncrease(int level)
		{
			return 0.4f + 0.3f * (float)level;
		}

		public const float INIITIAL_INCREASE = 0.4f;

		public const float LEVEL_INCREASE = 0.3f;
	}
}
