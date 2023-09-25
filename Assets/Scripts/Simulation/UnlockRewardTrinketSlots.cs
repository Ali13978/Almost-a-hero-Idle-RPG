using System;
using UnityEngine;

namespace Simulation
{
	public class UnlockRewardTrinketSlots : UnlockReward
	{
		public UnlockRewardTrinketSlots(int numSlotsToUnlock)
		{
			this.numSlotsToUnlock = numSlotsToUnlock;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return this.category;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.numTrinketSlots += this.numSlotsToUnlock;
			UnityEngine.Debug.Log("TRINKETS UNLOCKED");
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_TRINKET_SLOT"), this.numSlotsToUnlock);
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_TRINKET_SLOT"), this.numSlotsToUnlock);
		}

		public int numSlotsToUnlock;

		public UnlockReward.RewardCategory category = UnlockReward.RewardCategory.IMPORTANT_THING;
	}
}
