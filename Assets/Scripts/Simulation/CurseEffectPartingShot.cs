using System;

namespace Simulation
{
	public class CurseEffectPartingShot : CurseEffectData
	{
		public CurseEffectPartingShot()
		{
			this.baseData = new CurseDataBase
			{
				id = 1012,
				nameKey = "CURSE_PARTING_SHOT_NAME",
				conditionKey = "CHARM_CONDITION_DODGE_ATTACKS",
				descKey = "CURSE_PARTING_SHOT_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffDealDamageOnAnyDeath curseBuffDealDamageOnAnyDeath = new CurseBuffDealDamageOnAnyDeath
			{
				damageFactor = (double)this.GetTeamDamageFactor(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffDealDamageOnAnyDeath.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffDealDamageOnAnyDeath);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffDealDamageOnAnyDeath curseBuffDealDamageOnAnyDeath = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffDealDamageOnAnyDeath;
			curseBuffDealDamageOnAnyDeath.pic = 1f / (float)this.GetDispelReq();
			curseBuffDealDamageOnAnyDeath.damageFactor = (double)this.GetTeamDamageFactor();
			return curseBuffDealDamageOnAnyDeath;
		}

		private float GetTeamDamageFactor()
		{
			return 0.01f + 0.01f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 4;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetTeamDamageFactor(), false)));
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

		public const float BASE_TEAM_DAMAGE_FACTOR = 0.01f;

		public const float PER_LEVEL_TEAM_DAMAGE_FACTOR = 0.01f;

		public const int THRESHOLD = 4;
	}
}
