using System;

namespace Simulation
{
	public class UnlockRewardMineScrap : UnlockReward
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
			sim.UnlockMineScrap();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_MINE_SCRAP");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_MINE_SCRAP");
		}
	}
}
