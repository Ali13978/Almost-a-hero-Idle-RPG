using System;
using Static;

namespace Simulation
{
	internal class ShopPackStage800 : ShopPack, IHasPackState
	{
		public ShopPackStage800()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_STAGE_500";
			this.isIAP = true;
			this.iapIndex = IapIds.STAGE_800_OFFER;
			this.scrapsMax = 16000.0;
			this.scrapsMin = 16000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackStage800.purchased;
			}
			set
			{
				ShopPackStage800.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackStage800.appeared;
			}
			set
			{
				ShopPackStage800.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackStage800.purchased && sim.GetStandardMaxStageReached() >= ShopPackStage800.APPEAR_STAGE;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackStage800.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_STAGE_800_OFFER);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_STAGE_800_OFFER);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_STAGE_800_OFFER);
			ShopPackStage800.purchased = true;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 300f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_BIG_GEMS"), ShopPackStage800.APPEAR_STAGE);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_STAGE = 800;
	}
}
