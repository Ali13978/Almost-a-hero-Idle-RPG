using System;

namespace Simulation
{
	public class BuffDataDamageChest : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Enemy enemy = target as Enemy;
			if (enemy != null && enemy.IsChest())
			{
				damage.amount *= 1.0 + this.damageAdd;
			}
		}

		public double damageAdd;
	}
}
