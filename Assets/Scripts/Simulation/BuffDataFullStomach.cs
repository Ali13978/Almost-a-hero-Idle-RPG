using System;

namespace Simulation
{
	public class BuffDataFullStomach : BuffData
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
				totEffect.damageAddFactor += this.damageAdd;
			}
		}

		public double minHealthRatio;

		public double damageAdd;
	}
}
