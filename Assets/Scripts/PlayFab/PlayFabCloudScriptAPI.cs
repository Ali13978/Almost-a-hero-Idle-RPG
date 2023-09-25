using System;
using System.Collections.Generic;
using PlayFab.CloudScriptModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabCloudScriptAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void ExecuteEntityCloudScript(ExecuteEntityCloudScriptRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ExecuteCloudScriptResult>("/CloudScript/ExecuteEntityCloudScript", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
