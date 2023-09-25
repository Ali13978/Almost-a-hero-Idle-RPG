using System;
using Static;

namespace Simulation
{
	public class ShopPackCurrency : ShopPack
	{
		public ShopPackCurrency()
		{
			this.tags = ShopPack.Tags.STANDARD;
			this.isOffer = true;
			this.name = "SHOP_PACK_CURRENCY";
			this.isIAP = true;
			this.iapIndex = IapIds.CURRENCY_PACK;
			this.credits = 2000.0;
			this.tokensMin = 2000.0;
			this.tokensMax = 2000.0;
			this.scrapsMin = 2000.0;
			this.scrapsMax = 2000.0;
			this.totalTime = 43200.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			return ShopPackStarter.appeared;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.CURRENCY_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.CURRENCY_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.CURRENCY_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			if (sim.GetCredits().GetAmount() < 1000.0)
			{
				return 8f;
			}
			if (sim.GetScraps().GetAmount() < 1000.0)
			{
				return 7f;
			}
			if (sim.GetTokens().GetAmount() < 1000.0)
			{
				return 6f;
			}
			return 1f;
		}
	}
}
