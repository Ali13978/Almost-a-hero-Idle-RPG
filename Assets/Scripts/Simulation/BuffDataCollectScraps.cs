using System;
using Ui;

namespace Simulation
{
	public class BuffDataCollectScraps : BuffData
	{
		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return "BuffGeneric.CollectScraps";
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = (double)buff.GetGenericCounter() * this.costDecPerKill;
			if (num >= this.costDecMax - this.costDecPerKill * 0.10000000149011612)
			{
				num = this.costDecMax;
				totEffect.maxCostReductionReached.Add(true);
			}
			else
			{
				totEffect.maxCostReductionReached.Add(false);
			}
			totEffect.upgradeCostFactor *= 1.0 - num;
			UiManager.stateJustChanged = true;
		}

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnUpgradedSelf(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		public double costDecPerKill;

		public double costDecMax;
	}
}
