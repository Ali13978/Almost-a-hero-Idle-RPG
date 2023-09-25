using System;
using Tapdaq;
using UnityEngine;

public class TapDaqManager : IAdBridge
{
	public void EnableCallbacks()
	{
		this.Log("Ad Manager Enabled");
		TDCallbacks.RewardVideoValidated += this.OnRewardVideoValidated;
		TDCallbacks.AdAvailable += this.OnAdAvailable;
		TDCallbacks.AdClicked += this.OnAdClicked;
		TDCallbacks.AdClosed += this.OnAdClosed;
		TDCallbacks.AdDidDisplay += this.OnAdDidDisplay;
		TDCallbacks.AdError += this.OnAdError;
		TDCallbacks.AdNotAvailable += this.OnAdNotAvailable;
		TDCallbacks.AdWillDisplay += this.OnAdWillDisplay;
		TDCallbacks.TapdaqConfigLoaded += this.OnTapdaqConfigLoaded;
		TDCallbacks.TapdaqConfigFailedToLoad += this.OnTapdaqConfigFailedToLoad;
	}

	private void OnTapdaqConfigFailedToLoad(TDAdError obj)
	{
		this.rewardedAdManager.ConfigFailedToLoad(obj.message);
	}

	private void OnTapdaqConfigLoaded()
	{
		this.rewardedAdManager.OnInitialized();
	}

	private void OnAdWillDisplay(TDAdEvent obj)
	{
		this.Log("AD WILL DISPLAY: " + obj.message);
	}

	private void OnAdError(TDAdEvent obj)
	{
		this.rewardedAdManager.OnAdError(obj.message);
	}

	private void OnAdDidDisplay(TDAdEvent obj)
	{
		this.Log("AD DID DISPLAY:" + obj.message);
		this.rewardedAdManager.OnAdViewed(obj.message);
	}

	private void OnAdClosed(TDAdEvent obj)
	{
		this.rewardedAdManager.OnAdClosed(obj.message);
	}

	private void OnAdClicked(TDAdEvent obj)
	{
		this.Log("AD CLICKED: " + obj.message);
		this.rewardedAdManager.OnAdClicked(obj.message);
	}

	private void OnAdAvailable(TDAdEvent obj)
	{
		this.Log("AD LOADED: " + obj.tag);
		this.Log("AD LOADED: " + obj.message);
		this.Log("AD LOADED: " + obj.adType);
		this.rewardedAdManager.OnAdAvailable(obj.message);
	}

	private void OnAdNotAvailable(TDAdEvent obj)
	{
		this.rewardedAdManager.OnAdNotAvailable(obj.message);
	}

	private void OnRewardVideoValidated(TDVideoReward obj)
	{
		if (obj != null)
		{
			if (obj.RewardValid)
			{
				this.rewardedAdManager.OnRewardVideoValidated();
			}
			else
			{
				this.rewardedAdManager.OnRewardVideoNotValidated();
			}
		}
	}

	public void DisableCallbacks()
	{
		this.Log("Ad Manager Disabled");
		TDCallbacks.RewardVideoValidated -= this.OnRewardVideoValidated;
		TDCallbacks.AdAvailable -= this.OnAdAvailable;
		TDCallbacks.AdClicked -= this.OnAdClicked;
		TDCallbacks.AdClosed -= this.OnAdClosed;
		TDCallbacks.AdDidDisplay -= this.OnAdDidDisplay;
		TDCallbacks.AdError -= this.OnAdError;
		TDCallbacks.AdNotAvailable -= this.OnAdNotAvailable;
		TDCallbacks.AdWillDisplay -= this.OnAdWillDisplay;
		TDCallbacks.TapdaqConfigLoaded -= this.OnTapdaqConfigLoaded;
	}

	private void Log(object o)
	{
	}

	public void Update()
	{
	}

	public void Init()
	{
		TDSettings instance = TDSettings.getInstance();
		if (!Application.isEditor)
		{
			UnityEngine.Debug.Log("Adding test devices");
			instance.testDevices.Clear();
			instance.testDevices.Add(this.CreateTestDevice("Omer S7 Edge", "F3E4CDD00250D2CD7984C13B5CC4CCDB", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("Luis Xiaomi redmi A1", "A1F6C3DD7C89974D82D7D59566E70FE0", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("Xiaomi redmi note 4x", "431FDDFF2A614ED235288DE5DE9509E7", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("Google Pixel 2", "67C49491D64427A95AE240F2CFF0943E", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("7.0 Nougat Samsung Galaxy S7", "FA01E5CFAAFBA10025CD1421600572D2", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("5.0.2 Lollipop Sony Xperia Z1", "5DF77C3AF7D999891B663FDD826AEB15", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("4.4.2 Kit Kat BQ Aquaris E4", "D916A589D42C501400C2D5A7692CEAEC", TestDeviceType.Android));
			instance.testDevices.Add(this.CreateTestDevice("Lara iPhone 6", "2747029a6689a7efcf8c8167692c42fc", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 11.1.2 iPhone 6 Plus", "0f58573762e8571fc63397a5ef2ad55e", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 11.2.5 iPhone X", "fb0be7569b4494a949582fb09059c6d5", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 10.3.3 iPhone 5c", "e9f8261218e4ef1e34e43507fa2a0705", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 11.2.1 iPad mini 2", "13165468f1531e1b68d5d3d6df9f4b0b", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 11.0.3 iPad air", "6ba4253a815227299611e232e938e4df", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 11.0\tiPad air", "8904ccf444f8d9f47e55b333a577eb64", TestDeviceType.iOS));
			instance.testDevices.Add(this.CreateTestDevice("iOS 9.3.5 iPhone 5s", "ada5f0a39f2fb3347656b5d26b285e5d", TestDeviceType.iOS));
		}
		instance.isDebugMode = false;
		AdManager.Init();
	}

	public bool IsAdReady()
	{
		return AdManager.IsRewardedVideoReady(this.tag);
	}

	public void LoadAd()
	{
		AdManager.LoadRewardedVideo(this.tag);
	}

	public void ShowAd()
	{
		AdManager.ShowRewardVideo(this.tag, null);
	}

	private TestDevice CreateTestDevice(string name, string idADMOB, TestDeviceType deviceType)
	{
		return new TestDevice(name, deviceType)
		{
			adMobId = idADMOB,
			facebookId = string.Empty
		};
	}

	public bool IsInitialized()
	{
		return AdManager.IsInitialised();
	}

	private string tag = "default";

	public RewardedAdManager rewardedAdManager;
}
