using System;

namespace Simulation
{
	public class BuffDataDamageTotem : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy != null && unitHealthy.IsAlive())
			{
				totEffect.ringDamageFactor += this.damageAdd;
			}
		}

		public double damageAdd;
	}
}
