using System;

namespace Simulation
{
	public class SkillEnhancerHunterInstinct : SkillEnhancerBase
	{
		public SkillEnhancerHunterInstinct()
		{
			this.nameKey = "SKILL_NAME_HUNTER_INSTINCT";
			this.descKey = "SKILL_DESC_HUNTER_INSTINCT";
			this.requiredHeroLevel = 4;
			this.maxLevel = 10;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritDamage(0), false)), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.015f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritDamage(data.level), false)), AM.csa(GameMath.GetPercentString(SkillEnhancerHunterInstinct.GetCritChance(data.level), false)));
		}

		public static float GetCritDamage(int level)
		{
			return 0.5f + 0.1f * (float)level;
		}

		public static float GetCritChance(int level)
		{
			return 0.1f + 0.015f * (float)level;
		}

		public const float CRIT_DAMAGE_INIT = 0.5f;

		public const float CRIT_DAMAGE_PER_LEVEL = 0.1f;

		public const float CRIT_CHANCE_INIT = 0.1f;

		public const float CRIT_CHANCE_PER_LEVEL = 0.015f;
	}
}
