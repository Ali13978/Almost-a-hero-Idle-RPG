using System;
using Static;

namespace Simulation
{
	public class ShopPackBigGemTwo : ShopPack, IHasPackState
	{
		public ShopPackBigGemTwo()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_BIG_GEMS";
			this.isIAP = true;
			this.iapIndex = IapIds.MID_GEM_OFFER_TWO;
			this.credits = 6500.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackBigGemTwo.purchased;
			}
			set
			{
				ShopPackBigGemTwo.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackBigGemTwo.appeared;
			}
			set
			{
				ShopPackBigGemTwo.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackBigGemTwo.purchased && ShopPackBigGem.appeared && sim.GetStandardMaxStageReached() >= ShopPackBigGemTwo.APPEAR_STAGE;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackBigGemTwo.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_LATEGAME_GEM_OFFER);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_LATEGAME_GEM_OFFER);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_LATEGAME_GEM_OFFER);
			ShopPackBigGemTwo.purchased = true;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 300f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_BIG_GEMS"), ShopPackBigGemTwo.APPEAR_STAGE);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_STAGE = 1000;
	}
}
