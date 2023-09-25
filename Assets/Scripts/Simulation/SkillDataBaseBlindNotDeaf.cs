using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseBlindNotDeaf : SkillPassiveDataBase
	{
		public SkillDataBaseBlindNotDeaf()
		{
			this.nameKey = "SKILL_NAME_NOT_DEAF";
			this.descKey = "SKILL_DESC_NOT_DEAF";
			this.requiredHeroLevel = 5;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataBlindNotDeaf buffDataBlindNotDeaf = new BuffDataBlindNotDeaf();
			buffDataBlindNotDeaf.id = 18;
			data.passiveBuff = buffDataBlindNotDeaf;
			buffDataBlindNotDeaf.isPermenant = true;
			buffDataBlindNotDeaf.damageBuff = new BuffDataDamageAdd();
			buffDataBlindNotDeaf.damageBuff.id = 359;
			buffDataBlindNotDeaf.damageBuff.damageAdd = this.GetDamage(level);
			buffDataBlindNotDeaf.damageBuff.dur = this.GetDuration(level);
			buffDataBlindNotDeaf.damageBuff.isPermenant = false;
			buffDataBlindNotDeaf.damageBuff.isStackable = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBaseBlindNotDeaf.DAMAGE_LEVEL, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamage(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDuration(data.level))));
		}

		public double GetDamage(int level)
		{
			return SkillDataBaseBlindNotDeaf.DAMAGE_INIT + (double)level * SkillDataBaseBlindNotDeaf.DAMAGE_LEVEL;
		}

		public float GetDuration(int level)
		{
			return SkillDataBaseBlindNotDeaf.DURATION;
		}

		public static double DAMAGE_INIT = 0.08;

		public static double DAMAGE_LEVEL = 0.02;

		public static float DURATION = 6f;
	}
}
