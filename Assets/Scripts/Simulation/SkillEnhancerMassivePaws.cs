using System;

namespace Simulation
{
	public class SkillEnhancerMassivePaws : SkillEnhancerBase
	{
		public SkillEnhancerMassivePaws()
		{
			this.nameKey = "SKILL_NAME_MASSIVE_PAWS";
			this.descKey = "SKILL_DESC_MASSIVE_PAWS";
			this.requiredHeroLevel = 7;
			this.maxLevel = 6;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetFirstProbability(0), false)),
				AM.csa(SkillEnhancerMassivePaws.GetFirstEnemiesDamagedCount().ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetSecondProbability(0), false)),
				AM.csa(SkillEnhancerMassivePaws.GetSecondEnemiesDamagedCount().ToString())
			});
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetFirstProbability(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"),
				AM.csa(SkillEnhancerMassivePaws.GetFirstEnemiesDamagedCount().ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetSecondProbability(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"),
				AM.csa(SkillEnhancerMassivePaws.GetSecondEnemiesDamagedCount().ToString())
			});
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), new object[]
			{
				AM.csa(LM.Get("SKILL_NAME_BEAST_MODE")),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetFirstProbability(data.level), false)),
				AM.csa(SkillEnhancerMassivePaws.GetFirstEnemiesDamagedCount().ToString()),
				AM.csa(GameMath.GetPercentString(SkillEnhancerMassivePaws.GetSecondProbability(data.level), false)),
				AM.csa(SkillEnhancerMassivePaws.GetSecondEnemiesDamagedCount().ToString())
			});
		}

		public static float GetFirstProbability(int level)
		{
			return 0.2f + 0.05f * (float)level;
		}

		public static int GetFirstEnemiesDamagedCount()
		{
			return 2;
		}

		public static float GetSecondProbability(int level)
		{
			return 0.1f + 0.05f * (float)level;
		}

		public static int GetSecondEnemiesDamagedCount()
		{
			return 3;
		}

		public const float FIRST_PROBABILITY_INIT = 0.2f;

		public const float FIRST_PROBABILITY_PER_LEVEL = 0.05f;

		public const int FIRST_ENEMIES_COUNT = 2;

		public const float SECOND_PROBABILITY_INIT = 0.1f;

		public const float SECOND_PROBABILITY_PER_LEVEL = 0.05f;

		public const int SECOND_ENEMIES_COUNT = 3;
	}
}
