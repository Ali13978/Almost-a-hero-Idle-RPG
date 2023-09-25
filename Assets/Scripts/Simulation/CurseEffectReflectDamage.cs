using System;

namespace Simulation
{
	public class CurseEffectReflectDamage : CurseEffectData
	{
		public CurseEffectReflectDamage()
		{
			this.baseData = new CurseDataBase
			{
				id = 1004,
				nameKey = "CURSE_REFLECT_DAMAGE_NAME",
				conditionKey = "CHARM_CONDITION_HERO_DEATHS",
				descKey = "CURSE_REFLECT_DAMAGE_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffReflectDamage curseBuffReflectDamage = new CurseBuffReflectDamage
			{
				reflectFactor = this.GetReflectFactor(),
				pic = 1f,
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffReflectDamage.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffReflectDamage);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffReflectDamage curseBuffReflectDamage = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffReflectDamage;
			curseBuffReflectDamage.reflectFactor = this.GetReflectFactor();
			curseBuffReflectDamage.pic = 1f;
			return curseBuffReflectDamage;
		}

		public double GetReflectFactor()
		{
			return 1.0 - GameMath.PowInt(0.75, this.level + 1);
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetReflectFactor(), false)));
		}

		public override string GetConditionDescription()
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(1.ToString()));
		}

		public override string GetConditionDescriptionNoColor()
		{
			return string.Format(LM.Get(base.conditionKey), 1.ToString());
		}

		public override float GetWeight()
		{
			return 1.2f;
		}

		public const float PER_LEVEL_REFLECT_FACTOR = 0.25f;

		public const int DEATHS_THRESHOLD = 1;
	}
}
