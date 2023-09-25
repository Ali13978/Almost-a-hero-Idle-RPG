using System;

namespace Simulation
{
	public class UnlockRewardTrinketEnch : UnlockReward
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
			sim.hasTrinketSmith = true;
		}

		public override string GetRewardString()
		{
			return LM.Get("WIKI_TRINKET_SMITHING_TITLE");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_FORGING");
		}
	}
}
