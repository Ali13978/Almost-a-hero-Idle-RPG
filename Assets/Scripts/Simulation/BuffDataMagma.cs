using System;

namespace Simulation
{
	public class BuffDataMagma : BuffData
	{
		public BuffDataMagma(double critFactor)
		{
			this.critFactor = critFactor;
			this.id = 123;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.critFactorAdd += this.critFactor;
		}

		private double critFactor;
	}
}
