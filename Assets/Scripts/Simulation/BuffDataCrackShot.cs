using System;

namespace Simulation
{
	public class BuffDataCrackShot : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (buff.GetWorld().GetNumAliveEnemies() > 1)
			{
				return;
			}
			if (target.IsAlly(buff.GetBy()))
			{
				return;
			}
			damage.amount *= this.damageFactor;
		}

		public double damageFactor;
	}
}
