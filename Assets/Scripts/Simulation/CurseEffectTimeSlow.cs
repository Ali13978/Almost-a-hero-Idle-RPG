using System;
using UnityEngine;

namespace Simulation
{
	public class CurseEffectTimeSlow : CurseEffectData
	{
		public CurseEffectTimeSlow()
		{
			this.baseData = new CurseDataBase
			{
				id = 1002,
				nameKey = "CURSE_TIME_SLOW_NAME",
				conditionKey = "CHARM_CONDITION_HERO_CRIT_HITS",
				descKey = "CURSE_TIME_SLOW_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffTimeSlow curseBuffTimeSlow = new CurseBuffTimeSlow
			{
				timeFactor = 1f - this.GetTimeFactor(),
				pic = 0.06666667f,
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffTimeSlow.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffTimeSlow);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffTimeSlow curseBuffTimeSlow = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffTimeSlow;
			curseBuffTimeSlow.timeFactor = 1f - this.GetTimeFactor();
			return curseBuffTimeSlow;
		}

		public float GetTimeFactor()
		{
			return Mathf.Min(0.95f, 1f - (float)GameMath.PowInt(0.92000000178813934, this.level + 1));
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetTimeFactor(), false)));
		}

		public override string GetConditionDescription()
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(15.ToString()));
		}

		public override string GetConditionDescriptionNoColor()
		{
			return string.Format(LM.Get(base.conditionKey), 15.ToString());
		}

		public override float GetWeight()
		{
			return 0.3f;
		}

		public const float PER_LEVEL_TIME_FACTOR = 0.08f;

		public const int THRESHOLD = 15;
	}
}
