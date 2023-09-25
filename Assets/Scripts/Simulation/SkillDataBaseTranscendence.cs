using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTranscendence : SkillPassiveDataBase
	{
		public SkillDataBaseTranscendence()
		{
			this.nameKey = "SKILL_NAME_TRANSCENDENCE";
			this.descKey = "SKILL_DESC_TRANSCENDENCE";
			this.requiredHeroLevel = 7;
			this.maxLevel = 9;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTranscendence buffDataTranscendence = new BuffDataTranscendence();
			buffDataTranscendence.id = 188;
			data.passiveBuff = buffDataTranscendence;
			buffDataTranscendence.isPermenant = true;
			buffDataTranscendence.numHitReq = 5;
			buffDataTranscendence.healRatio = this.GetHealRatio(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(5.ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(5.ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.03, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(5.ToString()), AM.csa(GameMath.GetPercentString(this.GetHealRatio(data.level), false)));
		}

		public double GetHealRatio(int level)
		{
			return 0.03 + (double)level * 0.03;
		}

		private const int NUM_HIT_REQ = 5;

		private const double INIT_HEAL_RATIO = 0.03;

		private const double LEVEL_HEAL_RATIO = 0.03;
	}
}
