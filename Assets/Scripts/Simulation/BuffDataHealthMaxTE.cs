using System;

namespace Simulation
{
	public class BuffDataHealthMaxTE : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.healthMaxFactorTE += this.healthMaxFactorAdd;
		}

		public double healthMaxFactorAdd;
	}
}
