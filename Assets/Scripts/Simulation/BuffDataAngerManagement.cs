using System;

namespace Simulation
{
	public class BuffDataAngerManagement : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			int genericCounter = buff.GetGenericCounter();
			float num = (float)genericCounter * this.durInc;
			if (num < this.durMax)
			{
				buff.IncreaseGenericCounter();
				buff.GetBy().IncrementDurationOfBuff(typeof(BuffDataAnger), this.durInc);
			}
		}

		public float durInc;

		public float durMax;
	}
}
