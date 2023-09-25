using System;

namespace Simulation
{
	public class UnlockRewardDailies : UnlockReward
	{
		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.NEW_MECHANIC;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.UnlockDailies();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_DAILIES");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_DAILIES");
		}
	}
}
