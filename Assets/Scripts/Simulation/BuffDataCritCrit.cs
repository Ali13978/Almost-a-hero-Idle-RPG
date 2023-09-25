using System;

namespace Simulation
{
	public class BuffDataCritCrit : BuffData
	{
		public BuffDataCritCrit(float chance)
		{
			this.chance = chance;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isCrit && GameMath.GetProbabilityOutcome(this.chance, GameMath.RandType.NoSeed))
			{
				damage.amount *= 2.0;
			}
		}

		public float chance;
	}
}
