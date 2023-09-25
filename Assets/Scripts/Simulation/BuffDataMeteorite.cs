using System;

namespace Simulation
{
	public class BuffDataMeteorite : BuffData
	{
		public BuffDataMeteorite(float meteoritePeriod, double damageMeteorites, float durationAdd)
		{
			this.meteoritePeriod = meteoritePeriod;
			this.damageMeteorites = damageMeteorites;
			this.durationAdd = durationAdd;
			this.timer = meteoritePeriod;
			this.id = 130;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemEarthDurationAdd += this.durationAdd;
			this.timer -= dt;
			if (this.timer <= 0f)
			{
				this.timer = this.meteoritePeriod;
				totEffect.totemEarthMeteoriteAuto = true;
			}
		}

		public float meteoritePeriod;

		public double damageMeteorites = 0.25;

		public float durationAdd = 3f;

		private float timer;
	}
}
