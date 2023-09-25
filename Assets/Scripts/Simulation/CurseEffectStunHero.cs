using System;

namespace Simulation
{
	public class CurseEffectStunHero : CurseEffectData
	{
		public CurseEffectStunHero()
		{
			this.baseData = new CurseDataBase
			{
				id = 1008,
				nameKey = "CURSE_STUN_HERO_NAME",
				conditionKey = "CHARM_CONDITION_STUN_ENEMIES",
				descKey = "CURSE_STUN_HERO_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffStunHero curseBuffStunHero = new CurseBuffStunHero
			{
				stunDuration = this.GetStunDuration(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffStunHero.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffStunHero);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffStunHero curseBuffStunHero = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffStunHero;
			curseBuffStunHero.pic = 1f / (float)this.GetDispelReq();
			curseBuffStunHero.stunDuration = this.GetStunDuration();
			return curseBuffStunHero;
		}

		private float GetStunDuration()
		{
			return 2f + 0.5f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 3;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration())));
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

		public const float BASE_STUN_DURATION = 2f;

		public const float PER_LEVEL_STUN_DURATION = 0.5f;

		public const int THRESHOLD = 3;
	}
}
