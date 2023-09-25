using System;

namespace Simulation
{
	public class BuffDataDamageAdd : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAddFactor += this.damageAdd;
		}

		public double damageAdd;
	}
}
