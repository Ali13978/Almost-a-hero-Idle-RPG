using System;
using Static;

namespace Simulation
{
	public class ShopPackHalloweenGems : ShopPack, IHasPackState
	{
		public ShopPackHalloweenGems()
		{
			this.tags = ShopPack.Tags.SEASONAL;
			this.isOffer = true;
			this.name = "SHOP_PACK_HALLOWEEN_GEMS";
			this.isIAP = true;
			this.iapIndex = IapIds.HALLOWEEN_GEMS_PACK;
			this.credits = 2250.0;
			this.id = new OfferId?(OfferId.HALLOWEEN_GEMS);
			this.shouldBeAnnounceWithPopup = true;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackHalloweenGems.purchased;
			}
			set
			{
				ShopPackHalloweenGems.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackHalloweenGems.appeared;
			}
			set
			{
				ShopPackHalloweenGems.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (ShopPackHalloweenGems.purchased || !PlayfabManager.halloweenOfferConfigLoaded || !TrustedTime.IsReady())
			{
				return false;
			}
			if (this.totalTime == 0.0)
			{
				this.timeStart = PlayfabManager.halloweenOfferConfig.startDateParsed;
				this.totalTime = (PlayfabManager.halloweenOfferConfig.endDateParsed - this.timeStart).TotalSeconds;
			}
			DateTime t = TrustedTime.Get();
			return t >= this.timeStart && t < PlayfabManager.halloweenOfferConfig.endDateParsed;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.HALLOWEEN_GEMS);
			ShopPackHalloweenGems.appeared = true;
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.HALLOWEEN_GEMS);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.HALLOWEEN_GEMS);
			ShopPackHalloweenGems.purchased = true;
		}

		public override bool ShouldOverrideExisting(Simulator sim)
		{
			return true;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 300f;
		}

		public override string GetName()
		{
			return LM.Get("SHOP_PACK_HALLOWEEN_GEMS");
		}

		public static bool appeared;

		public static bool purchased;
	}
}
