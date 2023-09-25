using System;

namespace Simulation
{
	public class UnlockRewardMineToken : UnlockReward
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
			sim.UnlockMineToken();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_MINE_TOKEN");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_MINE_TOKEN");
		}
	}
}
