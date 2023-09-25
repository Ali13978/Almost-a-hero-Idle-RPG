using System;
using Static;

namespace Simulation
{
	public class ShopPackFiveTrinkets : ShopPack
	{
		public ShopPackFiveTrinkets()
		{
			this.tags = ShopPack.Tags.STANDARD;
			this.isOffer = true;
			this.name = "SHOP_PACK_FIVE_TRINKET";
			this.isIAP = false;
			this.numTrinketPacks = 5;
			this.totalTime = 28800.0;
			this.currency = CurrencyType.GEM;
			this.cost = 1000.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			return false;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.FIVE_TRINKETS_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.FIVE_TRINKETS_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.FIVE_TRINKETS_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return (sim.numTrinketSlots <= 0) ? 0f : 8f;
		}
	}
}
