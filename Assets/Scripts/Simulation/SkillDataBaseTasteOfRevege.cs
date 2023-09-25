using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillDataBaseTasteOfRevege : SkillPassiveDataBase
	{
		public SkillDataBaseTasteOfRevege()
		{
			this.descKey = "SKILL_DESC_TASTEOFREVENGE";
			this.nameKey = "SKILL_NAME_TASTEOFREVENGE";
			this.requiredHeroLevel = 6;
			this.maxLevel = 5;
		}

		public override void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> enhancers)
		{
			BuffDataTasteOfRevenge buffDataTasteOfRevenge = new BuffDataTasteOfRevenge
			{
				damageIncrease = this.GetDamageIncrease(level),
				addTime = this.GetDur(level),
				timeMax = 90f
			};
			buffDataTasteOfRevenge.id = 277;
			buffDataTasteOfRevenge.visuals |= 8;
			data.passiveBuff = buffDataTasteOfRevenge;
			buffDataTasteOfRevenge.isPermenant = true;
		}

		public override string GetDescZero()
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncrease(0), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(0))), AM.csa(GameMath.GetTimeInMilliSecondsString(90f)));
		}

		public override string GetDesc(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncrease(data.level), false)) + AM.csl(" (+" + GameMath.GetPercentString(0.1f, false) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))) + AM.csl(" (+" + GameMath.GetTimeInMilliSecondsString(2f) + ")"), AM.csa(GameMath.GetTimeInMilliSecondsString(90f)));
		}

		private float GetDamageIncrease(int level)
		{
			return 0.4f + (float)level * 0.1f;
		}

		private float GetDur(int level)
		{
			return 8f + (float)level * 2f;
		}

		public override string GetDescFull(SkillData data)
		{
			return string.Format(LM.Get(this.descKey), AM.csa(GameMath.GetPercentString(this.GetDamageIncrease(data.level), false)), AM.csa(GameMath.GetTimeInMilliSecondsString(this.GetDur(data.level))), AM.csa(GameMath.GetTimeInMilliSecondsString(90f)));
		}

		public const float INITIAL_DAMAGE_INCREASE = 0.4f;

		public const float DAMAGE_INCREASE_PER_LEVEL = 0.1f;

		public const float INITIAL_DUR = 8f;

		public const float DUR_PER_LEVEL = 2f;

		public const float TIME_MAX = 90f;
	}
}
