using System;
using System.Collections.Generic;
using Simulation;
using SocialRewards;
using UnityEngine;

namespace Ui
{
	public class UiCommandFreeCurrencyCollect : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			if (this.isHubShop)
			{
				activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, this.rewardCurrencyType, this.rewardAmount, this.dropPosition, 30, 0f, 0f, 1f, null, 0f);
			}
			else
			{
				int num = 10;
				if (this.rewardAmount < (double)num)
				{
					num = (int)this.rewardAmount;
				}
				double amount = this.rewardAmount / (double)num;
				for (int i = 0; i < num; i++)
				{
					Vector3 adDragonPos = activeWorld.adDragonPos;
					Vector3 vector = new Vector3(activeWorld.adDragonPos.x + GameMath.GetRandomFloat(-0.3f, 0.3f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.4f, 0.2f, GameMath.RandType.NoSeed), 0f);
					Vector3 velStart = new Vector3(GameMath.GetRandomFloat(0.8f, -0.8f, GameMath.RandType.NoSeed) - adDragonPos.x, GameMath.GetRandomFloat(1.2f, 1.8f, GameMath.RandType.NoSeed), 0f);
					DropCurrency dropCurrency = new DropCurrency(this.rewardCurrencyType, amount, activeWorld, false);
					dropCurrency.InitVel(0f, adDragonPos, vector.y, velStart);
					activeWorld.drops.Add(dropCurrency);
				}
			}
			if (this.isSocialReward)
			{
				Manager.OnRewardGiven(this.socialNetwork);
			}
			if (!this.isSocialReward && this.rewardCurrencyType == CurrencyType.GEM)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.AD_COLLECTED_SHOP, new Dictionary<string, object>
				{
					{
						"reward_currency",
						this.rewardCurrencyType
					},
					{
						"reward_amount",
						this.rewardAmount
					}
				}, null, null, true);
			}
			if (this.rewardCurrencyType == CurrencyType.CANDY)
			{
				sim.christmasTreatVideosWatchedSinceLastReset++;
			}
		}

		public bool isHubShop;

		public bool isSocialReward;

		public SocialRewards.Network socialNetwork;

		public DropPosition dropPosition;

		public CurrencyType rewardCurrencyType;

		public double rewardAmount;
	}
}
