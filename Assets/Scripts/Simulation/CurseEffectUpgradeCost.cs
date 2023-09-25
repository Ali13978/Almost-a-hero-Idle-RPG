using System;

namespace Simulation
{
	public class CurseEffectUpgradeCost : CurseEffectData
	{
		public CurseEffectUpgradeCost()
		{
			this.baseData = new CurseDataBase
			{
				id = 1006,
				nameKey = "CURSE_UPGRADE_COST_NAME",
				conditionKey = "CHARM_CONDITION_ENEMY_ATTACKS",
				descKey = "CURSE_UPGRADE_COST_DESC"
			};
		}

		public override void Apply(ChallengeRift challenge)
		{
			CurseBuffUpgradeCost curseBuffUpgradeCost = new CurseBuffUpgradeCost
			{
				upgradeCostFactor = 1.0 + this.GetCostIncrease(),
				pic = 0.02f,
				enchantmentData = this
			};
			if (this.level == -1)
			{
				curseBuffUpgradeCost.state = EnchantmentBuffState.INACTIVE;
			}
			challenge.AddCurseBuff(curseBuffUpgradeCost);
		}

		protected override CurseBuff RefreshBuffStats(ChallengeRift challenge)
		{
			CurseBuffUpgradeCost curseBuffUpgradeCost = challenge.curseBuffs.Find((CurseBuff c) => c.enchantmentData == this) as CurseBuffUpgradeCost;
			curseBuffUpgradeCost.upgradeCostFactor = 1.0 + this.GetCostIncrease();
			curseBuffUpgradeCost.pic = 0.02f;
			return curseBuffUpgradeCost;
		}

		public double GetCostIncrease()
		{
			return GameMath.PowDouble(1.25, (double)(this.level + 1)) - 1.0;
		}

		public override string GetDesc()
		{
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCostIncrease(), false)));
		}

		public override string GetConditionDescription()
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(50.ToString()));
		}

		public override string GetConditionDescriptionNoColor()
		{
			return string.Format(LM.Get(base.conditionKey), 50.ToString());
		}

		public override float GetWeight()
		{
			return 1f;
		}

		public const float PER_LEVEL_COST_INCREASE = 0.25f;

		public const int THRESHOLD = 50;
	}
}
