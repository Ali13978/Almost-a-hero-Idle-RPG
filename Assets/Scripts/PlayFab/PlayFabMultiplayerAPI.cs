using System;
using System.Collections.Generic;
using PlayFab.Internal;
using PlayFab.MultiplayerModels;

namespace PlayFab
{
	public static class PlayFabMultiplayerAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void CreateBuildWithCustomContainer(CreateBuildWithCustomContainerRequest request, Action<CreateBuildWithCustomContainerResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<CreateBuildWithCustomContainerResponse>("/MultiplayerServer/CreateBuildWithCustomContainer", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateBuildWithManagedContainer(CreateBuildWithManagedContainerRequest request, Action<CreateBuildWithManagedContainerResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<CreateBuildWithManagedContainerResponse>("/MultiplayerServer/CreateBuildWithManagedContainer", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateRemoteUser(CreateRemoteUserRequest request, Action<CreateRemoteUserResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<CreateRemoteUserResponse>("/MultiplayerServer/CreateRemoteUser", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteAsset(DeleteAssetRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/DeleteAsset", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteBuild(DeleteBuildRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/DeleteBuild", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteCertificate(DeleteCertificateRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/DeleteCertificate", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteRemoteUser(DeleteRemoteUserRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/DeleteRemoteUser", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void EnableMultiplayerServersForTitle(EnableMultiplayerServersForTitleRequest request, Action<EnableMultiplayerServersForTitleResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EnableMultiplayerServersForTitleResponse>("/MultiplayerServer/EnableMultiplayerServersForTitle", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetAssetUploadUrl(GetAssetUploadUrlRequest request, Action<GetAssetUploadUrlResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetAssetUploadUrlResponse>("/MultiplayerServer/GetAssetUploadUrl", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetBuild(GetBuildRequest request, Action<GetBuildResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetBuildResponse>("/MultiplayerServer/GetBuild", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetContainerRegistryCredentials(GetContainerRegistryCredentialsRequest request, Action<GetContainerRegistryCredentialsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetContainerRegistryCredentialsResponse>("/MultiplayerServer/GetContainerRegistryCredentials", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetMultiplayerServerDetails(GetMultiplayerServerDetailsRequest request, Action<GetMultiplayerServerDetailsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetMultiplayerServerDetailsResponse>("/MultiplayerServer/GetMultiplayerServerDetails", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetRemoteLoginEndpoint(GetRemoteLoginEndpointRequest request, Action<GetRemoteLoginEndpointResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetRemoteLoginEndpointResponse>("/MultiplayerServer/GetRemoteLoginEndpoint", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleEnabledForMultiplayerServersStatus(GetTitleEnabledForMultiplayerServersStatusRequest request, Action<GetTitleEnabledForMultiplayerServersStatusResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetTitleEnabledForMultiplayerServersStatusResponse>("/MultiplayerServer/GetTitleEnabledForMultiplayerServersStatus", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListArchivedMultiplayerServers(ListMultiplayerServersRequest request, Action<ListMultiplayerServersResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListMultiplayerServersResponse>("/MultiplayerServer/ListArchivedMultiplayerServers", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListAssetSummaries(ListAssetSummariesRequest request, Action<ListAssetSummariesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListAssetSummariesResponse>("/MultiplayerServer/ListAssetSummaries", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListBuildSummaries(ListBuildSummariesRequest request, Action<ListBuildSummariesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListBuildSummariesResponse>("/MultiplayerServer/ListBuildSummaries", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListCertificateSummaries(ListCertificateSummariesRequest request, Action<ListCertificateSummariesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListCertificateSummariesResponse>("/MultiplayerServer/ListCertificateSummaries", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListContainerImages(ListContainerImagesRequest request, Action<ListContainerImagesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListContainerImagesResponse>("/MultiplayerServer/ListContainerImages", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListContainerImageTags(ListContainerImageTagsRequest request, Action<ListContainerImageTagsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListContainerImageTagsResponse>("/MultiplayerServer/ListContainerImageTags", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListMultiplayerServers(ListMultiplayerServersRequest request, Action<ListMultiplayerServersResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListMultiplayerServersResponse>("/MultiplayerServer/ListMultiplayerServers", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListQosServers(ListQosServersRequest request, Action<ListQosServersResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListQosServersResponse>("/MultiplayerServer/ListQosServers", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListVirtualMachineSummaries(ListVirtualMachineSummariesRequest request, Action<ListVirtualMachineSummariesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListVirtualMachineSummariesResponse>("/MultiplayerServer/ListVirtualMachineSummaries", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RequestMultiplayerServer(RequestMultiplayerServerRequest request, Action<RequestMultiplayerServerResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<RequestMultiplayerServerResponse>("/MultiplayerServer/RequestMultiplayerServer", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RolloverContainerRegistryCredentials(RolloverContainerRegistryCredentialsRequest request, Action<RolloverContainerRegistryCredentialsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<RolloverContainerRegistryCredentialsResponse>("/MultiplayerServer/RolloverContainerRegistryCredentials", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ShutdownMultiplayerServer(ShutdownMultiplayerServerRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/ShutdownMultiplayerServer", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateBuildRegions(UpdateBuildRegionsRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/UpdateBuildRegions", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UploadCertificate(UploadCertificateRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/MultiplayerServer/UploadCertificate", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
