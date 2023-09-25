using System;

namespace Simulation
{
	public class BuffDataResistance : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemHeatMaxAdd += this.heatMaxAdd;
		}

		public float heatMaxAdd;
	}
}
