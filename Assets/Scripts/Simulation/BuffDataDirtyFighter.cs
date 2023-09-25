using System;

namespace Simulation
{
	public class BuffDataDirtyFighter : BuffData
	{
		public BuffDataDirtyFighter(double damageFactorAdd)
		{
			this.damageFactorAdd = damageFactorAdd;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.IsAlly(target))
			{
				bool flag = target.buffTotalEffect.attackSpeedAdd < 0f || target.HasBuffStun();
				if (flag)
				{
					damage.amount *= this.damageFactorAdd;
				}
			}
		}

		public double damageFactorAdd;
	}
}
