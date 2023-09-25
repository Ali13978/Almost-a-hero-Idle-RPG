using System;

namespace Simulation
{
	public class BuffDataDamageTotemInSelfDamage : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (unitHealthy.IsAlive())
			{
				double damage = unitHealthy.GetDamage();
				totEffect.ringDamageAdd += damage * this.factor;
			}
		}

		public double factor;
	}
}
