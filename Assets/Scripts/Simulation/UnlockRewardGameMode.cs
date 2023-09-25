using System;

namespace Simulation
{
	public abstract class UnlockRewardGameMode : UnlockReward
	{
		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.NEW_MECHANIC;
			}
		}
	}
}
