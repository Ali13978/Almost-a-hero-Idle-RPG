using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.LocalizationModels;

namespace PlayFab
{
	public static class PlayFabLocalizationAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void GetLanguageList(GetLanguageListRequest request, Action<GetLanguageListResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetLanguageListResponse>("/Locale/GetLanguageList", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
