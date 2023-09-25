using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseFriendship : SkillPassiveDataBase
	{
		public SkillDataBaseFriendship()
		{
			this.nameKey = "SKILL_NAME_FRIENDSHIP";
			this.descKey = "SKILL_DESC_FRIENDSHIP";
			this.requiredHeroLevel = 8;
			this.maxLevel = 14;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataFriendship buffDataFriendship = new BuffDataFriendship();
			buffDataFriendship.id = 88;
			data.passiveBuff = buffDataFriendship;
			buffDataFriendship.isPermenant = true;
			buffDataFriendship.damageAdd = (double)AM.LinearEquationFloat((float)level, 0.03f, 0.08f);
		}

		public float GetDamage(int level)
		{
			return 0.08f + 0.03f * (float)level;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)));
		}

		private const float INITIAL_BONUS = 0.08f;

		private const float LEVEL_BONUS = 0.03f;
	}
}
