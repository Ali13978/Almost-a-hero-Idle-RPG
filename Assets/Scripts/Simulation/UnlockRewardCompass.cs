using System;

namespace Simulation
{
	public class UnlockRewardCompass : UnlockReward
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
			sim.UnlockCompass();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_COMPASS");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_COMPASS");
		}
	}
}
