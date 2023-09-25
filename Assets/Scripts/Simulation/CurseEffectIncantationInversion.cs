using System;

namespace Simulation
{
	public class CurseEffectIncantationInversion : CurseEffectData
	{
		public CurseEffectIncantationInversion()
		{
			this.baseData = new CurseDataBase
			{
				id = 1018,
				nameKey = "CURSE_INCANTATION_INVERSION_NAME",
				conditionKey = "CHARM_CONDITION_ENEMY_ATTACKS",
				descKey = "CURSE_INCANTATION_INVERSION_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffDealTeamDamageOnAbilityCasted curseBuffDealTeamDamageOnAbilityCasted = new CurseBuffDealTeamDamageOnAbilityCasted
			{
				damageFactor = (double)this.GetHeroesTeamDamageFactor(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffDealTeamDamageOnAbilityCasted.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffDealTeamDamageOnAbilityCasted);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffDealTeamDamageOnAbilityCasted curseBuffDealTeamDamageOnAbilityCasted = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffDealTeamDamageOnAbilityCasted;
			curseBuffDealTeamDamageOnAbilityCasted.pic = 1f / (float)this.GetDispelReq();
			curseBuffDealTeamDamageOnAbilityCasted.damageFactor = (double)this.GetHeroesTeamDamageFactor();
			return curseBuffDealTeamDamageOnAbilityCasted;
		}

		private float GetHeroesTeamDamageFactor()
		{
			return 1f + 1f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 50;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetHeroesTeamDamageFactor(), false)));
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

		public const float BASE_TEAM_DAMAGE_FACTOR = 1f;

		public const float PER_LEVEL_TEAM_DAMAGE_FACTOR = 1f;

		public const int THRESHOLD = 50;
	}
}
