using System;

namespace Simulation
{
	public class BuffDataDamageArea : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAreaFactor += this.damageAdd;
		}

		public double damageAdd;
	}
}
