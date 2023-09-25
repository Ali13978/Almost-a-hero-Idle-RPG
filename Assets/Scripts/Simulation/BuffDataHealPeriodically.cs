using System;

namespace Simulation
{
	public class BuffDataHealPeriodically : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy == null || !unitHealthy.IsAlive())
			{
				return;
			}
			this.time += dt;
			if (this.time >= this.healingPeriod)
			{
				this.time -= this.healingPeriod;
				unitHealthy.Heal(this.healingRatio);
			}
		}

		public double healingRatio;

		public float healingPeriod;

		private float time;
	}
}
