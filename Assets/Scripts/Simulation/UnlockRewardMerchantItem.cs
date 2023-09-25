using System;

namespace Simulation
{
	public class UnlockRewardMerchantItem : UnlockReward
	{
		public UnlockRewardMerchantItem(MerchantItem merchantItem)
		{
			this.merchantItem = merchantItem;
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
			foreach (World world2 in sim.GetAllWorlds())
			{
				foreach (MerchantItem merchantItem in world2.merchantItems)
				{
					if (merchantItem.GetId() == this.merchantItem.GetId())
					{
						merchantItem.SetLevel(0);
						return;
					}
				}
			}
			throw new EntryPointNotFoundException();
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_MERCHANT_ITEM"), this.merchantItem.GetTitleString());
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_MERCHANT_ITEM"), this.merchantItem.GetTitleString());
		}

		public string GetMerchantItemId()
		{
			return this.merchantItem.GetId();
		}

		private MerchantItem merchantItem;
	}
}
