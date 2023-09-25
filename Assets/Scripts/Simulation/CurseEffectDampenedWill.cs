using System;

namespace Simulation
{
	public class CurseEffectDampenedWill : CurseEffectData
	{
		public CurseEffectDampenedWill()
		{
			this.baseData = new CurseDataBase
			{
				id = 1017,
				nameKey = "CURSE_DAMPENED_WILL_NAME",
				conditionKey = "CHARM_CONDITION_ACTIVATE_OTHER_CHARMS",
				descKey = "CURSE_DAMPENED_WILL_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffDeathTimer curseBuffDeathTimer = new CurseBuffDeathTimer
			{
				timerFactor = this.GetDeathTimerFactor(),
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffDeathTimer.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffDeathTimer);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffDeathTimer curseBuffDeathTimer = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffDeathTimer;
			curseBuffDeathTimer.pic = 1f / this.GetDispelReq();
			curseBuffDeathTimer.timerFactor = this.GetDeathTimerFactor();
			return curseBuffDeathTimer;
		}

		private float GetDeathTimerFactor()
		{
			return 0.15f + 0.15f * (float)this.level;
		}

		public float GetDispelReq()
		{
			return 3f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDeathTimerFactor(), false)));
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
			return 1.1f;
		}

		public const float BASE_DEATH_TIMER_FACTOR = 0.15f;

		public const float PER_LEVEL_DEATH_TIMER_FACTOR = 0.15f;

		public const int THRESHOLD = 3;
	}
}
