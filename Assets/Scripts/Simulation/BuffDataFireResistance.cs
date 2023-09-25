using System;

namespace Simulation
{
	public class BuffDataFireResistance : BuffData
	{
		public BuffDataFireResistance(float heatMaxAdd)
		{
			this.heatMaxAdd = heatMaxAdd;
			this.id = 83;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemHeatMaxAdd += this.heatMaxAdd;
		}

		private float heatMaxAdd;
	}
}
