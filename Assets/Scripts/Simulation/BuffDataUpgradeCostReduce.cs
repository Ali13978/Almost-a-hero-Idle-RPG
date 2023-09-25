using System;

namespace Simulation
{
	public class BuffDataUpgradeCostReduce : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.upgradeCostFactor *= 1.0 - this.reductionRatio;
		}

		public double reductionRatio;
	}
}
