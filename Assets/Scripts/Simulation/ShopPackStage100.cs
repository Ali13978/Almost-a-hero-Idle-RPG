using System;
using Static;

namespace Simulation
{
	internal class ShopPackStage100 : ShopPack, IHasPackState
	{
		public ShopPackStage100()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_STAGE_100";
			this.isIAP = true;
			this.iapIndex = IapIds.STAGE_100_OFFER;
			this.scrapsMax = 5000.0;
			this.scrapsMin = 5000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackStage100.purchased;
			}
			set
			{
				ShopPackStage100.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackStage100.appeared;
			}
			set
			{
				ShopPackStage100.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackStage100.purchased && sim.GetStandardMaxStageReached() >= ShopPackStage100.APPEAR_STAGE;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackStage100.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_STAGE_100_OFFER);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_STAGE_100_OFFER);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_STAGE_100_OFFER);
			ShopPackStage100.purchased = true;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 300f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_BIG_GEMS"), ShopPackStage100.APPEAR_STAGE);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_STAGE = 100;
	}
}
