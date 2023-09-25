using System;
using Static;

namespace Simulation
{
	public class ShopPackChristmasGemsBig : ShopPack, IHasPackState
	{
		public ShopPackChristmasGemsBig()
		{
			this.tags = ShopPack.Tags.OUT_OF_SHOP;
			this.isOffer = true;
			this.name = "SHOP_PACK_CHRISTMAS_GEMS_BIG";
			this.isIAP = true;
			this.iapIndex = IapIds.CHRISTMAS_GEMS_BIG_PACK;
			this.credits = 2000.0;
			this.candies = 60000.0;
			this.id = new OfferId?(OfferId.CHRISTMAS_GEMS_BIG);
			this.shouldBeAnnounceWithPopup = true;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackChristmasGemsBig.purchased;
			}
			set
			{
				ShopPackChristmasGemsBig.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackChristmasGemsBig.appeared;
			}
			set
			{
				ShopPackChristmasGemsBig.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (ShopPackChristmasGemsBig.purchased || !PlayfabManager.christmasOfferConfigLoaded || !TrustedTime.IsReady())
			{
				return false;
			}
			if (this.totalTime == 0.0)
			{
				this.timeStart = PlayfabManager.christmasOfferConfig.offerConfig.startDateParsed;
				this.totalTime = (PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed - this.timeStart).TotalSeconds;
			}
			DateTime t = TrustedTime.Get();
			return t >= this.timeStart && t < PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.CHRISTMAS_GEMS_BIG);
			ShopPackChristmasGemsBig.appeared = true;
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.CHRISTMAS_GEMS_BIG);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.CHRISTMAS_GEMS_BIG);
			ShopPackChristmasGemsBig.purchased = true;
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
			return LM.Get("SHOP_PACK_CHRISTMAS_GEMS_BIG");
		}

		public static bool appeared;

		public static bool purchased;

		public static bool dateInitialized;
	}
}
