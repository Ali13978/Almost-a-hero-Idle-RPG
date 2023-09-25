using System;

namespace Simulation
{
	public class ShopPackCharmPackBig : ShopPack
	{
		public ShopPackCharmPackBig()
		{
			this.isOffer = false;
			this.name = "CHARM_PACK_BIG";
			this.isIAP = false;
			this.currency = CurrencyType.AEON;
			this.cost = 3600.0;
			this.numCharms = 9;
		}
	}
}
