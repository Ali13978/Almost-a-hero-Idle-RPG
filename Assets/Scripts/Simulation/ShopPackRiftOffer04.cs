using System;
using Static;

namespace Simulation
{
	public class ShopPackRiftOffer04 : ShopPack, IHasPackState
	{
		public ShopPackRiftOffer04()
		{
			this.tags = (ShopPack.Tags.UNIQUE | ShopPack.Tags.RIFT);
			this.isOffer = true;
			this.name = "SHOP_PACK_GATE";
			this.isIAP = true;
			this.iapIndex = IapIds.RIFT_OFFER_04;
			this.credits = 2500.0;
			this.scrapsMin = 25000.0;
			this.scrapsMax = 25000.0;
			this.totalTime = 86400.0;
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackRiftOffer04.purchased;
			}
			set
			{
				ShopPackRiftOffer04.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackRiftOffer04.appeared;
			}
			set
			{
				ShopPackRiftOffer04.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (!TrustedTime.IsReady())
			{
				return false;
			}
			if (ShopPackRiftOffer04.purchased)
			{
				return false;
			}
			int latestUnlockedRiftChallengeIndex = sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex();
			if (latestUnlockedRiftChallengeIndex < ShopPackRiftOffer04.APPEAR_RIFT)
			{
				return false;
			}
			if (!ShopPackRiftOffer04.appeared)
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
			ShopPackRiftOffer04.appeared = true;
			PlayerStats.OnOfferOffered(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_04);
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_04);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.ONE_TIME_PURCHASE_RIFT_OFFER_04);
			ShopPackRiftOffer04.purchased = true;
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
			if (!ShopPackRiftOffer04.appeared)
			{
				return 100f;
			}
			return 1f;
		}

		public override string GetName()
		{
			return string.Format(LM.Get("SHOP_PACK_GATE"), ShopPackRiftOffer04.APPEAR_RIFT);
		}

		public static bool appeared;

		public static bool purchased;

		public static int APPEAR_RIFT = 80;
	}
}
