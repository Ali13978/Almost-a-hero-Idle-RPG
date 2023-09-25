using System;

namespace Simulation
{
	public class BuffDataShieldOnStunnedOpponentHit : BuffData
	{
		public BuffDataShieldOnStunnedOpponentHit(float shieldAmount)
		{
			this.shieldAmount = shieldAmount;
		}

		public override void OnOpponentTakenDamage(Buff buff, Unit opponent, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = attacker as UnitHealthy;
			if (unitHealthy == null || !unitHealthy.IsAlive() || !opponent.HasBuffStun())
			{
				return;
			}
			UnitHealthy unitHealthy2 = buff.GetBy() as UnitHealthy;
			if (unitHealthy2 != unitHealthy && unitHealthy2.IsAlly(unitHealthy))
			{
				unitHealthy.GainShield((double)this.shieldAmount, float.PositiveInfinity);
			}
		}

		private float shieldAmount;
	}
}
