using System;

namespace Simulation
{
	public class BuffDataProtectedShield : BuffData
	{
		public BuffDataProtectedShield(double defense)
		{
			this.defense = defense;
		}

		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.HasZeroShield())
			{
				damage.amount *= 1.0 - this.defense;
			}
		}

		private double defense;
	}
}
