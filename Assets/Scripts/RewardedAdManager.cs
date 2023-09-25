using System;
using System.Collections;
using Simulation;
using Tapdaq;
using UnityEngine;

public class RewardedAdManager : MonoBehaviour
{
	private void Start()
	{
		RewardedAdManager.inst = this;
		this.checkVideoAvailabiltyTimer = 0f;
		this.isWatchingAd = false;
		this.isWatchingAdCapped = false;
		this.shouldGiveReward = false;
		this.targetFlashOfferType = null;
		this.shouldGiveRewardCapped = false;
		this.initialized = false;
		this.adBridge = new TapDaqManager
		{
			rewardedAdManager = this
		};
		this.adBridge.Init();
		this.adBridge.EnableCallbacks();
	}

	public void OnAdViewed(string message)
	{
		AdjustTracker.TrackAdWatchedEventData(this.wasTheVideoCapped, this.ad_view);
	}

	public void OnAdClicked(string message)
	{
		AdjustTracker.TrackAdWatchedEventData(this.wasTheVideoCapped, this.ad_clicked);
	}

	public bool IsWatchingAnyAd()
	{
		return this.isWatchingAd || this.isWatchingAdCapped;
	}

	public void OnInitialized()
	{
		this.initialized = true;
		this.LoadRewardedVideo();
	}

	public void ConfigFailedToLoad(string message)
	{
		this.Log("Initialization error: " + message);
		this.initialized = false;
		base.StartCoroutine(this.ReTryInitialization());
	}

	private IEnumerator ReTryInitialization()
	{
		yield return new WaitForSeconds(2f);
		this.adBridge.Init();
		yield break;
	}

	public void OnAdError(string obj)
	{
		this.Log("AD ERROR: " + obj);
		this.hasFailed = true;
		this.shouldGiveReward = false;
		this.shouldGiveRewardCapped = false;
		this.isAdLoaded = false;
		base.StartCoroutine(this.ResetBools());
		Screen.orientation = ScreenOrientation.Portrait;
	}

	public void OnAdClosed(string obj)
	{
		this.Log("AD CLOSED " + obj);
		this.checkVideoAvailabiltyTimer = -2f;
		base.StartCoroutine(this.ResetBools());
		this.isAdLoaded = false;
	}

	public void OnAdAvailable(string obj)
	{
		this.isAdLoaded = true;
		this.Log("AD AVAILABLE: " + obj);
	}

	public void OnAdNotAvailable(string obj)
	{
		this.isAdLoaded = false;
		this.Log("AD NOT AVAIABLE: " + obj);
	}

	public void OnRewardVideoValidated()
	{
		if (!this.wasTheVideoCapped)
		{
			this.shouldGiveReward = true;
			this.shouldGiveRewardCapped = false;
			AdjustTracker.TrackAdWatchedEventData(false, this.ad_completed);
			
		}
		else
		{
			this.shouldGiveRewardCapped = true;
			this.shouldGiveReward = false;
			AdjustTracker.TrackAdWatchedEventData(true, this.ad_completed);
		}
		this.Log("AD VALIDATED");
		this.isAdLoaded = false;
		Screen.orientation = ScreenOrientation.Portrait;
	}

	public void OnRewardVideoNotValidated()
	{
		this.isAdLoaded = false;
	}

	public bool IsRewardedCappedVideoAvailable(DateTime lastCappedWatchedTime, CurrencyType currencyType, int videosWatched = 0)
	{
		return this.IsRewardedVideoAvailable() && this.GetNumCappedVideo(lastCappedWatchedTime, currencyType, videosWatched) > 0;
	}

	public int GetNumCappedVideo(DateTime lastCappedWatchedTime, CurrencyType currencyType, int videosWatched = 0)
	{
		if (!TrustedTime.IsReady())
		{
			return 0;
		}
		if (currencyType == CurrencyType.CANDY)
		{
			return 3 - videosWatched;
		}
		double num = GameMath.DeltaTimeInSecs(TrustedTime.Get(), lastCappedWatchedTime);
		return (int)Math.Min((double)RewardedAdManager.GetCappedCurrencyVideoMaxStack(currencyType), Math.Floor(num / (double)RewardedAdManager.GetCappedCurrencyVideoPeriod(currencyType)));
	}

	public void PrepareToShowRewardedVideo(FlashOffer targetFlashOffer)
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
		if (targetFlashOffer == null)
		{
			this.targetFlashOfferType = null;
		}
		else
		{
			this.targetFlashOfferType = new FlashOffer.Type?(targetFlashOffer.type);
		}
		this.isWatchingAd = true;
		this.wasTheVideoCapped = false;
		this.hasBeenWatchingAdWithoutIntermission = true;
		RewardedAdManager.prepTime = 1f;
	}

	public void PrepareToShowRewardedVideoCapped(Simulator sim, CurrencyType currencyType, double rewardAmount)
	{
		if (TrustedTime.IsReady())
		{
			RewardedAdManager.prepTime = 1f;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			this.wasTheVideoCapped = true;
			this.isWatchingAdCapped = true;
			this.hasBeenWatchingAdWithoutIntermission = true;
			this.currencyTypeForCappedVideo = currencyType;
			this.rewardAmountForCappedVideo = rewardAmount;
			if (currencyType != CurrencyType.CANDY)
			{
				int numCappedVideo = this.GetNumCappedVideo(sim.GetLastCappedCurrencyWatchedTime(currencyType), currencyType, 0);
				if (numCappedVideo == RewardedAdManager.GetCappedCurrencyVideoMaxStack(currencyType))
				{
					if (TrustedTime.IsReady())
					{
						sim.SetLastCappedCurrencyWatchedTime(currencyType, TrustedTime.Get().AddSeconds((double)(-(double)(RewardedAdManager.GetCappedCurrencyVideoMaxStack(currencyType) - 1) * RewardedAdManager.GetCappedCurrencyVideoPeriod(currencyType))));
					}
				}
				else
				{
					sim.SetLastCappedCurrencyWatchedTime(currencyType, sim.GetLastCappedCurrencyWatchedTime(currencyType).AddSeconds((double)RewardedAdManager.GetCappedCurrencyVideoPeriod(currencyType)));
				}
			}
		}
	}

	public static int GetCappedCurrencyVideoMaxStack(CurrencyType currencyType)
	{
		if (currencyType == CurrencyType.GEM)
		{
			return 3;
		}
		if (currencyType != CurrencyType.CANDY)
		{
			throw new NotImplementedException();
		}
		return 3;
	}

	public static int GetCappedCurrencyVideoPeriod(CurrencyType currencyType)
	{
		if (currencyType != CurrencyType.GEM)
		{
			throw new NotImplementedException();
		}
		return 1200;
	}

	private IEnumerator ResetBools()
	{
		yield return new WaitForSeconds(0.1f);
		this.isWatchingAd = false;
		this.isWatchingAdCapped = false;
		this.lastAddLoadTime = -60f;
		yield break;
	}

	private void Update()
	{
		this.adBridge.Update();
		if (!this.IsWatchingAnyAd())
		{
			float num = Time.time - this.lastAddLoadTime;
			if (!this.isAdLoaded && num > 15f)
			{
				if (this.IsRewardedVideoReady())
				{
					this.lastAddLoadTime = Time.time;
					this.isAdLoaded = true;
				}
				else
				{
					this.lastAddLoadTime = Time.time;
					this.LoadRewardedVideo();
				}
			}
		}
	}

	public bool IsRewardedVideoAvailable()
	{
		return this.IsRewardedVideoReady();
	}

	public static void Advance()
	{
		if (RewardedAdManager.prepTime > 0f)
		{
			RewardedAdManager.prepTime -= Time.deltaTime;
			if (RewardedAdManager.prepTime <= 0f)
			{
				RewardedAdManager.inst.adBridge.ShowAd();
			}
		}
	}

	public void OnShopOpened()
	{
		if (!this.adBridge.IsInitialized())
		{
			this.adBridge.Init();
		}
	}

	private bool IsRewardedVideoReady()
	{
		return this.adBridge.IsAdReady();
	}

	private void LoadRewardedVideo()
	{
		if (this.adBridge.IsInitialized())
		{
			this.Log("AD LOADING");
			this.adBridge.LoadAd();
		}
		else
		{
			this.initialized = false;
			this.Log("AD MANAGER NOT INITIALIZED : Retrying init");
			this.adBridge.Init();
		}
	}

	private void Log(object o)
	{
		UnityEngine.Debug.Log(o, this);
	}

	public static void LaunchMediationDebugger()
	{
		AdManager.LaunchMediationDebugger();
	}

	private const string CAPPED_ID = "free-credits";

	public static RewardedAdManager inst;

	public FlashOffer.Type? targetFlashOfferType;

	public bool isWatchingAd;

	public bool isWatchingAdCapped;

	public bool wasTheVideoCapped;

	public bool hasBeenWatchingAdWithoutIntermission;

	public CurrencyType currencyTypeForCappedVideo;

	public double rewardAmountForCappedVideo;

	public bool shouldGiveReward;

	public bool shouldGiveRewardCapped;

	public bool canGiveCrashReward;

	public bool hasFailed;

	private float checkVideoAvailabiltyTimer;

	private bool initialized;

	private bool isAdLoaded;

	public const int CAPPED_VIDEO_CREDITS_MAX_STACK = 3;

	public const int CAPPED_VIDEO_CREDITS_PERIOD = 1200;

	public const int CAPPED_VIDEO_CANDIES_MAX_STACK = 3;

	private float lastAddLoadTime;

	private IAdBridge adBridge;

	private string ad_view = "ad_view";

	private string ad_clicked = "ad_clicked";

	private string ad_completed = "ad_completed";

	public static float prepTime;
}
