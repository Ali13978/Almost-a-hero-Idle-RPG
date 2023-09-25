using System;

namespace Simulation
{
	public class BuffDataBurningBalls : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAddFactor += this.damageFactorAdd;
			totEffect.totemHeatFactor += this.heatFactorAdd;
		}

		public double damageFactorAdd;

		public float heatFactorAdd;
	}
}
