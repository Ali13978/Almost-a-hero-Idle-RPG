using System;

namespace Simulation
{
	public class BuffDataDamageTE : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTEFactor += this.damageAdd;
		}

		public double damageAdd;
	}
}
