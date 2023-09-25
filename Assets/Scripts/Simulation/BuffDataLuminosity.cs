using System;
using Ui;

namespace Simulation
{
	public class BuffDataLuminosity : BuffData
	{
		public BuffDataLuminosity(double reduction, double cap)
		{
			this.reduction = reduction;
			this.cap = cap;
			this.id = 120;
		}

		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return "BuffGeneric.Limunosity";
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = (double)buff.GetGenericCounter() * this.reduction;
			if (num > this.cap)
			{
				num = this.cap;
				totEffect.maxCostReductionReached.Add(true);
			}
			else
			{
				totEffect.maxCostReductionReached.Add(false);
			}
			totEffect.upgradeCostFactor *= 1.0 - num;
			UiManager.stateJustChanged = true;
		}

		public override void OnOverheated(Buff buff)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnUpgradedSelf(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		private double reduction;

		private double cap;
	}
}
