using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseMark : SkillPassiveDataBase
	{
		public SkillDataBaseMark()
		{
			this.nameKey = "SKILL_NAME_MARK";
			this.descKey = "SKILL_DESC_MARK";
			this.requiredHeroLevel = 4;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataMark buffDataMark = new BuffDataMark(this.GetChance(level), this.GetDamageBonus(level) + 1.0);
			buffDataMark.id = 125;
			data.passiveBuff = buffDataMark;
			buffDataMark.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(0), false)), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseMark.CHANCE_LEVEL, false) + ")"), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.2, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamageBonus(data.level), false)));
		}

		public float GetChance(int level)
		{
			return SkillDataBaseMark.CHANCE_BASE + (float)level * SkillDataBaseMark.CHANCE_LEVEL;
		}

		public double GetDamageBonus(int level)
		{
			return 0.9 + (double)level * 0.2;
		}

		public static float CHANCE_BASE = 0.14f;

		public static float CHANCE_LEVEL = 0.02f;

		private const double DMG_BASE = 0.9;

		private const double DMG_LEVEL = 0.2;
	}
}
