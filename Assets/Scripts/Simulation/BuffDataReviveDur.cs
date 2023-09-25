using System;

namespace Simulation
{
	public class BuffDataReviveDur : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.reviveDurFactor *= this.reviveDurFactorFactor;
		}

		public float reviveDurFactorFactor;
	}
}
