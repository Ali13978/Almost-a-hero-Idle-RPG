using System;

namespace Simulation
{
	public class CurseEffectHauntingVisage : CurseEffectData
	{
		public CurseEffectHauntingVisage()
		{
			this.baseData = new CurseDataBase
			{
				id = 1014,
				nameKey = "CURSE_HAUNTING_VISAGE_NAME",
				conditionKey = "CHARM_CONDITION_KILL_ENEMIES",
				descKey = "CURSE_HAUNTING_VISAGE_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffSilenceHeroPeriodically curseBuffSilenceHeroPeriodically = new CurseBuffSilenceHeroPeriodically
			{
				period = this.GetSilenceCursePeriod(),
				duration = 3f,
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffSilenceHeroPeriodically.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffSilenceHeroPeriodically);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffSilenceHeroPeriodically curseBuffSilenceHeroPeriodically = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffSilenceHeroPeriodically;
			curseBuffSilenceHeroPeriodically.pic = 1f / (float)this.GetDispelReq();
			curseBuffSilenceHeroPeriodically.period = this.GetSilenceCursePeriod();
			curseBuffSilenceHeroPeriodically.duration = 3f;
			return curseBuffSilenceHeroPeriodically;
		}

		private float GetSilenceCursePeriod()
		{
			return 10f + 1f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 15;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetTimeInSecondsString(this.GetSilenceCursePeriod())), AM.cds(GameMath.GetTimeInSecondsString(3f)));
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

		public const float BASE_SILENCE_PERIOD = 10f;

		public const float PER_LEVEL_SILENCE_PERIOD = 1f;

		public const float SILENCE_DURATION = 3f;

		public const int THRESHOLD = 15;
	}
}
