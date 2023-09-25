using System;

namespace Simulation
{
	public class BuffDataRemmants : BuffData
	{
		public override void OnDeathAny(Buff buff, UnitHealthy dead)
		{
			if (!GameMath.GetProbabilityOutcome(0.15f, GameMath.RandType.NoSeed))
			{
				return;
			}
			Unit by = buff.GetBy();
			if (!(by is TotemEarth))
			{
				return;
			}
			TotemEarth totemEarth = (TotemEarth)by;
			totemEarth.AddCharge(this.chargeAdd);
		}

		private const float CHANCE = 0.15f;

		public int chargeAdd;
	}
}
