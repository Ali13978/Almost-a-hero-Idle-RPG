using System;

namespace Simulation
{
	public class BuffDataHealthMax : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.healthMaxFactor += this.healthMaxAdd;
		}

		public double healthMaxAdd;
	}
}
