using System;
using Static;

namespace Simulation
{
	public class ShopPackStarter : ShopPack, IHasPackState
	{
		public ShopPackStarter()
		{
			this.tags = ShopPack.Tags.UNIQUE;
			this.isOffer = true;
			this.name = "SHOP_PACK_STARTER";
			this.isIAP = true;
			this.iapIndex = IapIds.STARTER_PACK;
			this.credits = 1300.0;
			this.numGears = new int[]
			{
				0,
				0,
				2,
				3,
				1,
				0
			};
			this.totalTime = 172800.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackStarter.purchased;
			}
			set
			{
				ShopPackStarter.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackStarter.appeared;
			}
			set
			{
				ShopPackStarter.appeared = value;
			}
		}

		public override double GetTotalTime()
		{
			if (ShopPackStarter.appearedBefore)
			{
				return 43200.0;
			}
			return 172800.0;
		}

		public override bool CanAppear(Simulator sim)
		{
			return !ShopPackStarter.purchased;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackStarter.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.STARTER_PACK);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.STARTER_PACK);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			ShopPackStarter.purchased = true;
			PlayerStats.OnOfferAccepted(OfferId.STARTER_PACK);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public static bool appeared;

		public static bool appearedBefore;

		public static bool purchased;
	}
}
