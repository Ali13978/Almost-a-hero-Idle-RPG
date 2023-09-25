using System;

namespace Simulation
{
	public class BuffDataTEExtraLoot : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			killed.statCache.goldToDrop *= this.goldFactor;
		}

		public double goldFactor;
	}
}
