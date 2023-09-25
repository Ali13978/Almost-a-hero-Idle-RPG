using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseOneShot : SkillPassiveDataBase
	{
		public SkillDataBaseOneShot()
		{
			this.nameKey = "SKILL_NAME_ONE_SHOT";
			this.descKey = "SKILL_DESC_ONE_SHOT";
			this.requiredHeroLevel = 9;
			this.maxLevel = 6;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataOneShot buffDataOneShot = new BuffDataOneShot();
			buffDataOneShot.id = 137;
			data.passiveBuff = buffDataOneShot;
			buffDataOneShot.isPermenant = true;
			buffDataOneShot.reqNumKill = this.GetNum(level);
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNum(0).ToString()));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNum(data.level).ToString()) + AM.csl(" (-" + SkillDataBaseOneShot.NUM_LEVEL.ToString() + ")"));
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(this.GetNum(data.level).ToString()));
		}

		public int GetNum(int level)
		{
			return SkillDataBaseOneShot.NUM_INIT - level * SkillDataBaseOneShot.NUM_LEVEL;
		}

		public static int NUM_INIT = 12;

		public static int NUM_LEVEL = 1;
	}
}
