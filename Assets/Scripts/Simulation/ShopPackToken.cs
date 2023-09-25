using System;
using Static;

namespace Simulation
{
	public class ShopPackToken : ShopPack
	{
		public ShopPackToken()
		{
			this.tags = ShopPack.Tags.STANDARD;
			this.isOffer = true;
			this.name = "SHOP_PACK_TOKEN";
			this.isIAP = false;
			this.currency = CurrencyType.GEM;
			this.cost = 750.0;
			this.tokensMin = 3000.0;
			this.tokensMax = 3000.0;
			this.totalTime = 43200.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			return ShopPackStarter.appeared;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.TOKEN_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.TOKEN_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.TOKEN_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			if (sim.GetTokens().GetAmount() < 100.0)
			{
				return 20f;
			}
			if (sim.GetTokens().GetAmount() > 2500.0)
			{
				return 1f;
			}
			return 5f;
		}
	}
}
