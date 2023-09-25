using System;

namespace Simulation
{
	public class CurseEffectCharmProgress : CurseEffectData
	{
		public CurseEffectCharmProgress()
		{
			this.baseData = new CurseDataBase
			{
				id = 1003,
				nameKey = "CURSE_CHARM_PROGRESS_NAME",
				conditionKey = "CHARM_CONDITION_COLLECT_GOLD_PIECES",
				descKey = "CURSE_CHARM_PROGRESS_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffCharmProgress curseBuffCharmProgress = new CurseBuffCharmProgress
			{
				factorLost = this.GetLoseProgressFactor(),
				factorLostEverySeconds = 4f,
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffCharmProgress.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffCharmProgress);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffCharmProgress curseBuffCharmProgress = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffCharmProgress;
			curseBuffCharmProgress.factorLost = this.GetLoseProgressFactor();
			curseBuffCharmProgress.factorLostEverySeconds = 4f;
			curseBuffCharmProgress.pic = 1f / this.GetDispelReq();
			return curseBuffCharmProgress;
		}

		private float GetLoseProgressFactor()
		{
			return 1f - (float)GameMath.PowInt(0.94000000134110451, this.level + 1);
		}

		public float GetDispelReq()
		{
			return 50f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetLoseProgressFactor(), false)), AM.cds(GameMath.GetTimeInSecondsString(4f)));
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

		public const float PER_LEVEL_LOSE_PROGRESS_FACTOR = 0.06f;

		public const float EVERY_SECONDS = 4f;

		public const int THRESHOLD = 50;
	}
}
