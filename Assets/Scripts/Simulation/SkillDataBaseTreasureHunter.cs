using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTreasureHunter : SkillPassiveDataBase
	{
		public SkillDataBaseTreasureHunter()
		{
			this.nameKey = "SKILL_NAME_TREASURE_HUNTER";
			this.descKey = "SKILL_DESC_TREASURE_HUNTER";
			this.requiredHeroLevel = 3;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataGoldChest buffDataGoldChest = new BuffDataGoldChest();
			buffDataGoldChest.id = 92;
			data.passiveBuff = buffDataGoldChest;
			buffDataGoldChest.isPermenant = true;
			buffDataGoldChest.isStackable = true;
			buffDataGoldChest.goldFactorAdd = this.GetGoldFactor(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetGoldFactor(data.level), false)));
		}

		public double GetGoldFactor(int level)
		{
			return 0.4 + (double)level * 0.05;
		}

		private const double INITIAL_GOLD = 0.4;

		private const double LEVEL_GOLD = 0.05;
	}
}
