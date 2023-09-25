using System;
using Static;

namespace Simulation
{
	public class ShopPackChristmasGemsSmall : ShopPack, IHasPackState
	{
		public ShopPackChristmasGemsSmall()
		{
			this.tags = ShopPack.Tags.OUT_OF_SHOP;
			this.isOffer = true;
			this.name = LM.Get("SHOP_PACK_CHRISTMAS_GEMS_SMALL");
			this.isIAP = true;
			this.iapIndex = IapIds.CHRISTMAS_GEMS_SMALL_PACK;
			this.credits = 1000.0;
			this.candies = 15000.0;
			this.id = new OfferId?(OfferId.CHRISTMAS_GEMS_SMALL);
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackChristmasGemsSmall.purchased;
			}
			set
			{
				ShopPackChristmasGemsSmall.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackChristmasGemsSmall.appeared;
			}
			set
			{
				ShopPackChristmasGemsSmall.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (ShopPackChristmasGemsSmall.purchased || !PlayfabManager.christmasOfferConfigLoaded || !TrustedTime.IsReady())
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
			PlayerStats.OnOfferOffered(OfferId.CHRISTMAS_GEMS_SMALL);
			ShopPackChristmasGemsSmall.appeared = true;
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.CHRISTMAS_GEMS_SMALL);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.CHRISTMAS_GEMS_SMALL);
			ShopPackChristmasGemsSmall.purchased = true;
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
			return LM.Get("SHOP_PACK_CHRISTMAS_GEMS_SMALL");
		}

		public static bool appeared;

		public static bool purchased;
	}
}
