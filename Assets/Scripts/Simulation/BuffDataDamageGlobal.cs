using System;

namespace Simulation
{
	public class BuffDataDamageGlobal : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroDamageFactor += this.damageAdd;
		}

		public double damageAdd;
	}
}
