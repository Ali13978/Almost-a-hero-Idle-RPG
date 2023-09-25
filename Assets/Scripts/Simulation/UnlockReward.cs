using System;

namespace Simulation
{
	public abstract class UnlockReward
	{
		public abstract void Give(Simulator sim, World world);

		public abstract string GetRewardString();

		public abstract string GetRewardedString();

		public abstract UnlockReward.RewardCategory rewardCategory { get; }

		public enum RewardCategory
		{
			NEW_MECHANIC,
			IMPORTANT_THING,
			HERO,
			CURRENCY
		}
	}
}
