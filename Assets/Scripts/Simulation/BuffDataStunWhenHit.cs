using System;

namespace Simulation
{
	public class BuffDataStunWhenHit : BuffData
	{
		public BuffDataStunWhenHit()
		{
			this.isStackable = false;
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			UnitHealthy unitHealthy = attacker as UnitHealthy;
			if (unitHealthy == null || !unitHealthy.IsAlive())
			{
				return;
			}
			UnitHealthy unitHealthy2 = buff.GetBy() as UnitHealthy;
			if (unitHealthy2.IsAlly(unitHealthy))
			{
				return;
			}
			if (GameMath.GetProbabilityOutcome(this.stunChance, GameMath.RandType.NoSeed))
			{
				unitHealthy.AddBuff(new BuffDataStun
				{
					dur = this.stunDuration,
					id = 325
				}, 0, false);
			}
		}

		public float stunChance;

		public float stunDuration;
	}
}
