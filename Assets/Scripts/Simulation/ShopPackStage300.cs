using System;
using Static;

namespace Simulation
{
	internal class ShopPackStage300 : ShopPack, IHasPackState
	{
		public ShopPackStage300()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_STAGE_200";
			this.isIAP = true;
			this.iapIndex = IapIds.STAGE_300_OFFER;
			this.credits = 3500.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackStage300.purchased;
			}
			set
			{
				ShopPackStage300.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackStage300.appeared;
			}
			set
			{
				ShopPackStage300.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackStage300.purchased && sim.GetStandardMaxStageReached() >= ShopPackStage300.APPEAR_STAGE;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackStage300.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_STAGE_300_OFFER);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_STAGE_300_OFFER);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_STAGE_300_OFFER);
			ShopPackStage300.purchased = true;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 300f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_BIG_GEMS"), ShopPackStage300.APPEAR_STAGE);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_STAGE = 300;
	}
}
