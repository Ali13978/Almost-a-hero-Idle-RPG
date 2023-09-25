using System;

namespace Simulation
{
	public class ShopPackTrinket : ShopPack
	{
		public ShopPackTrinket()
		{
			this.isOffer = false;
			this.name = "SHOP_PACK_TRINKET";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 50.0;
			this.numTrinkets = 1;
		}

		public CurrencyType specialCurrency = CurrencyType.AEON;

		public double specialCost = 300.0;
	}
}
