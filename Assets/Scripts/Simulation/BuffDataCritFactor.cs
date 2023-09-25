using System;

namespace Simulation
{
	public class BuffDataCritFactor : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.critFactorAdd += this.critFactorAdd;
		}

		public double critFactorAdd;
	}
}
