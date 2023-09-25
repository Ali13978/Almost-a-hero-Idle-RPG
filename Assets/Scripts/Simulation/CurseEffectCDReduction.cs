using System;

namespace Simulation
{
	public class CurseEffectCDReduction : CurseEffectData
	{
		public CurseEffectCDReduction()
		{
			this.baseData = new CurseDataBase
			{
				id = 1000,
				nameKey = "CURSE_CD_REDUCTION_NAME",
				conditionKey = "CHARM_CONDITION_HERO_ATTACKS",
				descKey = "CURSE_CD_REDUCTION_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffCDReduction curseBuffCDReduction = new CurseBuffCDReduction
			{
				cooldownFactor = 1f - this.GetCdSpeedRedFactor(),
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffCDReduction.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffCDReduction);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffCDReduction curseBuffCDReduction = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffCDReduction;
			curseBuffCDReduction.cooldownFactor = 1f - this.GetCdSpeedRedFactor();
			curseBuffCDReduction.pic = 1f / this.GetDispelReq();
			return curseBuffCDReduction;
		}

		public float GetDispelReq()
		{
			return 75f;
		}

		public float GetCdSpeedRedFactor()
		{
			return 1f - (float)GameMath.PowInt(0.880000002682209, this.level + 1);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCdSpeedRedFactor(), true)));
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

		public const float PER_LEVEL_CD_SPEED_RED_FACTOR = 0.12f;

		public const int THRESHOLD = 75;
	}
}
