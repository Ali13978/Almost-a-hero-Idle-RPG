using System;

namespace Simulation
{
	public class BuffDataPunishment : BuffData
	{
		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (!(attacker is Enemy))
			{
				return;
			}
			if (damage.amount <= 0.0)
			{
				return;
			}
			buff.IncreaseGenericCounter();
			if (!(attacker is UnitHealthy))
			{
				return;
			}
			UnitHealthy damaged = (UnitHealthy)attacker;
			if (buff.GetGenericCounter() >= this.numHitReq)
			{
				buff.ZeroGenericCounter();
				Unit by = buff.GetBy();
				Damage damage2 = new Damage(by.GetDpsTeam() * this.damageFactor, false, false, false, false);
				if (GameMath.GetProbabilityOutcome(by.GetCritChance(), GameMath.RandType.NoSeed))
				{
					damage2.amount *= by.GetCritFactor();
					damage2.isCrit = true;
				}
				buff.GetWorld().DamageMain(by, damaged, damage2);
			}
		}

		public override void OnDeathSelf(Buff buff)
		{
			buff.ZeroGenericCounter();
		}

		public int numHitReq;

		public double damageFactor;
	}
}
