using System;
using Static;

namespace Simulation
{
	public class ShopPackSecondAnniversaryGems : ShopPack, IHasPackState
	{
		public ShopPackSecondAnniversaryGems()
		{
			this.tags = ShopPack.Tags.OUT_OF_SHOP;
			this.isOffer = true;
			this.name = "SECOND_ANNIVERSARY_GEMS_PACK";
			this.isIAP = true;
			this.iapIndex = IapIds.SECOND_ANNIVERSARY_GEMS;
			this.credits = 2750.0;
			this.id = new OfferId?(OfferId.SECOND_ANNIVERSARY_GEMS);
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackSecondAnniversaryGems.purchased;
			}
			set
			{
				ShopPackSecondAnniversaryGems.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackSecondAnniversaryGems.appeared;
			}
			set
			{
				ShopPackSecondAnniversaryGems.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (ShopPackSecondAnniversaryGems.purchased || !TrustedTime.IsReady())
			{
				return false;
			}
			EventConfig.InternalEvent internalEventConfig = PlayfabManager.eventsInfo.GetInternalEventConfig("secondAnniversary", "gemsOffer");
			if (internalEventConfig == null)
			{
				return false;
			}
			if (this.totalTime == 0.0)
			{
				this.timeStart = internalEventConfig.startDate;
				this.totalTime = internalEventConfig.durationHours * 3600.0;
			}
			DateTime t = TrustedTime.Get();
			return t >= this.timeStart && t < internalEventConfig.endDate;
		}

		public override void OnAppeared()
		{
			base.OnAppeared();
			PlayerStats.OnOfferOffered(OfferId.SECOND_ANNIVERSARY_GEMS);
			ShopPackSecondAnniversaryGems.appeared = true;
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.SECOND_ANNIVERSARY_GEMS);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.SECOND_ANNIVERSARY_GEMS);
			ShopPackSecondAnniversaryGems.purchased = true;
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
			return string.Format(LM.Get("SECOND_ANNIVERSARY_GEMS_PACK"), 3);
		}

		public static bool appeared;

		public static bool purchased;
	}
}
