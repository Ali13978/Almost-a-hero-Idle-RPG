using System;
using Static;

namespace Simulation
{
	public class ShopPackSecondAnniversaryCurrencyBundleTwo : ShopPack, IHasPackState
	{
		public ShopPackSecondAnniversaryCurrencyBundleTwo()
		{
			this.tags = ShopPack.Tags.OUT_OF_SHOP;
			this.isOffer = true;
			this.name = "SECOND_ANNIVERSARY_GEMS_PACK";
			this.isIAP = true;
			this.iapIndex = IapIds.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO;
			this.credits = 22000.0;
			this.scrapsMin = 22000.0;
			this.scrapsMax = 22000.0;
			this.tokensMin = 22000.0;
			this.tokensMax = 22000.0;
			this.id = new OfferId?(OfferId.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO);
		}

		public bool isPurchased
		{
			get
			{
				return ShopPackSecondAnniversaryCurrencyBundleTwo.purchased;
			}
			set
			{
				ShopPackSecondAnniversaryCurrencyBundleTwo.purchased = value;
			}
		}

		public bool isAppeared
		{
			get
			{
				return ShopPackSecondAnniversaryCurrencyBundleTwo.appeared;
			}
			set
			{
				ShopPackSecondAnniversaryCurrencyBundleTwo.appeared = value;
			}
		}

		public override bool CanAppear(Simulator sim)
		{
			if (ShopPackSecondAnniversaryCurrencyBundleTwo.purchased || !TrustedTime.IsReady())
			{
				return false;
			}
			EventConfig.InternalEvent internalEventConfig = PlayfabManager.eventsInfo.GetInternalEventConfig("secondAnniversary", "currencyBundle2");
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
			PlayerStats.OnOfferOffered(OfferId.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO);
			ShopPackSecondAnniversaryCurrencyBundleTwo.appeared = true;
		}

		public override void OnCheckout()
		{
			base.OnCheckout();
			PlayerStats.OnOfferCheckout(OfferId.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO);
		}

		public override void OnPurchaseCompleted()
		{
			base.OnPurchaseCompleted();
			PlayerStats.OnOfferAccepted(OfferId.SECOND_ANNIVERSARY_CURRENCY_PACK_TWO);
			ShopPackSecondAnniversaryCurrencyBundleTwo.purchased = true;
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
			return string.Format(LM.Get("SECOND_ANNIVERSARY_GEMS_PACK"), 4);
		}

		public static bool appeared;

		public static bool purchased;
	}
}
