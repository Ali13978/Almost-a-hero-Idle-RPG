using System;
using Static;

namespace Simulation
{
	public class ShopPackRiftOffer01 : ShopPack, IHasPackState
	{
		public ShopPackRiftOffer01()
		{
			this.tags = (ShopPack.Tags.UNIQUE | ShopPack.Tags.RIFT);
			this.isOffer = true;
			this.name = "SHOP_PACK_GATE";
			this.isIAP = true;
			this.iapIndex = IapIds.RIFT_OFFER_01;
			this.credits = 2500.0;
			this.scrapsMin = 10000.0;
			this.scrapsMax = 10000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackRiftOffer01.purchased;
			}
			set
			{
				ShopPackRiftOffer01.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackRiftOffer01.appeared;
			}
			set
			{
				ShopPackRiftOffer01.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (!TrustedTime.IsReady())
			{
				return false;
			}
			if (ShopPackRiftOffer01.purchased)
			{
				return false;
			}
			int latestUnlockedRiftChallengeIndex = sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex();
			if (latestUnlockedRiftChallengeIndex < ShopPackRiftOffer01.APPEAR_RIFT)
			{
				return false;
			}
			if (!ShopPackRiftOffer01.appeared)
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
			ShopPackRiftOffer01.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_01);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_01);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_01);
			ShopPackRiftOffer01.purchased = true;
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
			if (!ShopPackRiftOffer01.appeared)
			{
				return 100f;
			}
			return 1f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_GATE"), ShopPackRiftOffer01.APPEAR_RIFT);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_RIFT = 10;
	}
}
