using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBasePunishment : SkillPassiveDataBase
	{
		public SkillDataBasePunishment()
		{
			this.nameKey = "SKILL_NAME_PUNISHMENT";
			this.descKey = "SKILL_DESC_PUNISHMENT";
			this.requiredHeroLevel = 5;
			this.maxLevel = 8;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataPunishment buffDataPunishment = new BuffDataPunishment();
			buffDataPunishment.id = 143;
			data.passiveBuff = buffDataPunishment;
			buffDataPunishment.isPermenant = true;
			buffDataPunishment.numHitReq = this.GetNumHitReq(level);
			buffDataPunishment.damageFactor = this.GetDamage(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumHitReq(0).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumHitReq(data.level).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.5, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNumHitReq(data.level).ToString()), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)));
		}

		public int GetNumHitReq(int level)
		{
			return 3;
		}

		public double GetDamage(int level)
		{
			return 3.0 + (double)level * 0.5;
		}

		private const int INIT_NUM_HIT_REQ = 3;

		private const double INIT_DAMAGE = 3.0;

		private const double LEVEL_DAMAGE = 0.5;
	}
}
