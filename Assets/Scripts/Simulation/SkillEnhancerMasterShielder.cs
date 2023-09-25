using System;

namespace Simulation
{
	public class SkillEnhancerMasterShielder : SkillEnhancerBase
	{
		public SkillEnhancerMasterShielder()
		{
			this.nameKey = "SKILL_NAME_MASTER_SHIELDER";
			this.descKey = "SKILL_DESC_MASTER_SHIELDER";
			this.requiredHeroLevel = 11;
			this.maxLevel = 4;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHIELDEM_ALL")), AM.csa(GameMath.GetTimeInSecondsString(this.GetCD(0))));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHIELDEM_ALL")), AM.csa(GameMath.GetTimeInSecondsString(this.GetCD(data.level))) + AM.csl(" (+" + GameMath.GetTimeInSecondsString(SkillEnhancerMasterShielder.LEVEL_RED) + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(LM.Get("SKILL_NAME_SHIELDEM_ALL")), AM.csa(GameMath.GetTimeInSecondsString(this.GetCD(data.level))));
		}

		public float GetCD(int level)
		{
			return SkillEnhancerMasterShielder.INIT_RED + SkillEnhancerMasterShielder.LEVEL_RED * (float)level;
		}

		public static float INIT_RED = 8f;

		public static float LEVEL_RED = 8f;
	}
}
