using System;
using Static;

namespace Simulation
{
	public class ShopPackBigGem : ShopPack, IHasPackState
	{
		public ShopPackBigGem()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_BIG_GEMS";
			this.isIAP = true;
			this.iapIndex = IapIds.MID_GEM_OFFER;
			this.credits = 3000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackBigGem.purchased;
			}
			set
			{
				ShopPackBigGem.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackBigGem.appeared;
			}
			set
			{
				ShopPackBigGem.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackBigGem.purchased && sim.GetStandardMaxStageReached() >= ShopPackBigGem.APPEAR_STAGE;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackBigGem.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_MIDEGAME_GEM_OFFER);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_MIDEGAME_GEM_OFFER);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_MIDEGAME_GEM_OFFER);
			ShopPackBigGem.purchased = true;
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
			return string.Format(LM.Get("SHOP_PACK_BIG_GEMS"), ShopPackBigGem.APPEAR_STAGE);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_STAGE = 500;
	}
}
