using System;

namespace Simulation
{
	public class BuffDataRecycleEarth : BuffData
	{
		public BuffDataRecycleEarth(float durationRecharge)
		{
			this.durationRecharge = durationRecharge;
			this.id = 146;
		}

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			(buff.GetBy() as TotemEarth).RechargeMeteorShower(this.durationRecharge);
		}

		private float durationRecharge;
	}
}
