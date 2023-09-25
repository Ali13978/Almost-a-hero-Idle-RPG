using System;
using Static;

namespace Simulation
{
	public class ShopPackXmas : ShopPack
	{
		public ShopPackXmas()
		{
			this.isOffer = true;
			this.name = "SHOP_PACK_HOLIDAY";
			this.isIAP = true;
			this.iapIndex = IapIds.XMAS_PACK;
			this.credits = 4000.0;
			this.tokensMin = 3000.0;
			this.tokensMax = 3000.0;
			this.scrapsMin = 3000.0;
			this.scrapsMax = 3000.0;
			this.totalTime = 43200.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			return ShopPackStarter.appeared && !ShopPackXmas.purchased;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.XMAS_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.XMAS_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			ShopPackXmas.purchased = true;
			PlayerStats.OnOfferAccepted(OfferId.XMAS_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 20f;
		}

		public static bool purchased;
	}
}
