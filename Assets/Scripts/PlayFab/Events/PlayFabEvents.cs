using System;
using System.Diagnostics;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using PlayFab.EventsModels;
using PlayFab.GroupsModels;
using PlayFab.Internal;
using PlayFab.LocalizationModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using PlayFab.SharedModels;

namespace PlayFab.Events
{
	public class PlayFabEvents
	{
		private PlayFabEvents()
		{
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityTokenRequest> OnAuthenticationGetEntityTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetEntityTokenResponse> OnAuthenticationGetEntityTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LoginResult> OnLoginResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest> OnAcceptTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse> OnAcceptTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddFriendRequest> OnAddFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddFriendResult> OnAddFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest> OnAddGenericIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddGenericIDResult> OnAddGenericIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest> OnAddOrUpdateContactEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult> OnAddOrUpdateContactEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddSharedGroupMembersRequest> OnAddSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddSharedGroupMembersResult> OnAddSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest> OnAddUsernamePasswordRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult> OnAddUsernamePasswordResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddUserVirtualCurrencyRequest> OnAddUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnAddUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest> OnAndroidDevicePushNotificationRegistrationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult> OnAndroidDevicePushNotificationRegistrationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest> OnAttributeInstallRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AttributeInstallResult> OnAttributeInstallResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest> OnCancelTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CancelTradeResponse> OnCancelTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest> OnConfirmPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult> OnConfirmPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ConsumeItemRequest> OnConsumeItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ConsumeItemResult> OnConsumeItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ConsumePSNEntitlementsRequest> OnConsumePSNEntitlementsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ConsumePSNEntitlementsResult> OnConsumePSNEntitlementsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ConsumeXboxEntitlementsRequest> OnConsumeXboxEntitlementsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ConsumeXboxEntitlementsResult> OnConsumeXboxEntitlementsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateSharedGroupRequest> OnCreateSharedGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateSharedGroupResult> OnCreateSharedGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest> OnExecuteCloudScriptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult> OnExecuteCloudScriptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest> OnGetAccountInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult> OnGetAccountInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListUsersCharactersRequest> OnGetAllUsersCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListUsersCharactersResult> OnGetAllUsersCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCatalogItemsRequest> OnGetCatalogItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCatalogItemsResult> OnGetCatalogItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterInventoryRequest> OnGetCharacterInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterInventoryResult> OnGetCharacterInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterLeaderboardRequest> OnGetCharacterLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterLeaderboardResult> OnGetCharacterLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterStatisticsRequest> OnGetCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterStatisticsResult> OnGetCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetContentDownloadUrlRequest> OnGetContentDownloadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetContentDownloadUrlResult> OnGetContentDownloadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest> OnGetCurrentGamesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CurrentGamesResult> OnGetCurrentGamesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardRequest> OnGetFriendLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult> OnGetFriendLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest> OnGetFriendLeaderboardAroundPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult> OnGetFriendLeaderboardAroundPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendsListRequest> OnGetFriendsListRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetFriendsListResult> OnGetFriendsListResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest> OnGetGameServerRegionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult> OnGetGameServerRegionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardRequest> OnGetLeaderboardRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult> OnGetLeaderboardResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest> OnGetLeaderboardAroundCharacterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundCharacterResult> OnGetLeaderboardAroundCharacterResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest> OnGetLeaderboardAroundPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult> OnGetLeaderboardAroundPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest> OnGetLeaderboardForUserCharactersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardForUsersCharactersResult> OnGetLeaderboardForUserCharactersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest> OnGetPaymentTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult> OnGetPaymentTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest> OnGetPhotonAuthenticationTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult> OnGetPhotonAuthenticationTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerCombinedInfoRequest> OnGetPlayerCombinedInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerCombinedInfoResult> OnGetPlayerCombinedInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerProfileRequest> OnGetPlayerProfileRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerProfileResult> OnGetPlayerProfileResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest> OnGetPlayerSegmentsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerSegmentsResult> OnGetPlayerSegmentsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticsRequest> OnGetPlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticsResult> OnGetPlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticVersionsRequest> OnGetPlayerStatisticVersionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticVersionsResult> OnGetPlayerStatisticVersionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerTagsRequest> OnGetPlayerTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerTagsResult> OnGetPlayerTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest> OnGetPlayerTradesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse> OnGetPlayerTradesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest> OnGetPlayFabIDsFromFacebookIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult> OnGetPlayFabIDsFromFacebookIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest> OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult> OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest> OnGetPlayFabIDsFromGameCenterIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult> OnGetPlayFabIDsFromGameCenterIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest> OnGetPlayFabIDsFromGenericIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult> OnGetPlayFabIDsFromGenericIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest> OnGetPlayFabIDsFromGoogleIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult> OnGetPlayFabIDsFromGoogleIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest> OnGetPlayFabIDsFromKongregateIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult> OnGetPlayFabIDsFromKongregateIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest> OnGetPlayFabIDsFromPSNAccountIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult> OnGetPlayFabIDsFromPSNAccountIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest> OnGetPlayFabIDsFromSteamIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult> OnGetPlayFabIDsFromSteamIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest> OnGetPlayFabIDsFromTwitchIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult> OnGetPlayFabIDsFromTwitchIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest> OnGetPlayFabIDsFromXboxLiveIDsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult> OnGetPlayFabIDsFromXboxLiveIDsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPublisherDataRequest> OnGetPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPublisherDataResult> OnGetPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest> OnGetPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetPurchaseResult> OnGetPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetSharedGroupDataRequest> OnGetSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetSharedGroupDataResult> OnGetSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetStoreItemsRequest> OnGetStoreItemsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetStoreItemsResult> OnGetStoreItemsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTimeRequest> OnGetTimeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTimeResult> OnGetTimeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleDataRequest> OnGetTitleDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTitleDataResult> OnGetTitleDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleNewsRequest> OnGetTitleNewsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTitleNewsResult> OnGetTitleNewsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest> OnGetTitlePublicKeyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult> OnGetTitlePublicKeyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest> OnGetTradeStatusRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse> OnGetTradeStatusResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserInventoryRequest> OnGetUserInventoryRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserInventoryResult> OnGetUserInventoryResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserReadOnlyDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserReadOnlyDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest> OnGetWindowsHelloChallengeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse> OnGetWindowsHelloChallengeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GrantCharacterToUserRequest> OnGrantCharacterToUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GrantCharacterToUserResult> OnGrantCharacterToUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest> OnLinkAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult> OnLinkAndroidDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest> OnLinkCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult> OnLinkCustomIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest> OnLinkFacebookAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult> OnLinkFacebookAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest> OnLinkFacebookInstantGamesIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkFacebookInstantGamesIdResult> OnLinkFacebookInstantGamesIdResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest> OnLinkGameCenterAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult> OnLinkGameCenterAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest> OnLinkGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult> OnLinkGoogleAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest> OnLinkIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult> OnLinkIOSDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest> OnLinkKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult> OnLinkKongregateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest> OnLinkNintendoSwitchDeviceIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult> OnLinkNintendoSwitchDeviceIdResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkOpenIdConnectRequest> OnLinkOpenIdConnectRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<EmptyResult> OnLinkOpenIdConnectResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkPSNAccountRequest> OnLinkPSNAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkPSNAccountResult> OnLinkPSNAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest> OnLinkSteamAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult> OnLinkSteamAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest> OnLinkTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult> OnLinkTwitchResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest> OnLinkWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse> OnLinkWindowsHelloResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LinkXboxAccountRequest> OnLinkXboxAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<LinkXboxAccountResult> OnLinkXboxAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest> OnLoginWithAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest> OnLoginWithCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest> OnLoginWithEmailAddressRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest> OnLoginWithFacebookRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest> OnLoginWithFacebookInstantGamesIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest> OnLoginWithGameCenterRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest> OnLoginWithGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest> OnLoginWithIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest> OnLoginWithKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest> OnLoginWithNintendoSwitchDeviceIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithOpenIdConnectRequest> OnLoginWithOpenIdConnectRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest> OnLoginWithPlayFabRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithPSNRequest> OnLoginWithPSNRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest> OnLoginWithSteamRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest> OnLoginWithTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest> OnLoginWithWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithXboxRequest> OnLoginWithXboxRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest> OnMatchmakeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<MatchmakeResult> OnMatchmakeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest> OnOpenTradeRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<OpenTradeResponse> OnOpenTradeResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest> OnPayForPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult> OnPayForPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest> OnPurchaseItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PurchaseItemResult> OnPurchaseItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RedeemCouponRequest> OnRedeemCouponRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RedeemCouponResult> OnRedeemCouponResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RefreshPSNAuthTokenRequest> OnRefreshPSNAuthTokenRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnRefreshPSNAuthTokenResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest> OnRegisterForIOSPushNotificationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult> OnRegisterForIOSPushNotificationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest> OnRegisterPlayFabUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult> OnRegisterPlayFabUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest> OnRegisterWithWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest> OnRemoveContactEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult> OnRemoveContactEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveFriendRequest> OnRemoveFriendRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveFriendResult> OnRemoveFriendResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest> OnRemoveGenericIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult> OnRemoveGenericIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveSharedGroupMembersRequest> OnRemoveSharedGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RemoveSharedGroupMembersResult> OnRemoveSharedGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest> OnReportDeviceInfoRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnReportDeviceInfoResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest> OnReportPlayerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult> OnReportPlayerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest> OnRestoreIOSPurchasesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult> OnRestoreIOSPurchasesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SendAccountRecoveryEmailRequest> OnSendAccountRecoveryEmailRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SendAccountRecoveryEmailResult> OnSendAccountRecoveryEmailResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetFriendTagsRequest> OnSetFriendTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult> OnSetFriendTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetPlayerSecretRequest> OnSetPlayerSecretRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetPlayerSecretResult> OnSetPlayerSecretResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<StartGameRequest> OnStartGameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<StartGameResult> OnStartGameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest> OnStartPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<StartPurchaseResult> OnStartPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest> OnSubtractUserVirtualCurrencyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnSubtractUserVirtualCurrencyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest> OnUnlinkAndroidDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult> OnUnlinkAndroidDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest> OnUnlinkCustomIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult> OnUnlinkCustomIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest> OnUnlinkFacebookAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult> OnUnlinkFacebookAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest> OnUnlinkFacebookInstantGamesIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult> OnUnlinkFacebookInstantGamesIdResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest> OnUnlinkGameCenterAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult> OnUnlinkGameCenterAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest> OnUnlinkGoogleAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult> OnUnlinkGoogleAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest> OnUnlinkIOSDeviceIDRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult> OnUnlinkIOSDeviceIDResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest> OnUnlinkKongregateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult> OnUnlinkKongregateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest> OnUnlinkNintendoSwitchDeviceIdRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult> OnUnlinkNintendoSwitchDeviceIdResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UninkOpenIdConnectRequest> OnUnlinkOpenIdConnectRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkOpenIdConnectResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkPSNAccountRequest> OnUnlinkPSNAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkPSNAccountResult> OnUnlinkPSNAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest> OnUnlinkSteamAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult> OnUnlinkSteamAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest> OnUnlinkTwitchRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult> OnUnlinkTwitchResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest> OnUnlinkWindowsHelloRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse> OnUnlinkWindowsHelloResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkXboxAccountRequest> OnUnlinkXboxAccountRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlinkXboxAccountResult> OnUnlinkXboxAccountResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlockContainerInstanceRequest> OnUnlockContainerInstanceRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerInstanceResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnlockContainerItemRequest> OnUnlockContainerItemRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerItemResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateAvatarUrlRequest> OnUpdateAvatarUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUpdateAvatarUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCharacterDataRequest> OnUpdateCharacterDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateCharacterDataResult> OnUpdateCharacterDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCharacterStatisticsRequest> OnUpdateCharacterStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateCharacterStatisticsResult> OnUpdateCharacterStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticsRequest> OnUpdatePlayerStatisticsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticsResult> OnUpdatePlayerStatisticsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateSharedGroupDataRequest> OnUpdateSharedGroupDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateSharedGroupDataResult> OnUpdateSharedGroupDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserPublisherDataRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserPublisherDataResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest> OnUpdateUserTitleDisplayNameRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserTitleDisplayNameResult> OnUpdateUserTitleDisplayNameResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest> OnValidateAmazonIAPReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult> OnValidateAmazonIAPReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest> OnValidateGooglePlayPurchaseRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult> OnValidateGooglePlayPurchaseResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest> OnValidateIOSReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult> OnValidateIOSReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest> OnValidateWindowsStoreReceiptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult> OnValidateWindowsStoreReceiptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest> OnWriteCharacterEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWriteCharacterEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest> OnWritePlayerEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWritePlayerEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteTitleEventRequest> OnWriteTitleEventRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWriteTitleEventResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteEntityCloudScriptRequest> OnCloudScriptExecuteEntityCloudScriptRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult> OnCloudScriptExecuteEntityCloudScriptResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AbortFileUploadsRequest> OnDataAbortFileUploadsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<AbortFileUploadsResponse> OnDataAbortFileUploadsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteFilesRequest> OnDataDeleteFilesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<DeleteFilesResponse> OnDataDeleteFilesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<FinalizeFileUploadsRequest> OnDataFinalizeFileUploadsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<FinalizeFileUploadsResponse> OnDataFinalizeFileUploadsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetFilesRequest> OnDataGetFilesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetFilesResponse> OnDataGetFilesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetObjectsRequest> OnDataGetObjectsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetObjectsResponse> OnDataGetObjectsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<InitiateFileUploadsRequest> OnDataInitiateFileUploadsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<InitiateFileUploadsResponse> OnDataInitiateFileUploadsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetObjectsRequest> OnDataSetObjectsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetObjectsResponse> OnDataSetObjectsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest> OnEventsWriteEventsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<WriteEventsResponse> OnEventsWriteEventsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AcceptGroupApplicationRequest> OnGroupsAcceptGroupApplicationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupApplicationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AcceptGroupInvitationRequest> OnGroupsAcceptGroupInvitationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupInvitationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<AddMembersRequest> OnGroupsAddMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAddMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ApplyToGroupRequest> OnGroupsApplyToGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ApplyToGroupResponse> OnGroupsApplyToGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<BlockEntityRequest> OnGroupsBlockEntityRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsBlockEntityResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ChangeMemberRoleRequest> OnGroupsChangeMemberRoleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsChangeMemberRoleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateGroupRequest> OnGroupsCreateGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateGroupResponse> OnGroupsCreateGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateGroupRoleRequest> OnGroupsCreateRoleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateGroupRoleResponse> OnGroupsCreateRoleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteGroupRequest> OnGroupsDeleteGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteRoleRequest> OnGroupsDeleteRoleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteRoleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetGroupRequest> OnGroupsGetGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetGroupResponse> OnGroupsGetGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<InviteToGroupRequest> OnGroupsInviteToGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<InviteToGroupResponse> OnGroupsInviteToGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<IsMemberRequest> OnGroupsIsMemberRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<IsMemberResponse> OnGroupsIsMemberResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupApplicationsRequest> OnGroupsListGroupApplicationsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListGroupApplicationsResponse> OnGroupsListGroupApplicationsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupBlocksRequest> OnGroupsListGroupBlocksRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListGroupBlocksResponse> OnGroupsListGroupBlocksResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupInvitationsRequest> OnGroupsListGroupInvitationsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListGroupInvitationsResponse> OnGroupsListGroupInvitationsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupMembersRequest> OnGroupsListGroupMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListGroupMembersResponse> OnGroupsListGroupMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListMembershipRequest> OnGroupsListMembershipRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListMembershipResponse> OnGroupsListMembershipResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListMembershipOpportunitiesRequest> OnGroupsListMembershipOpportunitiesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListMembershipOpportunitiesResponse> OnGroupsListMembershipOpportunitiesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGroupApplicationRequest> OnGroupsRemoveGroupApplicationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupApplicationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGroupInvitationRequest> OnGroupsRemoveGroupInvitationRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupInvitationResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RemoveMembersRequest> OnGroupsRemoveMembersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveMembersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UnblockEntityRequest> OnGroupsUnblockEntityRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsUnblockEntityResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateGroupRequest> OnGroupsUpdateGroupRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateGroupResponse> OnGroupsUpdateGroupResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateGroupRoleRequest> OnGroupsUpdateRoleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<UpdateGroupRoleResponse> OnGroupsUpdateRoleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetLanguageListRequest> OnLocalizationGetLanguageListRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetLanguageListResponse> OnLocalizationGetLanguageListResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildWithCustomContainerRequest> OnMultiplayerCreateBuildWithCustomContainerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateBuildWithCustomContainerResponse> OnMultiplayerCreateBuildWithCustomContainerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildWithManagedContainerRequest> OnMultiplayerCreateBuildWithManagedContainerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateBuildWithManagedContainerResponse> OnMultiplayerCreateBuildWithManagedContainerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<CreateRemoteUserRequest> OnMultiplayerCreateRemoteUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<CreateRemoteUserResponse> OnMultiplayerCreateRemoteUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteAssetRequest> OnMultiplayerDeleteAssetRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteAssetResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteBuildRequest> OnMultiplayerDeleteBuildRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteCertificateRequest> OnMultiplayerDeleteCertificateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteCertificateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<DeleteRemoteUserRequest> OnMultiplayerDeleteRemoteUserRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteRemoteUserResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest> OnMultiplayerEnableMultiplayerServersForTitleRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<EnableMultiplayerServersForTitleResponse> OnMultiplayerEnableMultiplayerServersForTitleResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetAssetUploadUrlRequest> OnMultiplayerGetAssetUploadUrlRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetAssetUploadUrlResponse> OnMultiplayerGetAssetUploadUrlResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetBuildRequest> OnMultiplayerGetBuildRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetBuildResponse> OnMultiplayerGetBuildResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetContainerRegistryCredentialsRequest> OnMultiplayerGetContainerRegistryCredentialsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetContainerRegistryCredentialsResponse> OnMultiplayerGetContainerRegistryCredentialsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerDetailsRequest> OnMultiplayerGetMultiplayerServerDetailsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerDetailsResponse> OnMultiplayerGetMultiplayerServerDetailsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetRemoteLoginEndpointRequest> OnMultiplayerGetRemoteLoginEndpointRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetRemoteLoginEndpointResponse> OnMultiplayerGetRemoteLoginEndpointResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListArchivedMultiplayerServersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListArchivedMultiplayerServersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListAssetSummariesRequest> OnMultiplayerListAssetSummariesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListAssetSummariesResponse> OnMultiplayerListAssetSummariesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListBuildSummariesRequest> OnMultiplayerListBuildSummariesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListBuildSummariesResponse> OnMultiplayerListBuildSummariesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListCertificateSummariesRequest> OnMultiplayerListCertificateSummariesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListCertificateSummariesResponse> OnMultiplayerListCertificateSummariesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListContainerImagesRequest> OnMultiplayerListContainerImagesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListContainerImagesResponse> OnMultiplayerListContainerImagesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListContainerImageTagsRequest> OnMultiplayerListContainerImageTagsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListContainerImageTagsResponse> OnMultiplayerListContainerImageTagsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListMultiplayerServersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListMultiplayerServersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListQosServersRequest> OnMultiplayerListQosServersRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListQosServersResponse> OnMultiplayerListQosServersResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ListVirtualMachineSummariesRequest> OnMultiplayerListVirtualMachineSummariesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<ListVirtualMachineSummariesResponse> OnMultiplayerListVirtualMachineSummariesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RequestMultiplayerServerRequest> OnMultiplayerRequestMultiplayerServerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RequestMultiplayerServerResponse> OnMultiplayerRequestMultiplayerServerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest> OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse> OnMultiplayerRolloverContainerRegistryCredentialsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<ShutdownMultiplayerServerRequest> OnMultiplayerShutdownMultiplayerServerRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerShutdownMultiplayerServerResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionsRequest> OnMultiplayerUpdateBuildRegionsRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUpdateBuildRegionsResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<UploadCertificateRequest> OnMultiplayerUploadCertificateRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUploadCertificateResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetGlobalPolicyRequest> OnProfilesGetGlobalPolicyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetGlobalPolicyResponse> OnProfilesGetGlobalPolicyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityProfileRequest> OnProfilesGetProfileRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetEntityProfileResponse> OnProfilesGetProfileResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityProfilesRequest> OnProfilesGetProfilesRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<GetEntityProfilesResponse> OnProfilesGetProfilesResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetGlobalPolicyRequest> OnProfilesSetGlobalPolicyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetGlobalPolicyResponse> OnProfilesSetGlobalPolicyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetProfileLanguageRequest> OnProfilesSetProfileLanguageRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetProfileLanguageResponse> OnProfilesSetProfileLanguageResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabRequestEvent<SetEntityProfilePolicyRequest> OnProfilesSetProfilePolicyRequestEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabResultEvent<SetEntityProfilePolicyResponse> OnProfilesSetProfilePolicyResultEvent;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PlayFabEvents.PlayFabErrorEvent OnGlobalErrorEvent;

		public static PlayFabEvents Init()
		{
			if (PlayFabEvents._instance == null)
			{
				PlayFabEvents._instance = new PlayFabEvents();
			}
			PlayFabHttp.ApiProcessingEventHandler += PlayFabEvents._instance.OnProcessingEvent;
			PlayFabHttp.ApiProcessingErrorEventHandler += PlayFabEvents._instance.OnProcessingErrorEvent;
			return PlayFabEvents._instance;
		}

		public void UnregisterInstance(object instance)
		{
			if (this.OnLoginResultEvent != null)
			{
				foreach (Delegate @delegate in this.OnLoginResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(@delegate.Target, instance))
					{
						this.OnLoginResultEvent -= (PlayFabEvents.PlayFabResultEvent<LoginResult>)@delegate;
					}
				}
			}
			if (this.OnAcceptTradeRequestEvent != null)
			{
				foreach (Delegate delegate2 in this.OnAcceptTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate2.Target, instance))
					{
						this.OnAcceptTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest>)delegate2;
					}
				}
			}
			if (this.OnAcceptTradeResultEvent != null)
			{
				foreach (Delegate delegate3 in this.OnAcceptTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate3.Target, instance))
					{
						this.OnAcceptTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse>)delegate3;
					}
				}
			}
			if (this.OnAddFriendRequestEvent != null)
			{
				foreach (Delegate delegate4 in this.OnAddFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate4.Target, instance))
					{
						this.OnAddFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddFriendRequest>)delegate4;
					}
				}
			}
			if (this.OnAddFriendResultEvent != null)
			{
				foreach (Delegate delegate5 in this.OnAddFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate5.Target, instance))
					{
						this.OnAddFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddFriendResult>)delegate5;
					}
				}
			}
			if (this.OnAddGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate6 in this.OnAddGenericIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate6.Target, instance))
					{
						this.OnAddGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest>)delegate6;
					}
				}
			}
			if (this.OnAddGenericIDResultEvent != null)
			{
				foreach (Delegate delegate7 in this.OnAddGenericIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate7.Target, instance))
					{
						this.OnAddGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddGenericIDResult>)delegate7;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate8 in this.OnAddOrUpdateContactEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate8.Target, instance))
					{
						this.OnAddOrUpdateContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest>)delegate8;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailResultEvent != null)
			{
				foreach (Delegate delegate9 in this.OnAddOrUpdateContactEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate9.Target, instance))
					{
						this.OnAddOrUpdateContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult>)delegate9;
					}
				}
			}
			if (this.OnAddSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate10 in this.OnAddSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate10.Target, instance))
					{
						this.OnAddSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddSharedGroupMembersRequest>)delegate10;
					}
				}
			}
			if (this.OnAddSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate11 in this.OnAddSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate11.Target, instance))
					{
						this.OnAddSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddSharedGroupMembersResult>)delegate11;
					}
				}
			}
			if (this.OnAddUsernamePasswordRequestEvent != null)
			{
				foreach (Delegate delegate12 in this.OnAddUsernamePasswordRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate12.Target, instance))
					{
						this.OnAddUsernamePasswordRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest>)delegate12;
					}
				}
			}
			if (this.OnAddUsernamePasswordResultEvent != null)
			{
				foreach (Delegate delegate13 in this.OnAddUsernamePasswordResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate13.Target, instance))
					{
						this.OnAddUsernamePasswordResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult>)delegate13;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate14 in this.OnAddUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate14.Target, instance))
					{
						this.OnAddUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddUserVirtualCurrencyRequest>)delegate14;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate15 in this.OnAddUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate15.Target, instance))
					{
						this.OnAddUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)delegate15;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
			{
				foreach (Delegate delegate16 in this.OnAndroidDevicePushNotificationRegistrationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate16.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest>)delegate16;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
			{
				foreach (Delegate delegate17 in this.OnAndroidDevicePushNotificationRegistrationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate17.Target, instance))
					{
						this.OnAndroidDevicePushNotificationRegistrationResultEvent -= (PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult>)delegate17;
					}
				}
			}
			if (this.OnAttributeInstallRequestEvent != null)
			{
				foreach (Delegate delegate18 in this.OnAttributeInstallRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate18.Target, instance))
					{
						this.OnAttributeInstallRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest>)delegate18;
					}
				}
			}
			if (this.OnAttributeInstallResultEvent != null)
			{
				foreach (Delegate delegate19 in this.OnAttributeInstallResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate19.Target, instance))
					{
						this.OnAttributeInstallResultEvent -= (PlayFabEvents.PlayFabResultEvent<AttributeInstallResult>)delegate19;
					}
				}
			}
			if (this.OnCancelTradeRequestEvent != null)
			{
				foreach (Delegate delegate20 in this.OnCancelTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate20.Target, instance))
					{
						this.OnCancelTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest>)delegate20;
					}
				}
			}
			if (this.OnCancelTradeResultEvent != null)
			{
				foreach (Delegate delegate21 in this.OnCancelTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate21.Target, instance))
					{
						this.OnCancelTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelTradeResponse>)delegate21;
					}
				}
			}
			if (this.OnConfirmPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate22 in this.OnConfirmPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate22.Target, instance))
					{
						this.OnConfirmPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest>)delegate22;
					}
				}
			}
			if (this.OnConfirmPurchaseResultEvent != null)
			{
				foreach (Delegate delegate23 in this.OnConfirmPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate23.Target, instance))
					{
						this.OnConfirmPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult>)delegate23;
					}
				}
			}
			if (this.OnConsumeItemRequestEvent != null)
			{
				foreach (Delegate delegate24 in this.OnConsumeItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate24.Target, instance))
					{
						this.OnConsumeItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumeItemRequest>)delegate24;
					}
				}
			}
			if (this.OnConsumeItemResultEvent != null)
			{
				foreach (Delegate delegate25 in this.OnConsumeItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate25.Target, instance))
					{
						this.OnConsumeItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumeItemResult>)delegate25;
					}
				}
			}
			if (this.OnConsumePSNEntitlementsRequestEvent != null)
			{
				foreach (Delegate delegate26 in this.OnConsumePSNEntitlementsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate26.Target, instance))
					{
						this.OnConsumePSNEntitlementsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumePSNEntitlementsRequest>)delegate26;
					}
				}
			}
			if (this.OnConsumePSNEntitlementsResultEvent != null)
			{
				foreach (Delegate delegate27 in this.OnConsumePSNEntitlementsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate27.Target, instance))
					{
						this.OnConsumePSNEntitlementsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumePSNEntitlementsResult>)delegate27;
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsRequestEvent != null)
			{
				foreach (Delegate delegate28 in this.OnConsumeXboxEntitlementsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate28.Target, instance))
					{
						this.OnConsumeXboxEntitlementsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumeXboxEntitlementsRequest>)delegate28;
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsResultEvent != null)
			{
				foreach (Delegate delegate29 in this.OnConsumeXboxEntitlementsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate29.Target, instance))
					{
						this.OnConsumeXboxEntitlementsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumeXboxEntitlementsResult>)delegate29;
					}
				}
			}
			if (this.OnCreateSharedGroupRequestEvent != null)
			{
				foreach (Delegate delegate30 in this.OnCreateSharedGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate30.Target, instance))
					{
						this.OnCreateSharedGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateSharedGroupRequest>)delegate30;
					}
				}
			}
			if (this.OnCreateSharedGroupResultEvent != null)
			{
				foreach (Delegate delegate31 in this.OnCreateSharedGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate31.Target, instance))
					{
						this.OnCreateSharedGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateSharedGroupResult>)delegate31;
					}
				}
			}
			if (this.OnExecuteCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate32 in this.OnExecuteCloudScriptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate32.Target, instance))
					{
						this.OnExecuteCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest>)delegate32;
					}
				}
			}
			if (this.OnExecuteCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate33 in this.OnExecuteCloudScriptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate33.Target, instance))
					{
						this.OnExecuteCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult>)delegate33;
					}
				}
			}
			if (this.OnGetAccountInfoRequestEvent != null)
			{
				foreach (Delegate delegate34 in this.OnGetAccountInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate34.Target, instance))
					{
						this.OnGetAccountInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest>)delegate34;
					}
				}
			}
			if (this.OnGetAccountInfoResultEvent != null)
			{
				foreach (Delegate delegate35 in this.OnGetAccountInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate35.Target, instance))
					{
						this.OnGetAccountInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult>)delegate35;
					}
				}
			}
			if (this.OnGetAllUsersCharactersRequestEvent != null)
			{
				foreach (Delegate delegate36 in this.OnGetAllUsersCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate36.Target, instance))
					{
						this.OnGetAllUsersCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListUsersCharactersRequest>)delegate36;
					}
				}
			}
			if (this.OnGetAllUsersCharactersResultEvent != null)
			{
				foreach (Delegate delegate37 in this.OnGetAllUsersCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate37.Target, instance))
					{
						this.OnGetAllUsersCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListUsersCharactersResult>)delegate37;
					}
				}
			}
			if (this.OnGetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate38 in this.OnGetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate38.Target, instance))
					{
						this.OnGetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCatalogItemsRequest>)delegate38;
					}
				}
			}
			if (this.OnGetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate39 in this.OnGetCatalogItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate39.Target, instance))
					{
						this.OnGetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCatalogItemsResult>)delegate39;
					}
				}
			}
			if (this.OnGetCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate40 in this.OnGetCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate40.Target, instance))
					{
						this.OnGetCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest>)delegate40;
					}
				}
			}
			if (this.OnGetCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate41 in this.OnGetCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate41.Target, instance))
					{
						this.OnGetCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult>)delegate41;
					}
				}
			}
			if (this.OnGetCharacterInventoryRequestEvent != null)
			{
				foreach (Delegate delegate42 in this.OnGetCharacterInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate42.Target, instance))
					{
						this.OnGetCharacterInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterInventoryRequest>)delegate42;
					}
				}
			}
			if (this.OnGetCharacterInventoryResultEvent != null)
			{
				foreach (Delegate delegate43 in this.OnGetCharacterInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate43.Target, instance))
					{
						this.OnGetCharacterInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterInventoryResult>)delegate43;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate44 in this.OnGetCharacterLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate44.Target, instance))
					{
						this.OnGetCharacterLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterLeaderboardRequest>)delegate44;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate45 in this.OnGetCharacterLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate45.Target, instance))
					{
						this.OnGetCharacterLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterLeaderboardResult>)delegate45;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate46 in this.OnGetCharacterReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate46.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest>)delegate46;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate47 in this.OnGetCharacterReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate47.Target, instance))
					{
						this.OnGetCharacterReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult>)delegate47;
					}
				}
			}
			if (this.OnGetCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate48 in this.OnGetCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate48.Target, instance))
					{
						this.OnGetCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterStatisticsRequest>)delegate48;
					}
				}
			}
			if (this.OnGetCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate49 in this.OnGetCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate49.Target, instance))
					{
						this.OnGetCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterStatisticsResult>)delegate49;
					}
				}
			}
			if (this.OnGetContentDownloadUrlRequestEvent != null)
			{
				foreach (Delegate delegate50 in this.OnGetContentDownloadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate50.Target, instance))
					{
						this.OnGetContentDownloadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContentDownloadUrlRequest>)delegate50;
					}
				}
			}
			if (this.OnGetContentDownloadUrlResultEvent != null)
			{
				foreach (Delegate delegate51 in this.OnGetContentDownloadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate51.Target, instance))
					{
						this.OnGetContentDownloadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContentDownloadUrlResult>)delegate51;
					}
				}
			}
			if (this.OnGetCurrentGamesRequestEvent != null)
			{
				foreach (Delegate delegate52 in this.OnGetCurrentGamesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate52.Target, instance))
					{
						this.OnGetCurrentGamesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest>)delegate52;
					}
				}
			}
			if (this.OnGetCurrentGamesResultEvent != null)
			{
				foreach (Delegate delegate53 in this.OnGetCurrentGamesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate53.Target, instance))
					{
						this.OnGetCurrentGamesResultEvent -= (PlayFabEvents.PlayFabResultEvent<CurrentGamesResult>)delegate53;
					}
				}
			}
			if (this.OnGetFriendLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate54 in this.OnGetFriendLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate54.Target, instance))
					{
						this.OnGetFriendLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardRequest>)delegate54;
					}
				}
			}
			if (this.OnGetFriendLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate55 in this.OnGetFriendLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate55.Target, instance))
					{
						this.OnGetFriendLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult>)delegate55;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate56 in this.OnGetFriendLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate56.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest>)delegate56;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate57 in this.OnGetFriendLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate57.Target, instance))
					{
						this.OnGetFriendLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult>)delegate57;
					}
				}
			}
			if (this.OnGetFriendsListRequestEvent != null)
			{
				foreach (Delegate delegate58 in this.OnGetFriendsListRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate58.Target, instance))
					{
						this.OnGetFriendsListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendsListRequest>)delegate58;
					}
				}
			}
			if (this.OnGetFriendsListResultEvent != null)
			{
				foreach (Delegate delegate59 in this.OnGetFriendsListResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate59.Target, instance))
					{
						this.OnGetFriendsListResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFriendsListResult>)delegate59;
					}
				}
			}
			if (this.OnGetGameServerRegionsRequestEvent != null)
			{
				foreach (Delegate delegate60 in this.OnGetGameServerRegionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate60.Target, instance))
					{
						this.OnGetGameServerRegionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest>)delegate60;
					}
				}
			}
			if (this.OnGetGameServerRegionsResultEvent != null)
			{
				foreach (Delegate delegate61 in this.OnGetGameServerRegionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate61.Target, instance))
					{
						this.OnGetGameServerRegionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult>)delegate61;
					}
				}
			}
			if (this.OnGetLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate62 in this.OnGetLeaderboardRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate62.Target, instance))
					{
						this.OnGetLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardRequest>)delegate62;
					}
				}
			}
			if (this.OnGetLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate63 in this.OnGetLeaderboardResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate63.Target, instance))
					{
						this.OnGetLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult>)delegate63;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterRequestEvent != null)
			{
				foreach (Delegate delegate64 in this.OnGetLeaderboardAroundCharacterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate64.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest>)delegate64;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterResultEvent != null)
			{
				foreach (Delegate delegate65 in this.OnGetLeaderboardAroundCharacterResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate65.Target, instance))
					{
						this.OnGetLeaderboardAroundCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundCharacterResult>)delegate65;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate66 in this.OnGetLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate66.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest>)delegate66;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate67 in this.OnGetLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate67.Target, instance))
					{
						this.OnGetLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult>)delegate67;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersRequestEvent != null)
			{
				foreach (Delegate delegate68 in this.OnGetLeaderboardForUserCharactersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate68.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest>)delegate68;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersResultEvent != null)
			{
				foreach (Delegate delegate69 in this.OnGetLeaderboardForUserCharactersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate69.Target, instance))
					{
						this.OnGetLeaderboardForUserCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardForUsersCharactersResult>)delegate69;
					}
				}
			}
			if (this.OnGetPaymentTokenRequestEvent != null)
			{
				foreach (Delegate delegate70 in this.OnGetPaymentTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate70.Target, instance))
					{
						this.OnGetPaymentTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest>)delegate70;
					}
				}
			}
			if (this.OnGetPaymentTokenResultEvent != null)
			{
				foreach (Delegate delegate71 in this.OnGetPaymentTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate71.Target, instance))
					{
						this.OnGetPaymentTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult>)delegate71;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenRequestEvent != null)
			{
				foreach (Delegate delegate72 in this.OnGetPhotonAuthenticationTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate72.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest>)delegate72;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenResultEvent != null)
			{
				foreach (Delegate delegate73 in this.OnGetPhotonAuthenticationTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate73.Target, instance))
					{
						this.OnGetPhotonAuthenticationTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult>)delegate73;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoRequestEvent != null)
			{
				foreach (Delegate delegate74 in this.OnGetPlayerCombinedInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate74.Target, instance))
					{
						this.OnGetPlayerCombinedInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerCombinedInfoRequest>)delegate74;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoResultEvent != null)
			{
				foreach (Delegate delegate75 in this.OnGetPlayerCombinedInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate75.Target, instance))
					{
						this.OnGetPlayerCombinedInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerCombinedInfoResult>)delegate75;
					}
				}
			}
			if (this.OnGetPlayerProfileRequestEvent != null)
			{
				foreach (Delegate delegate76 in this.OnGetPlayerProfileRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate76.Target, instance))
					{
						this.OnGetPlayerProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerProfileRequest>)delegate76;
					}
				}
			}
			if (this.OnGetPlayerProfileResultEvent != null)
			{
				foreach (Delegate delegate77 in this.OnGetPlayerProfileResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate77.Target, instance))
					{
						this.OnGetPlayerProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerProfileResult>)delegate77;
					}
				}
			}
			if (this.OnGetPlayerSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate78 in this.OnGetPlayerSegmentsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate78.Target, instance))
					{
						this.OnGetPlayerSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest>)delegate78;
					}
				}
			}
			if (this.OnGetPlayerSegmentsResultEvent != null)
			{
				foreach (Delegate delegate79 in this.OnGetPlayerSegmentsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate79.Target, instance))
					{
						this.OnGetPlayerSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerSegmentsResult>)delegate79;
					}
				}
			}
			if (this.OnGetPlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate80 in this.OnGetPlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate80.Target, instance))
					{
						this.OnGetPlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticsRequest>)delegate80;
					}
				}
			}
			if (this.OnGetPlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate81 in this.OnGetPlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate81.Target, instance))
					{
						this.OnGetPlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticsResult>)delegate81;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsRequestEvent != null)
			{
				foreach (Delegate delegate82 in this.OnGetPlayerStatisticVersionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate82.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticVersionsRequest>)delegate82;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsResultEvent != null)
			{
				foreach (Delegate delegate83 in this.OnGetPlayerStatisticVersionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate83.Target, instance))
					{
						this.OnGetPlayerStatisticVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticVersionsResult>)delegate83;
					}
				}
			}
			if (this.OnGetPlayerTagsRequestEvent != null)
			{
				foreach (Delegate delegate84 in this.OnGetPlayerTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate84.Target, instance))
					{
						this.OnGetPlayerTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerTagsRequest>)delegate84;
					}
				}
			}
			if (this.OnGetPlayerTagsResultEvent != null)
			{
				foreach (Delegate delegate85 in this.OnGetPlayerTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate85.Target, instance))
					{
						this.OnGetPlayerTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerTagsResult>)delegate85;
					}
				}
			}
			if (this.OnGetPlayerTradesRequestEvent != null)
			{
				foreach (Delegate delegate86 in this.OnGetPlayerTradesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate86.Target, instance))
					{
						this.OnGetPlayerTradesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest>)delegate86;
					}
				}
			}
			if (this.OnGetPlayerTradesResultEvent != null)
			{
				foreach (Delegate delegate87 in this.OnGetPlayerTradesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate87.Target, instance))
					{
						this.OnGetPlayerTradesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse>)delegate87;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
			{
				foreach (Delegate delegate88 in this.OnGetPlayFabIDsFromFacebookIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate88.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest>)delegate88;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
			{
				foreach (Delegate delegate89 in this.OnGetPlayFabIDsFromFacebookIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate89.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult>)delegate89;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
			{
				foreach (Delegate delegate90 in this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate90.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest>)delegate90;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
			{
				foreach (Delegate delegate91 in this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate91.Target, instance))
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult>)delegate91;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
			{
				foreach (Delegate delegate92 in this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate92.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest>)delegate92;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
			{
				foreach (Delegate delegate93 in this.OnGetPlayFabIDsFromGameCenterIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate93.Target, instance))
					{
						this.OnGetPlayFabIDsFromGameCenterIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult>)delegate93;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
			{
				foreach (Delegate delegate94 in this.OnGetPlayFabIDsFromGenericIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate94.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest>)delegate94;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
			{
				foreach (Delegate delegate95 in this.OnGetPlayFabIDsFromGenericIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate95.Target, instance))
					{
						this.OnGetPlayFabIDsFromGenericIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult>)delegate95;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
			{
				foreach (Delegate delegate96 in this.OnGetPlayFabIDsFromGoogleIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate96.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest>)delegate96;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
			{
				foreach (Delegate delegate97 in this.OnGetPlayFabIDsFromGoogleIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate97.Target, instance))
					{
						this.OnGetPlayFabIDsFromGoogleIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult>)delegate97;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
			{
				foreach (Delegate delegate98 in this.OnGetPlayFabIDsFromKongregateIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate98.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest>)delegate98;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
			{
				foreach (Delegate delegate99 in this.OnGetPlayFabIDsFromKongregateIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate99.Target, instance))
					{
						this.OnGetPlayFabIDsFromKongregateIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult>)delegate99;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
			{
				foreach (Delegate delegate100 in this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate100.Target, instance))
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest>)delegate100;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
			{
				foreach (Delegate delegate101 in this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate101.Target, instance))
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>)delegate101;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
			{
				foreach (Delegate delegate102 in this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate102.Target, instance))
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest>)delegate102;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
			{
				foreach (Delegate delegate103 in this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate103.Target, instance))
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult>)delegate103;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
			{
				foreach (Delegate delegate104 in this.OnGetPlayFabIDsFromSteamIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate104.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest>)delegate104;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
			{
				foreach (Delegate delegate105 in this.OnGetPlayFabIDsFromSteamIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate105.Target, instance))
					{
						this.OnGetPlayFabIDsFromSteamIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult>)delegate105;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
			{
				foreach (Delegate delegate106 in this.OnGetPlayFabIDsFromTwitchIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate106.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest>)delegate106;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
			{
				foreach (Delegate delegate107 in this.OnGetPlayFabIDsFromTwitchIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate107.Target, instance))
					{
						this.OnGetPlayFabIDsFromTwitchIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult>)delegate107;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
			{
				foreach (Delegate delegate108 in this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate108.Target, instance))
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest>)delegate108;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
			{
				foreach (Delegate delegate109 in this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate109.Target, instance))
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult>)delegate109;
					}
				}
			}
			if (this.OnGetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate110 in this.OnGetPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate110.Target, instance))
					{
						this.OnGetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPublisherDataRequest>)delegate110;
					}
				}
			}
			if (this.OnGetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate111 in this.OnGetPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate111.Target, instance))
					{
						this.OnGetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPublisherDataResult>)delegate111;
					}
				}
			}
			if (this.OnGetPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate112 in this.OnGetPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate112.Target, instance))
					{
						this.OnGetPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest>)delegate112;
					}
				}
			}
			if (this.OnGetPurchaseResultEvent != null)
			{
				foreach (Delegate delegate113 in this.OnGetPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate113.Target, instance))
					{
						this.OnGetPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPurchaseResult>)delegate113;
					}
				}
			}
			if (this.OnGetSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate114 in this.OnGetSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate114.Target, instance))
					{
						this.OnGetSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetSharedGroupDataRequest>)delegate114;
					}
				}
			}
			if (this.OnGetSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate115 in this.OnGetSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate115.Target, instance))
					{
						this.OnGetSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetSharedGroupDataResult>)delegate115;
					}
				}
			}
			if (this.OnGetStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate116 in this.OnGetStoreItemsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate116.Target, instance))
					{
						this.OnGetStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetStoreItemsRequest>)delegate116;
					}
				}
			}
			if (this.OnGetStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate117 in this.OnGetStoreItemsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate117.Target, instance))
					{
						this.OnGetStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetStoreItemsResult>)delegate117;
					}
				}
			}
			if (this.OnGetTimeRequestEvent != null)
			{
				foreach (Delegate delegate118 in this.OnGetTimeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate118.Target, instance))
					{
						this.OnGetTimeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTimeRequest>)delegate118;
					}
				}
			}
			if (this.OnGetTimeResultEvent != null)
			{
				foreach (Delegate delegate119 in this.OnGetTimeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate119.Target, instance))
					{
						this.OnGetTimeResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTimeResult>)delegate119;
					}
				}
			}
			if (this.OnGetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate120 in this.OnGetTitleDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate120.Target, instance))
					{
						this.OnGetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleDataRequest>)delegate120;
					}
				}
			}
			if (this.OnGetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate121 in this.OnGetTitleDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate121.Target, instance))
					{
						this.OnGetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleDataResult>)delegate121;
					}
				}
			}
			if (this.OnGetTitleNewsRequestEvent != null)
			{
				foreach (Delegate delegate122 in this.OnGetTitleNewsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate122.Target, instance))
					{
						this.OnGetTitleNewsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleNewsRequest>)delegate122;
					}
				}
			}
			if (this.OnGetTitleNewsResultEvent != null)
			{
				foreach (Delegate delegate123 in this.OnGetTitleNewsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate123.Target, instance))
					{
						this.OnGetTitleNewsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleNewsResult>)delegate123;
					}
				}
			}
			if (this.OnGetTitlePublicKeyRequestEvent != null)
			{
				foreach (Delegate delegate124 in this.OnGetTitlePublicKeyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate124.Target, instance))
					{
						this.OnGetTitlePublicKeyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest>)delegate124;
					}
				}
			}
			if (this.OnGetTitlePublicKeyResultEvent != null)
			{
				foreach (Delegate delegate125 in this.OnGetTitlePublicKeyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate125.Target, instance))
					{
						this.OnGetTitlePublicKeyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult>)delegate125;
					}
				}
			}
			if (this.OnGetTradeStatusRequestEvent != null)
			{
				foreach (Delegate delegate126 in this.OnGetTradeStatusRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate126.Target, instance))
					{
						this.OnGetTradeStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest>)delegate126;
					}
				}
			}
			if (this.OnGetTradeStatusResultEvent != null)
			{
				foreach (Delegate delegate127 in this.OnGetTradeStatusResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate127.Target, instance))
					{
						this.OnGetTradeStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse>)delegate127;
					}
				}
			}
			if (this.OnGetUserDataRequestEvent != null)
			{
				foreach (Delegate delegate128 in this.OnGetUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate128.Target, instance))
					{
						this.OnGetUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate128;
					}
				}
			}
			if (this.OnGetUserDataResultEvent != null)
			{
				foreach (Delegate delegate129 in this.OnGetUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate129.Target, instance))
					{
						this.OnGetUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate129;
					}
				}
			}
			if (this.OnGetUserInventoryRequestEvent != null)
			{
				foreach (Delegate delegate130 in this.OnGetUserInventoryRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate130.Target, instance))
					{
						this.OnGetUserInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserInventoryRequest>)delegate130;
					}
				}
			}
			if (this.OnGetUserInventoryResultEvent != null)
			{
				foreach (Delegate delegate131 in this.OnGetUserInventoryResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate131.Target, instance))
					{
						this.OnGetUserInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserInventoryResult>)delegate131;
					}
				}
			}
			if (this.OnGetUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate132 in this.OnGetUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate132.Target, instance))
					{
						this.OnGetUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate132;
					}
				}
			}
			if (this.OnGetUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate133 in this.OnGetUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate133.Target, instance))
					{
						this.OnGetUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate133;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate134 in this.OnGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate134.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate134;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate135 in this.OnGetUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate135.Target, instance))
					{
						this.OnGetUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate135;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate136 in this.OnGetUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate136.Target, instance))
					{
						this.OnGetUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate136;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate137 in this.OnGetUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate137.Target, instance))
					{
						this.OnGetUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate137;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeRequestEvent != null)
			{
				foreach (Delegate delegate138 in this.OnGetWindowsHelloChallengeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate138.Target, instance))
					{
						this.OnGetWindowsHelloChallengeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest>)delegate138;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeResultEvent != null)
			{
				foreach (Delegate delegate139 in this.OnGetWindowsHelloChallengeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate139.Target, instance))
					{
						this.OnGetWindowsHelloChallengeResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse>)delegate139;
					}
				}
			}
			if (this.OnGrantCharacterToUserRequestEvent != null)
			{
				foreach (Delegate delegate140 in this.OnGrantCharacterToUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate140.Target, instance))
					{
						this.OnGrantCharacterToUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GrantCharacterToUserRequest>)delegate140;
					}
				}
			}
			if (this.OnGrantCharacterToUserResultEvent != null)
			{
				foreach (Delegate delegate141 in this.OnGrantCharacterToUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate141.Target, instance))
					{
						this.OnGrantCharacterToUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<GrantCharacterToUserResult>)delegate141;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate142 in this.OnLinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate142.Target, instance))
					{
						this.OnLinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest>)delegate142;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate143 in this.OnLinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate143.Target, instance))
					{
						this.OnLinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult>)delegate143;
					}
				}
			}
			if (this.OnLinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate144 in this.OnLinkCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate144.Target, instance))
					{
						this.OnLinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest>)delegate144;
					}
				}
			}
			if (this.OnLinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate145 in this.OnLinkCustomIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate145.Target, instance))
					{
						this.OnLinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult>)delegate145;
					}
				}
			}
			if (this.OnLinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate146 in this.OnLinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate146.Target, instance))
					{
						this.OnLinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest>)delegate146;
					}
				}
			}
			if (this.OnLinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate147 in this.OnLinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate147.Target, instance))
					{
						this.OnLinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult>)delegate147;
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate148 in this.OnLinkFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate148.Target, instance))
					{
						this.OnLinkFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest>)delegate148;
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdResultEvent != null)
			{
				foreach (Delegate delegate149 in this.OnLinkFacebookInstantGamesIdResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate149.Target, instance))
					{
						this.OnLinkFacebookInstantGamesIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkFacebookInstantGamesIdResult>)delegate149;
					}
				}
			}
			if (this.OnLinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate150 in this.OnLinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate150.Target, instance))
					{
						this.OnLinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest>)delegate150;
					}
				}
			}
			if (this.OnLinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate151 in this.OnLinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate151.Target, instance))
					{
						this.OnLinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult>)delegate151;
					}
				}
			}
			if (this.OnLinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate152 in this.OnLinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate152.Target, instance))
					{
						this.OnLinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest>)delegate152;
					}
				}
			}
			if (this.OnLinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate153 in this.OnLinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate153.Target, instance))
					{
						this.OnLinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult>)delegate153;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate154 in this.OnLinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate154.Target, instance))
					{
						this.OnLinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest>)delegate154;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate155 in this.OnLinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate155.Target, instance))
					{
						this.OnLinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult>)delegate155;
					}
				}
			}
			if (this.OnLinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate156 in this.OnLinkKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate156.Target, instance))
					{
						this.OnLinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest>)delegate156;
					}
				}
			}
			if (this.OnLinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate157 in this.OnLinkKongregateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate157.Target, instance))
					{
						this.OnLinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult>)delegate157;
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate158 in this.OnLinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate158.Target, instance))
					{
						this.OnLinkNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest>)delegate158;
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdResultEvent != null)
			{
				foreach (Delegate delegate159 in this.OnLinkNintendoSwitchDeviceIdResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate159.Target, instance))
					{
						this.OnLinkNintendoSwitchDeviceIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult>)delegate159;
					}
				}
			}
			if (this.OnLinkOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate160 in this.OnLinkOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate160.Target, instance))
					{
						this.OnLinkOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkOpenIdConnectRequest>)delegate160;
					}
				}
			}
			if (this.OnLinkOpenIdConnectResultEvent != null)
			{
				foreach (Delegate delegate161 in this.OnLinkOpenIdConnectResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate161.Target, instance))
					{
						this.OnLinkOpenIdConnectResultEvent -= (PlayFabEvents.PlayFabResultEvent<EmptyResult>)delegate161;
					}
				}
			}
			if (this.OnLinkPSNAccountRequestEvent != null)
			{
				foreach (Delegate delegate162 in this.OnLinkPSNAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate162.Target, instance))
					{
						this.OnLinkPSNAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkPSNAccountRequest>)delegate162;
					}
				}
			}
			if (this.OnLinkPSNAccountResultEvent != null)
			{
				foreach (Delegate delegate163 in this.OnLinkPSNAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate163.Target, instance))
					{
						this.OnLinkPSNAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkPSNAccountResult>)delegate163;
					}
				}
			}
			if (this.OnLinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate164 in this.OnLinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate164.Target, instance))
					{
						this.OnLinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest>)delegate164;
					}
				}
			}
			if (this.OnLinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate165 in this.OnLinkSteamAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate165.Target, instance))
					{
						this.OnLinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult>)delegate165;
					}
				}
			}
			if (this.OnLinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate166 in this.OnLinkTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate166.Target, instance))
					{
						this.OnLinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest>)delegate166;
					}
				}
			}
			if (this.OnLinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate167 in this.OnLinkTwitchResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate167.Target, instance))
					{
						this.OnLinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult>)delegate167;
					}
				}
			}
			if (this.OnLinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate168 in this.OnLinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate168.Target, instance))
					{
						this.OnLinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest>)delegate168;
					}
				}
			}
			if (this.OnLinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate169 in this.OnLinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate169.Target, instance))
					{
						this.OnLinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse>)delegate169;
					}
				}
			}
			if (this.OnLinkXboxAccountRequestEvent != null)
			{
				foreach (Delegate delegate170 in this.OnLinkXboxAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate170.Target, instance))
					{
						this.OnLinkXboxAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkXboxAccountRequest>)delegate170;
					}
				}
			}
			if (this.OnLinkXboxAccountResultEvent != null)
			{
				foreach (Delegate delegate171 in this.OnLinkXboxAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate171.Target, instance))
					{
						this.OnLinkXboxAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkXboxAccountResult>)delegate171;
					}
				}
			}
			if (this.OnLoginWithAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate172 in this.OnLoginWithAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate172.Target, instance))
					{
						this.OnLoginWithAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest>)delegate172;
					}
				}
			}
			if (this.OnLoginWithCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate173 in this.OnLoginWithCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate173.Target, instance))
					{
						this.OnLoginWithCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest>)delegate173;
					}
				}
			}
			if (this.OnLoginWithEmailAddressRequestEvent != null)
			{
				foreach (Delegate delegate174 in this.OnLoginWithEmailAddressRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate174.Target, instance))
					{
						this.OnLoginWithEmailAddressRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest>)delegate174;
					}
				}
			}
			if (this.OnLoginWithFacebookRequestEvent != null)
			{
				foreach (Delegate delegate175 in this.OnLoginWithFacebookRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate175.Target, instance))
					{
						this.OnLoginWithFacebookRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest>)delegate175;
					}
				}
			}
			if (this.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate176 in this.OnLoginWithFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate176.Target, instance))
					{
						this.OnLoginWithFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest>)delegate176;
					}
				}
			}
			if (this.OnLoginWithGameCenterRequestEvent != null)
			{
				foreach (Delegate delegate177 in this.OnLoginWithGameCenterRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate177.Target, instance))
					{
						this.OnLoginWithGameCenterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest>)delegate177;
					}
				}
			}
			if (this.OnLoginWithGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate178 in this.OnLoginWithGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate178.Target, instance))
					{
						this.OnLoginWithGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest>)delegate178;
					}
				}
			}
			if (this.OnLoginWithIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate179 in this.OnLoginWithIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate179.Target, instance))
					{
						this.OnLoginWithIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest>)delegate179;
					}
				}
			}
			if (this.OnLoginWithKongregateRequestEvent != null)
			{
				foreach (Delegate delegate180 in this.OnLoginWithKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate180.Target, instance))
					{
						this.OnLoginWithKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest>)delegate180;
					}
				}
			}
			if (this.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate181 in this.OnLoginWithNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate181.Target, instance))
					{
						this.OnLoginWithNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest>)delegate181;
					}
				}
			}
			if (this.OnLoginWithOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate182 in this.OnLoginWithOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate182.Target, instance))
					{
						this.OnLoginWithOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithOpenIdConnectRequest>)delegate182;
					}
				}
			}
			if (this.OnLoginWithPlayFabRequestEvent != null)
			{
				foreach (Delegate delegate183 in this.OnLoginWithPlayFabRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate183.Target, instance))
					{
						this.OnLoginWithPlayFabRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest>)delegate183;
					}
				}
			}
			if (this.OnLoginWithPSNRequestEvent != null)
			{
				foreach (Delegate delegate184 in this.OnLoginWithPSNRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate184.Target, instance))
					{
						this.OnLoginWithPSNRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithPSNRequest>)delegate184;
					}
				}
			}
			if (this.OnLoginWithSteamRequestEvent != null)
			{
				foreach (Delegate delegate185 in this.OnLoginWithSteamRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate185.Target, instance))
					{
						this.OnLoginWithSteamRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest>)delegate185;
					}
				}
			}
			if (this.OnLoginWithTwitchRequestEvent != null)
			{
				foreach (Delegate delegate186 in this.OnLoginWithTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate186.Target, instance))
					{
						this.OnLoginWithTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest>)delegate186;
					}
				}
			}
			if (this.OnLoginWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate187 in this.OnLoginWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate187.Target, instance))
					{
						this.OnLoginWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest>)delegate187;
					}
				}
			}
			if (this.OnLoginWithXboxRequestEvent != null)
			{
				foreach (Delegate delegate188 in this.OnLoginWithXboxRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate188.Target, instance))
					{
						this.OnLoginWithXboxRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithXboxRequest>)delegate188;
					}
				}
			}
			if (this.OnMatchmakeRequestEvent != null)
			{
				foreach (Delegate delegate189 in this.OnMatchmakeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate189.Target, instance))
					{
						this.OnMatchmakeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest>)delegate189;
					}
				}
			}
			if (this.OnMatchmakeResultEvent != null)
			{
				foreach (Delegate delegate190 in this.OnMatchmakeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate190.Target, instance))
					{
						this.OnMatchmakeResultEvent -= (PlayFabEvents.PlayFabResultEvent<MatchmakeResult>)delegate190;
					}
				}
			}
			if (this.OnOpenTradeRequestEvent != null)
			{
				foreach (Delegate delegate191 in this.OnOpenTradeRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate191.Target, instance))
					{
						this.OnOpenTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest>)delegate191;
					}
				}
			}
			if (this.OnOpenTradeResultEvent != null)
			{
				foreach (Delegate delegate192 in this.OnOpenTradeResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate192.Target, instance))
					{
						this.OnOpenTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<OpenTradeResponse>)delegate192;
					}
				}
			}
			if (this.OnPayForPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate193 in this.OnPayForPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate193.Target, instance))
					{
						this.OnPayForPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest>)delegate193;
					}
				}
			}
			if (this.OnPayForPurchaseResultEvent != null)
			{
				foreach (Delegate delegate194 in this.OnPayForPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate194.Target, instance))
					{
						this.OnPayForPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult>)delegate194;
					}
				}
			}
			if (this.OnPurchaseItemRequestEvent != null)
			{
				foreach (Delegate delegate195 in this.OnPurchaseItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate195.Target, instance))
					{
						this.OnPurchaseItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest>)delegate195;
					}
				}
			}
			if (this.OnPurchaseItemResultEvent != null)
			{
				foreach (Delegate delegate196 in this.OnPurchaseItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate196.Target, instance))
					{
						this.OnPurchaseItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PurchaseItemResult>)delegate196;
					}
				}
			}
			if (this.OnRedeemCouponRequestEvent != null)
			{
				foreach (Delegate delegate197 in this.OnRedeemCouponRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate197.Target, instance))
					{
						this.OnRedeemCouponRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RedeemCouponRequest>)delegate197;
					}
				}
			}
			if (this.OnRedeemCouponResultEvent != null)
			{
				foreach (Delegate delegate198 in this.OnRedeemCouponResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate198.Target, instance))
					{
						this.OnRedeemCouponResultEvent -= (PlayFabEvents.PlayFabResultEvent<RedeemCouponResult>)delegate198;
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenRequestEvent != null)
			{
				foreach (Delegate delegate199 in this.OnRefreshPSNAuthTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate199.Target, instance))
					{
						this.OnRefreshPSNAuthTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RefreshPSNAuthTokenRequest>)delegate199;
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenResultEvent != null)
			{
				foreach (Delegate delegate200 in this.OnRefreshPSNAuthTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate200.Target, instance))
					{
						this.OnRefreshPSNAuthTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate200;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationRequestEvent != null)
			{
				foreach (Delegate delegate201 in this.OnRegisterForIOSPushNotificationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate201.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest>)delegate201;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationResultEvent != null)
			{
				foreach (Delegate delegate202 in this.OnRegisterForIOSPushNotificationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate202.Target, instance))
					{
						this.OnRegisterForIOSPushNotificationResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult>)delegate202;
					}
				}
			}
			if (this.OnRegisterPlayFabUserRequestEvent != null)
			{
				foreach (Delegate delegate203 in this.OnRegisterPlayFabUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate203.Target, instance))
					{
						this.OnRegisterPlayFabUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest>)delegate203;
					}
				}
			}
			if (this.OnRegisterPlayFabUserResultEvent != null)
			{
				foreach (Delegate delegate204 in this.OnRegisterPlayFabUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate204.Target, instance))
					{
						this.OnRegisterPlayFabUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult>)delegate204;
					}
				}
			}
			if (this.OnRegisterWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate205 in this.OnRegisterWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate205.Target, instance))
					{
						this.OnRegisterWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest>)delegate205;
					}
				}
			}
			if (this.OnRemoveContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate206 in this.OnRemoveContactEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate206.Target, instance))
					{
						this.OnRemoveContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest>)delegate206;
					}
				}
			}
			if (this.OnRemoveContactEmailResultEvent != null)
			{
				foreach (Delegate delegate207 in this.OnRemoveContactEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate207.Target, instance))
					{
						this.OnRemoveContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult>)delegate207;
					}
				}
			}
			if (this.OnRemoveFriendRequestEvent != null)
			{
				foreach (Delegate delegate208 in this.OnRemoveFriendRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate208.Target, instance))
					{
						this.OnRemoveFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveFriendRequest>)delegate208;
					}
				}
			}
			if (this.OnRemoveFriendResultEvent != null)
			{
				foreach (Delegate delegate209 in this.OnRemoveFriendResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate209.Target, instance))
					{
						this.OnRemoveFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveFriendResult>)delegate209;
					}
				}
			}
			if (this.OnRemoveGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate210 in this.OnRemoveGenericIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate210.Target, instance))
					{
						this.OnRemoveGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest>)delegate210;
					}
				}
			}
			if (this.OnRemoveGenericIDResultEvent != null)
			{
				foreach (Delegate delegate211 in this.OnRemoveGenericIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate211.Target, instance))
					{
						this.OnRemoveGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult>)delegate211;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate212 in this.OnRemoveSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate212.Target, instance))
					{
						this.OnRemoveSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveSharedGroupMembersRequest>)delegate212;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate213 in this.OnRemoveSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate213.Target, instance))
					{
						this.OnRemoveSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveSharedGroupMembersResult>)delegate213;
					}
				}
			}
			if (this.OnReportDeviceInfoRequestEvent != null)
			{
				foreach (Delegate delegate214 in this.OnReportDeviceInfoRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate214.Target, instance))
					{
						this.OnReportDeviceInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest>)delegate214;
					}
				}
			}
			if (this.OnReportDeviceInfoResultEvent != null)
			{
				foreach (Delegate delegate215 in this.OnReportDeviceInfoResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate215.Target, instance))
					{
						this.OnReportDeviceInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate215;
					}
				}
			}
			if (this.OnReportPlayerRequestEvent != null)
			{
				foreach (Delegate delegate216 in this.OnReportPlayerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate216.Target, instance))
					{
						this.OnReportPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest>)delegate216;
					}
				}
			}
			if (this.OnReportPlayerResultEvent != null)
			{
				foreach (Delegate delegate217 in this.OnReportPlayerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate217.Target, instance))
					{
						this.OnReportPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult>)delegate217;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesRequestEvent != null)
			{
				foreach (Delegate delegate218 in this.OnRestoreIOSPurchasesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate218.Target, instance))
					{
						this.OnRestoreIOSPurchasesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest>)delegate218;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesResultEvent != null)
			{
				foreach (Delegate delegate219 in this.OnRestoreIOSPurchasesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate219.Target, instance))
					{
						this.OnRestoreIOSPurchasesResultEvent -= (PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult>)delegate219;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailRequestEvent != null)
			{
				foreach (Delegate delegate220 in this.OnSendAccountRecoveryEmailRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate220.Target, instance))
					{
						this.OnSendAccountRecoveryEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SendAccountRecoveryEmailRequest>)delegate220;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailResultEvent != null)
			{
				foreach (Delegate delegate221 in this.OnSendAccountRecoveryEmailResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate221.Target, instance))
					{
						this.OnSendAccountRecoveryEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<SendAccountRecoveryEmailResult>)delegate221;
					}
				}
			}
			if (this.OnSetFriendTagsRequestEvent != null)
			{
				foreach (Delegate delegate222 in this.OnSetFriendTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate222.Target, instance))
					{
						this.OnSetFriendTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetFriendTagsRequest>)delegate222;
					}
				}
			}
			if (this.OnSetFriendTagsResultEvent != null)
			{
				foreach (Delegate delegate223 in this.OnSetFriendTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate223.Target, instance))
					{
						this.OnSetFriendTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult>)delegate223;
					}
				}
			}
			if (this.OnSetPlayerSecretRequestEvent != null)
			{
				foreach (Delegate delegate224 in this.OnSetPlayerSecretRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate224.Target, instance))
					{
						this.OnSetPlayerSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetPlayerSecretRequest>)delegate224;
					}
				}
			}
			if (this.OnSetPlayerSecretResultEvent != null)
			{
				foreach (Delegate delegate225 in this.OnSetPlayerSecretResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate225.Target, instance))
					{
						this.OnSetPlayerSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetPlayerSecretResult>)delegate225;
					}
				}
			}
			if (this.OnStartGameRequestEvent != null)
			{
				foreach (Delegate delegate226 in this.OnStartGameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate226.Target, instance))
					{
						this.OnStartGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartGameRequest>)delegate226;
					}
				}
			}
			if (this.OnStartGameResultEvent != null)
			{
				foreach (Delegate delegate227 in this.OnStartGameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate227.Target, instance))
					{
						this.OnStartGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartGameResult>)delegate227;
					}
				}
			}
			if (this.OnStartPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate228 in this.OnStartPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate228.Target, instance))
					{
						this.OnStartPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest>)delegate228;
					}
				}
			}
			if (this.OnStartPurchaseResultEvent != null)
			{
				foreach (Delegate delegate229 in this.OnStartPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate229.Target, instance))
					{
						this.OnStartPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartPurchaseResult>)delegate229;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate230 in this.OnSubtractUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate230.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest>)delegate230;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate231 in this.OnSubtractUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate231.Target, instance))
					{
						this.OnSubtractUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)delegate231;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate232 in this.OnUnlinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate232.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest>)delegate232;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate233 in this.OnUnlinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate233.Target, instance))
					{
						this.OnUnlinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult>)delegate233;
					}
				}
			}
			if (this.OnUnlinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate234 in this.OnUnlinkCustomIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate234.Target, instance))
					{
						this.OnUnlinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest>)delegate234;
					}
				}
			}
			if (this.OnUnlinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate235 in this.OnUnlinkCustomIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate235.Target, instance))
					{
						this.OnUnlinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult>)delegate235;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate236 in this.OnUnlinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate236.Target, instance))
					{
						this.OnUnlinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest>)delegate236;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate237 in this.OnUnlinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate237.Target, instance))
					{
						this.OnUnlinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult>)delegate237;
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate238 in this.OnUnlinkFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate238.Target, instance))
					{
						this.OnUnlinkFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest>)delegate238;
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdResultEvent != null)
			{
				foreach (Delegate delegate239 in this.OnUnlinkFacebookInstantGamesIdResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate239.Target, instance))
					{
						this.OnUnlinkFacebookInstantGamesIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult>)delegate239;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate240 in this.OnUnlinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate240.Target, instance))
					{
						this.OnUnlinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest>)delegate240;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate241 in this.OnUnlinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate241.Target, instance))
					{
						this.OnUnlinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult>)delegate241;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate242 in this.OnUnlinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate242.Target, instance))
					{
						this.OnUnlinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest>)delegate242;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate243 in this.OnUnlinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate243.Target, instance))
					{
						this.OnUnlinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult>)delegate243;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate244 in this.OnUnlinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate244.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest>)delegate244;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate245 in this.OnUnlinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate245.Target, instance))
					{
						this.OnUnlinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult>)delegate245;
					}
				}
			}
			if (this.OnUnlinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate246 in this.OnUnlinkKongregateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate246.Target, instance))
					{
						this.OnUnlinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest>)delegate246;
					}
				}
			}
			if (this.OnUnlinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate247 in this.OnUnlinkKongregateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate247.Target, instance))
					{
						this.OnUnlinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult>)delegate247;
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate248 in this.OnUnlinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate248.Target, instance))
					{
						this.OnUnlinkNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest>)delegate248;
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
			{
				foreach (Delegate delegate249 in this.OnUnlinkNintendoSwitchDeviceIdResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate249.Target, instance))
					{
						this.OnUnlinkNintendoSwitchDeviceIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult>)delegate249;
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate250 in this.OnUnlinkOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate250.Target, instance))
					{
						this.OnUnlinkOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UninkOpenIdConnectRequest>)delegate250;
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectResultEvent != null)
			{
				foreach (Delegate delegate251 in this.OnUnlinkOpenIdConnectResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate251.Target, instance))
					{
						this.OnUnlinkOpenIdConnectResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate251;
					}
				}
			}
			if (this.OnUnlinkPSNAccountRequestEvent != null)
			{
				foreach (Delegate delegate252 in this.OnUnlinkPSNAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate252.Target, instance))
					{
						this.OnUnlinkPSNAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkPSNAccountRequest>)delegate252;
					}
				}
			}
			if (this.OnUnlinkPSNAccountResultEvent != null)
			{
				foreach (Delegate delegate253 in this.OnUnlinkPSNAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate253.Target, instance))
					{
						this.OnUnlinkPSNAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkPSNAccountResult>)delegate253;
					}
				}
			}
			if (this.OnUnlinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate254 in this.OnUnlinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate254.Target, instance))
					{
						this.OnUnlinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest>)delegate254;
					}
				}
			}
			if (this.OnUnlinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate255 in this.OnUnlinkSteamAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate255.Target, instance))
					{
						this.OnUnlinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult>)delegate255;
					}
				}
			}
			if (this.OnUnlinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate256 in this.OnUnlinkTwitchRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate256.Target, instance))
					{
						this.OnUnlinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest>)delegate256;
					}
				}
			}
			if (this.OnUnlinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate257 in this.OnUnlinkTwitchResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate257.Target, instance))
					{
						this.OnUnlinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult>)delegate257;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate258 in this.OnUnlinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate258.Target, instance))
					{
						this.OnUnlinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest>)delegate258;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate259 in this.OnUnlinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate259.Target, instance))
					{
						this.OnUnlinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse>)delegate259;
					}
				}
			}
			if (this.OnUnlinkXboxAccountRequestEvent != null)
			{
				foreach (Delegate delegate260 in this.OnUnlinkXboxAccountRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate260.Target, instance))
					{
						this.OnUnlinkXboxAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkXboxAccountRequest>)delegate260;
					}
				}
			}
			if (this.OnUnlinkXboxAccountResultEvent != null)
			{
				foreach (Delegate delegate261 in this.OnUnlinkXboxAccountResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate261.Target, instance))
					{
						this.OnUnlinkXboxAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkXboxAccountResult>)delegate261;
					}
				}
			}
			if (this.OnUnlockContainerInstanceRequestEvent != null)
			{
				foreach (Delegate delegate262 in this.OnUnlockContainerInstanceRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate262.Target, instance))
					{
						this.OnUnlockContainerInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlockContainerInstanceRequest>)delegate262;
					}
				}
			}
			if (this.OnUnlockContainerInstanceResultEvent != null)
			{
				foreach (Delegate delegate263 in this.OnUnlockContainerInstanceResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate263.Target, instance))
					{
						this.OnUnlockContainerInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult>)delegate263;
					}
				}
			}
			if (this.OnUnlockContainerItemRequestEvent != null)
			{
				foreach (Delegate delegate264 in this.OnUnlockContainerItemRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate264.Target, instance))
					{
						this.OnUnlockContainerItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlockContainerItemRequest>)delegate264;
					}
				}
			}
			if (this.OnUnlockContainerItemResultEvent != null)
			{
				foreach (Delegate delegate265 in this.OnUnlockContainerItemResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate265.Target, instance))
					{
						this.OnUnlockContainerItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult>)delegate265;
					}
				}
			}
			if (this.OnUpdateAvatarUrlRequestEvent != null)
			{
				foreach (Delegate delegate266 in this.OnUpdateAvatarUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate266.Target, instance))
					{
						this.OnUpdateAvatarUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateAvatarUrlRequest>)delegate266;
					}
				}
			}
			if (this.OnUpdateAvatarUrlResultEvent != null)
			{
				foreach (Delegate delegate267 in this.OnUpdateAvatarUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate267.Target, instance))
					{
						this.OnUpdateAvatarUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate267;
					}
				}
			}
			if (this.OnUpdateCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate268 in this.OnUpdateCharacterDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate268.Target, instance))
					{
						this.OnUpdateCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCharacterDataRequest>)delegate268;
					}
				}
			}
			if (this.OnUpdateCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate269 in this.OnUpdateCharacterDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate269.Target, instance))
					{
						this.OnUpdateCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCharacterDataResult>)delegate269;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate270 in this.OnUpdateCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate270.Target, instance))
					{
						this.OnUpdateCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCharacterStatisticsRequest>)delegate270;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate271 in this.OnUpdateCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate271.Target, instance))
					{
						this.OnUpdateCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCharacterStatisticsResult>)delegate271;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate272 in this.OnUpdatePlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate272.Target, instance))
					{
						this.OnUpdatePlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticsRequest>)delegate272;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate273 in this.OnUpdatePlayerStatisticsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate273.Target, instance))
					{
						this.OnUpdatePlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticsResult>)delegate273;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate274 in this.OnUpdateSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate274.Target, instance))
					{
						this.OnUpdateSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateSharedGroupDataRequest>)delegate274;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate275 in this.OnUpdateSharedGroupDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate275.Target, instance))
					{
						this.OnUpdateSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateSharedGroupDataResult>)delegate275;
					}
				}
			}
			if (this.OnUpdateUserDataRequestEvent != null)
			{
				foreach (Delegate delegate276 in this.OnUpdateUserDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate276.Target, instance))
					{
						this.OnUpdateUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest>)delegate276;
					}
				}
			}
			if (this.OnUpdateUserDataResultEvent != null)
			{
				foreach (Delegate delegate277 in this.OnUpdateUserDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate277.Target, instance))
					{
						this.OnUpdateUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult>)delegate277;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate278 in this.OnUpdateUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate278.Target, instance))
					{
						this.OnUpdateUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest>)delegate278;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate279 in this.OnUpdateUserPublisherDataResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate279.Target, instance))
					{
						this.OnUpdateUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult>)delegate279;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameRequestEvent != null)
			{
				foreach (Delegate delegate280 in this.OnUpdateUserTitleDisplayNameRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate280.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest>)delegate280;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameResultEvent != null)
			{
				foreach (Delegate delegate281 in this.OnUpdateUserTitleDisplayNameResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate281.Target, instance))
					{
						this.OnUpdateUserTitleDisplayNameResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserTitleDisplayNameResult>)delegate281;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptRequestEvent != null)
			{
				foreach (Delegate delegate282 in this.OnValidateAmazonIAPReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate282.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest>)delegate282;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptResultEvent != null)
			{
				foreach (Delegate delegate283 in this.OnValidateAmazonIAPReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate283.Target, instance))
					{
						this.OnValidateAmazonIAPReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult>)delegate283;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate284 in this.OnValidateGooglePlayPurchaseRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate284.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest>)delegate284;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseResultEvent != null)
			{
				foreach (Delegate delegate285 in this.OnValidateGooglePlayPurchaseResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate285.Target, instance))
					{
						this.OnValidateGooglePlayPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult>)delegate285;
					}
				}
			}
			if (this.OnValidateIOSReceiptRequestEvent != null)
			{
				foreach (Delegate delegate286 in this.OnValidateIOSReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate286.Target, instance))
					{
						this.OnValidateIOSReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest>)delegate286;
					}
				}
			}
			if (this.OnValidateIOSReceiptResultEvent != null)
			{
				foreach (Delegate delegate287 in this.OnValidateIOSReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate287.Target, instance))
					{
						this.OnValidateIOSReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult>)delegate287;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptRequestEvent != null)
			{
				foreach (Delegate delegate288 in this.OnValidateWindowsStoreReceiptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate288.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest>)delegate288;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptResultEvent != null)
			{
				foreach (Delegate delegate289 in this.OnValidateWindowsStoreReceiptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate289.Target, instance))
					{
						this.OnValidateWindowsStoreReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult>)delegate289;
					}
				}
			}
			if (this.OnWriteCharacterEventRequestEvent != null)
			{
				foreach (Delegate delegate290 in this.OnWriteCharacterEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate290.Target, instance))
					{
						this.OnWriteCharacterEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest>)delegate290;
					}
				}
			}
			if (this.OnWriteCharacterEventResultEvent != null)
			{
				foreach (Delegate delegate291 in this.OnWriteCharacterEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate291.Target, instance))
					{
						this.OnWriteCharacterEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate291;
					}
				}
			}
			if (this.OnWritePlayerEventRequestEvent != null)
			{
				foreach (Delegate delegate292 in this.OnWritePlayerEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate292.Target, instance))
					{
						this.OnWritePlayerEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest>)delegate292;
					}
				}
			}
			if (this.OnWritePlayerEventResultEvent != null)
			{
				foreach (Delegate delegate293 in this.OnWritePlayerEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate293.Target, instance))
					{
						this.OnWritePlayerEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate293;
					}
				}
			}
			if (this.OnWriteTitleEventRequestEvent != null)
			{
				foreach (Delegate delegate294 in this.OnWriteTitleEventRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate294.Target, instance))
					{
						this.OnWriteTitleEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteTitleEventRequest>)delegate294;
					}
				}
			}
			if (this.OnWriteTitleEventResultEvent != null)
			{
				foreach (Delegate delegate295 in this.OnWriteTitleEventResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate295.Target, instance))
					{
						this.OnWriteTitleEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate295;
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenRequestEvent != null)
			{
				foreach (Delegate delegate296 in this.OnAuthenticationGetEntityTokenRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate296.Target, instance))
					{
						this.OnAuthenticationGetEntityTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityTokenRequest>)delegate296;
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenResultEvent != null)
			{
				foreach (Delegate delegate297 in this.OnAuthenticationGetEntityTokenResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate297.Target, instance))
					{
						this.OnAuthenticationGetEntityTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityTokenResponse>)delegate297;
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate298 in this.OnCloudScriptExecuteEntityCloudScriptRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate298.Target, instance))
					{
						this.OnCloudScriptExecuteEntityCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteEntityCloudScriptRequest>)delegate298;
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate299 in this.OnCloudScriptExecuteEntityCloudScriptResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate299.Target, instance))
					{
						this.OnCloudScriptExecuteEntityCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult>)delegate299;
					}
				}
			}
			if (this.OnDataAbortFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate300 in this.OnDataAbortFileUploadsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate300.Target, instance))
					{
						this.OnDataAbortFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AbortFileUploadsRequest>)delegate300;
					}
				}
			}
			if (this.OnDataAbortFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate301 in this.OnDataAbortFileUploadsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate301.Target, instance))
					{
						this.OnDataAbortFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<AbortFileUploadsResponse>)delegate301;
					}
				}
			}
			if (this.OnDataDeleteFilesRequestEvent != null)
			{
				foreach (Delegate delegate302 in this.OnDataDeleteFilesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate302.Target, instance))
					{
						this.OnDataDeleteFilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteFilesRequest>)delegate302;
					}
				}
			}
			if (this.OnDataDeleteFilesResultEvent != null)
			{
				foreach (Delegate delegate303 in this.OnDataDeleteFilesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate303.Target, instance))
					{
						this.OnDataDeleteFilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeleteFilesResponse>)delegate303;
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate304 in this.OnDataFinalizeFileUploadsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate304.Target, instance))
					{
						this.OnDataFinalizeFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<FinalizeFileUploadsRequest>)delegate304;
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate305 in this.OnDataFinalizeFileUploadsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate305.Target, instance))
					{
						this.OnDataFinalizeFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<FinalizeFileUploadsResponse>)delegate305;
					}
				}
			}
			if (this.OnDataGetFilesRequestEvent != null)
			{
				foreach (Delegate delegate306 in this.OnDataGetFilesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate306.Target, instance))
					{
						this.OnDataGetFilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFilesRequest>)delegate306;
					}
				}
			}
			if (this.OnDataGetFilesResultEvent != null)
			{
				foreach (Delegate delegate307 in this.OnDataGetFilesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate307.Target, instance))
					{
						this.OnDataGetFilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFilesResponse>)delegate307;
					}
				}
			}
			if (this.OnDataGetObjectsRequestEvent != null)
			{
				foreach (Delegate delegate308 in this.OnDataGetObjectsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate308.Target, instance))
					{
						this.OnDataGetObjectsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetObjectsRequest>)delegate308;
					}
				}
			}
			if (this.OnDataGetObjectsResultEvent != null)
			{
				foreach (Delegate delegate309 in this.OnDataGetObjectsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate309.Target, instance))
					{
						this.OnDataGetObjectsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetObjectsResponse>)delegate309;
					}
				}
			}
			if (this.OnDataInitiateFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate310 in this.OnDataInitiateFileUploadsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate310.Target, instance))
					{
						this.OnDataInitiateFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InitiateFileUploadsRequest>)delegate310;
					}
				}
			}
			if (this.OnDataInitiateFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate311 in this.OnDataInitiateFileUploadsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate311.Target, instance))
					{
						this.OnDataInitiateFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<InitiateFileUploadsResponse>)delegate311;
					}
				}
			}
			if (this.OnDataSetObjectsRequestEvent != null)
			{
				foreach (Delegate delegate312 in this.OnDataSetObjectsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate312.Target, instance))
					{
						this.OnDataSetObjectsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetObjectsRequest>)delegate312;
					}
				}
			}
			if (this.OnDataSetObjectsResultEvent != null)
			{
				foreach (Delegate delegate313 in this.OnDataSetObjectsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate313.Target, instance))
					{
						this.OnDataSetObjectsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetObjectsResponse>)delegate313;
					}
				}
			}
			if (this.OnEventsWriteEventsRequestEvent != null)
			{
				foreach (Delegate delegate314 in this.OnEventsWriteEventsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate314.Target, instance))
					{
						this.OnEventsWriteEventsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest>)delegate314;
					}
				}
			}
			if (this.OnEventsWriteEventsResultEvent != null)
			{
				foreach (Delegate delegate315 in this.OnEventsWriteEventsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate315.Target, instance))
					{
						this.OnEventsWriteEventsResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventsResponse>)delegate315;
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationRequestEvent != null)
			{
				foreach (Delegate delegate316 in this.OnGroupsAcceptGroupApplicationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate316.Target, instance))
					{
						this.OnGroupsAcceptGroupApplicationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptGroupApplicationRequest>)delegate316;
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationResultEvent != null)
			{
				foreach (Delegate delegate317 in this.OnGroupsAcceptGroupApplicationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate317.Target, instance))
					{
						this.OnGroupsAcceptGroupApplicationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate317;
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationRequestEvent != null)
			{
				foreach (Delegate delegate318 in this.OnGroupsAcceptGroupInvitationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate318.Target, instance))
					{
						this.OnGroupsAcceptGroupInvitationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptGroupInvitationRequest>)delegate318;
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationResultEvent != null)
			{
				foreach (Delegate delegate319 in this.OnGroupsAcceptGroupInvitationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate319.Target, instance))
					{
						this.OnGroupsAcceptGroupInvitationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate319;
					}
				}
			}
			if (this.OnGroupsAddMembersRequestEvent != null)
			{
				foreach (Delegate delegate320 in this.OnGroupsAddMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate320.Target, instance))
					{
						this.OnGroupsAddMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddMembersRequest>)delegate320;
					}
				}
			}
			if (this.OnGroupsAddMembersResultEvent != null)
			{
				foreach (Delegate delegate321 in this.OnGroupsAddMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate321.Target, instance))
					{
						this.OnGroupsAddMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate321;
					}
				}
			}
			if (this.OnGroupsApplyToGroupRequestEvent != null)
			{
				foreach (Delegate delegate322 in this.OnGroupsApplyToGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate322.Target, instance))
					{
						this.OnGroupsApplyToGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ApplyToGroupRequest>)delegate322;
					}
				}
			}
			if (this.OnGroupsApplyToGroupResultEvent != null)
			{
				foreach (Delegate delegate323 in this.OnGroupsApplyToGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate323.Target, instance))
					{
						this.OnGroupsApplyToGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<ApplyToGroupResponse>)delegate323;
					}
				}
			}
			if (this.OnGroupsBlockEntityRequestEvent != null)
			{
				foreach (Delegate delegate324 in this.OnGroupsBlockEntityRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate324.Target, instance))
					{
						this.OnGroupsBlockEntityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<BlockEntityRequest>)delegate324;
					}
				}
			}
			if (this.OnGroupsBlockEntityResultEvent != null)
			{
				foreach (Delegate delegate325 in this.OnGroupsBlockEntityResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate325.Target, instance))
					{
						this.OnGroupsBlockEntityResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate325;
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleRequestEvent != null)
			{
				foreach (Delegate delegate326 in this.OnGroupsChangeMemberRoleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate326.Target, instance))
					{
						this.OnGroupsChangeMemberRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ChangeMemberRoleRequest>)delegate326;
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleResultEvent != null)
			{
				foreach (Delegate delegate327 in this.OnGroupsChangeMemberRoleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate327.Target, instance))
					{
						this.OnGroupsChangeMemberRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate327;
					}
				}
			}
			if (this.OnGroupsCreateGroupRequestEvent != null)
			{
				foreach (Delegate delegate328 in this.OnGroupsCreateGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate328.Target, instance))
					{
						this.OnGroupsCreateGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateGroupRequest>)delegate328;
					}
				}
			}
			if (this.OnGroupsCreateGroupResultEvent != null)
			{
				foreach (Delegate delegate329 in this.OnGroupsCreateGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate329.Target, instance))
					{
						this.OnGroupsCreateGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateGroupResponse>)delegate329;
					}
				}
			}
			if (this.OnGroupsCreateRoleRequestEvent != null)
			{
				foreach (Delegate delegate330 in this.OnGroupsCreateRoleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate330.Target, instance))
					{
						this.OnGroupsCreateRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateGroupRoleRequest>)delegate330;
					}
				}
			}
			if (this.OnGroupsCreateRoleResultEvent != null)
			{
				foreach (Delegate delegate331 in this.OnGroupsCreateRoleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate331.Target, instance))
					{
						this.OnGroupsCreateRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateGroupRoleResponse>)delegate331;
					}
				}
			}
			if (this.OnGroupsDeleteGroupRequestEvent != null)
			{
				foreach (Delegate delegate332 in this.OnGroupsDeleteGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate332.Target, instance))
					{
						this.OnGroupsDeleteGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteGroupRequest>)delegate332;
					}
				}
			}
			if (this.OnGroupsDeleteGroupResultEvent != null)
			{
				foreach (Delegate delegate333 in this.OnGroupsDeleteGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate333.Target, instance))
					{
						this.OnGroupsDeleteGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate333;
					}
				}
			}
			if (this.OnGroupsDeleteRoleRequestEvent != null)
			{
				foreach (Delegate delegate334 in this.OnGroupsDeleteRoleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate334.Target, instance))
					{
						this.OnGroupsDeleteRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteRoleRequest>)delegate334;
					}
				}
			}
			if (this.OnGroupsDeleteRoleResultEvent != null)
			{
				foreach (Delegate delegate335 in this.OnGroupsDeleteRoleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate335.Target, instance))
					{
						this.OnGroupsDeleteRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate335;
					}
				}
			}
			if (this.OnGroupsGetGroupRequestEvent != null)
			{
				foreach (Delegate delegate336 in this.OnGroupsGetGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate336.Target, instance))
					{
						this.OnGroupsGetGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetGroupRequest>)delegate336;
					}
				}
			}
			if (this.OnGroupsGetGroupResultEvent != null)
			{
				foreach (Delegate delegate337 in this.OnGroupsGetGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate337.Target, instance))
					{
						this.OnGroupsGetGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetGroupResponse>)delegate337;
					}
				}
			}
			if (this.OnGroupsInviteToGroupRequestEvent != null)
			{
				foreach (Delegate delegate338 in this.OnGroupsInviteToGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate338.Target, instance))
					{
						this.OnGroupsInviteToGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InviteToGroupRequest>)delegate338;
					}
				}
			}
			if (this.OnGroupsInviteToGroupResultEvent != null)
			{
				foreach (Delegate delegate339 in this.OnGroupsInviteToGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate339.Target, instance))
					{
						this.OnGroupsInviteToGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<InviteToGroupResponse>)delegate339;
					}
				}
			}
			if (this.OnGroupsIsMemberRequestEvent != null)
			{
				foreach (Delegate delegate340 in this.OnGroupsIsMemberRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate340.Target, instance))
					{
						this.OnGroupsIsMemberRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<IsMemberRequest>)delegate340;
					}
				}
			}
			if (this.OnGroupsIsMemberResultEvent != null)
			{
				foreach (Delegate delegate341 in this.OnGroupsIsMemberResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate341.Target, instance))
					{
						this.OnGroupsIsMemberResultEvent -= (PlayFabEvents.PlayFabResultEvent<IsMemberResponse>)delegate341;
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsRequestEvent != null)
			{
				foreach (Delegate delegate342 in this.OnGroupsListGroupApplicationsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate342.Target, instance))
					{
						this.OnGroupsListGroupApplicationsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupApplicationsRequest>)delegate342;
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsResultEvent != null)
			{
				foreach (Delegate delegate343 in this.OnGroupsListGroupApplicationsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate343.Target, instance))
					{
						this.OnGroupsListGroupApplicationsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupApplicationsResponse>)delegate343;
					}
				}
			}
			if (this.OnGroupsListGroupBlocksRequestEvent != null)
			{
				foreach (Delegate delegate344 in this.OnGroupsListGroupBlocksRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate344.Target, instance))
					{
						this.OnGroupsListGroupBlocksRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupBlocksRequest>)delegate344;
					}
				}
			}
			if (this.OnGroupsListGroupBlocksResultEvent != null)
			{
				foreach (Delegate delegate345 in this.OnGroupsListGroupBlocksResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate345.Target, instance))
					{
						this.OnGroupsListGroupBlocksResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupBlocksResponse>)delegate345;
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsRequestEvent != null)
			{
				foreach (Delegate delegate346 in this.OnGroupsListGroupInvitationsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate346.Target, instance))
					{
						this.OnGroupsListGroupInvitationsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupInvitationsRequest>)delegate346;
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsResultEvent != null)
			{
				foreach (Delegate delegate347 in this.OnGroupsListGroupInvitationsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate347.Target, instance))
					{
						this.OnGroupsListGroupInvitationsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupInvitationsResponse>)delegate347;
					}
				}
			}
			if (this.OnGroupsListGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate348 in this.OnGroupsListGroupMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate348.Target, instance))
					{
						this.OnGroupsListGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupMembersRequest>)delegate348;
					}
				}
			}
			if (this.OnGroupsListGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate349 in this.OnGroupsListGroupMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate349.Target, instance))
					{
						this.OnGroupsListGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupMembersResponse>)delegate349;
					}
				}
			}
			if (this.OnGroupsListMembershipRequestEvent != null)
			{
				foreach (Delegate delegate350 in this.OnGroupsListMembershipRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate350.Target, instance))
					{
						this.OnGroupsListMembershipRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMembershipRequest>)delegate350;
					}
				}
			}
			if (this.OnGroupsListMembershipResultEvent != null)
			{
				foreach (Delegate delegate351 in this.OnGroupsListMembershipResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate351.Target, instance))
					{
						this.OnGroupsListMembershipResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMembershipResponse>)delegate351;
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesRequestEvent != null)
			{
				foreach (Delegate delegate352 in this.OnGroupsListMembershipOpportunitiesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate352.Target, instance))
					{
						this.OnGroupsListMembershipOpportunitiesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMembershipOpportunitiesRequest>)delegate352;
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesResultEvent != null)
			{
				foreach (Delegate delegate353 in this.OnGroupsListMembershipOpportunitiesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate353.Target, instance))
					{
						this.OnGroupsListMembershipOpportunitiesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMembershipOpportunitiesResponse>)delegate353;
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationRequestEvent != null)
			{
				foreach (Delegate delegate354 in this.OnGroupsRemoveGroupApplicationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate354.Target, instance))
					{
						this.OnGroupsRemoveGroupApplicationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGroupApplicationRequest>)delegate354;
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationResultEvent != null)
			{
				foreach (Delegate delegate355 in this.OnGroupsRemoveGroupApplicationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate355.Target, instance))
					{
						this.OnGroupsRemoveGroupApplicationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate355;
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationRequestEvent != null)
			{
				foreach (Delegate delegate356 in this.OnGroupsRemoveGroupInvitationRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate356.Target, instance))
					{
						this.OnGroupsRemoveGroupInvitationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGroupInvitationRequest>)delegate356;
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationResultEvent != null)
			{
				foreach (Delegate delegate357 in this.OnGroupsRemoveGroupInvitationResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate357.Target, instance))
					{
						this.OnGroupsRemoveGroupInvitationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate357;
					}
				}
			}
			if (this.OnGroupsRemoveMembersRequestEvent != null)
			{
				foreach (Delegate delegate358 in this.OnGroupsRemoveMembersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate358.Target, instance))
					{
						this.OnGroupsRemoveMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveMembersRequest>)delegate358;
					}
				}
			}
			if (this.OnGroupsRemoveMembersResultEvent != null)
			{
				foreach (Delegate delegate359 in this.OnGroupsRemoveMembersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate359.Target, instance))
					{
						this.OnGroupsRemoveMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate359;
					}
				}
			}
			if (this.OnGroupsUnblockEntityRequestEvent != null)
			{
				foreach (Delegate delegate360 in this.OnGroupsUnblockEntityRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate360.Target, instance))
					{
						this.OnGroupsUnblockEntityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnblockEntityRequest>)delegate360;
					}
				}
			}
			if (this.OnGroupsUnblockEntityResultEvent != null)
			{
				foreach (Delegate delegate361 in this.OnGroupsUnblockEntityResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate361.Target, instance))
					{
						this.OnGroupsUnblockEntityResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate361;
					}
				}
			}
			if (this.OnGroupsUpdateGroupRequestEvent != null)
			{
				foreach (Delegate delegate362 in this.OnGroupsUpdateGroupRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate362.Target, instance))
					{
						this.OnGroupsUpdateGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateGroupRequest>)delegate362;
					}
				}
			}
			if (this.OnGroupsUpdateGroupResultEvent != null)
			{
				foreach (Delegate delegate363 in this.OnGroupsUpdateGroupResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate363.Target, instance))
					{
						this.OnGroupsUpdateGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateGroupResponse>)delegate363;
					}
				}
			}
			if (this.OnGroupsUpdateRoleRequestEvent != null)
			{
				foreach (Delegate delegate364 in this.OnGroupsUpdateRoleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate364.Target, instance))
					{
						this.OnGroupsUpdateRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateGroupRoleRequest>)delegate364;
					}
				}
			}
			if (this.OnGroupsUpdateRoleResultEvent != null)
			{
				foreach (Delegate delegate365 in this.OnGroupsUpdateRoleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate365.Target, instance))
					{
						this.OnGroupsUpdateRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateGroupRoleResponse>)delegate365;
					}
				}
			}
			if (this.OnLocalizationGetLanguageListRequestEvent != null)
			{
				foreach (Delegate delegate366 in this.OnLocalizationGetLanguageListRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate366.Target, instance))
					{
						this.OnLocalizationGetLanguageListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLanguageListRequest>)delegate366;
					}
				}
			}
			if (this.OnLocalizationGetLanguageListResultEvent != null)
			{
				foreach (Delegate delegate367 in this.OnLocalizationGetLanguageListResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate367.Target, instance))
					{
						this.OnLocalizationGetLanguageListResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLanguageListResponse>)delegate367;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
			{
				foreach (Delegate delegate368 in this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate368.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildWithCustomContainerRequest>)delegate368;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
			{
				foreach (Delegate delegate369 in this.OnMultiplayerCreateBuildWithCustomContainerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate369.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithCustomContainerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateBuildWithCustomContainerResponse>)delegate369;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
			{
				foreach (Delegate delegate370 in this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate370.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildWithManagedContainerRequest>)delegate370;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
			{
				foreach (Delegate delegate371 in this.OnMultiplayerCreateBuildWithManagedContainerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate371.Target, instance))
					{
						this.OnMultiplayerCreateBuildWithManagedContainerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateBuildWithManagedContainerResponse>)delegate371;
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserRequestEvent != null)
			{
				foreach (Delegate delegate372 in this.OnMultiplayerCreateRemoteUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate372.Target, instance))
					{
						this.OnMultiplayerCreateRemoteUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateRemoteUserRequest>)delegate372;
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserResultEvent != null)
			{
				foreach (Delegate delegate373 in this.OnMultiplayerCreateRemoteUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate373.Target, instance))
					{
						this.OnMultiplayerCreateRemoteUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateRemoteUserResponse>)delegate373;
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetRequestEvent != null)
			{
				foreach (Delegate delegate374 in this.OnMultiplayerDeleteAssetRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate374.Target, instance))
					{
						this.OnMultiplayerDeleteAssetRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteAssetRequest>)delegate374;
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetResultEvent != null)
			{
				foreach (Delegate delegate375 in this.OnMultiplayerDeleteAssetResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate375.Target, instance))
					{
						this.OnMultiplayerDeleteAssetResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate375;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRequestEvent != null)
			{
				foreach (Delegate delegate376 in this.OnMultiplayerDeleteBuildRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate376.Target, instance))
					{
						this.OnMultiplayerDeleteBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteBuildRequest>)delegate376;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildResultEvent != null)
			{
				foreach (Delegate delegate377 in this.OnMultiplayerDeleteBuildResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate377.Target, instance))
					{
						this.OnMultiplayerDeleteBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate377;
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateRequestEvent != null)
			{
				foreach (Delegate delegate378 in this.OnMultiplayerDeleteCertificateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate378.Target, instance))
					{
						this.OnMultiplayerDeleteCertificateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteCertificateRequest>)delegate378;
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateResultEvent != null)
			{
				foreach (Delegate delegate379 in this.OnMultiplayerDeleteCertificateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate379.Target, instance))
					{
						this.OnMultiplayerDeleteCertificateResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate379;
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserRequestEvent != null)
			{
				foreach (Delegate delegate380 in this.OnMultiplayerDeleteRemoteUserRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate380.Target, instance))
					{
						this.OnMultiplayerDeleteRemoteUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteRemoteUserRequest>)delegate380;
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserResultEvent != null)
			{
				foreach (Delegate delegate381 in this.OnMultiplayerDeleteRemoteUserResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate381.Target, instance))
					{
						this.OnMultiplayerDeleteRemoteUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate381;
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
			{
				foreach (Delegate delegate382 in this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate382.Target, instance))
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest>)delegate382;
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
			{
				foreach (Delegate delegate383 in this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate383.Target, instance))
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent -= (PlayFabEvents.PlayFabResultEvent<EnableMultiplayerServersForTitleResponse>)delegate383;
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
			{
				foreach (Delegate delegate384 in this.OnMultiplayerGetAssetUploadUrlRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate384.Target, instance))
					{
						this.OnMultiplayerGetAssetUploadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAssetUploadUrlRequest>)delegate384;
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlResultEvent != null)
			{
				foreach (Delegate delegate385 in this.OnMultiplayerGetAssetUploadUrlResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate385.Target, instance))
					{
						this.OnMultiplayerGetAssetUploadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAssetUploadUrlResponse>)delegate385;
					}
				}
			}
			if (this.OnMultiplayerGetBuildRequestEvent != null)
			{
				foreach (Delegate delegate386 in this.OnMultiplayerGetBuildRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate386.Target, instance))
					{
						this.OnMultiplayerGetBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetBuildRequest>)delegate386;
					}
				}
			}
			if (this.OnMultiplayerGetBuildResultEvent != null)
			{
				foreach (Delegate delegate387 in this.OnMultiplayerGetBuildResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate387.Target, instance))
					{
						this.OnMultiplayerGetBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetBuildResponse>)delegate387;
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
			{
				foreach (Delegate delegate388 in this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate388.Target, instance))
					{
						this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContainerRegistryCredentialsRequest>)delegate388;
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
			{
				foreach (Delegate delegate389 in this.OnMultiplayerGetContainerRegistryCredentialsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate389.Target, instance))
					{
						this.OnMultiplayerGetContainerRegistryCredentialsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContainerRegistryCredentialsResponse>)delegate389;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
			{
				foreach (Delegate delegate390 in this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate390.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerDetailsRequest>)delegate390;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
			{
				foreach (Delegate delegate391 in this.OnMultiplayerGetMultiplayerServerDetailsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate391.Target, instance))
					{
						this.OnMultiplayerGetMultiplayerServerDetailsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerDetailsResponse>)delegate391;
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
			{
				foreach (Delegate delegate392 in this.OnMultiplayerGetRemoteLoginEndpointRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate392.Target, instance))
					{
						this.OnMultiplayerGetRemoteLoginEndpointRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetRemoteLoginEndpointRequest>)delegate392;
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
			{
				foreach (Delegate delegate393 in this.OnMultiplayerGetRemoteLoginEndpointResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate393.Target, instance))
					{
						this.OnMultiplayerGetRemoteLoginEndpointResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetRemoteLoginEndpointResponse>)delegate393;
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
			{
				foreach (Delegate delegate394 in this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate394.Target, instance))
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest>)delegate394;
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
			{
				foreach (Delegate delegate395 in this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate395.Target, instance))
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse>)delegate395;
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
			{
				foreach (Delegate delegate396 in this.OnMultiplayerListArchivedMultiplayerServersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate396.Target, instance))
					{
						this.OnMultiplayerListArchivedMultiplayerServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest>)delegate396;
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
			{
				foreach (Delegate delegate397 in this.OnMultiplayerListArchivedMultiplayerServersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate397.Target, instance))
					{
						this.OnMultiplayerListArchivedMultiplayerServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse>)delegate397;
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesRequestEvent != null)
			{
				foreach (Delegate delegate398 in this.OnMultiplayerListAssetSummariesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate398.Target, instance))
					{
						this.OnMultiplayerListAssetSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListAssetSummariesRequest>)delegate398;
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesResultEvent != null)
			{
				foreach (Delegate delegate399 in this.OnMultiplayerListAssetSummariesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate399.Target, instance))
					{
						this.OnMultiplayerListAssetSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListAssetSummariesResponse>)delegate399;
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesRequestEvent != null)
			{
				foreach (Delegate delegate400 in this.OnMultiplayerListBuildSummariesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate400.Target, instance))
					{
						this.OnMultiplayerListBuildSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListBuildSummariesRequest>)delegate400;
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesResultEvent != null)
			{
				foreach (Delegate delegate401 in this.OnMultiplayerListBuildSummariesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate401.Target, instance))
					{
						this.OnMultiplayerListBuildSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListBuildSummariesResponse>)delegate401;
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesRequestEvent != null)
			{
				foreach (Delegate delegate402 in this.OnMultiplayerListCertificateSummariesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate402.Target, instance))
					{
						this.OnMultiplayerListCertificateSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListCertificateSummariesRequest>)delegate402;
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesResultEvent != null)
			{
				foreach (Delegate delegate403 in this.OnMultiplayerListCertificateSummariesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate403.Target, instance))
					{
						this.OnMultiplayerListCertificateSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListCertificateSummariesResponse>)delegate403;
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesRequestEvent != null)
			{
				foreach (Delegate delegate404 in this.OnMultiplayerListContainerImagesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate404.Target, instance))
					{
						this.OnMultiplayerListContainerImagesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListContainerImagesRequest>)delegate404;
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesResultEvent != null)
			{
				foreach (Delegate delegate405 in this.OnMultiplayerListContainerImagesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate405.Target, instance))
					{
						this.OnMultiplayerListContainerImagesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListContainerImagesResponse>)delegate405;
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsRequestEvent != null)
			{
				foreach (Delegate delegate406 in this.OnMultiplayerListContainerImageTagsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate406.Target, instance))
					{
						this.OnMultiplayerListContainerImageTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListContainerImageTagsRequest>)delegate406;
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsResultEvent != null)
			{
				foreach (Delegate delegate407 in this.OnMultiplayerListContainerImageTagsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate407.Target, instance))
					{
						this.OnMultiplayerListContainerImageTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListContainerImageTagsResponse>)delegate407;
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersRequestEvent != null)
			{
				foreach (Delegate delegate408 in this.OnMultiplayerListMultiplayerServersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate408.Target, instance))
					{
						this.OnMultiplayerListMultiplayerServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest>)delegate408;
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersResultEvent != null)
			{
				foreach (Delegate delegate409 in this.OnMultiplayerListMultiplayerServersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate409.Target, instance))
					{
						this.OnMultiplayerListMultiplayerServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse>)delegate409;
					}
				}
			}
			if (this.OnMultiplayerListQosServersRequestEvent != null)
			{
				foreach (Delegate delegate410 in this.OnMultiplayerListQosServersRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate410.Target, instance))
					{
						this.OnMultiplayerListQosServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListQosServersRequest>)delegate410;
					}
				}
			}
			if (this.OnMultiplayerListQosServersResultEvent != null)
			{
				foreach (Delegate delegate411 in this.OnMultiplayerListQosServersResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate411.Target, instance))
					{
						this.OnMultiplayerListQosServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListQosServersResponse>)delegate411;
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
			{
				foreach (Delegate delegate412 in this.OnMultiplayerListVirtualMachineSummariesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate412.Target, instance))
					{
						this.OnMultiplayerListVirtualMachineSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListVirtualMachineSummariesRequest>)delegate412;
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
			{
				foreach (Delegate delegate413 in this.OnMultiplayerListVirtualMachineSummariesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate413.Target, instance))
					{
						this.OnMultiplayerListVirtualMachineSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListVirtualMachineSummariesResponse>)delegate413;
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
			{
				foreach (Delegate delegate414 in this.OnMultiplayerRequestMultiplayerServerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate414.Target, instance))
					{
						this.OnMultiplayerRequestMultiplayerServerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RequestMultiplayerServerRequest>)delegate414;
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerResultEvent != null)
			{
				foreach (Delegate delegate415 in this.OnMultiplayerRequestMultiplayerServerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate415.Target, instance))
					{
						this.OnMultiplayerRequestMultiplayerServerResultEvent -= (PlayFabEvents.PlayFabResultEvent<RequestMultiplayerServerResponse>)delegate415;
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
			{
				foreach (Delegate delegate416 in this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate416.Target, instance))
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest>)delegate416;
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
			{
				foreach (Delegate delegate417 in this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate417.Target, instance))
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent -= (PlayFabEvents.PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse>)delegate417;
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
			{
				foreach (Delegate delegate418 in this.OnMultiplayerShutdownMultiplayerServerRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate418.Target, instance))
					{
						this.OnMultiplayerShutdownMultiplayerServerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ShutdownMultiplayerServerRequest>)delegate418;
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
			{
				foreach (Delegate delegate419 in this.OnMultiplayerShutdownMultiplayerServerResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate419.Target, instance))
					{
						this.OnMultiplayerShutdownMultiplayerServerResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate419;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
			{
				foreach (Delegate delegate420 in this.OnMultiplayerUpdateBuildRegionsRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate420.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionsRequest>)delegate420;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsResultEvent != null)
			{
				foreach (Delegate delegate421 in this.OnMultiplayerUpdateBuildRegionsResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate421.Target, instance))
					{
						this.OnMultiplayerUpdateBuildRegionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate421;
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateRequestEvent != null)
			{
				foreach (Delegate delegate422 in this.OnMultiplayerUploadCertificateRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate422.Target, instance))
					{
						this.OnMultiplayerUploadCertificateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UploadCertificateRequest>)delegate422;
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateResultEvent != null)
			{
				foreach (Delegate delegate423 in this.OnMultiplayerUploadCertificateResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate423.Target, instance))
					{
						this.OnMultiplayerUploadCertificateResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate423;
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyRequestEvent != null)
			{
				foreach (Delegate delegate424 in this.OnProfilesGetGlobalPolicyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate424.Target, instance))
					{
						this.OnProfilesGetGlobalPolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetGlobalPolicyRequest>)delegate424;
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyResultEvent != null)
			{
				foreach (Delegate delegate425 in this.OnProfilesGetGlobalPolicyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate425.Target, instance))
					{
						this.OnProfilesGetGlobalPolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetGlobalPolicyResponse>)delegate425;
					}
				}
			}
			if (this.OnProfilesGetProfileRequestEvent != null)
			{
				foreach (Delegate delegate426 in this.OnProfilesGetProfileRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate426.Target, instance))
					{
						this.OnProfilesGetProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityProfileRequest>)delegate426;
					}
				}
			}
			if (this.OnProfilesGetProfileResultEvent != null)
			{
				foreach (Delegate delegate427 in this.OnProfilesGetProfileResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate427.Target, instance))
					{
						this.OnProfilesGetProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityProfileResponse>)delegate427;
					}
				}
			}
			if (this.OnProfilesGetProfilesRequestEvent != null)
			{
				foreach (Delegate delegate428 in this.OnProfilesGetProfilesRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate428.Target, instance))
					{
						this.OnProfilesGetProfilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityProfilesRequest>)delegate428;
					}
				}
			}
			if (this.OnProfilesGetProfilesResultEvent != null)
			{
				foreach (Delegate delegate429 in this.OnProfilesGetProfilesResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate429.Target, instance))
					{
						this.OnProfilesGetProfilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityProfilesResponse>)delegate429;
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyRequestEvent != null)
			{
				foreach (Delegate delegate430 in this.OnProfilesSetGlobalPolicyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate430.Target, instance))
					{
						this.OnProfilesSetGlobalPolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetGlobalPolicyRequest>)delegate430;
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyResultEvent != null)
			{
				foreach (Delegate delegate431 in this.OnProfilesSetGlobalPolicyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate431.Target, instance))
					{
						this.OnProfilesSetGlobalPolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetGlobalPolicyResponse>)delegate431;
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageRequestEvent != null)
			{
				foreach (Delegate delegate432 in this.OnProfilesSetProfileLanguageRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate432.Target, instance))
					{
						this.OnProfilesSetProfileLanguageRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetProfileLanguageRequest>)delegate432;
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageResultEvent != null)
			{
				foreach (Delegate delegate433 in this.OnProfilesSetProfileLanguageResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate433.Target, instance))
					{
						this.OnProfilesSetProfileLanguageResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetProfileLanguageResponse>)delegate433;
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyRequestEvent != null)
			{
				foreach (Delegate delegate434 in this.OnProfilesSetProfilePolicyRequestEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate434.Target, instance))
					{
						this.OnProfilesSetProfilePolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetEntityProfilePolicyRequest>)delegate434;
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyResultEvent != null)
			{
				foreach (Delegate delegate435 in this.OnProfilesSetProfilePolicyResultEvent.GetInvocationList())
				{
					if (object.ReferenceEquals(delegate435.Target, instance))
					{
						this.OnProfilesSetProfilePolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetEntityProfilePolicyResponse>)delegate435;
					}
				}
			}
		}

		private void OnProcessingErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (PlayFabEvents._instance.OnGlobalErrorEvent != null)
			{
				PlayFabEvents._instance.OnGlobalErrorEvent(request, error);
			}
		}

		private void OnProcessingEvent(ApiProcessingEventArgs e)
		{
			if (e.EventType == ApiProcessingEventType.Pre)
			{
				Type type = e.Request.GetType();
				if (type == typeof(AcceptTradeRequest) && PlayFabEvents._instance.OnAcceptTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeRequestEvent((AcceptTradeRequest)e.Request);
					return;
				}
				if (type == typeof(AddFriendRequest) && PlayFabEvents._instance.OnAddFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendRequestEvent((AddFriendRequest)e.Request);
					return;
				}
				if (type == typeof(AddGenericIDRequest) && PlayFabEvents._instance.OnAddGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDRequestEvent((AddGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(AddOrUpdateContactEmailRequest) && PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent((AddOrUpdateContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(AddSharedGroupMembersRequest) && PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent((AddSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(AddUsernamePasswordRequest) && PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent((AddUsernamePasswordRequest)e.Request);
					return;
				}
				if (type == typeof(AddUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent((AddUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(AndroidDevicePushNotificationRegistrationRequest) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent((AndroidDevicePushNotificationRegistrationRequest)e.Request);
					return;
				}
				if (type == typeof(AttributeInstallRequest) && PlayFabEvents._instance.OnAttributeInstallRequestEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallRequestEvent((AttributeInstallRequest)e.Request);
					return;
				}
				if (type == typeof(CancelTradeRequest) && PlayFabEvents._instance.OnCancelTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeRequestEvent((CancelTradeRequest)e.Request);
					return;
				}
				if (type == typeof(ConfirmPurchaseRequest) && PlayFabEvents._instance.OnConfirmPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseRequestEvent((ConfirmPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumeItemRequest) && PlayFabEvents._instance.OnConsumeItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemRequestEvent((ConsumeItemRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumePSNEntitlementsRequest) && PlayFabEvents._instance.OnConsumePSNEntitlementsRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumePSNEntitlementsRequestEvent((ConsumePSNEntitlementsRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumeXboxEntitlementsRequest) && PlayFabEvents._instance.OnConsumeXboxEntitlementsRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumeXboxEntitlementsRequestEvent((ConsumeXboxEntitlementsRequest)e.Request);
					return;
				}
				if (type == typeof(CreateSharedGroupRequest) && PlayFabEvents._instance.OnCreateSharedGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupRequestEvent((CreateSharedGroupRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteCloudScriptRequest) && PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent((ExecuteCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(GetAccountInfoRequest) && PlayFabEvents._instance.OnGetAccountInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoRequestEvent((GetAccountInfoRequest)e.Request);
					return;
				}
				if (type == typeof(ListUsersCharactersRequest) && PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent((ListUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(GetCatalogItemsRequest) && PlayFabEvents._instance.OnGetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsRequestEvent((GetCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataRequestEvent((GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterInventoryRequest) && PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent((GetCharacterInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterLeaderboardRequest) && PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent((GetCharacterLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent((GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterStatisticsRequest) && PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent((GetCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(GetContentDownloadUrlRequest) && PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent((GetContentDownloadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(CurrentGamesRequest) && PlayFabEvents._instance.OnGetCurrentGamesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesRequestEvent((CurrentGamesRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendLeaderboardRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent((GetFriendLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent((GetFriendLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendsListRequest) && PlayFabEvents._instance.OnGetFriendsListRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListRequestEvent((GetFriendsListRequest)e.Request);
					return;
				}
				if (type == typeof(GameServerRegionsRequest) && PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent((GameServerRegionsRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardRequest) && PlayFabEvents._instance.OnGetLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardRequestEvent((GetLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundCharacterRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent((GetLeaderboardAroundCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent((GetLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardForUsersCharactersRequest) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent((GetLeaderboardForUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(GetPaymentTokenRequest) && PlayFabEvents._instance.OnGetPaymentTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenRequestEvent((GetPaymentTokenRequest)e.Request);
					return;
				}
				if (type == typeof(GetPhotonAuthenticationTokenRequest) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent((GetPhotonAuthenticationTokenRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerCombinedInfoRequest) && PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent((GetPlayerCombinedInfoRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerProfileRequest) && PlayFabEvents._instance.OnGetPlayerProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileRequestEvent((GetPlayerProfileRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerSegmentsRequest) && PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent((GetPlayerSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerStatisticsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent((GetPlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerStatisticVersionsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent((GetPlayerStatisticVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerTagsRequest) && PlayFabEvents._instance.OnGetPlayerTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsRequestEvent((GetPlayerTagsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerTradesRequest) && PlayFabEvents._instance.OnGetPlayerTradesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesRequestEvent((GetPlayerTradesRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromFacebookIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent((GetPlayFabIDsFromFacebookIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent((GetPlayFabIDsFromFacebookInstantGamesIdsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGameCenterIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent((GetPlayFabIDsFromGameCenterIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGenericIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent((GetPlayFabIDsFromGenericIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGoogleIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent((GetPlayFabIDsFromGoogleIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromKongregateIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent((GetPlayFabIDsFromKongregateIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromPSNAccountIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent((GetPlayFabIDsFromPSNAccountIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromSteamIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent((GetPlayFabIDsFromSteamIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromTwitchIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent((GetPlayFabIDsFromTwitchIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromXboxLiveIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent((GetPlayFabIDsFromXboxLiveIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPublisherDataRequest) && PlayFabEvents._instance.OnGetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataRequestEvent((GetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetPurchaseRequest) && PlayFabEvents._instance.OnGetPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseRequestEvent((GetPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(GetSharedGroupDataRequest) && PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent((GetSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetStoreItemsRequest) && PlayFabEvents._instance.OnGetStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsRequestEvent((GetStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTimeRequest) && PlayFabEvents._instance.OnGetTimeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeRequestEvent((GetTimeRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleDataRequest) && PlayFabEvents._instance.OnGetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataRequestEvent((GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleNewsRequest) && PlayFabEvents._instance.OnGetTitleNewsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsRequestEvent((GetTitleNewsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitlePublicKeyRequest) && PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent((GetTitlePublicKeyRequest)e.Request);
					return;
				}
				if (type == typeof(GetTradeStatusRequest) && PlayFabEvents._instance.OnGetTradeStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusRequestEvent((GetTradeStatusRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserInventoryRequest) && PlayFabEvents._instance.OnGetUserInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryRequestEvent((GetUserInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetWindowsHelloChallengeRequest) && PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent((GetWindowsHelloChallengeRequest)e.Request);
					return;
				}
				if (type == typeof(GrantCharacterToUserRequest) && PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent((GrantCharacterToUserRequest)e.Request);
					return;
				}
				if (type == typeof(LinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent((LinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkCustomIDRequest) && PlayFabEvents._instance.OnLinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDRequestEvent((LinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkFacebookAccountRequest) && PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent((LinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnLinkFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookInstantGamesIdRequestEvent((LinkFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGameCenterAccountRequest) && PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent((LinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGoogleAccountRequest) && PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent((LinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent((LinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkKongregateAccountRequest) && PlayFabEvents._instance.OnLinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateRequestEvent((LinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdRequestEvent((LinkNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(LinkOpenIdConnectRequest) && PlayFabEvents._instance.OnLinkOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkOpenIdConnectRequestEvent((LinkOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(LinkPSNAccountRequest) && PlayFabEvents._instance.OnLinkPSNAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkPSNAccountRequestEvent((LinkPSNAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkSteamAccountRequest) && PlayFabEvents._instance.OnLinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountRequestEvent((LinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkTwitchAccountRequest) && PlayFabEvents._instance.OnLinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchRequestEvent((LinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent((LinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkXboxAccountRequest) && PlayFabEvents._instance.OnLinkXboxAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkXboxAccountRequestEvent((LinkXboxAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent((LoginWithAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithCustomIDRequest) && PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent((LoginWithCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithEmailAddressRequest) && PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent((LoginWithEmailAddressRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithFacebookRequest) && PlayFabEvents._instance.OnLoginWithFacebookRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithFacebookRequestEvent((LoginWithFacebookRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithFacebookInstantGamesIdRequestEvent((LoginWithFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGameCenterRequest) && PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent((LoginWithGameCenterRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGoogleAccountRequest) && PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent((LoginWithGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithIOSDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent((LoginWithIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithKongregateRequest) && PlayFabEvents._instance.OnLoginWithKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithKongregateRequestEvent((LoginWithKongregateRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent((LoginWithNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithOpenIdConnectRequest) && PlayFabEvents._instance.OnLoginWithOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithOpenIdConnectRequestEvent((LoginWithOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithPlayFabRequest) && PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent((LoginWithPlayFabRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithPSNRequest) && PlayFabEvents._instance.OnLoginWithPSNRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithPSNRequestEvent((LoginWithPSNRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithSteamRequest) && PlayFabEvents._instance.OnLoginWithSteamRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithSteamRequestEvent((LoginWithSteamRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithTwitchRequest) && PlayFabEvents._instance.OnLoginWithTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithTwitchRequestEvent((LoginWithTwitchRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithWindowsHelloRequest) && PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent((LoginWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithXboxRequest) && PlayFabEvents._instance.OnLoginWithXboxRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithXboxRequestEvent((LoginWithXboxRequest)e.Request);
					return;
				}
				if (type == typeof(MatchmakeRequest) && PlayFabEvents._instance.OnMatchmakeRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeRequestEvent((MatchmakeRequest)e.Request);
					return;
				}
				if (type == typeof(OpenTradeRequest) && PlayFabEvents._instance.OnOpenTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeRequestEvent((OpenTradeRequest)e.Request);
					return;
				}
				if (type == typeof(PayForPurchaseRequest) && PlayFabEvents._instance.OnPayForPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseRequestEvent((PayForPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PurchaseItemRequest) && PlayFabEvents._instance.OnPurchaseItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemRequestEvent((PurchaseItemRequest)e.Request);
					return;
				}
				if (type == typeof(RedeemCouponRequest) && PlayFabEvents._instance.OnRedeemCouponRequestEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponRequestEvent((RedeemCouponRequest)e.Request);
					return;
				}
				if (type == typeof(RefreshPSNAuthTokenRequest) && PlayFabEvents._instance.OnRefreshPSNAuthTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnRefreshPSNAuthTokenRequestEvent((RefreshPSNAuthTokenRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterForIOSPushNotificationRequest) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent((RegisterForIOSPushNotificationRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterPlayFabUserRequest) && PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent((RegisterPlayFabUserRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterWithWindowsHelloRequest) && PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent((RegisterWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveContactEmailRequest) && PlayFabEvents._instance.OnRemoveContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailRequestEvent((RemoveContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveFriendRequest) && PlayFabEvents._instance.OnRemoveFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendRequestEvent((RemoveFriendRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGenericIDRequest) && PlayFabEvents._instance.OnRemoveGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDRequestEvent((RemoveGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveSharedGroupMembersRequest) && PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent((RemoveSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(DeviceInfoRequest) && PlayFabEvents._instance.OnReportDeviceInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoRequestEvent((DeviceInfoRequest)e.Request);
					return;
				}
				if (type == typeof(ReportPlayerClientRequest) && PlayFabEvents._instance.OnReportPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerRequestEvent((ReportPlayerClientRequest)e.Request);
					return;
				}
				if (type == typeof(RestoreIOSPurchasesRequest) && PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent((RestoreIOSPurchasesRequest)e.Request);
					return;
				}
				if (type == typeof(SendAccountRecoveryEmailRequest) && PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent((SendAccountRecoveryEmailRequest)e.Request);
					return;
				}
				if (type == typeof(SetFriendTagsRequest) && PlayFabEvents._instance.OnSetFriendTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsRequestEvent((SetFriendTagsRequest)e.Request);
					return;
				}
				if (type == typeof(SetPlayerSecretRequest) && PlayFabEvents._instance.OnSetPlayerSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretRequestEvent((SetPlayerSecretRequest)e.Request);
					return;
				}
				if (type == typeof(StartGameRequest) && PlayFabEvents._instance.OnStartGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartGameRequestEvent((StartGameRequest)e.Request);
					return;
				}
				if (type == typeof(StartPurchaseRequest) && PlayFabEvents._instance.OnStartPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseRequestEvent((StartPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(SubtractUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent((SubtractUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent((UnlinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkCustomIDRequest) && PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent((UnlinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkFacebookAccountRequest) && PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent((UnlinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdRequestEvent((UnlinkFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGameCenterAccountRequest) && PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent((UnlinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGoogleAccountRequest) && PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent((UnlinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent((UnlinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkKongregateAccountRequest) && PlayFabEvents._instance.OnUnlinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateRequestEvent((UnlinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent((UnlinkNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(UninkOpenIdConnectRequest) && PlayFabEvents._instance.OnUnlinkOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkOpenIdConnectRequestEvent((UninkOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkPSNAccountRequest) && PlayFabEvents._instance.OnUnlinkPSNAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkPSNAccountRequestEvent((UnlinkPSNAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkSteamAccountRequest) && PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent((UnlinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkTwitchAccountRequest) && PlayFabEvents._instance.OnUnlinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchRequestEvent((UnlinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent((UnlinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkXboxAccountRequest) && PlayFabEvents._instance.OnUnlinkXboxAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkXboxAccountRequestEvent((UnlinkXboxAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlockContainerInstanceRequest) && PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent((UnlockContainerInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(UnlockContainerItemRequest) && PlayFabEvents._instance.OnUnlockContainerItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemRequestEvent((UnlockContainerItemRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateAvatarUrlRequest) && PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent((UpdateAvatarUrlRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCharacterDataRequest) && PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent((UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCharacterStatisticsRequest) && PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent((UpdateCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdatePlayerStatisticsRequest) && PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent((UpdatePlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateSharedGroupDataRequest) && PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent((UpdateSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataRequestEvent((UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent((UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserTitleDisplayNameRequest) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent((UpdateUserTitleDisplayNameRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateAmazonReceiptRequest) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent((ValidateAmazonReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateGooglePlayPurchaseRequest) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent((ValidateGooglePlayPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateIOSReceiptRequest) && PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent((ValidateIOSReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateWindowsReceiptRequest) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent((ValidateWindowsReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientCharacterEventRequest) && PlayFabEvents._instance.OnWriteCharacterEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventRequestEvent((WriteClientCharacterEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientPlayerEventRequest) && PlayFabEvents._instance.OnWritePlayerEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventRequestEvent((WriteClientPlayerEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteTitleEventRequest) && PlayFabEvents._instance.OnWriteTitleEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventRequestEvent((WriteTitleEventRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityTokenRequest) && PlayFabEvents._instance.OnAuthenticationGetEntityTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationGetEntityTokenRequestEvent((GetEntityTokenRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteEntityCloudScriptRequest) && PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent((ExecuteEntityCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(AbortFileUploadsRequest) && PlayFabEvents._instance.OnDataAbortFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataAbortFileUploadsRequestEvent((AbortFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteFilesRequest) && PlayFabEvents._instance.OnDataDeleteFilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataDeleteFilesRequestEvent((DeleteFilesRequest)e.Request);
					return;
				}
				if (type == typeof(FinalizeFileUploadsRequest) && PlayFabEvents._instance.OnDataFinalizeFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataFinalizeFileUploadsRequestEvent((FinalizeFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(GetFilesRequest) && PlayFabEvents._instance.OnDataGetFilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataGetFilesRequestEvent((GetFilesRequest)e.Request);
					return;
				}
				if (type == typeof(GetObjectsRequest) && PlayFabEvents._instance.OnDataGetObjectsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataGetObjectsRequestEvent((GetObjectsRequest)e.Request);
					return;
				}
				if (type == typeof(InitiateFileUploadsRequest) && PlayFabEvents._instance.OnDataInitiateFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataInitiateFileUploadsRequestEvent((InitiateFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(SetObjectsRequest) && PlayFabEvents._instance.OnDataSetObjectsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataSetObjectsRequestEvent((SetObjectsRequest)e.Request);
					return;
				}
				if (type == typeof(WriteEventsRequest) && PlayFabEvents._instance.OnEventsWriteEventsRequestEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteEventsRequestEvent((WriteEventsRequest)e.Request);
					return;
				}
				if (type == typeof(AcceptGroupApplicationRequest) && PlayFabEvents._instance.OnGroupsAcceptGroupApplicationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupApplicationRequestEvent((AcceptGroupApplicationRequest)e.Request);
					return;
				}
				if (type == typeof(AcceptGroupInvitationRequest) && PlayFabEvents._instance.OnGroupsAcceptGroupInvitationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupInvitationRequestEvent((AcceptGroupInvitationRequest)e.Request);
					return;
				}
				if (type == typeof(AddMembersRequest) && PlayFabEvents._instance.OnGroupsAddMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAddMembersRequestEvent((AddMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ApplyToGroupRequest) && PlayFabEvents._instance.OnGroupsApplyToGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsApplyToGroupRequestEvent((ApplyToGroupRequest)e.Request);
					return;
				}
				if (type == typeof(BlockEntityRequest) && PlayFabEvents._instance.OnGroupsBlockEntityRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsBlockEntityRequestEvent((BlockEntityRequest)e.Request);
					return;
				}
				if (type == typeof(ChangeMemberRoleRequest) && PlayFabEvents._instance.OnGroupsChangeMemberRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsChangeMemberRoleRequestEvent((ChangeMemberRoleRequest)e.Request);
					return;
				}
				if (type == typeof(CreateGroupRequest) && PlayFabEvents._instance.OnGroupsCreateGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateGroupRequestEvent((CreateGroupRequest)e.Request);
					return;
				}
				if (type == typeof(CreateGroupRoleRequest) && PlayFabEvents._instance.OnGroupsCreateRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateRoleRequestEvent((CreateGroupRoleRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteGroupRequest) && PlayFabEvents._instance.OnGroupsDeleteGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteGroupRequestEvent((DeleteGroupRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteRoleRequest) && PlayFabEvents._instance.OnGroupsDeleteRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteRoleRequestEvent((DeleteRoleRequest)e.Request);
					return;
				}
				if (type == typeof(GetGroupRequest) && PlayFabEvents._instance.OnGroupsGetGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsGetGroupRequestEvent((GetGroupRequest)e.Request);
					return;
				}
				if (type == typeof(InviteToGroupRequest) && PlayFabEvents._instance.OnGroupsInviteToGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsInviteToGroupRequestEvent((InviteToGroupRequest)e.Request);
					return;
				}
				if (type == typeof(IsMemberRequest) && PlayFabEvents._instance.OnGroupsIsMemberRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsIsMemberRequestEvent((IsMemberRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupApplicationsRequest) && PlayFabEvents._instance.OnGroupsListGroupApplicationsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupApplicationsRequestEvent((ListGroupApplicationsRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupBlocksRequest) && PlayFabEvents._instance.OnGroupsListGroupBlocksRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupBlocksRequestEvent((ListGroupBlocksRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupInvitationsRequest) && PlayFabEvents._instance.OnGroupsListGroupInvitationsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupInvitationsRequestEvent((ListGroupInvitationsRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupMembersRequest) && PlayFabEvents._instance.OnGroupsListGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupMembersRequestEvent((ListGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ListMembershipRequest) && PlayFabEvents._instance.OnGroupsListMembershipRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipRequestEvent((ListMembershipRequest)e.Request);
					return;
				}
				if (type == typeof(ListMembershipOpportunitiesRequest) && PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesRequestEvent((ListMembershipOpportunitiesRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGroupApplicationRequest) && PlayFabEvents._instance.OnGroupsRemoveGroupApplicationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupApplicationRequestEvent((RemoveGroupApplicationRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGroupInvitationRequest) && PlayFabEvents._instance.OnGroupsRemoveGroupInvitationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupInvitationRequestEvent((RemoveGroupInvitationRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveMembersRequest) && PlayFabEvents._instance.OnGroupsRemoveMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveMembersRequestEvent((RemoveMembersRequest)e.Request);
					return;
				}
				if (type == typeof(UnblockEntityRequest) && PlayFabEvents._instance.OnGroupsUnblockEntityRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUnblockEntityRequestEvent((UnblockEntityRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateGroupRequest) && PlayFabEvents._instance.OnGroupsUpdateGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateGroupRequestEvent((UpdateGroupRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateGroupRoleRequest) && PlayFabEvents._instance.OnGroupsUpdateRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateRoleRequestEvent((UpdateGroupRoleRequest)e.Request);
					return;
				}
				if (type == typeof(GetLanguageListRequest) && PlayFabEvents._instance.OnLocalizationGetLanguageListRequestEvent != null)
				{
					PlayFabEvents._instance.OnLocalizationGetLanguageListRequestEvent((GetLanguageListRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildWithCustomContainerRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent((CreateBuildWithCustomContainerRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildWithManagedContainerRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent((CreateBuildWithManagedContainerRequest)e.Request);
					return;
				}
				if (type == typeof(CreateRemoteUserRequest) && PlayFabEvents._instance.OnMultiplayerCreateRemoteUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateRemoteUserRequestEvent((CreateRemoteUserRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteAssetRequest) && PlayFabEvents._instance.OnMultiplayerDeleteAssetRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteAssetRequestEvent((DeleteAssetRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteBuildRequest) && PlayFabEvents._instance.OnMultiplayerDeleteBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildRequestEvent((DeleteBuildRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteCertificateRequest) && PlayFabEvents._instance.OnMultiplayerDeleteCertificateRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteCertificateRequestEvent((DeleteCertificateRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteRemoteUserRequest) && PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserRequestEvent((DeleteRemoteUserRequest)e.Request);
					return;
				}
				if (type == typeof(EnableMultiplayerServersForTitleRequest) && PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent((EnableMultiplayerServersForTitleRequest)e.Request);
					return;
				}
				if (type == typeof(GetAssetUploadUrlRequest) && PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlRequestEvent((GetAssetUploadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(GetBuildRequest) && PlayFabEvents._instance.OnMultiplayerGetBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildRequestEvent((GetBuildRequest)e.Request);
					return;
				}
				if (type == typeof(GetContainerRegistryCredentialsRequest) && PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent((GetContainerRegistryCredentialsRequest)e.Request);
					return;
				}
				if (type == typeof(GetMultiplayerServerDetailsRequest) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent((GetMultiplayerServerDetailsRequest)e.Request);
					return;
				}
				if (type == typeof(GetRemoteLoginEndpointRequest) && PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent((GetRemoteLoginEndpointRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleEnabledForMultiplayerServersStatusRequest) && PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent((GetTitleEnabledForMultiplayerServersStatusRequest)e.Request);
					return;
				}
				if (type == typeof(ListMultiplayerServersRequest) && PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListAssetSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListAssetSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListAssetSummariesRequestEvent((ListAssetSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(ListBuildSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListBuildSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildSummariesRequestEvent((ListBuildSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(ListCertificateSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListCertificateSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListCertificateSummariesRequestEvent((ListCertificateSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(ListContainerImagesRequest) && PlayFabEvents._instance.OnMultiplayerListContainerImagesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImagesRequestEvent((ListContainerImagesRequest)e.Request);
					return;
				}
				if (type == typeof(ListContainerImageTagsRequest) && PlayFabEvents._instance.OnMultiplayerListContainerImageTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImageTagsRequestEvent((ListContainerImageTagsRequest)e.Request);
					return;
				}
				if (type == typeof(ListMultiplayerServersRequest) && PlayFabEvents._instance.OnMultiplayerListMultiplayerServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListQosServersRequest) && PlayFabEvents._instance.OnMultiplayerListQosServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersRequestEvent((ListQosServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListVirtualMachineSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesRequestEvent((ListVirtualMachineSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(RequestMultiplayerServerRequest) && PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerRequestEvent((RequestMultiplayerServerRequest)e.Request);
					return;
				}
				if (type == typeof(RolloverContainerRegistryCredentialsRequest) && PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent((RolloverContainerRegistryCredentialsRequest)e.Request);
					return;
				}
				if (type == typeof(ShutdownMultiplayerServerRequest) && PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerRequestEvent((ShutdownMultiplayerServerRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateBuildRegionsRequest) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsRequestEvent((UpdateBuildRegionsRequest)e.Request);
					return;
				}
				if (type == typeof(UploadCertificateRequest) && PlayFabEvents._instance.OnMultiplayerUploadCertificateRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUploadCertificateRequestEvent((UploadCertificateRequest)e.Request);
					return;
				}
				if (type == typeof(GetGlobalPolicyRequest) && PlayFabEvents._instance.OnProfilesGetGlobalPolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetGlobalPolicyRequestEvent((GetGlobalPolicyRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityProfileRequest) && PlayFabEvents._instance.OnProfilesGetProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfileRequestEvent((GetEntityProfileRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityProfilesRequest) && PlayFabEvents._instance.OnProfilesGetProfilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfilesRequestEvent((GetEntityProfilesRequest)e.Request);
					return;
				}
				if (type == typeof(SetGlobalPolicyRequest) && PlayFabEvents._instance.OnProfilesSetGlobalPolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetGlobalPolicyRequestEvent((SetGlobalPolicyRequest)e.Request);
					return;
				}
				if (type == typeof(SetProfileLanguageRequest) && PlayFabEvents._instance.OnProfilesSetProfileLanguageRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfileLanguageRequestEvent((SetProfileLanguageRequest)e.Request);
					return;
				}
				if (type == typeof(SetEntityProfilePolicyRequest) && PlayFabEvents._instance.OnProfilesSetProfilePolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfilePolicyRequestEvent((SetEntityProfilePolicyRequest)e.Request);
					return;
				}
			}
			else
			{
				Type type2 = e.Result.GetType();
				if (type2 == typeof(LoginResult) && PlayFabEvents._instance.OnLoginResultEvent != null)
				{
					PlayFabEvents._instance.OnLoginResultEvent((LoginResult)e.Result);
					return;
				}
				if (type2 == typeof(AcceptTradeResponse) && PlayFabEvents._instance.OnAcceptTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeResultEvent((AcceptTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(AddFriendResult) && PlayFabEvents._instance.OnAddFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendResultEvent((AddFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(AddGenericIDResult) && PlayFabEvents._instance.OnAddGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDResultEvent((AddGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(AddOrUpdateContactEmailResult) && PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent((AddOrUpdateContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(AddSharedGroupMembersResult) && PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent((AddSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(AddUsernamePasswordResult) && PlayFabEvents._instance.OnAddUsernamePasswordResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordResultEvent((AddUsernamePasswordResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(AndroidDevicePushNotificationRegistrationResult) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent((AndroidDevicePushNotificationRegistrationResult)e.Result);
					return;
				}
				if (type2 == typeof(AttributeInstallResult) && PlayFabEvents._instance.OnAttributeInstallResultEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallResultEvent((AttributeInstallResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelTradeResponse) && PlayFabEvents._instance.OnCancelTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeResultEvent((CancelTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(ConfirmPurchaseResult) && PlayFabEvents._instance.OnConfirmPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseResultEvent((ConfirmPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumeItemResult) && PlayFabEvents._instance.OnConsumeItemResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemResultEvent((ConsumeItemResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumePSNEntitlementsResult) && PlayFabEvents._instance.OnConsumePSNEntitlementsResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumePSNEntitlementsResultEvent((ConsumePSNEntitlementsResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumeXboxEntitlementsResult) && PlayFabEvents._instance.OnConsumeXboxEntitlementsResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumeXboxEntitlementsResultEvent((ConsumeXboxEntitlementsResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateSharedGroupResult) && PlayFabEvents._instance.OnCreateSharedGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupResultEvent((CreateSharedGroupResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnExecuteCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptResultEvent((PlayFab.ClientModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(GetAccountInfoResult) && PlayFabEvents._instance.OnGetAccountInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoResultEvent((GetAccountInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(ListUsersCharactersResult) && PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent((ListUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCatalogItemsResult) && PlayFabEvents._instance.OnGetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsResultEvent((GetCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataResultEvent((GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterInventoryResult) && PlayFabEvents._instance.OnGetCharacterInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryResultEvent((GetCharacterInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterLeaderboardResult) && PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent((GetCharacterLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent((GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterStatisticsResult) && PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent((GetCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetContentDownloadUrlResult) && PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent((GetContentDownloadUrlResult)e.Result);
					return;
				}
				if (type2 == typeof(CurrentGamesResult) && PlayFabEvents._instance.OnGetCurrentGamesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesResultEvent((CurrentGamesResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardResult) && PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent((GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetFriendLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent((GetFriendLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(GetFriendsListResult) && PlayFabEvents._instance.OnGetFriendsListResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListResultEvent((GetFriendsListResult)e.Result);
					return;
				}
				if (type2 == typeof(GameServerRegionsResult) && PlayFabEvents._instance.OnGetGameServerRegionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsResultEvent((GameServerRegionsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardResult) && PlayFabEvents._instance.OnGetLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardResultEvent((GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundCharacterResult) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent((GetLeaderboardAroundCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent((GetLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardForUsersCharactersResult) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent((GetLeaderboardForUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPaymentTokenResult) && PlayFabEvents._instance.OnGetPaymentTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenResultEvent((GetPaymentTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPhotonAuthenticationTokenResult) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent((GetPhotonAuthenticationTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerCombinedInfoResult) && PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent((GetPlayerCombinedInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerProfileResult) && PlayFabEvents._instance.OnGetPlayerProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileResultEvent((GetPlayerProfileResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerSegmentsResult) && PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent((GetPlayerSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerStatisticsResult) && PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent((GetPlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerStatisticVersionsResult) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent((GetPlayerStatisticVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerTagsResult) && PlayFabEvents._instance.OnGetPlayerTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsResultEvent((GetPlayerTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerTradesResponse) && PlayFabEvents._instance.OnGetPlayerTradesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesResultEvent((GetPlayerTradesResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromFacebookIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent((GetPlayFabIDsFromFacebookIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent((GetPlayFabIDsFromFacebookInstantGamesIdsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGameCenterIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent((GetPlayFabIDsFromGameCenterIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGenericIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent((GetPlayFabIDsFromGenericIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGoogleIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent((GetPlayFabIDsFromGoogleIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromKongregateIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent((GetPlayFabIDsFromKongregateIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromPSNAccountIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent((GetPlayFabIDsFromPSNAccountIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromSteamIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent((GetPlayFabIDsFromSteamIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromTwitchIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent((GetPlayFabIDsFromTwitchIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromXboxLiveIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent((GetPlayFabIDsFromXboxLiveIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPublisherDataResult) && PlayFabEvents._instance.OnGetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataResultEvent((GetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPurchaseResult) && PlayFabEvents._instance.OnGetPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseResultEvent((GetPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(GetSharedGroupDataResult) && PlayFabEvents._instance.OnGetSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataResultEvent((GetSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetStoreItemsResult) && PlayFabEvents._instance.OnGetStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsResultEvent((GetStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTimeResult) && PlayFabEvents._instance.OnGetTimeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeResultEvent((GetTimeResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleDataResult) && PlayFabEvents._instance.OnGetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataResultEvent((GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleNewsResult) && PlayFabEvents._instance.OnGetTitleNewsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsResultEvent((GetTitleNewsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitlePublicKeyResult) && PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent((GetTitlePublicKeyResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTradeStatusResponse) && PlayFabEvents._instance.OnGetTradeStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusResultEvent((GetTradeStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserInventoryResult) && PlayFabEvents._instance.OnGetUserInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryResultEvent((GetUserInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetWindowsHelloChallengeResponse) && PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent((GetWindowsHelloChallengeResponse)e.Result);
					return;
				}
				if (type2 == typeof(GrantCharacterToUserResult) && PlayFabEvents._instance.OnGrantCharacterToUserResultEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserResultEvent((GrantCharacterToUserResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent((LinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkCustomIDResult) && PlayFabEvents._instance.OnLinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDResultEvent((LinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkFacebookAccountResult) && PlayFabEvents._instance.OnLinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountResultEvent((LinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkFacebookInstantGamesIdResult) && PlayFabEvents._instance.OnLinkFacebookInstantGamesIdResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookInstantGamesIdResultEvent((LinkFacebookInstantGamesIdResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGameCenterAccountResult) && PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent((LinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGoogleAccountResult) && PlayFabEvents._instance.OnLinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountResultEvent((LinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkIOSDeviceIDResult) && PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent((LinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkKongregateAccountResult) && PlayFabEvents._instance.OnLinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateResultEvent((LinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkNintendoSwitchDeviceIdResult) && PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdResultEvent((LinkNintendoSwitchDeviceIdResult)e.Result);
					return;
				}
				if (type2 == typeof(EmptyResult) && PlayFabEvents._instance.OnLinkOpenIdConnectResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkOpenIdConnectResultEvent((EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkPSNAccountResult) && PlayFabEvents._instance.OnLinkPSNAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkPSNAccountResultEvent((LinkPSNAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkSteamAccountResult) && PlayFabEvents._instance.OnLinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountResultEvent((LinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkTwitchAccountResult) && PlayFabEvents._instance.OnLinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchResultEvent((LinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnLinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloResultEvent((LinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(LinkXboxAccountResult) && PlayFabEvents._instance.OnLinkXboxAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkXboxAccountResultEvent((LinkXboxAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(MatchmakeResult) && PlayFabEvents._instance.OnMatchmakeResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeResultEvent((MatchmakeResult)e.Result);
					return;
				}
				if (type2 == typeof(OpenTradeResponse) && PlayFabEvents._instance.OnOpenTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeResultEvent((OpenTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(PayForPurchaseResult) && PlayFabEvents._instance.OnPayForPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseResultEvent((PayForPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PurchaseItemResult) && PlayFabEvents._instance.OnPurchaseItemResultEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemResultEvent((PurchaseItemResult)e.Result);
					return;
				}
				if (type2 == typeof(RedeemCouponResult) && PlayFabEvents._instance.OnRedeemCouponResultEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponResultEvent((RedeemCouponResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnRefreshPSNAuthTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnRefreshPSNAuthTokenResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(RegisterForIOSPushNotificationResult) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent((RegisterForIOSPushNotificationResult)e.Result);
					return;
				}
				if (type2 == typeof(RegisterPlayFabUserResult) && PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent((RegisterPlayFabUserResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveContactEmailResult) && PlayFabEvents._instance.OnRemoveContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailResultEvent((RemoveContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveFriendResult) && PlayFabEvents._instance.OnRemoveFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendResultEvent((RemoveFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveGenericIDResult) && PlayFabEvents._instance.OnRemoveGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDResultEvent((RemoveGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveSharedGroupMembersResult) && PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent((RemoveSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnReportDeviceInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(ReportPlayerClientResult) && PlayFabEvents._instance.OnReportPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerResultEvent((ReportPlayerClientResult)e.Result);
					return;
				}
				if (type2 == typeof(RestoreIOSPurchasesResult) && PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent((RestoreIOSPurchasesResult)e.Result);
					return;
				}
				if (type2 == typeof(SendAccountRecoveryEmailResult) && PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent((SendAccountRecoveryEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(SetFriendTagsResult) && PlayFabEvents._instance.OnSetFriendTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsResultEvent((SetFriendTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(SetPlayerSecretResult) && PlayFabEvents._instance.OnSetPlayerSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretResultEvent((SetPlayerSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(StartGameResult) && PlayFabEvents._instance.OnStartGameResultEvent != null)
				{
					PlayFabEvents._instance.OnStartGameResultEvent((StartGameResult)e.Result);
					return;
				}
				if (type2 == typeof(StartPurchaseResult) && PlayFabEvents._instance.OnStartPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseResultEvent((StartPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent((UnlinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkCustomIDResult) && PlayFabEvents._instance.OnUnlinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDResultEvent((UnlinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkFacebookAccountResult) && PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent((UnlinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkFacebookInstantGamesIdResult) && PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdResultEvent((UnlinkFacebookInstantGamesIdResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGameCenterAccountResult) && PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent((UnlinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGoogleAccountResult) && PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent((UnlinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkIOSDeviceIDResult) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent((UnlinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkKongregateAccountResult) && PlayFabEvents._instance.OnUnlinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateResultEvent((UnlinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkNintendoSwitchDeviceIdResult) && PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdResultEvent((UnlinkNintendoSwitchDeviceIdResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUnlinkOpenIdConnectResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkOpenIdConnectResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkPSNAccountResult) && PlayFabEvents._instance.OnUnlinkPSNAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkPSNAccountResultEvent((UnlinkPSNAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkSteamAccountResult) && PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent((UnlinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkTwitchAccountResult) && PlayFabEvents._instance.OnUnlinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchResultEvent((UnlinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent((UnlinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkXboxAccountResult) && PlayFabEvents._instance.OnUnlinkXboxAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkXboxAccountResultEvent((UnlinkXboxAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent((UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerItemResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemResultEvent((UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCharacterDataResult) && PlayFabEvents._instance.OnUpdateCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataResultEvent((UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCharacterStatisticsResult) && PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent((UpdateCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdatePlayerStatisticsResult) && PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent((UpdatePlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateSharedGroupDataResult) && PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent((UpdateSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataResultEvent((UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent((UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserTitleDisplayNameResult) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent((UpdateUserTitleDisplayNameResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateAmazonReceiptResult) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent((ValidateAmazonReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateGooglePlayPurchaseResult) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent((ValidateGooglePlayPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateIOSReceiptResult) && PlayFabEvents._instance.OnValidateIOSReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptResultEvent((ValidateIOSReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateWindowsReceiptResult) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent((ValidateWindowsReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWriteCharacterEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWritePlayerEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWriteTitleEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityTokenResponse) && PlayFabEvents._instance.OnAuthenticationGetEntityTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationGetEntityTokenResultEvent((GetEntityTokenResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptResultEvent((PlayFab.CloudScriptModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(AbortFileUploadsResponse) && PlayFabEvents._instance.OnDataAbortFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataAbortFileUploadsResultEvent((AbortFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(DeleteFilesResponse) && PlayFabEvents._instance.OnDataDeleteFilesResultEvent != null)
				{
					PlayFabEvents._instance.OnDataDeleteFilesResultEvent((DeleteFilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(FinalizeFileUploadsResponse) && PlayFabEvents._instance.OnDataFinalizeFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataFinalizeFileUploadsResultEvent((FinalizeFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetFilesResponse) && PlayFabEvents._instance.OnDataGetFilesResultEvent != null)
				{
					PlayFabEvents._instance.OnDataGetFilesResultEvent((GetFilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetObjectsResponse) && PlayFabEvents._instance.OnDataGetObjectsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataGetObjectsResultEvent((GetObjectsResponse)e.Result);
					return;
				}
				if (type2 == typeof(InitiateFileUploadsResponse) && PlayFabEvents._instance.OnDataInitiateFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataInitiateFileUploadsResultEvent((InitiateFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetObjectsResponse) && PlayFabEvents._instance.OnDataSetObjectsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataSetObjectsResultEvent((SetObjectsResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventsResponse) && PlayFabEvents._instance.OnEventsWriteEventsResultEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteEventsResultEvent((WriteEventsResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAcceptGroupApplicationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAcceptGroupInvitationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAddMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAddMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(ApplyToGroupResponse) && PlayFabEvents._instance.OnGroupsApplyToGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsApplyToGroupResultEvent((ApplyToGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsBlockEntityResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsBlockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsChangeMemberRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsChangeMemberRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateGroupResponse) && PlayFabEvents._instance.OnGroupsCreateGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateGroupResultEvent((CreateGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateGroupRoleResponse) && PlayFabEvents._instance.OnGroupsCreateRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateRoleResultEvent((CreateGroupRoleResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsDeleteGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteGroupResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsDeleteRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetGroupResponse) && PlayFabEvents._instance.OnGroupsGetGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsGetGroupResultEvent((GetGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(InviteToGroupResponse) && PlayFabEvents._instance.OnGroupsInviteToGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsInviteToGroupResultEvent((InviteToGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(IsMemberResponse) && PlayFabEvents._instance.OnGroupsIsMemberResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsIsMemberResultEvent((IsMemberResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupApplicationsResponse) && PlayFabEvents._instance.OnGroupsListGroupApplicationsResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupApplicationsResultEvent((ListGroupApplicationsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupBlocksResponse) && PlayFabEvents._instance.OnGroupsListGroupBlocksResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupBlocksResultEvent((ListGroupBlocksResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupInvitationsResponse) && PlayFabEvents._instance.OnGroupsListGroupInvitationsResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupInvitationsResultEvent((ListGroupInvitationsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupMembersResponse) && PlayFabEvents._instance.OnGroupsListGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupMembersResultEvent((ListGroupMembersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMembershipResponse) && PlayFabEvents._instance.OnGroupsListMembershipResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipResultEvent((ListMembershipResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMembershipOpportunitiesResponse) && PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesResultEvent((ListMembershipOpportunitiesResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveGroupApplicationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveGroupInvitationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsUnblockEntityResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUnblockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateGroupResponse) && PlayFabEvents._instance.OnGroupsUpdateGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateGroupResultEvent((UpdateGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateGroupRoleResponse) && PlayFabEvents._instance.OnGroupsUpdateRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateRoleResultEvent((UpdateGroupRoleResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetLanguageListResponse) && PlayFabEvents._instance.OnLocalizationGetLanguageListResultEvent != null)
				{
					PlayFabEvents._instance.OnLocalizationGetLanguageListResultEvent((GetLanguageListResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateBuildWithCustomContainerResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent((CreateBuildWithCustomContainerResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateBuildWithManagedContainerResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent((CreateBuildWithManagedContainerResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateRemoteUserResponse) && PlayFabEvents._instance.OnMultiplayerCreateRemoteUserResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateRemoteUserResultEvent((CreateRemoteUserResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteAssetResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteAssetResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteCertificateResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(EnableMultiplayerServersForTitleResponse) && PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent((EnableMultiplayerServersForTitleResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetAssetUploadUrlResponse) && PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlResultEvent((GetAssetUploadUrlResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetBuildResponse) && PlayFabEvents._instance.OnMultiplayerGetBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildResultEvent((GetBuildResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetContainerRegistryCredentialsResponse) && PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent((GetContainerRegistryCredentialsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetMultiplayerServerDetailsResponse) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent((GetMultiplayerServerDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetRemoteLoginEndpointResponse) && PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointResultEvent((GetRemoteLoginEndpointResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleEnabledForMultiplayerServersStatusResponse) && PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent((GetTitleEnabledForMultiplayerServersStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMultiplayerServersResponse) && PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListAssetSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListAssetSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListAssetSummariesResultEvent((ListAssetSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListBuildSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListBuildSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildSummariesResultEvent((ListBuildSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListCertificateSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListCertificateSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListCertificateSummariesResultEvent((ListCertificateSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListContainerImagesResponse) && PlayFabEvents._instance.OnMultiplayerListContainerImagesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImagesResultEvent((ListContainerImagesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListContainerImageTagsResponse) && PlayFabEvents._instance.OnMultiplayerListContainerImageTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImageTagsResultEvent((ListContainerImageTagsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMultiplayerServersResponse) && PlayFabEvents._instance.OnMultiplayerListMultiplayerServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListQosServersResponse) && PlayFabEvents._instance.OnMultiplayerListQosServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersResultEvent((ListQosServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListVirtualMachineSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesResultEvent((ListVirtualMachineSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(RequestMultiplayerServerResponse) && PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerResultEvent((RequestMultiplayerServerResponse)e.Result);
					return;
				}
				if (type2 == typeof(RolloverContainerRegistryCredentialsResponse) && PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent((RolloverContainerRegistryCredentialsResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUploadCertificateResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUploadCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetGlobalPolicyResponse) && PlayFabEvents._instance.OnProfilesGetGlobalPolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetGlobalPolicyResultEvent((GetGlobalPolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityProfileResponse) && PlayFabEvents._instance.OnProfilesGetProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfileResultEvent((GetEntityProfileResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityProfilesResponse) && PlayFabEvents._instance.OnProfilesGetProfilesResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfilesResultEvent((GetEntityProfilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetGlobalPolicyResponse) && PlayFabEvents._instance.OnProfilesSetGlobalPolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetGlobalPolicyResultEvent((SetGlobalPolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetProfileLanguageResponse) && PlayFabEvents._instance.OnProfilesSetProfileLanguageResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfileLanguageResultEvent((SetProfileLanguageResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetEntityProfilePolicyResponse) && PlayFabEvents._instance.OnProfilesSetProfilePolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfilePolicyResultEvent((SetEntityProfilePolicyResponse)e.Result);
					return;
				}
			}
		}

		private static PlayFabEvents _instance;

		public delegate void PlayFabErrorEvent(PlayFabRequestCommon request, PlayFabError error);

		public delegate void PlayFabResultEvent<in TResult>(TResult result) where TResult : PlayFabResultCommon;

		public delegate void PlayFabRequestEvent<in TRequest>(TRequest request) where TRequest : PlayFabRequestCommon;
	}
}
