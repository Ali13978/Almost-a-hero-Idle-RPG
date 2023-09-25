using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseStubbernness : SkillPassiveDataBase
	{
		public SkillDataBaseStubbernness()
		{
			this.nameKey = "SKILL_NAME_STUBBERNNESS";
			this.descKey = "SKILL_DESC_STUBBERNNESS";
			this.requiredHeroLevel = 6;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataReviveDur buffDataReviveDur = new BuffDataReviveDur();
			buffDataReviveDur.id = 153;
			data.passiveBuff = buffDataReviveDur;
			buffDataReviveDur.isPermenant = true;
			buffDataReviveDur.isStackable = true;
			buffDataReviveDur.reviveDurFactorFactor = 1f - this.GetReduction(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)));
		}

		public float GetReduction(int level)
		{
			return 0.2f + (float)level * 0.1f;
		}

		private const float INIT_REDUCTION = 0.2f;

		private const float LEVEL_REDUCTION = 0.1f;
	}
}
