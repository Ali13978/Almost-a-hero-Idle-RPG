using System;

namespace Simulation
{
	public class ShopPackLootpackRare : ShopPack
	{
		public ShopPackLootpackRare()
		{
			this.isOffer = false;
			this.name = "UI_SHOP_CHEST_1_L";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 100.0;
			this.numGears = new int[]
			{
				3,
				1,
				1,
				0,
				0,
				0
			};
			this.tokensMin = 15.0;
			this.tokensMax = 45.0;
			this.scrapsMin = 30.0;
			this.scrapsMax = 90.0;
			this.runeChance = 0f;
			this.numRunes = 0;
			this.spamProtection = true;
			this.undiscoveredGearAssuredFrequency = 5;
		}
	}
}
