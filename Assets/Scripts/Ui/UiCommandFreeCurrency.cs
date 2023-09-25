using System;
using System.Collections.Generic;
using Simulation;
using Static;

namespace Ui
{
	public class UiCommandFreeCurrency : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.GetActiveWorld().shouldSave = true;
			RewardedAdManager.inst.PrepareToShowRewardedVideoCapped(sim, this.currencyType, this.rewardAmount);
			if (this.currencyType == CurrencyType.GEM)
			{
				PlayerStats.OnFreeCredits();
			}
			else if (this.currencyType == CurrencyType.CANDY && sim.christmasTreatVideosWatchedSinceLastReset + 1 == 3)
			{
				sim.christmasCandyCappedVideoNotificationSeen = false;
			}
			if (this.currencyType == CurrencyType.GEM)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.AD_STARTED_SHOP, new Dictionary<string, object>
				{
					{
						"reward_currency",
						this.currencyType
					},
					{
						"reward_amount",
						this.rewardAmount
					}
				}, null, null, true);
			}
		}

		public CurrencyType currencyType;

		public double rewardAmount;
	}
}
