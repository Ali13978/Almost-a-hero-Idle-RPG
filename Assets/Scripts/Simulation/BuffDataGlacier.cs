using System;

namespace Simulation
{
	public class BuffDataGlacier : BuffData
	{
		public BuffDataGlacier(double stunExtraDamage, double normalDamage)
		{
			this.stunExtraDamage = stunExtraDamage;
			this.normalDamage = normalDamage;
			this.id = 91;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (target.HasBuffStun())
			{
				damage.amount *= 1.0 + this.stunExtraDamage;
			}
			else
			{
				damage.amount *= 1.0 + this.normalDamage;
			}
		}

		private double stunExtraDamage;

		private double normalDamage;
	}
}
