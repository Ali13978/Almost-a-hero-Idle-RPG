using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBlock : SkillPassiveDataBase
	{
		public SkillDataBaseBlock()
		{
			this.nameKey = "SKILL_NAME_BLOCK";
			this.descKey = "SKILL_DESC_BLOCK";
			this.requiredHeroLevel = 3;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataBlock buffDataBlock = new BuffDataBlock();
			buffDataBlock.id = 19;
			data.passiveBuff = buffDataBlock;
			buffDataBlock.isPermenant = true;
			buffDataBlock.chance = this.GetChance(level);
			buffDataBlock.damageBlockFactor = this.GetBlockFactor(level);
		}

		private double GetBlockFactor(int level)
		{
			return 0.3 + (double)level * 0.03;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetBlockFactor(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetBlockFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetBlockFactor(data.level), false)));
		}

		public float GetChance(int level)
		{
			return 0.6f + 0.02f * (float)level;
		}

		private const double BLOCK_FACTOR_INIT = 0.3;

		private const double BLOCK_FACTOR_LEVEL = 0.03;

		private const float INIT_BLOCK_CHANCE = 0.6f;

		private const float LEVEL_BLOCK_CHANCE = 0.02f;
	}
}
