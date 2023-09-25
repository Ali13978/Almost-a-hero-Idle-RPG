using System;

namespace Simulation
{
	public class BuffDataDamageGlobalTE : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroDamageFactorTE += this.damageAdd;
		}

		public double damageAdd;
	}
}
