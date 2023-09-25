using System;

namespace Simulation
{
	public class BuffDataUpgradeCostReduceAllTE : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.upgradeCostFactorTE *= 1.0 - this.reductionRatio;
		}

		public double reductionRatio;
	}
}
