using System;

namespace Simulation
{
	public class BuffDataWeakPoint : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (target.HasBuffStun() || target.GetAttackSpeed() < 1f)
			{
				damage.amount += buff.GetBy().GetDpsTeam() * this.damageFactor;
			}
		}

		public double damageFactor;
	}
}
