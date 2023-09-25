using System;
using Static;

namespace Simulation
{
	public class ShopPackRegional01 : ShopPackRegionalBase, IHasPackState
	{
		public ShopPackRegional01()
		{
			this.tags = ShopPack.Tags.REGIONAL;
			this.isOffer = true;
			this.name = "UI_REGIONAL_OFFER_POPUP_HEADER";
			this.isIAP = true;
			this.iapIndex = IapIds.REGIONAL_01;
			this.originalIapIndex = IapIds.GEM_PACK_04;
			this.discountPercentage = 0.5f;
			this.credits = 6000.0;
			this.totalTime = 604800.0;
			this.shouldBeAnnounceWithPopup = true;
			this.id = new OfferId?(OfferId.REGIONAL_01);
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackRegional01.purchased;
			}
			set
			{
				ShopPackRegional01.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackRegional01.appeared;
			}
			set
			{
				ShopPackRegional01.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackRegional01.purchased && base.CanAppear(sim);
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackRegional01.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.REGIONAL_01);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.REGIONAL_01);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			ShopPackRegional01.purchased = true;
			PlayerStats.OnOfferAccepted(OfferId.REGIONAL_01);
		}

		public static bool purchased;

		public static bool appeared;
	}
}
