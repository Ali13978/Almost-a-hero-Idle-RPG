using System;

namespace Simulation
{
	public class CurseEffectMoltenGold : CurseEffectData
	{
		public CurseEffectMoltenGold()
		{
			this.baseData = new CurseDataBase
			{
				id = 1015,
				nameKey = "CURSE_MOLTEN_GOLD_NAME",
				conditionKey = "CHARM_CONDITION_ACTIVATE_OTHER_CHARMS",
				descKey = "CURSE_MOLTEN_GOLD_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffDealTeamDamageOnGoldCollected curseBuffDealTeamDamageOnGoldCollected = new CurseBuffDealTeamDamageOnGoldCollected
			{
				damageFactor = (double)this.GetHeroTeamDamageFactor(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffDealTeamDamageOnGoldCollected.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffDealTeamDamageOnGoldCollected);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffDealTeamDamageOnGoldCollected curseBuffDealTeamDamageOnGoldCollected = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffDealTeamDamageOnGoldCollected;
			curseBuffDealTeamDamageOnGoldCollected.pic = 1f / (float)this.GetDispelReq();
			curseBuffDealTeamDamageOnGoldCollected.damageFactor = (double)this.GetHeroTeamDamageFactor();
			return curseBuffDealTeamDamageOnGoldCollected;
		}

		private float GetHeroTeamDamageFactor()
		{
			return 0.01f + 0.01f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 4;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetHeroTeamDamageFactor(), false)));
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
