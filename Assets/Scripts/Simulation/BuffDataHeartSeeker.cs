using System;

namespace Simulation
{
	public class BuffDataHeartSeeker : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (damage.isCrit)
			{
				return;
			}
			if (target.GetHealthRatio() > this.healthRatioLimit)
			{
				return;
			}
			if (!GameMath.GetProbabilityOutcome(this.critChance, GameMath.RandType.NoSeed))
			{
				return;
			}
			damage.isCrit = true;
			damage.amount *= buff.GetBy().GetCritFactor();
		}

		public double healthRatioLimit;

		public float critChance;
	}
}
