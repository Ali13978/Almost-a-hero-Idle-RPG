using System;

namespace Simulation
{
	public class UnlockRewardHero : UnlockReward
	{
		public UnlockRewardHero(string heroId, string heroNameKey)
		{
			this.heroId = heroId;
			this.heroNameKey = heroNameKey;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.HERO;
			}
		}

		public string GetHeroId()
		{
			return this.heroId;
		}

		public override void Give(Simulator sim, World world)
		{
			sim.UnlockHero(this.heroId);
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_HERO"), LM.Get(this.heroNameKey));
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_HERO"), LM.Get(this.heroNameKey));
		}

		private string heroId;

		private string heroNameKey;
	}
}
