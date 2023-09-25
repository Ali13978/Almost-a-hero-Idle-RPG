using System;

namespace Simulation
{
	public class CurseEffectAntiRegeneration : CurseEffectData
	{
		public CurseEffectAntiRegeneration()
		{
			this.baseData = new CurseDataBase
			{
				id = 1009,
				nameKey = "CURSE_ANTI_REGENERATION_NAME",
				conditionKey = "CHARM_CONDITION_ACTIVATE_OTHER_CHARMS",
				descKey = "CURSE_ANTI_REGENERATION_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffAntiRegeneration curseBuffAntiRegeneration = new CurseBuffAntiRegeneration
			{
				damageFactor = this.GetDamageFactor(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffAntiRegeneration.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffAntiRegeneration);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffAntiRegeneration curseBuffAntiRegeneration = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffAntiRegeneration;
			curseBuffAntiRegeneration.damageFactor = this.GetDamageFactor();
			curseBuffAntiRegeneration.pic = 1f / (float)this.GetDispelReq();
			return curseBuffAntiRegeneration;
		}

		public int GetDispelReq()
		{
			return 6;
		}

		public double GetDamageFactor()
		{
			return 1.0 - GameMath.PowInt(0.99000000022351742, this.level + 1);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageFactor(), false)));
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

		public const double PER_LEVEL_DAMAGE_FACTOR = 0.0099999997764825821;

		public const int THRESHOLD = 6;
	}
}
