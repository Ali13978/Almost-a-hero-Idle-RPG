using System;

namespace Simulation
{
	public class UnlockRewardMythicalArtifactSlot : UnlockReward
	{
		public UnlockRewardMythicalArtifactSlot(int numSlotsToUnlock)
		{
			this.numSlotsToUnlock = numSlotsToUnlock;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return this.defaultRewardCategory;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.artifactsManager.NumArtifactSlotsMythical += this.numSlotsToUnlock;
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_MYTH_ART_SLOT");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_MYTH_ART_SLOT");
		}

		public int numSlotsToUnlock;

		public UnlockReward.RewardCategory defaultRewardCategory = UnlockReward.RewardCategory.IMPORTANT_THING;
	}
}
