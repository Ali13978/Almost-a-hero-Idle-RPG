using System;

namespace Simulation
{
	public class SkillEnhancerBaseDistraction : SkillEnhancerBase
	{
		public SkillEnhancerBaseDistraction()
		{
			this.nameKey = "SKILL_NAME_DISTRACTION";
			this.descKey = "SKILL_DESC_DISTRACTION";
			this.requiredHeroLevel = 10;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_GREED_GRENADE")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDistraction.GetDamageFactor(0), false)), AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerBaseDistraction.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_GREED_GRENADE")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDistraction.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillEnhancerBaseDistraction.LEVEL_DAMAGE_FACTOR, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerBaseDistraction.GetDuration(data.level))));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_GREED_GRENADE")), AM.csa(GameMath.GetPercentString(SkillEnhancerBaseDistraction.GetDamageFactor(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerBaseDistraction.GetDuration(data.level))));
		}

		public static float GetDuration(int level)
		{
			return SkillEnhancerBaseDistraction.INITIAL_DURATION + SkillEnhancerBaseDistraction.LEVEL_DURATION * (float)level;
		}

		public static float GetDamageFactor(int level)
		{
			return SkillEnhancerBaseDistraction.INITIAL_DAMAGE_FACTOR + SkillEnhancerBaseDistraction.LEVEL_DAMAGE_FACTOR * (float)level;
		}

		public static float INITIAL_DAMAGE_FACTOR = 0.1f;

		public static float LEVEL_DAMAGE_FACTOR = 0.1f;

		public static float INITIAL_DURATION = 10f;

		public static float LEVEL_DURATION;
	}
}
