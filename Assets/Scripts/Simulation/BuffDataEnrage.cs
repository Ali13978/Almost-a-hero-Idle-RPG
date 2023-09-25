using System;

namespace Simulation
{
	public class BuffDataEnrage : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)buff.GetBy();
			if (unitHealthy.GetHealthRatio() <= this.healthRatioMin)
			{
				totEffect.attackSpeedAdd += this.attackSpeedAdd;
			}
		}

		public double healthRatioMin;

		public float attackSpeedAdd;
	}
}
