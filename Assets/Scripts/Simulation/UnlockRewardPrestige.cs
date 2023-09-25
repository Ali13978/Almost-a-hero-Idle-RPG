using System;

namespace Simulation
{
	public class UnlockRewardPrestige : UnlockReward
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
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_PRESTIGE");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_PRESTIGE");
		}
	}
}
