using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTerror : SkillPassiveDataBase
	{
		public SkillDataBaseTerror()
		{
			this.descKey = "SKILL_DESC_TERROR";
			this.nameKey = "SKILL_NAME_TERROR";
			this.requiredHeroLevel = 3;
			this.maxLevel = 4;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTerror passiveBuff = new BuffDataTerror
			{
				isPermenant = true,
				terrorizeDuration = this.GetDur(level),
				terrorizeChance = this.GetFearChance(level),
				missChance = this.GetMissChance(level),
				id = 278
			};
			data.passiveBuff = passiveBuff;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetFearChance(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(0))), AM.csa(GameMath.GetPercentString(this.GetMissChance(0), false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetFearChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.02f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")"), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")"));
		}

		private float GetFearChance(int level)
		{
			return 0.05f + (float)level * 0.02f;
		}

		private float GetMissChance(int level)
		{
			return 0.3f + (float)level * 0.05f;
		}

		private float GetDur(int level)
		{
			return 3f + (float)level * 0.5f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetFearChance(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))), AM.csa(GameMath.GetPercentString(this.GetMissChance(data.level), false)));
		}

		public const float INITIAL_FEAR_CHANCE = 0.05f;

		public const float FEAR_CHANCE_PER_LEVEL = 0.02f;

		public const float INITIAL_DUR = 3f;

		public const float DUR_PER_LEVEL = 0.5f;

		public const float INITIAL_MISS_CHANCE = 0.3f;

		public const float MISS_CHANCE_PER_LEVEL = 0.05f;
	}
}
