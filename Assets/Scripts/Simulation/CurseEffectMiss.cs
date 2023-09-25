using System;
using UnityEngine;

namespace Simulation
{
	public class CurseEffectMiss : CurseEffectData
	{
		public CurseEffectMiss()
		{
			this.baseData = new CurseDataBase
			{
				id = 1005,
				nameKey = "CURSE_MISS_NAME",
				conditionKey = "CHARM_CONDITION_ABILITIES_CAST",
				descKey = "CURSE_MISS_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffMiss curseBuffMiss = new CurseBuffMiss
			{
				missProbability = this.GetMissFactor(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffMiss.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffMiss);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffMiss curseBuffMiss = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffMiss;
			curseBuffMiss.missProbability = this.GetMissFactor();
			curseBuffMiss.pic = 1f / (float)this.GetDispelReq();
			return curseBuffMiss;
		}

		private float GetMissFactor()
		{
			return Mathf.Min(0.95f, 1f - (float)GameMath.PowInt(0.89999999850988388, this.level + 1));
		}

		public int GetDispelReq()
		{
			return 3;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetMissFactor(), false)));
		}

		public override string GetConditionDescription()
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetDispelReq().ToString()));
		}

		public override string GetConditionDescriptionNoColor()
		{
			return string.Format(LM.Get(base.conditionKey), this.GetDispelReq().ToString());
		}

		public override float GetWeight()
		{
			return 1f;
		}

		public const float PER_LEVEL_MISS_FACTOR = 0.1f;

		public const int THRESHOLD = 3;
	}
}
