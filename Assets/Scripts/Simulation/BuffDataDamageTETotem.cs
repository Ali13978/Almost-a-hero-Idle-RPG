using System;

namespace Simulation
{
	public class BuffDataDamageTETotem : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.ringDamageTEFactor += this.damageAdd;
		}

		public double damageAdd;
	}
}
