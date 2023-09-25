using System;

namespace Simulation
{
	public class BuffDataRoar : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageMulFactor *= (double)this.heroDamageFactor;
		}

		public float heroDamageFactor;
	}
}
