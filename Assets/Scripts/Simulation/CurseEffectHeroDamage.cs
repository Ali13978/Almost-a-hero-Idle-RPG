using System;

namespace Simulation
{
	public class CurseEffectHeroDamage : CurseEffectData
	{
		public CurseEffectHeroDamage()
		{
			this.baseData = new CurseDataBase
			{
				id = 1007,
				nameKey = "CURSE_HERO_DAMAGE_NAME",
				conditionKey = "CHARM_CONDITION_HERO_HEALTH_RESTORED",
				descKey = "CURSE_HERO_DAMAGE_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffHeroDamage curseBuffHeroDamage = new CurseBuffHeroDamage
			{
				damageFactor = 1.0 - this.GetDamageRedFactor(),
				pic = 1f / this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffHeroDamage.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffHeroDamage);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffHeroDamage curseBuffHeroDamage = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffHeroDamage;
			curseBuffHeroDamage.damageFactor = 1.0 - this.GetDamageRedFactor();
			curseBuffHeroDamage.pic = 1f / this.GetDispelReq();
			return curseBuffHeroDamage;
		}

		public double GetDamageRedFactor()
		{
			return Math.Min(0.95, 1.0 - GameMath.PowInt(0.880000002682209, this.level + 1));
		}

		public float GetDispelReq()
		{
			return 0.6f;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageRedFactor(), true)));
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
			return 1f;
		}

		public const double BASE_DAMAGE_REDUCE_FACTOR = 0.11999999731779099;

		public const float THRESHOLD = 0.6f;
	}
}
