using System;

namespace Simulation
{
	public class BuffDataDealDamage : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (buff.GetBy() is UnitHealthy)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				unitHealthy.TakeDamage(new Damage(unitHealthy.GetHealthMax() * this.damagePer, false, false, false, false)
				{
					isPure = true,
					isExact = true
				}, unitHealthy, 0.001);
				buff.DecreaseLifeCounter();
			}
		}

		public double damagePer;
	}
}
