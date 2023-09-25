using System;

namespace Simulation
{
	public class CurseEffectHeavyLimbs : CurseEffectData
	{
		public CurseEffectHeavyLimbs()
		{
			this.baseData = new CurseDataBase
			{
				id = 1010,
				nameKey = "CURSE_HEAVY_LIMBS_NAME",
				conditionKey = "CHARM_CONDITION_PASS_WAVES",
				descKey = "CURSE_HEAVY_LIMBS_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffAttackSpeedReduction curseBuffAttackSpeedReduction = new CurseBuffAttackSpeedReduction
			{
				attackSpeedFactor = 1f - this.GetHeroAttackSpeedReduction(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffAttackSpeedReduction.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffAttackSpeedReduction);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffAttackSpeedReduction curseBuffAttackSpeedReduction = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffAttackSpeedReduction;
			curseBuffAttackSpeedReduction.pic = 1f / (float)this.GetDispelReq();
			curseBuffAttackSpeedReduction.attackSpeedFactor = 1f - this.GetHeroAttackSpeedReduction();
			return curseBuffAttackSpeedReduction;
		}

		private float GetHeroAttackSpeedReduction()
		{
			return 0.3f + 0.1f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 5;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetHeroAttackSpeedReduction(), false)));
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

		public const float BASE_ATTACK_SPEED_REDUCTION = 0.3f;

		public const float PER_LEVEL_ATTACK_SPEED_REDUCTION = 0.1f;

		public const int THRESHOLD = 5;
	}
}
