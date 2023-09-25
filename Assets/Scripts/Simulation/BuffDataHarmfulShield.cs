using System;

namespace Simulation
{
	public class BuffDataHarmfulShield : BuffData
	{
		public BuffDataHarmfulShield(double damageAdd)
		{
			this.damageAdd = damageAdd;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (target.IsAlly(unitHealthy))
			{
				return;
			}
			if (!unitHealthy.HasZeroShield())
			{
				damage.amount *= 1.0 + this.damageAdd;
			}
		}

		private double damageAdd;
	}
}
