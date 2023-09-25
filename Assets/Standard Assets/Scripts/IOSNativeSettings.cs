using System;
using System.Collections.Generic;
using SA.IOSNative.Gestures;
using SA.IOSNative.Models;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class IOSNativeSettings : ScriptableObject
{
	public static IOSNativeSettings Instance
	{
		get
		{
			if (IOSNativeSettings.instance == null)
			{
				IOSNativeSettings.instance = (Resources.Load("IOSNativeSettings") as IOSNativeSettings);
				if (IOSNativeSettings.instance == null)
				{
					IOSNativeSettings.instance = ScriptableObject.CreateInstance<IOSNativeSettings>();
				}
			}
			return IOSNativeSettings.instance;
		}
	}

	public const string VERSION_NUMBER = "9.11/24";

	public bool EnableGameCenterAPI = true;

	public bool EnableInAppsAPI = true;

	public bool EnableCameraAPI = true;

	public bool EnableSocialSharingAPI = true;

	public bool EnablePickerAPI;

	public bool EnableMediaPlayerAPI;

	public bool EnableReplayKit;

	public bool EnableCloudKit;

	public bool EnableSoomla;

	public bool EnableGestureAPI;

	public bool EnableForceTouchAPI;

	public bool EnablePushNotificationsAPI;

	public bool EnableContactsAPI;

	public bool EnableAppEventsAPI;

	public bool EnableUserNotificationsAPI;

	public bool EnablePermissionAPI;

	public string AppleId = "XXXXXXXXX";

	public int ToolbarIndex;

	public bool ExpandMoreActionsMenu = true;

	public bool ExpandModulesSettings = true;

	public bool InAppsEditorTesting = true;

	public bool CheckInternetBeforeLoadRequest;

	public bool PromotedPurchaseSupport = true;

	public TransactionsHandlingMode TransactionsHandlingMode;

	public List<string> DefaultStoreProductsView = new List<string>();

	public List<Product> InAppProducts = new List<Product>();

	public bool ShowStoreKitProducts = true;

	public List<GK_Leaderboard> Leaderboards = new List<GK_Leaderboard>();

	public List<GK_AchievementTemplate> Achievements = new List<GK_AchievementTemplate>();

	public bool UseGCRequestCaching;

	public bool UsePPForAchievements;

	public bool AutoLoadUsersSmallImages = true;

	public bool AutoLoadUsersBigImages;

	public bool ShowLeaderboards = true;

	public bool ShowAchievementsParams = true;

	public bool AdEditorTesting = true;

	public int EditorFillRateIndex = 4;

	public int EditorFillRate = 100;

	public int MaxImageLoadSize = 512;

	public float JPegCompressionRate = 0.8f;

	public IOSGalleryLoadImageFormat GalleryImageFormat = IOSGalleryLoadImageFormat.JPEG;

	public int RPK_iPadViewType;

	public string CameraUsageDescription = "for making pictures";

	public string PhotoLibraryUsageDescription = "for taking pictures";

	public string AppleMusicUsageDescription = "for playing music";

	public string ContactsUsageDescription = "for contacts reading";

	public List<UrlType> UrlTypes = new List<UrlType>();

	public List<UrlType> ApplicationQueriesSchemes = new List<UrlType>();

	public List<ForceTouchMenuItem> ForceTouchMenu = new List<ForceTouchMenuItem>();

	public bool DisablePluginLogs;

	public string SoomlaDownloadLink = "http://goo.gl/7LYwuj";

	public string SoomlaDocsLink = "https://goo.gl/JFkpNa";

	public string SoomlaGameKey = string.Empty;

	public string SoomlaEnvKey = string.Empty;

	public bool OneSignalEnabled;

	public string OneSignalDocsLink = "https://goo.gl/Royty6";

	private const string ISNSettingsAssetName = "IOSNativeSettings";

	private const string ISNSettingsAssetExtension = ".asset";

	private static IOSNativeSettings instance;
}
