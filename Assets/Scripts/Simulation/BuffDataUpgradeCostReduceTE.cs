using System;

namespace Simulation
{
	public class BuffDataUpgradeCostReduceTE : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.upgradeCostFactorTE *= 1.0 - this.reductionRatio;
		}

		public double reductionRatio;
	}
}
