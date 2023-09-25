using System;

namespace Simulation
{
	public class ShopPackLootpackEpic : ShopPack
	{
		public ShopPackLootpackEpic()
		{
			this.isOffer = false;
			this.name = "UI_SHOP_CHEST_2_L";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 400.0;
			this.numGears = new int[]
			{
				0,
				2,
				2,
				2,
				0,
				0
			};
			this.tokensMin = 0.0;
			this.tokensMax = 0.0;
			this.scrapsMin = 0.0;
			this.scrapsMax = 0.0;
			this.runeChance = 0.5f;
			this.numRunes = 1;
			this.runeAssuredFrequency = 5;
		}
	}
}
