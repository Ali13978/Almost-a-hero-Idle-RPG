using System;

namespace Simulation
{
	public class BuffDataEvasion : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			if (unitHealthy.GetHealthRatio() > this.healthRatioMax)
			{
				return;
			}
			totEffect.dodgeChanceAdd += this.dodgeChanceAdd;
		}

		public double healthRatioMax;

		public float dodgeChanceAdd;
	}
}
