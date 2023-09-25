using System;

namespace Simulation
{
	public class UnlockRewardTrinketPack : UnlockReward
	{
		public UnlockRewardTrinketPack()
		{
			this.Amount = 1;
		}

		public UnlockRewardTrinketPack(int amount)
		{
			this.Amount = amount;
		}

		public int Amount { get; private set; }

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.IMPORTANT_THING;
			}
		}

		public override void Give(Simulator sim, World world)
		{
			sim.numTrinketPacks += this.Amount;
		}

		public override string GetRewardString()
		{
			return (this.Amount <= 1) ? LM.Get("UNLOCK_REWARD_TRINKET_PACK") : string.Format(LM.Get("SHOP_PACK_FIVE_TRINKET_DESC"), this.Amount);
		}

		public override string GetRewardedString()
		{
			return (this.Amount <= 1) ? LM.Get("UNLOCK_REWARDED_TRINKET_PACK") : string.Format(LM.Get("UNLOCK_REWARDED_TRINKET_PACK_MULTIPLE"), this.Amount);
		}
	}
}
