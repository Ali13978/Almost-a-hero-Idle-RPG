using System;

namespace Simulation
{
	public class BuffDataDamageUndamaged : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.DecreaseLifeCounter();
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (target.GetHealthRatio() > this.healthFactor)
			{
				damage.amount *= 1.0 + this.damageFactor;
			}
		}

		public double damageFactor;

		public double healthFactor;
	}
}
