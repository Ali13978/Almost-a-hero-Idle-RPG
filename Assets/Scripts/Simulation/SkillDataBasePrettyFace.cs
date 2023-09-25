using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBasePrettyFace : SkillPassiveDataBase
	{
		public SkillDataBasePrettyFace()
		{
			this.nameKey = "SKILL_NAME_PRETTY_FACE";
			this.descKey = "SKILL_DESC_PRETTY_FACE";
			this.requiredHeroLevel = 8;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTaunt buffDataTaunt = new BuffDataTaunt();
			buffDataTaunt.id = 182;
			data.passiveBuff = buffDataTaunt;
			buffDataTaunt.isPermenant = true;
			buffDataTaunt.tauntAdd = -this.GetTaunt(level);
		}

		private float GetTaunt(int level)
		{
			return SkillDataBasePrettyFace.TAUNT_BASE + (float)level * SkillDataBasePrettyFace.TAUNT_LEVEL;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(SkillDataBasePrettyFace.TAUNT_LEVEL, false) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetTaunt(data.level), false)));
		}

		public static float TAUNT_BASE = 0.08f;

		public static float TAUNT_LEVEL = 0.08f;
	}
}
