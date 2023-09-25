using System;

namespace Simulation
{
	public class ShopPackCharmPackSmall : ShopPack
	{
		public ShopPackCharmPackSmall()
		{
			this.isOffer = false;
			this.name = "CHARM_PACK";
			this.isIAP = false;
			this.currency = CurrencyType.AEON;
			this.cost = 400.0;
			this.numCharms = 1;
		}
	}
}
