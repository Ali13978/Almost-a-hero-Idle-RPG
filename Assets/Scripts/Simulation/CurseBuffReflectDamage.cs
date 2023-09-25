using System;

namespace Simulation
{
	public class CurseBuffReflectDamage : CurseBuff
	{
		public override void OnPostDamage(Unit damager, UnitHealthy damaged, Damage damage)
		{
			if (this.state == EnchantmentBuffState.INACTIVE)
			{
				return;
			}
			if (!(damager is Hero))
			{
				return;
			}
			UnitHealthy damaged2 = damager as UnitHealthy;
			Damage copy = damage.GetCopy();
			copy.amount *= this.reflectFactor;
			if (copy.blockFactor > 0.0)
			{
				copy.amount *= 1.0 / damage.blockFactor;
			}
			this.world.DamageMain(null, damaged2, copy);
		}

		public override void OnDeathAny(Unit unit)
		{
			if (unit is Hero)
			{
				this.AddProgress(this.pic);
			}
		}

		public double reflectFactor;
	}
}
