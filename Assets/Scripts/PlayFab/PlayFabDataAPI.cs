using System;
using System.Collections.Generic;
using PlayFab.DataModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabDataAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AbortFileUploads(AbortFileUploadsRequest request, Action<AbortFileUploadsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<AbortFileUploadsResponse>("/File/AbortFileUploads", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteFiles(DeleteFilesRequest request, Action<DeleteFilesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<DeleteFilesResponse>("/File/DeleteFiles", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void FinalizeFileUploads(FinalizeFileUploadsRequest request, Action<FinalizeFileUploadsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<FinalizeFileUploadsResponse>("/File/FinalizeFileUploads", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFiles(GetFilesRequest request, Action<GetFilesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetFilesResponse>("/File/GetFiles", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetObjects(GetObjectsRequest request, Action<GetObjectsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetObjectsResponse>("/Object/GetObjects", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void InitiateFileUploads(InitiateFileUploadsRequest request, Action<InitiateFileUploadsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<InitiateFileUploadsResponse>("/File/InitiateFileUploads", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetObjects(SetObjectsRequest request, Action<SetObjectsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<SetObjectsResponse>("/Object/SetObjects", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
