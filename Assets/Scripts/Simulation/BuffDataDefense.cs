using System;

namespace Simulation
{
	public class BuffDataDefense : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTakenFactor *= this.damageTakenFactor;
		}

		public double damageTakenFactor;
	}
}
