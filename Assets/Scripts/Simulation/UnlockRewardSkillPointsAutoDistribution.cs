using System;

namespace Simulation
{
	public class UnlockRewardSkillPointsAutoDistribution : UnlockReward
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
			sim.UnlockSkillPoinstAutoDistribution();
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_SKILL_POINTS_RANDOM_DISTRIBUTION");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_SKILL_POINTS_RANDOM_DISTRIBUTION");
		}
	}
}
