using System;

namespace Simulation
{
	public class BuffDataElegance : BuffData
	{
		public override void OnDodged(Buff buff, Unit attacker, Damage damage)
		{
			if ((float)buff.GetGenericCounter() * this.durAdd < this.durTotMax)
			{
				buff.IncreaseGenericCounter();
				buff.GetBy().IncrementDurationOfBuff(typeof(BuffDataSwiftMoves), this.durAdd);
			}
		}

		public float durAdd;

		public float durTotMax;
	}
}
