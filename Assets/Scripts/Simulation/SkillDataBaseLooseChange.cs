using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseLooseChange : SkillPassiveDataBase
	{
		public SkillDataBaseLooseChange()
		{
			this.nameKey = "SKILL_NAME_LOOSE_CHANGE";
			this.descKey = "SKILL_DESC_LOOSE_CHANGE";
			this.requiredHeroLevel = 14;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataDropGoldOnDeath buffDataDropGoldOnDeath = new BuffDataDropGoldOnDeath
			{
				dropGoldFactorAdd = this.GetGold(level),
				chance = this.GetChance(level)
			};
			data.passiveBuff = buffDataDropGoldOnDeath;
			buffDataDropGoldOnDeath.isPermenant = true;
			buffDataDropGoldOnDeath.id = 285;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetGold(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")")), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false) + AM.csl(" (+" + GameMath.GetPercentString(0.4, false) + ")")));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetGold(data.level), false)));
		}

		public double GetGold(int level)
		{
			return 1.4 + (double)level * 0.4;
		}

		public float GetChance(int level)
		{
			return 0.4f + (float)level * 0.05f;
		}

		public const float INITIAL_GOLD_CHANCE = 0.4f;

		public const float LEVEL_GOLD_CHANCE = 0.05f;

		private const double INITIAL_GOLD = 1.4;

		private const double LEVEL_GOLD = 0.4;

		public const int LEVEL_MAX = 9;
	}
}
