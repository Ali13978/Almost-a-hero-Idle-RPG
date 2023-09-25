using System;

namespace Simulation
{
	public class UnlockRewardMerchant : UnlockReward
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
			sim.UnlockMerchant();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_MERCHANT");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_MERCHANT");
		}
	}
}
