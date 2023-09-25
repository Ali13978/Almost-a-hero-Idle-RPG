using System;
using System.Collections.Generic;
using PlayFab.AuthenticationModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabAuthenticationAPI
	{
		public static bool IsEntityLoggedIn()
		{
			IPlayFabTransportPlugin plugin = PluginManager.GetPlugin<IPlayFabTransportPlugin>(PluginContract.PlayFab_Transport, string.Empty);
			return !string.IsNullOrEmpty(plugin.EntityToken);
		}

		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void GetEntityToken(GetEntityTokenRequest request, Action<GetEntityTokenResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			AuthType authType = AuthType.None;
			if (authType == AuthType.None && PlayFabClientAPI.IsClientLoggedIn())
			{
				authType = AuthType.LoginSession;
			}
			PlayFabHttp.MakeApiCall<GetEntityTokenResponse>("/Authentication/GetEntityToken", request, authType, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
