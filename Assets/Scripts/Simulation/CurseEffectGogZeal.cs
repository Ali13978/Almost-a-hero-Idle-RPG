using System;

namespace Simulation
{
	public class CurseEffectGogZeal : CurseEffectData
	{
		public CurseEffectGogZeal()
		{
			this.baseData = new CurseDataBase
			{
				id = 1016,
				nameKey = "CURSE_GOG_ZEAL_NAME",
				conditionKey = "CHARM_CONDITION_ABILITIES_CAST",
				descKey = "CURSE_GOG_ZEAL_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffEnemyAttackSpeedIncrement curseBuffEnemyAttackSpeedIncrement = new CurseBuffEnemyAttackSpeedIncrement
			{
				attackSpeedIncrement = this.GetEnemyAttackSpeedIncrement(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffEnemyAttackSpeedIncrement.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffEnemyAttackSpeedIncrement);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffEnemyAttackSpeedIncrement curseBuffEnemyAttackSpeedIncrement = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffEnemyAttackSpeedIncrement;
			curseBuffEnemyAttackSpeedIncrement.pic = 1f / (float)this.GetDispelReq();
			curseBuffEnemyAttackSpeedIncrement.attackSpeedIncrement = this.GetEnemyAttackSpeedIncrement();
			return curseBuffEnemyAttackSpeedIncrement;
		}

		private float GetEnemyAttackSpeedIncrement()
		{
			return 0.5f + 0.5f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 3;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetEnemyAttackSpeedIncrement(), false)));
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

		public const float BASE_ATTACK_SPEED = 0.5f;

		public const float PER_LEVEL_ATTACK_SPEED = 0.5f;

		public const int THRESHOLD = 3;
	}
}
