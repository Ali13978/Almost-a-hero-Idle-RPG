using System;

namespace Simulation
{
	public class BuffDataPoweredSkill : BuffData
	{
		public BuffDataPoweredSkill(double skillPowerFactor)
		{
			this.skillPowerFactor = skillPowerFactor;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (damage.type == DamageType.SKILL && !unitHealthy.IsAlly(target))
			{
				damage.amount *= this.skillPowerFactor;
			}
		}

		public double skillPowerFactor;
	}
}
