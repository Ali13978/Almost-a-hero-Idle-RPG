using System;

namespace Simulation
{
	public class BuffDataEarthSpirit : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (buff.GetTime() > (float)buff.GetGenericCounter() * this.chargeGainPeriod)
			{
				buff.IncreaseGenericCounter();
				Unit by = buff.GetBy();
				if (!(by is TotemEarth))
				{
					return;
				}
				TotemEarth totemEarth = (TotemEarth)by;
				totemEarth.AddCharge(1);
			}
		}

		public float chargeGainPeriod;
	}
}
