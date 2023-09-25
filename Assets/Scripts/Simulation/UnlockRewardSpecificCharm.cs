using System;

namespace Simulation
{
	public class UnlockRewardSpecificCharm : UnlockReward
	{
		public UnlockRewardSpecificCharm(CharmEffectData ced, int numDuplicates)
		{
			this.ced = ced;
			this.numDuplicates = numDuplicates;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.IMPORTANT_THING;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.AddCharmCard(this.ced.BaseData.id, this.numDuplicates);
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_SPECIFIC_CHARM"), this.numDuplicates.ToString(), LM.Get(this.ced.nameKey));
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_SPECIFIC_CHARM"), this.numDuplicates.ToString(), LM.Get(this.ced.nameKey));
		}

		public CharmEffectData ced;

		public int numDuplicates;
	}
}
