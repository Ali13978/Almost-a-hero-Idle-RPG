using System;
using System.Reflection;
using UnityEngine;

namespace SA.Common
{
	public class Config
	{
		public static string FB_SDK_VersionCode
		{
			get
			{
				string result = "Undefined";
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					Type type = assembly.GetType("Facebook.FBBuildVersionAttribute");
					Type type2 = assembly.GetType("Facebook.IFacebook");
					if (type2 != null && type != null)
					{
						MethodInfo method = type.GetMethod("GetVersionAttributeOfType", BindingFlags.Static | BindingFlags.Public);
						if (method != null)
						{
							object obj = method.Invoke(null, new object[]
							{
								type2
							});
							PropertyInfo property = type.GetProperty("SdkVersion");
							if (obj != null && property != null)
							{
								string text = property.GetValue(obj, null) as string;
								if (text != null)
								{
									result = text;
								}
							}
						}
						break;
					}
				}
				Type type3 = Type.GetType("Facebook.Unity.FacebookSdkVersion");
				if (type3 != null)
				{
					PropertyInfo property2 = type3.GetProperty("Build", BindingFlags.Static | BindingFlags.Public);
					if (property2 != null)
					{
						result = (string)property2.GetValue(null, null);
					}
				}
				else
				{
					foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
					{
						Type type4 = assembly2.GetType("Facebook.Unity.FacebookSdkVersion");
						if (type4 != null)
						{
							PropertyInfo property3 = type4.GetProperty("Build", BindingFlags.Static | BindingFlags.Public);
							if (property3 != null)
							{
								result = (string)property3.GetValue(null, null);
							}
						}
					}
				}
				return result;
			}
		}

		public static int FB_SDK_MajorVersionCode
		{
			get
			{
				string fb_SDK_VersionCode = Config.FB_SDK_VersionCode;
				int result = 0;
				if (fb_SDK_VersionCode.Equals("Undefined"))
				{
					return result;
				}
				try
				{
					string[] array = fb_SDK_VersionCode.Split(new char[]
					{
						'.'
					});
					result = Convert.ToInt32(array[0]);
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogWarning("FB_SDK_MajorVersionCode failed: " + ex.Message);
				}
				return result;
			}
		}

		public const string LIB_VERSION = "24";

		public const int VERSION_UNDEFINED = 0;

		public const string VERSION_UNDEFINED_STRING = "Undefined";

		public const string SUPPORT_EMAIL = "support@stansassets.com";

		public const string WEBSITE_ROOT_URL = "https://stansassets.com/";

		public const string BUNDLES_PATH = "Plugins/StansAssets/Bundles/";

		public const string MODULS_PATH = "Plugins/StansAssets/Modules/";

		public const string SUPPORT_MODULS_PATH = "Plugins/StansAssets/Support/";

		public const string COMMON_LIB_PATH = "Plugins/StansAssets/Support/Common/";

		public const string VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/";

		public const string NATIVE_LIBRARIES_PATH = "Plugins/StansAssets/Support/NativeLibraries/";

		public const string EDITOR_TESTING_LIB_PATH = "Plugins/StansAssets/Support/EditorTesting/";

		public const string SETTINGS_REMOVE_PATH = "Plugins/StansAssets/Support/Settings/";

		public const string SETTINGS_PATH = "Plugins/StansAssets/Support/Settings/Resources/";

		public const string ANDROID_DESTANATION_PATH = "Plugins/Android/";

		public const string ANDROID_SOURCE_PATH = "Plugins/StansAssets/Support/NativeLibraries/Android/";

		public const string IOS_DESTANATION_PATH = "Plugins/IOS/";

		public const string IOS_SOURCE_PATH = "Plugins/StansAssets/Support/NativeLibraries/IOS/";

		public const string SPOTIFY_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/Spotify_VersionInfo.txt";

		public const string AN_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/AN_VersionInfo.txt";

		public const string UM_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/UM_VersionInfo.txt";

		public const string GMA_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/GMA_VersionInfo.txt";

		public const string MSP_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/MSP_VersionInfo.txt";

		public const string ISN_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/ISN_VersionInfo.txt";

		public const string MNP_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/MNP_VersionInfo.txt";

		public const string AMN_VERSION_INFO_PATH = "Plugins/StansAssets/Support/Versions/AMN_VersionInfo.txt";
	}
}
