using System;

namespace Simulation
{
	public class CurseEffectDealDamage : CurseEffectData
	{
		public CurseEffectDealDamage()
		{
			this.baseData = new CurseDataBase
			{
				id = 1001,
				nameKey = "CURSE_DEAL_DAMAGE_NAME",
				conditionKey = "CHARM_CONDITION_KILL_ENEMIES",
				descKey = "CURSE_DEAL_DAMAGE_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffDealDamage curseBuffDealDamage = new CurseBuffDealDamage
			{
				damageFactor = this.GetDamageFactor(),
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffDealDamage.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffDealDamage);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffDealDamage curseBuffDealDamage = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffDealDamage;
			curseBuffDealDamage.damageFactor = this.GetDamageFactor();
			curseBuffDealDamage.pic = 1f / this.GetDispelReq();
			return curseBuffDealDamage;
		}

		private double GetDamageFactor()
		{
			return 1.0 - GameMath.PowInt(0.84999999403953552, this.level + 1);
		}

		public float GetDispelReq()
		{
			return 7f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageFactor(), true)));
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
			return 0.8f;
		}

		public const double PER_LEVEL_DAMAGE_FACTOR = 0.15000000596046448;

		public const int THRESHOLD = 7;
	}
}
