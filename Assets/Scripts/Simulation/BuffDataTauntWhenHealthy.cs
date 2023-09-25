using System;

namespace Simulation
{
	public class BuffDataTauntWhenHealthy : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			if (unitHealthy.GetHealthRatio() >= this.minHealthRatio)
			{
				totEffect.tauntAdd += this.tauntAdd;
			}
		}

		public double minHealthRatio;

		public float tauntAdd;
	}
}
