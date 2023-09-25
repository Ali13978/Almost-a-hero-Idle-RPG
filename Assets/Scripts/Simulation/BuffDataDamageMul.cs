using System;

namespace Simulation
{
	public class BuffDataDamageMul : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageMulFactor *= this.damageMul;
		}

		public double damageMul;
	}
}
