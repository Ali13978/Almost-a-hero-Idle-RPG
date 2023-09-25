using System;
using Ui;

namespace Simulation
{
	public class BuffDataResilience : BuffData
	{
		public override bool IsToSave()
		{
			return true;
		}

		public override string GetSaveDataGenericKey()
		{
			return "BuffGeneric.Resilience";
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			double num = (double)buff.GetGenericCounter() * this.costDecPerHit;
			if (num >= this.costDecMax)
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

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnUpgradedSelf(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		public double costDecPerHit;

		public double costDecMax;
	}
}
