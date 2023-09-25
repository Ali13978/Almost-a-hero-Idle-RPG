using System;
using Simulation;
using UnityEngine;

namespace Ui
{
	public class UiCommandServerRewardCollect : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			RewardOrigin rewardOrigin = this.rewardOrigin;
			if (rewardOrigin != RewardOrigin.Server)
			{
				if (rewardOrigin != RewardOrigin.InGame)
				{
					if (rewardOrigin == RewardOrigin.News)
					{
						this.GiveRewardLinkedToTitleNews(Main.DequeueNextNews(), sim);
					}
				}
				else
				{
					this.GivePendingReward(sim);
				}
			}
			else
			{
				this.GivePlayfabReward(sim);
			}
		}

		private void GivePlayfabReward(Simulator sim)
		{
			PlayfabManager.EraseReward(delegate(bool isSuccess)
			{
				if (isSuccess)
				{
					PlayfabManager.RewardData rewardToClaim = Main.rewardToClaim;
					Main.rewardToClaim = null;
					this.GiveReward(rewardToClaim, sim);
				}
			});
		}

		private void GivePendingReward(Simulator sim)
		{
			PlayfabManager.RewardData data = sim.rewardsToGive[0];
			sim.rewardsToGive.RemoveFastAt(0);
			this.GiveReward(data, sim);
		}

		private void GiveRewardLinkedToTitleNews(PlayfabManager.NewsData news, Simulator sim)
		{
			sim.lastNewsTimestam = news.dateTime;
			this.GiveReward(news.body.reward, sim);
		}

		private void GiveReward(PlayfabManager.RewardData data, Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			if (this.isHub)
			{
				int num = 0;
				if (data.amountCredits > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.GEM, data.amountCredits, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountScraps > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.SCRAP, data.amountScraps, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountMythstones > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.MYTHSTONE, data.amountMythstones, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountToken > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.TOKEN, data.amountToken, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountAeons > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.AEON, data.amountAeons, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountCandies > 0.0)
				{
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.CANDY, data.amountCandies, this.GetDropPosition(num++), 30, 0f, 0f, 1f, null, 0f);
				}
				if (data.amountTrinketBoxes > 0)
				{
					DropPosition dropPos = new DropPosition
					{
						startPos = Vector3.zero,
						endPos = -Vector3.up * 0.1f,
						invPos = this.shopTabButton.position,
						showSideCurrency = false
					};
					activeWorld.RainCurrencyOnUi(UiState.HUB, CurrencyType.TRINKET_BOX, (double)data.amountTrinketBoxes, dropPos, 30, 0f, 0f, 1f, null, 0f);
				}
			}
			else
			{
				this.Rain(activeWorld, CurrencyType.GEM, data.amountCredits);
				this.Rain(activeWorld, CurrencyType.MYTHSTONE, data.amountMythstones);
				this.Rain(activeWorld, CurrencyType.SCRAP, data.amountScraps);
				this.Rain(activeWorld, CurrencyType.TOKEN, data.amountToken);
				this.Rain(activeWorld, CurrencyType.AEON, data.amountAeons);
				this.Rain(activeWorld, CurrencyType.CANDY, data.amountCandies);
				this.Rain(activeWorld, CurrencyType.TRINKET_BOX, (double)data.amountTrinketBoxes);
			}
			if (data.skinIds != null)
			{
				foreach (int id in data.skinIds)
				{
					sim.AddBoughtSkin(sim.GetSkinData(id));
				}
			}
		}

		private DropPosition GetDropPosition(int index)
		{
			return new DropPosition
			{
				startPos = Vector3.zero,
				endPos = -Vector3.up * 0.1f,
				invPos = ((index >= this.currencySidesTop.Length) ? this.currencySidesTop[this.currencySidesTop.Length - 1].currencyFinalPosReference.position : this.currencySidesTop[index].currencyFinalPosReference.position),
				showSideCurrency = true
			};
		}

		private void Rain(World world, CurrencyType currencyType, double amount)
		{
			if (amount <= 0.0)
			{
				return;
			}
			int num;
			if (currencyType == CurrencyType.TRINKET_BOX)
			{
				num = (int)amount;
			}
			else
			{
				num = ((amount >= 30.0) ? 30 : ((int)amount));
			}
			double amount2 = amount / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.6f, 0.6f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.25f, 0.15f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				DropCurrency dropCurrency = new DropCurrency(currencyType, amount2, world, false);
				dropCurrency.InitVel(0f, startPos, vector.y, Vector3.zero);
				world.drops.Add(dropCurrency);
			}
		}

		public bool isHub;

		public PanelCurrencySide[] currencySidesTop;

		public RectTransform shopTabButton;

		public RewardOrigin rewardOrigin;
	}
}
