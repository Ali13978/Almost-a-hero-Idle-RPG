using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseForge : SkillPassiveDataBase
	{
		public SkillDataBaseForge()
		{
			this.nameKey = "SKILL_NAME_FORGE";
			this.descKey = "SKILL_DESC_FORGE";
			this.requiredHeroLevel = 9;
			this.maxLevel = 11;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataForge buffDataForge = new BuffDataForge();
			buffDataForge.id = 85;
			data.passiveBuff = buffDataForge;
			buffDataForge.isPermenant = true;
			BuffDataDamageAdd buffDataDamageAdd = new BuffDataDamageAdd();
			buffDataDamageAdd.id = 41;
			buffDataForge.effect = buffDataDamageAdd;
			buffDataDamageAdd.isStackable = false;
			buffDataDamageAdd.dur = this.GetDuration(level);
			buffDataDamageAdd.damageAdd = this.GetDamageAdd(level);
			buffDataDamageAdd.visuals = 8;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(0), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.15, false) + ")"), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(2f) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageAdd(data.level), false)), AM.csa(GameMath.GetTimeInSecondsString(this.GetDuration(data.level))));
		}

		public double GetDamageAdd(int level)
		{
			return 0.3 + 0.15 * (double)level;
		}

		public float GetDuration(int level)
		{
			return 18f + 2f * (float)level;
		}

		private const double INITIAL_DAMAGE_BONUS = 0.3;

		private const double LEVEL_DAMAGE_BONUS = 0.15;

		private const float INIT_DURATION = 18f;

		private const float LEVEL_DURATION = 2f;
	}
}
