using System;
using PlayFab.ClientModels;
using Static;

namespace Simulation
{
	public abstract class ShopPackRegionalBase : ShopPack
	{
		public ShopPackRegionalBase()
		{
			this.tags = ShopPack.Tags.REGIONAL;
			this.isOffer = true;
		}

		public override double GetTotalTime()
		{
			return this.totalTime;
		}

		public override bool CanAppear(Simulator sim)
		{
			if (!TrustedTime.IsReady())
			{
				return false;
			}
			if (!PlayfabManager.isRegionalOfferStatusLoaded)
			{
				return false;
			}
			if (!PlayfabManager.isRegionalOffersEnabled)
			{
				return false;
			}
			DateTime t = TrustedTime.Get();
			if (t < this.timeStart || t > this.timeStart.AddSeconds(this.totalTime))
			{
				return false;
			}
			foreach (CountryCode item in this.countryCodes)
			{
				if (PlayerStats.CountriesPlayerLoggedIn.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		public override bool ShouldDiscard(Simulator sim)
		{
			return PlayfabManager.isRegionalOfferStatusLoaded && !PlayfabManager.isRegionalOffersEnabled;
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public CountryCode[] countryCodes;
	}
}
