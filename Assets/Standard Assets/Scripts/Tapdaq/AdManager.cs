using System;
using UnityEngine;

namespace Tapdaq
{
	public class AdManager
	{
		internal AdManager()
		{
		}

		public static AdManager instance
		{
			get
			{
				if (AdManager.reference == null)
				{
					AdManager.reference = new AdManager();
				}
				return AdManager.reference;
			}
		}

		public static void Init()
		{
			AdManager.instance._Init(TDStatus.UNKNOWN, TDStatus.UNKNOWN, TDStatus.UNKNOWN);
		}

		public static void InitWithConsent(TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven, TDStatus isAgeRestrictedUser)
		{
			AdManager.instance._Init(isUserSubjectToGDPR, isConsentGiven, isAgeRestrictedUser);
		}

		[Obsolete("Please, use 'InitWithConsent (TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven)' method.")]
		public static void InitWithConsent(bool isConsentGiven)
		{
			AdManager.instance._Init((!isConsentGiven) ? TDStatus.FALSE : TDStatus.TRUE, (!isConsentGiven) ? TDStatus.FALSE : TDStatus.TRUE, TDStatus.UNKNOWN);
		}

		[Obsolete("Please, use 'InitWithConsent (TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven, TDStatus isAgeRestrictedUser)' method.")]
		public static void InitWithConsent(TDStatus isConsentGiven)
		{
			AdManager.instance._Init(isConsentGiven, isConsentGiven, TDStatus.UNKNOWN);
		}

		[Obsolete("Please, use 'InitWithConsent (TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven, TDStatus isAgeRestrictedUser)' method.")]
		public static void InitWithConsent(bool isConsentGiven, bool isAgeRestrictedUser)
		{
			AdManager.instance._Init((!isConsentGiven) ? TDStatus.FALSE : TDStatus.TRUE, (!isConsentGiven) ? TDStatus.FALSE : TDStatus.TRUE, (!isAgeRestrictedUser) ? TDStatus.FALSE : TDStatus.TRUE);
		}

		private void _Init(TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven, TDStatus isAgeRestrictedUser)
		{
			if (!this.settings)
			{
				this.settings = TDSettings.getInstance();
			}
			TDEventHandler.instance.Init();
			string text = string.Empty;
			string text2 = string.Empty;
			text = this.settings.android_applicationID;
			text2 = this.settings.android_clientKey;
			AdManager.LogMessage(TDLogSeverity.debug, "TapdaqSDK/Application ID -- " + text);
			AdManager.LogMessage(TDLogSeverity.debug, "TapdaqSDK/Client Key -- " + text2);
			this.Initialize(text, text2, isUserSubjectToGDPR, isConsentGiven, isAgeRestrictedUser);
		}

		private void Initialize(string appID, string clientKey, TDStatus isUserSubjectToGDPR, TDStatus isConsentGiven, TDStatus isAgeRestrictedUser)
		{
			AdManager.LogUnsupportedPlatform();
			AdManager.LogMessage(TDLogSeverity.debug, "TapdaqSDK/Initializing");
			string text = new TestDevicesList(this.settings.testDevices, TestDeviceType.Android).ToString();
			TDDebugLogger.Log("testDevices:\n" + text);
			AdManager.CallAndroidStaticMethod("InitiateTapdaq", new object[]
			{
				appID,
				clientKey,
				text,
				this.settings.isDebugMode,
				this.settings.autoReloadAds,
				"unity_7.0.3",
				(int)isUserSubjectToGDPR,
				(int)isConsentGiven,
				(int)isAgeRestrictedUser
			});
		}

		private static T GetAndroidStatic<T>(string methodName, params object[] paramList)
		{
			AdManager.LogUnsupportedPlatform();
			if (Application.platform == RuntimePlatform.Android)
			{
				try
				{
					using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.tapdaq.unityplugin.TapdaqUnity"))
					{
						return androidJavaClass.CallStatic<T>(methodName, paramList);
					}
				}
				catch (Exception obj)
				{
					TDDebugLogger.LogException(obj);
				}
			}
			TDDebugLogger.LogError("Error while call static method");
			return default(T);
		}

		private static void CallAndroidStaticMethod(string methodName, params object[] paramList)
		{
			AdManager.CallAndroidStaticMethodFromClass("com.tapdaq.unityplugin.TapdaqUnity", methodName, true, paramList);
		}

		private static void CallAndroidStaticMethodFromClass(string className, string methodName, bool logException, params object[] paramList)
		{
			AdManager.LogUnsupportedPlatform();
			if (Application.platform == RuntimePlatform.Android)
			{
				try
				{
					using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(className))
					{
						androidJavaClass.CallStatic(methodName, paramList);
					}
				}
				catch (Exception ex)
				{
					if (logException)
					{
						TDDebugLogger.Log(string.Concat(new string[]
						{
							"CallAndroidStaticMethod:  ",
							methodName,
							"    FromClass: ",
							className,
							" failed. Message: ",
							ex.Message
						}));
					}
				}
			}
		}

		private static void LogObsoleteWithTagMethod(string methodName)
		{
			TDDebugLogger.LogError(string.Concat(new string[]
			{
				"'",
				methodName,
				"WithTag(string tag)' is Obsolete. Please, use '",
				methodName,
				"(string tag)' instead"
			}));
		}

		private static void LogUnsupportedPlatform()
		{
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				AdManager.LogMessage(TDLogSeverity.warning, "We support iOS and Android platforms only.");
			}
		}

		public void _UnexpectedErrorHandler(string msg)
		{
			TDDebugLogger.Log(":: Ad test ::" + msg);
			AdManager.LogMessage(TDLogSeverity.error, msg);
		}

		public static void LogMessage(TDLogSeverity severity, string message)
		{
			string str = "Tapdaq Unity SDK: ";
			if (severity == TDLogSeverity.warning)
			{
				TDDebugLogger.LogWarning(str + message);
			}
			else if (severity == TDLogSeverity.error)
			{
				TDDebugLogger.LogError(str + message);
			}
			else
			{
				TDDebugLogger.Log(str + message);
			}
		}

		public void FetchFailed(string msg)
		{
			TDDebugLogger.Log(msg);
			AdManager.LogMessage(TDLogSeverity.debug, "unable to fetch more ads");
		}

		public static void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				AdManager.CallAndroidStaticMethod("OnPause", new object[0]);
			}
			else
			{
				AdManager.CallAndroidStaticMethod("OnResume", new object[0]);
			}
		}

		public static bool IsInitialised()
		{
			return AdManager.GetAndroidStatic<bool>("IsInitialised", new object[0]);
		}

		public static void LaunchMediationDebugger()
		{
			AdManager.CallAndroidStaticMethod("ShowMediationDebugger", new object[0]);
		}

		public static void SetUserSubjectToGDPR(TDStatus isUserSubjectToGDPR)
		{
			AdManager.CallAndroidStaticMethod("SetUserSubjectToGDPR", new object[]
			{
				(int)isUserSubjectToGDPR
			});
		}

		public static TDStatus IsUserSubjectToGDPR()
		{
			return (TDStatus)AdManager.GetAndroidStatic<int>("IsUserSubjectToGDPR", new object[0]);
		}

		public static void SetConsentGiven(bool isConsentGiven)
		{
			AdManager.CallAndroidStaticMethod("SetConsentGiven", new object[]
			{
				isConsentGiven
			});
		}

		public static bool IsConsentGiven()
		{
			return AdManager.GetAndroidStatic<bool>("IsConsentGiven", new object[0]);
		}

		public static void SetIsAgeRestrictedUser(bool isAgeRestrictedUser)
		{
			AdManager.CallAndroidStaticMethod("SetAgeRestrictedUser", new object[]
			{
				isAgeRestrictedUser
			});
		}

		public static bool IsAgeRestrictedUser()
		{
			return AdManager.GetAndroidStatic<bool>("IsAgeRestrictedUser", new object[0]);
		}

		public static void SetAdMobContentRating(string rating)
		{
			AdManager.CallAndroidStaticMethod("SetAdMobContentRating", new object[]
			{
				rating
			});
		}

		public static string GetAdMobContentRating()
		{
			return AdManager.GetAndroidStatic<string>("GetAdMobContentRating", new object[0]);
		}

		public static void LoadInterstitial(string tag = "default")
		{
			AdManager.CallAndroidStaticMethod("LoadInterstitial", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'LoadInterstitial(string tag)' method.")]
		public static void LoadInterstitialWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("LoadInterstitial");
			AdManager.LoadInterstitial(tag);
		}

		public static void ShowInterstitial(string tag = "default")
		{
			AdManager.CallAndroidStaticMethod("ShowInterstitial", new object[]
			{
				tag
			});
		}

		public static bool IsInterstitialReady(string tag = "default")
		{
			return AdManager.GetAndroidStatic<bool>("IsInterstitialReady", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'IsInterstitialReady(string tag)' method.")]
		public static bool IsInterstitialReadyWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("IsInterstitialReady");
			return AdManager.IsInterstitialReady(tag);
		}

		public static bool IsBannerReady()
		{
			return AdManager.GetAndroidStatic<bool>("IsBannerReady", new object[0]);
		}

		public static void RequestBanner(TDMBannerSize size)
		{
			AdManager.CallAndroidStaticMethod("LoadBannerOfType", new object[]
			{
				size.ToString()
			});
		}

		public static void ShowBanner(TDBannerPosition position)
		{
			AdManager.CallAndroidStaticMethod("ShowBanner", new object[]
			{
				position.ToString()
			});
		}

		public static void HideBanner()
		{
			AdManager.CallAndroidStaticMethod("HideBanner", new object[0]);
		}

		public static void LoadVideo(string tag = "default")
		{
			AdManager.CallAndroidStaticMethod("LoadVideo", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'LoadVideo(string tag)' method.")]
		public static void LoadVideoWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("LoadVideo");
			AdManager.LoadVideo(tag);
		}

		public static void ShowVideo(string tag = "default")
		{
			AdManager.CallAndroidStaticMethod("ShowVideo", new object[]
			{
				tag
			});
		}

		public static bool IsVideoReady(string tag = "default")
		{
			return AdManager.GetAndroidStatic<bool>("IsVideoReady", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'IsVideoReady(string tag)' method.")]
		public static bool IsVideoReadyWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("IsVideoReady");
			return AdManager.IsVideoReady(tag);
		}

		public static void LoadRewardedVideo(string tag = "default")
		{
			AdManager.CallAndroidStaticMethod("LoadRewardedVideo", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'LoadRewardedVideo(string tag)' method.")]
		public static void LoadRewardedVideoWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("LoadRewardedVideo");
			AdManager.LoadRewardedVideo(tag);
		}

		public static void ShowRewardVideo(string tag = "default", string hashedUserId = null)
		{
			AdManager.CallAndroidStaticMethod("ShowRewardedVideo", new object[]
			{
				tag,
				hashedUserId
			});
		}

		public static bool IsRewardedVideoReady(string tag = "default")
		{
			return AdManager.GetAndroidStatic<bool>("IsRewardedVideoReady", new object[]
			{
				tag
			});
		}

		[Obsolete("Please, use 'IsRewardedVideoReady(string tag)' method.")]
		public static bool IsRewardedVideoReadyWithTag(string tag)
		{
			AdManager.LogObsoleteWithTagMethod("IsRewardedVideoReady");
			return AdManager.IsRewardedVideoReady(tag);
		}

		public static bool IsOfferwallReady()
		{
			return AdManager.GetAndroidStatic<bool>("IsOfferwallReady", new object[0]);
		}

		public static void LoadOfferwall()
		{
			AdManager.CallAndroidStaticMethod("LoadOfferwall", new object[0]);
		}

		public static void ShowOfferwall()
		{
			AdManager.CallAndroidStaticMethod("ShowOfferwall", new object[0]);
		}

		[Obsolete("For Android use 'SendIAP_Android(String in_app_purchase_data, String in_app_purchase_signature, String name, double price, String currency, String locale)' \nFor iOS use 'SendIAP_iOS(String transactionId, String productId, String name, double price, String currency, String locale)' methods.")]
		public static void SendIAP(string name, double price, string locale)
		{
			AdManager.SendIAP_Android(null, null, name, price, null, locale);
		}

		public static void SendIAP_iOS(string transactionId, string productId, string name, double price, string currency, string locale)
		{
		}

		public static void SendIAP_Android(string in_app_purchase_data, string in_app_purchase_signature, string name, double price, string currency, string locale)
		{
			AdManager.CallAndroidStaticMethod("SendIAP", new object[]
			{
				in_app_purchase_data,
				in_app_purchase_signature,
				name,
				price,
				currency,
				locale
			});
		}

		public static string GetRewardId(string tag)
		{
			return AdManager.GetAndroidStatic<string>("GetRewardId", new object[]
			{
				tag
			});
		}

		private static AdManager reference;

		private const string unsupportedPlatformMessage = "We support iOS and Android platforms only.";

		private const string TAPDAQ_PLACEMENT_DEFAULT = "default";

		private TDSettings settings;
	}
}
