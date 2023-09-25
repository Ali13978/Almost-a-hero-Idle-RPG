using System;
using System.Runtime.CompilerServices;
using PlayFab.ClientModels;

namespace PlayFab.Internal
{
	public static class PlayFabIdfa
	{
		public static void DoAttributeInstall()
		{
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
			if (PlayFabIdfa._003C_003Ef__mg_0024cache0 == null)
			{
				PlayFabIdfa._003C_003Ef__mg_0024cache0 = new Action<AttributeInstallResult>(PlayFabIdfa.OnAttributeInstall);
			}
			PlayFabClientAPI.AttributeInstall(request, PlayFabIdfa._003C_003Ef__mg_0024cache0, null, null, null);
		}

		private static void OnAttributeInstall(AttributeInstallResult result)
		{
			PlayFabSettings.AdvertisingIdType += "_Successful";
		}

		public static void OnPlayFabLogin()
		{
			if (PlayFabSettings.DisableAdvertising)
			{
				return;
			}
			if (PlayFabSettings.AdvertisingIdType != null && PlayFabSettings.AdvertisingIdValue != null)
			{
				PlayFabIdfa.DoAttributeInstall();
			}
		}

		[CompilerGenerated]
		private static Action<AttributeInstallResult> _003C_003Ef__mg_0024cache0;
	}
}
