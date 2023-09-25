using System;

namespace Simulation
{
	public class SkillEnhancerRageDriven : SkillEnhancerBase
	{
		public SkillEnhancerRageDriven()
		{
			this.nameKey = "SKILL_NAME_RAGE_DRIVEN";
			this.descKey = "SKILL_DESC_RAGE_DRIVEN";
			this.requiredHeroLevel = 11;
			this.maxLevel = 8;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(3.ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerRageDriven.GetAttackSpeed(0), false)),
				AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerRageDriven.GetBuffDuration(0)))
			});
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(3.ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerRageDriven.GetAttackSpeed(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5f, false) + ")"),
				AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerRageDriven.GetBuffDuration(data.level)))
			});
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(3.ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerRageDriven.GetAttackSpeed(data.level), false)),
				AM.csa(GameMath.GetTimeInSecondsString(SkillEnhancerRageDriven.GetBuffDuration(data.level)))
			});
		}

		public static float GetAttackSpeed(int level)
		{
			return 2f + (float)level * 0.5f;
		}

		public static float GetBuffDuration(int level)
		{
			return 1.5f + (float)level * 0f;
		}

		public const float ATTACK_SPEED_INIT = 2f;

		public const float ATTACK_SPEED_PER_LEVEL = 0.5f;

		public const float BUFF_DURATION_INIT = 1.5f;

		public const float BUFF_DURATION_PER_LEVEL = 0f;

		public const int ATTACKS_COUNT = 3;
	}
}
