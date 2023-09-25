using System;

namespace Simulation
{
	public abstract class ChallengeUpgrade
	{
		public abstract string GetDescription(World world);

		public abstract void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal);

		public double GetCost(World w)
		{
			return this.cost * w.universalBonus.milestoneCostFactor;
		}

		public int stageReq;

		public int waveReq;

		public double cost;
	}
}
