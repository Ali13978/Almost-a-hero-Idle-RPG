using System;

namespace Simulation
{
	public class BuffDataRemnants : BuffData
	{
		public BuffDataRemnants(float durationAdd)
		{
			this.durationAdd = durationAdd;
			this.id = 148;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemEarthMeteoriteTap = true;
			totEffect.totemEarthDurationAdd += this.durationAdd;
		}

		private float durationAdd;
	}
}
