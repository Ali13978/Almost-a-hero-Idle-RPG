using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PlayFab.ClientModels;
using PlayFab.SharedModels;
using UnityEngine;

namespace PlayFab.Internal
{
	public static class PlayFabDeviceUtil
	{
		private static void DoAttributeInstall()
		{
			if (!PlayFabDeviceUtil._needsAttribution || PlayFabSettings.DisableAdvertising)
			{
				return;
			}
			AttributeInstallRequest attributeInstallRequest = new AttributeInstallRequest();
			string advertisingIdType = PlayFabSettings.AdvertisingIdType;
			if (advertisingIdType != null)
			{
				if (!(advertisingIdType == "Adid"))
				{
					if (advertisingIdType == "Idfa")
					{
						attributeInstallRequest.Idfa = PlayFabSettings.AdvertisingIdValue;
					}
				}
				else
				{
					attributeInstallRequest.Adid = PlayFabSettings.AdvertisingIdValue;
				}
			}
			AttributeInstallRequest request = attributeInstallRequest;
			if (PlayFabDeviceUtil._003C_003Ef__mg_0024cache0 == null)
			{
				PlayFabDeviceUtil._003C_003Ef__mg_0024cache0 = new Action<AttributeInstallResult>(PlayFabDeviceUtil.OnAttributeInstall);
			}
			PlayFabClientAPI.AttributeInstall(request, PlayFabDeviceUtil._003C_003Ef__mg_0024cache0, null, null, null);
		}

		private static void OnAttributeInstall(AttributeInstallResult result)
		{
			PlayFabSettings.AdvertisingIdType += "_Successful";
		}

		private static void SendDeviceInfoToPlayFab()
		{
			if (PlayFabSettings.DisableDeviceInfo || !PlayFabDeviceUtil._gatherDeviceInfo)
			{
				return;
			}
			ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
			DeviceInfoRequest deviceInfoRequest = new DeviceInfoRequest
			{
				Info = plugin.DeserializeObject<Dictionary<string, object>>(plugin.SerializeObject(new PlayFabDataGatherer()))
			};
			DeviceInfoRequest request = deviceInfoRequest;
			Action<EmptyResponse> resultCallback = null;
			if (PlayFabDeviceUtil._003C_003Ef__mg_0024cache1 == null)
			{
				PlayFabDeviceUtil._003C_003Ef__mg_0024cache1 = new Action<PlayFabError>(PlayFabDeviceUtil.OnGatherFail);
			}
			PlayFabClientAPI.ReportDeviceInfo(request, resultCallback, PlayFabDeviceUtil._003C_003Ef__mg_0024cache1, null, null);
		}

		private static void OnGatherFail(PlayFabError error)
		{
			UnityEngine.Debug.Log("OnGatherFail: " + error.GenerateErrorReport());
		}

		public static void OnPlayFabLogin(PlayFabResultCommon result)
		{
			LoginResult loginResult = result as LoginResult;
			RegisterPlayFabUserResult registerPlayFabUserResult = result as RegisterPlayFabUserResult;
			if (loginResult == null && registerPlayFabUserResult == null)
			{
				return;
			}
			UserSettings settingsForUser = null;
			string playFabId = null;
			string entityId = null;
			string entityType = null;
			if (loginResult != null)
			{
				settingsForUser = loginResult.SettingsForUser;
				playFabId = loginResult.PlayFabId;
				if (loginResult.EntityToken != null)
				{
					entityId = loginResult.EntityToken.Entity.Id;
					entityType = loginResult.EntityToken.Entity.Type;
				}
			}
			else if (registerPlayFabUserResult != null)
			{
				settingsForUser = registerPlayFabUserResult.SettingsForUser;
				playFabId = registerPlayFabUserResult.PlayFabId;
				if (registerPlayFabUserResult.EntityToken != null)
				{
					entityId = registerPlayFabUserResult.EntityToken.Entity.Id;
					entityType = registerPlayFabUserResult.EntityToken.Entity.Type;
				}
			}
			PlayFabDeviceUtil._OnPlayFabLogin(settingsForUser, playFabId, entityId, entityType);
		}

		private static void _OnPlayFabLogin(UserSettings settingsForUser, string playFabId, string entityId, string entityType)
		{
			PlayFabDeviceUtil._needsAttribution = (PlayFabDeviceUtil._gatherDeviceInfo = (PlayFabDeviceUtil._gatherScreenTime = false));
			if (settingsForUser != null)
			{
				PlayFabDeviceUtil._needsAttribution = settingsForUser.NeedsAttribution;
				PlayFabDeviceUtil._gatherDeviceInfo = settingsForUser.GatherDeviceInfo;
				PlayFabDeviceUtil._gatherScreenTime = settingsForUser.GatherFocusInfo;
			}
			if (PlayFabSettings.AdvertisingIdType != null && PlayFabSettings.AdvertisingIdValue != null)
			{
				PlayFabDeviceUtil.DoAttributeInstall();
			}
			else
			{
				PlayFabDeviceUtil.GetAdvertIdFromUnity();
			}
			PlayFabDeviceUtil.SendDeviceInfoToPlayFab();
			if (!string.IsNullOrEmpty(entityId) && !string.IsNullOrEmpty(entityType) && PlayFabDeviceUtil._gatherScreenTime)
			{
				PlayFabHttp.InitializeScreenTimeTracker(entityId, entityType, playFabId);
			}
			else
			{
				PlayFabSettings.DisableFocusTimeCollection = true;
			}
		}

		private static void GetAdvertIdFromUnity()
		{
			Application.RequestAdvertisingIdentifierAsync(delegate(string advertisingId, bool trackingEnabled, string error)
			{
				PlayFabSettings.DisableAdvertising = !trackingEnabled;
				if (!trackingEnabled)
				{
					return;
				}
				PlayFabSettings.AdvertisingIdType = "Adid";
				PlayFabSettings.AdvertisingIdValue = advertisingId;
				PlayFabDeviceUtil.DoAttributeInstall();
			});
		}

		private static bool _needsAttribution;

		private static bool _gatherDeviceInfo;

		private static bool _gatherScreenTime;

		[CompilerGenerated]
		private static Action<AttributeInstallResult> _003C_003Ef__mg_0024cache0;

		[CompilerGenerated]
		private static Action<PlayFabError> _003C_003Ef__mg_0024cache1;
	}
}
