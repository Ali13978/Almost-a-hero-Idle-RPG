using System;
using System.Collections.Generic;
using Simulation;

namespace Ui
{
	public class UiCommandConvertCandyIntoScraps : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			Currency candies = sim.GetCandies();
			List<Simulation.MerchantItem> eventMerchantItems = sim.GetWorld(GameMode.STANDARD).eventMerchantItems;
			candies.Increment(Math.Floor((double)eventMerchantItems[0].GetNumInInventory() * 225.0));
			candies.Increment(Math.Floor((double)eventMerchantItems[1].GetNumInInventory() * 338.0));
			candies.Increment(Math.Floor((double)eventMerchantItems[2].GetNumInInventory() * 281.0));
			for (int i = eventMerchantItems.Count - 1; i >= 0; i--)
			{
				eventMerchantItems[i].SetNumInInventory(0);
			}
			double amount = candies.GetAmount();
			sim.GetCandies().InitZero();
			sim.GetActiveWorld().RainCurrencyOnUi(UiState.CONVERT_XMAS_SCRAP, CurrencyType.SCRAP, Math.Floor(amount * 0.1), this.dropPosition, 30, 0f, 0f, 1f, null, 0f);
		}

		public DropPosition dropPosition;
	}
}
