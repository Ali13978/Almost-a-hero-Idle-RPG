using System;

namespace Tapdaq
{
	[Serializable]
	public class TDAdEvent
	{
		public TDAdEvent()
		{
		}

		public TDAdEvent(string adType, string message, string tag = null)
		{
			this.adType = adType;
			this.message = message;
			this.tag = tag;
		}

		public TDAdType GetTypeOfEvent()
		{
			if (this.adType == "INTERSTITIAL")
			{
				return TDAdType.TDAdTypeInterstitial;
			}
			if (this.adType == "BANNER")
			{
				return TDAdType.TDAdTypeBanner;
			}
			if (this.adType == "VIDEO")
			{
				return TDAdType.TDAdTypeVideo;
			}
			if (this.adType == "REWARD_AD")
			{
				return TDAdType.TDAdTypeRewardedVideo;
			}
			if (this.adType == "OFFERWALL")
			{
				return TDAdType.TDAdTypeOfferwall;
			}
			return TDAdType.TDAdTypeNone;
		}

		public bool IsInterstitialEvent()
		{
			return this.GetTypeOfEvent() == TDAdType.TDAdTypeInterstitial;
		}

		public bool IsVideoEvent()
		{
			return this.GetTypeOfEvent() == TDAdType.TDAdTypeVideo;
		}

		public bool IsRewardedVideoEvent()
		{
			return this.GetTypeOfEvent() == TDAdType.TDAdTypeRewardedVideo;
		}

		public bool IsBannerEvent()
		{
			return this.GetTypeOfEvent() == TDAdType.TDAdTypeBanner;
		}

		public bool IsOfferwallEvent()
		{
			return this.adType == "OFFERWALL";
		}

		public string adType;

		public string message;

		public string tag;

		public TDAdError error;
	}
}
