using System;

namespace Simulation
{
	public class BuffDataDamageReduction : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTakenFactor *= 1.0 - this.damageReductionFactor;
		}

		public double damageReductionFactor;
	}
}
