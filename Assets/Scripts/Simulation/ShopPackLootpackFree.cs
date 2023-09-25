using System;

namespace Simulation
{
	public class ShopPackLootpackFree : ShopPack
	{
		public ShopPackLootpackFree()
		{
			this.isOffer = false;
			this.name = "UI_SHOP_CHEST_0_L";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 0.0;
			int[] array = new int[6];
			array[0] = 2;
			this.numGears = array;
			this.tokensMin = 5.0;
			this.tokensMax = 15.0;
			this.scrapsMin = 20.0;
			this.scrapsMax = 60.0;
			this.runeChance = 0f;
			this.numRunes = 0;
			this.undiscoveredGearAssuredFrequency = 12;
		}
	}
}
