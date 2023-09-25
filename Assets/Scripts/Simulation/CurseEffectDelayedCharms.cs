using System;

namespace Simulation
{
	public class CurseEffectDelayedCharms : CurseEffectData
	{
		public CurseEffectDelayedCharms()
		{
			this.baseData = new CurseDataBase
			{
				id = 1013,
				nameKey = "CURSE_DELAYED_CHARMS_NAME",
				conditionKey = "CHARM_CONDITION_HERO_HEALTH_RESTORED",
				descKey = "CURSE_DELAYED_CHARMS_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffCharmSelectionTimer curseBuffCharmSelectionTimer = new CurseBuffCharmSelectionTimer
			{
				timerFactor = 1f - this.GetCharmsTimerFactor(),
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffCharmSelectionTimer.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffCharmSelectionTimer);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffCharmSelectionTimer curseBuffCharmSelectionTimer = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffCharmSelectionTimer;
			curseBuffCharmSelectionTimer.pic = 1f / this.GetDispelReq();
			curseBuffCharmSelectionTimer.timerFactor = 1f - this.GetCharmsTimerFactor();
			return curseBuffCharmSelectionTimer;
		}

		private float GetCharmsTimerFactor()
		{
			return GameMath.GetMinFloat(0.1f + 0.1f * (float)this.level, 1f);
		}

		public float GetDispelReq()
		{
			return 0.15f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCharmsTimerFactor(), false)));
		}

		public override string GetConditionDescription()
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(GameMath.GetPercentString(this.GetDispelReq(), false)));
		}

		public override string GetConditionDescriptionNoColor()
		{
			return string.Format(LM.Get(base.conditionKey), GameMath.GetPercentString(this.GetDispelReq(), false));
		}

		public override float GetWeight()
		{
			return 1.1f;
		}

		public const float BASE_CHARM_TIMER_FACTOR = 0.1f;

		public const float PER_LEVEL_CHARM_TIMER_FACTOR = 0.1f;

		public const float THRESHOLD = 0.15f;
	}
}
