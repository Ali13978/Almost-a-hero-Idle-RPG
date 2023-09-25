using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseStrikeMirror : SkillPassiveDataBase
	{
		public SkillDataBaseStrikeMirror()
		{
			this.nameKey = "SKILL_NAME_STRIKE_MIRROR";
			this.descKey = "SKILL_DESC_STRIKE_MIRROR";
			this.requiredHeroLevel = 5;
			this.maxLevel = 11;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataMirrorDamageAlly passiveBuff = new BuffDataMirrorDamageAlly
			{
				isPermenant = true,
				mirrorChance = this.GetMirrorChance(level),
				mirrorPercentage = (double)this.GetDamagePercentage(level),
				id = 280
			};
			data.passiveBuff = passiveBuff;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(0.06f, false)), AM.csa(GameMath.GetPercentString(0.1f, false)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMirrorChance(data.level), false) + AM.csl(" (+" + GameMath.GetPercentString(0.03f, false) + ")")), AM.csa(GameMath.GetPercentString(this.GetDamagePercentage(data.level), false) + AM.csl(" (+" + GameMath.GetPercentString(0.05f, false) + ")")));
		}

		private float GetMirrorChance(int level)
		{
			return 0.06f + (float)level * 0.03f;
		}

		private float GetDamagePercentage(int level)
		{
			return 0.1f + (float)level * 0.05f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetMirrorChance(data.level), false)), AM.csa(GameMath.GetPercentString(this.GetDamagePercentage(data.level), false)));
		}

		public const float MIRROR_CHANCE_INITIAL = 0.06f;

		public const float MIRROR_CHANCE_PER_LEVEL = 0.03f;

		public const float MIRROR_PERCENTAGE_INITIAL = 0.1f;

		public const float MIRROR_PERCENTAGE_PER_LEVEL = 0.05f;
	}
}
