using System;

namespace Simulation
{
	public class BuffDataBasicAttacksDamage : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.type == DamageType.NONE)
			{
				damage.amount *= this.damageFactor;
			}
		}

		public double damageFactor;
	}
}
