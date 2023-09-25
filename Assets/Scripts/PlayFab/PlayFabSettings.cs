using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PlayFab
{
	public static class PlayFabSettings
	{
		private static PlayFabSharedSettings PlayFabSharedPrivate
		{
			get
			{
				if (PlayFabSettings._playFabShared == null)
				{
					PlayFabSettings._playFabShared = PlayFabSettings.GetSharedSettingsObjectPrivate();
				}
				return PlayFabSettings._playFabShared;
			}
		}

		public static void SetSharedSettings(PlayFabSharedSettings settings)
		{
			PlayFabSettings._playFabShared = settings;
		}

		private static PlayFabSharedSettings GetSharedSettingsObjectPrivate()
		{
			PlayFabSharedSettings playFabSharedSettings = Resources.Load<PlayFabSharedSettings>("PlayFabSharedSettings");
			if (playFabSharedSettings == null)
			{
				throw new Exception("PlayFabSharedSettings object not found");
			}
			return playFabSharedSettings;
		}

		public static string DeviceUniqueIdentifier
		{
			get
			{
				string empty = string.Empty;
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getContentResolver", new object[0]);
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.provider.Settings$Secure");
				return androidJavaClass2.CallStatic<string>("getString", new object[]
				{
					androidJavaObject,
					"android_id"
				});
			}
		}

		private static string ProductionEnvironmentUrlPrivate
		{
			get
			{
				return string.IsNullOrEmpty(PlayFabSettings.PlayFabSharedPrivate.ProductionEnvironmentUrl) ? ".playfabapi.com" : PlayFabSettings.PlayFabSharedPrivate.ProductionEnvironmentUrl;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.ProductionEnvironmentUrl = value;
			}
		}

		public static string TitleId
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.TitleId;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.TitleId = value;
			}
		}

		public static string VerticalName
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.VerticalName;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.VerticalName = value;
			}
		}

		public static PlayFabLogLevel LogLevel
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LogLevel;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LogLevel = value;
			}
		}

		public static WebRequestType RequestType
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestType;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestType = value;
			}
		}

		public static int RequestTimeout
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestTimeout;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestTimeout = value;
			}
		}

		public static bool RequestKeepAlive
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.RequestKeepAlive;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.RequestKeepAlive = value;
			}
		}

		public static bool CompressApiData
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.CompressApiData;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.CompressApiData = value;
			}
		}

		public static string LoggerHost
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LoggerHost;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LoggerHost = value;
			}
		}

		public static int LoggerPort
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LoggerPort;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LoggerPort = value;
			}
		}

		public static bool EnableRealTimeLogging
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.EnableRealTimeLogging;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.EnableRealTimeLogging = value;
			}
		}

		public static int LogCapLimit
		{
			get
			{
				return PlayFabSettings.PlayFabSharedPrivate.LogCapLimit;
			}
			set
			{
				PlayFabSettings.PlayFabSharedPrivate.LogCapLimit = value;
			}
		}

		public static string GetFullUrl(string apiCall, Dictionary<string, string> getParams)
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			string productionEnvironmentUrlPrivate = PlayFabSettings.ProductionEnvironmentUrlPrivate;
			if (!productionEnvironmentUrlPrivate.StartsWith("http"))
			{
				if (!string.IsNullOrEmpty(PlayFabSettings.VerticalName))
				{
					stringBuilder.Append("https://").Append(PlayFabSettings.VerticalName);
				}
				else
				{
					stringBuilder.Append("https://").Append(PlayFabSettings.TitleId);
				}
			}
			stringBuilder.Append(productionEnvironmentUrlPrivate).Append(apiCall);
			if (getParams != null)
			{
				bool flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in getParams)
				{
					if (flag)
					{
						stringBuilder.Append("?");
						flag = false;
					}
					else
					{
						stringBuilder.Append("&");
					}
					stringBuilder.Append(keyValuePair.Key).Append("=").Append(keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		public const string AD_TYPE_IDFA = "Idfa";

		public const string AD_TYPE_ANDROID_ID = "Adid";

		public static string AdvertisingIdType = null;

		public static string AdvertisingIdValue = null;

		public static bool DisableAdvertising = false;

		public static bool DisableDeviceInfo = false;

		public static bool DisableFocusTimeCollection = false;

		private static PlayFabSharedSettings _playFabShared = null;

		public const string SdkVersion = "2.58.181218";

		public const string BuildIdentifier = "jbuild_unitysdk__sdk-unity-5-slave_0";

		public const string VersionString = "UnitySDK-2.58.181218";

		public static readonly Dictionary<string, string> RequestGetParams = new Dictionary<string, string>
		{
			{
				"sdk",
				"UnitySDK-2.58.181218"
			}
		};

		private const string DefaultPlayFabApiUrlPrivate = ".playfabapi.com";
	}
}
