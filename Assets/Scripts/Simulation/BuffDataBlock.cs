using System;

namespace Simulation
{
	public class BuffDataBlock : BuffData
	{
		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (!GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				return;
			}
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			unitHealthy.OnBlocked(attacker, damage, this.damageBlockFactor);
			damage.amount *= 1.0 - this.damageBlockFactor;
			damage.blockFactor = 1.0 - this.damageBlockFactor;
		}

		public float chance;

		public double damageBlockFactor;
	}
}
