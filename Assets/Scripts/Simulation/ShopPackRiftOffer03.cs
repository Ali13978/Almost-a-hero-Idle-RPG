using System;
using Static;

namespace Simulation
{
	public class ShopPackRiftOffer03 : ShopPack, IHasPackState
	{
		public ShopPackRiftOffer03()
		{
			this.tags = (ShopPack.Tags.UNIQUE | ShopPack.Tags.RIFT);
			this.isOffer = true;
			this.name = "SHOP_PACK_GATE";
			this.isIAP = true;
			this.iapIndex = IapIds.RIFT_OFFER_03;
			this.credits = 4000.0;
			this.scrapsMin = 4000.0;
			this.scrapsMax = 4000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackRiftOffer03.purchased;
			}
			set
			{
				ShopPackRiftOffer03.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackRiftOffer03.appeared;
			}
			set
			{
				ShopPackRiftOffer03.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (!TrustedTime.IsReady())
			{
				return false;
			}
			if (ShopPackRiftOffer03.purchased)
			{
				return false;
			}
			int latestUnlockedRiftChallengeIndex = sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex();
			if (latestUnlockedRiftChallengeIndex < ShopPackRiftOffer03.APPEAR_RIFT)
			{
				return false;
			}
			if (!ShopPackRiftOffer03.appeared)
			{
				return true;
			}
			double totalSeconds = (TrustedTime.Get() - sim.lastRiftOfferEndTime).TotalSeconds;
			double num = 604800.0;
			return totalSeconds > num;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			ShopPackRiftOffer03.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_03);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_03);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_03);
			ShopPackRiftOffer03.purchased = true;
		}

		public override bool ShouldOverrideExisting(Simulator sim)
		{
			return false;
		}

		public override bool CanBeDiscarded(Simulator sim)
		{
			return false;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			if (!ShopPackRiftOffer03.appeared)
			{
				return 100f;
			}
			return 1f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_GATE"), ShopPackRiftOffer03.APPEAR_RIFT);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_RIFT = 50;
	}
}
