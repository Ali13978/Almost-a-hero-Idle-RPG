using System;

namespace Simulation
{
	public class BuffDataToughness : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			if (unitHealthy.GetHealthRatio() <= this.healthRatioReqMax)
			{
				totEffect.damageTakenFactor *= this.damageTakenFactor;
			}
		}

		public double healthRatioReqMax;

		public double damageTakenFactor;
	}
}
