using System;
using Ui;

namespace Simulation
{
	public class BuffDataReduceUpgradeCost : BuffData
	{
		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return "BuffGeneric.ReduceUpdradeCost";
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = (double)buff.GetGenericCounter() * this.costDecPerHit;
			if (num >= this.costDecMax - this.costDecPerHit * 0.10000000149011612)
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

		public override void OnUpgradedSelf(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		public double costDecPerHit;

		public double costDecMax;
	}
}
