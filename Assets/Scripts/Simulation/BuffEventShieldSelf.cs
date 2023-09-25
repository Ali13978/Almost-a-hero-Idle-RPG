using System;

namespace Simulation
{
	public class BuffEventShieldSelf : BuffEvent
	{
		public override void Apply(Unit by, World world)
		{
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			unitHealthy.GainShield(this.ratio, this.dur);
		}

		public double ratio;

		public float dur;
	}
}
