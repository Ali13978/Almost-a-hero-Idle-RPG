using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseParticipationTrophy : SkillPassiveDataBase
	{
		public SkillDataBaseParticipationTrophy()
		{
			this.nameKey = "SKILL_NAME_PARTICIPATION_TROPHY";
			this.descKey = "SKILL_DESC_PARTICIPATION_TROPHY";
			this.requiredHeroLevel = 8;
			this.maxLevel = 10;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataPaticipationThrophy buffDataPaticipationThrophy = new BuffDataPaticipationThrophy((double)this.GetDamageFactor(level));
			buffDataPaticipationThrophy.id = 328;
			buffDataPaticipationThrophy.duration = 3f;
			data.passiveBuff = buffDataPaticipationThrophy;
			buffDataPaticipationThrophy.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(0), false)), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.3f, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageFactor(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(3f)));
		}

		public float GetDamageFactor(int level)
		{
			return 2f + (float)level * 0.3f;
		}

		public const float DAMAGE_FACTOR_INITIAL = 2f;

		public const float DAMAGE_FACTOR_PER_LEVEL = 0.3f;

		public const float DAMAGE_DURATION = 3f;
	}
}
