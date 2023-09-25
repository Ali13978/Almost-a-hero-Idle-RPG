using System;

namespace Simulation
{
	public class BuffDataRestlessBody : BuffData
	{
		public override void OnNewWave(Buff buff)
		{
			Unit by = buff.GetBy();
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = (UnitHealthy)by;
			unitHealthy.Heal(this.healRatio);
		}

		public double healRatio;
	}
}
