using System;

namespace Simulation
{
	public class BuffDataDamageTakenIncreased : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTakenFactor *= this.damageFactor;
		}

		public double damageFactor;
	}
}
