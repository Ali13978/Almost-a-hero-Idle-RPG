using System;

namespace Simulation
{
	public class UnlockRewardCharmPack : UnlockReward
	{
		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.IMPORTANT_THING;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.numSmallCharmPacks++;
		}

		public override string GetRewardString()
		{
			return LM.Get("CHARM_PACK");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_CHARM_PACK");
		}
	}
}
