using System;

namespace Simulation
{
	public class UnlockRewardSkin : UnlockReward
	{
		public UnlockRewardSkin(int skinId)
		{
			this.skinId = skinId;
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
			sim.UnlockSkin(this.skinId);
		}

		public override string GetRewardString()
		{
			return "#SKINSKINSKIN";
		}

		public override string GetRewardedString()
		{
			return "#SKINSKINSKIN";
		}

		public int skinId;
	}
}
