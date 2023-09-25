using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	public class SaveDataMigrator_2_9_0 : SaveDataMigrator
	{
		public SaveDataMigrator_2_9_0()
		{
			this.comingVersion = new Version("2.9.0");
		}

		protected override SaveData Migrate(SaveData saveData)
		{
			StoreManager.christmasEventNotifications = true;
			StoreManager.flashOffersNotifications = true;
			if (saveData.notifs != -2147483648)
			{
				saveData.notifs = (saveData.notifs | 64 | 16);
			}
			if (saveData.collectedUnlockIds != null)
			{
				List<uint> list = new List<uint>
				{
					UnlockIds.RIFT_REWARD_018,
					UnlockIds.RIFT_REWARD_024,
					UnlockIds.RIFT_REWARD_031,
					UnlockIds.RIFT_REWARD_035,
					UnlockIds.RIFT_REWARD_041,
					UnlockIds.RIFT_REWARD_046,
					UnlockIds.RIFT_REWARD_056,
					UnlockIds.RIFT_REWARD_064,
					UnlockIds.RIFT_REWARD_075,
					UnlockIds.RIFT_REWARD_076,
					UnlockIds.RIFT_REWARD_077,
					UnlockIds.RIFT_REWARD_083,
					UnlockIds.RIFT_REWARD_088,
					UnlockIds.RIFT_REWARD_097,
					UnlockIds.RIFT_REWARD_098,
					UnlockIds.RIFT_REWARD_103,
					UnlockIds.RIFT_REWARD_108,
					UnlockIds.RIFT_REWARD_113,
					UnlockIds.RIFT_REWARD_118
				};
				PlayfabManager.RewardData rewardData = new PlayfabManager.RewardData
				{
					title = LM.Get("TRINKETS_REWARD_FROM_GOG_HEADER"),
					desc = LM.Get("TRINKETS_REWARD_FROM_GOG_BODY")
				};
				foreach (uint item in saveData.collectedUnlockIds)
				{
					if (list.Contains(item))
					{
						rewardData.amountTrinketBoxes += 4;
					}
				}
				if (rewardData.HasReward())
				{
					if (saveData.rewardsToGive == null)
					{
						saveData.rewardsToGive = new List<PlayfabManager.RewardData>();
					}
					saveData.rewardsToGive.Add(rewardData);
				}
			}
			if (saveData.newStats == null)
			{
				saveData.newStats = new List<int>
				{
					170,
					180,
					200,
					210,
					70,
					190
				};
			}
			return saveData;
		}
	}
}
