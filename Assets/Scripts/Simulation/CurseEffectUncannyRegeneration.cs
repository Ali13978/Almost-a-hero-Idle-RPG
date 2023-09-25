using System;

namespace Simulation
{
	public class CurseEffectUncannyRegeneration : CurseEffectData
	{
		public CurseEffectUncannyRegeneration()
		{
			this.baseData = new CurseDataBase
			{
				id = 1011,
				nameKey = "CURSE_UNCANNY_REGENERATION_NAME",
				conditionKey = "CHARM_CONDITION_COLLECT_GOLD_PIECES",
				descKey = "CURSE_UNCANNY_REGENERATION_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffEnemyHealingPerSecond curseBuffEnemyHealingPerSecond = new CurseBuffEnemyHealingPerSecond
			{
				healingFactor = (double)this.GetEnemyHealingRatio(),
				pic = 1f / (float)this.GetDispelReq(),
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffEnemyHealingPerSecond.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffEnemyHealingPerSecond);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffEnemyHealingPerSecond curseBuffEnemyHealingPerSecond = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffEnemyHealingPerSecond;
			curseBuffEnemyHealingPerSecond.pic = 1f / (float)this.GetDispelReq();
			curseBuffEnemyHealingPerSecond.healingFactor = (double)this.GetEnemyHealingRatio();
			return curseBuffEnemyHealingPerSecond;
		}

		private float GetEnemyHealingRatio()
		{
			return 0.01f + 0.01f * (float)this.level;
		}

		public int GetDispelReq()
		{
			return 50;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetEnemyHealingRatio(), false)));
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

		public const float BASE_HEAL = 0.01f;

		public const float PER_LEVEL_HEAL = 0.01f;

		public const int THRESHOLD = 50;
	}
}
