using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseRottenApple : SkillPassiveDataBase
	{
		public SkillDataBaseRottenApple()
		{
			this.nameKey = "SKILL_NAME_ROTTEN_APPLE";
			this.descKey = "SKILL_DESC_ROTTEN_APPLE";
			this.requiredHeroLevel = 5;
			this.maxLevel = 11;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataRottenApple buffDataRottenApple = new BuffDataRottenApple();
			buffDataRottenApple.id = 156;
			data.passiveBuff = buffDataRottenApple;
			buffDataRottenApple.isPermenant = true;
			BuffDataDefense buffDataDefense = new BuffDataDefense();
			buffDataDefense.id = 57;
			buffDataRottenApple.effect = buffDataDefense;
			buffDataDefense.dur = float.PositiveInfinity;
			buffDataDefense.isStackable = false;
			buffDataDefense.damageTakenFactor = 1.0 + this.GetDamageAmp(level);
			buffDataDefense.visuals |= 32;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAmp(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAmp(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.04, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAmp(data.level), false)));
		}

		public double GetDamageAmp(int level)
		{
			return 0.06 + (double)level * 0.04;
		}

		private const double INITIAL_AMP = 0.06;

		private const double LEVEL_AMP = 0.04;
	}
}
