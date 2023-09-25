using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseSharpEdge : SkillPassiveDataBase
	{
		public SkillDataBaseSharpEdge()
		{
			this.nameKey = "SKILL_NAME_SHARP_EDGE";
			this.descKey = "SKILL_DESC_SHARP_EDGE";
			this.requiredHeroLevel = 11;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataSharpEdge buffDataSharpEdge = new BuffDataSharpEdge();
			buffDataSharpEdge.id = 157;
			data.passiveBuff = buffDataSharpEdge;
			buffDataSharpEdge.isPermenant = true;
			buffDataSharpEdge.effect = new BuffDataDefense();
			buffDataSharpEdge.effect.id = 54;
			buffDataSharpEdge.effect.damageTakenFactor = 1.0 + (double)this.GetReduction(level);
			buffDataSharpEdge.effect.visuals |= 32;
			buffDataSharpEdge.effect.isStackable = false;
			buffDataSharpEdge.effect.dur = 10f;
		}

		public float GetReduction(int level)
		{
			return 0.14f + 0.04f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(0), false)), AM.csa(GameMath.GetTimeInSecondsString(10f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.04f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(10f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetReduction(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(10f)));
		}

		private const float INITIAL_RED = 0.14f;

		private const float LEVEL_RED = 0.04f;

		private const float DURATION = 10f;
	}
}
