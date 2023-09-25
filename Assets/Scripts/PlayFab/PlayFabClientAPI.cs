using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabClientAPI
	{
		public static bool IsClientLoggedIn()
		{
			return PlayFabHttp.IsClientLoggedIn();
		}

		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AcceptTrade(AcceptTradeRequest request, Action<AcceptTradeResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AcceptTradeResponse>("/Client/AcceptTrade", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddFriend(AddFriendRequest request, Action<AddFriendResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AddFriendResult>("/Client/AddFriend", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddGenericID(AddGenericIDRequest request, Action<AddGenericIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AddGenericIDResult>("/Client/AddGenericID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddOrUpdateContactEmail(AddOrUpdateContactEmailRequest request, Action<AddOrUpdateContactEmailResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AddOrUpdateContactEmailResult>("/Client/AddOrUpdateContactEmail", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddSharedGroupMembers(AddSharedGroupMembersRequest request, Action<AddSharedGroupMembersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AddSharedGroupMembersResult>("/Client/AddSharedGroupMembers", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddUsernamePassword(AddUsernamePasswordRequest request, Action<AddUsernamePasswordResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AddUsernamePasswordResult>("/Client/AddUsernamePassword", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddUserVirtualCurrency(AddUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Client/AddUserVirtualCurrency", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AndroidDevicePushNotificationRegistration(AndroidDevicePushNotificationRegistrationRequest request, Action<AndroidDevicePushNotificationRegistrationResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AndroidDevicePushNotificationRegistrationResult>("/Client/AndroidDevicePushNotificationRegistration", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AttributeInstall(AttributeInstallRequest request, Action<AttributeInstallResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<AttributeInstallResult>("/Client/AttributeInstall", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CancelTrade(CancelTradeRequest request, Action<CancelTradeResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<CancelTradeResponse>("/Client/CancelTrade", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ConfirmPurchase(ConfirmPurchaseRequest request, Action<ConfirmPurchaseResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ConfirmPurchaseResult>("/Client/ConfirmPurchase", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ConsumeItem(ConsumeItemRequest request, Action<ConsumeItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ConsumeItemResult>("/Client/ConsumeItem", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ConsumePSNEntitlements(ConsumePSNEntitlementsRequest request, Action<ConsumePSNEntitlementsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ConsumePSNEntitlementsResult>("/Client/ConsumePSNEntitlements", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ConsumeXboxEntitlements(ConsumeXboxEntitlementsRequest request, Action<ConsumeXboxEntitlementsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ConsumeXboxEntitlementsResult>("/Client/ConsumeXboxEntitlements", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateSharedGroup(CreateSharedGroupRequest request, Action<CreateSharedGroupResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<CreateSharedGroupResult>("/Client/CreateSharedGroup", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ExecuteCloudScript(ExecuteCloudScriptRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ExecuteCloudScriptResult>("/Client/ExecuteCloudScript", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ExecuteCloudScript<TOut>(ExecuteCloudScriptRequest request, Action<ExecuteCloudScriptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			Action<ExecuteCloudScriptResult> resultCallback2 = delegate(ExecuteCloudScriptResult wrappedResult)
			{
				ISerializerPlugin plugin = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, string.Empty);
				string text = plugin.SerializeObject(wrappedResult.FunctionResult);
				try
				{
					wrappedResult.FunctionResult = plugin.DeserializeObject<TOut>(text);
				}
				catch (Exception)
				{
					wrappedResult.FunctionResult = text;
					wrappedResult.Logs.Add(new LogStatement
					{
						Level = "Warning",
						Data = text,
						Message = "Sdk Message: Could not deserialize result as: " + typeof(TOut).Name
					});
				}
				resultCallback(wrappedResult);
			};
			PlayFabClientAPI.ExecuteCloudScript(request, resultCallback2, errorCallback, customData, extraHeaders);
		}

		public static void GetAccountInfo(GetAccountInfoRequest request, Action<GetAccountInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetAccountInfoResult>("/Client/GetAccountInfo", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetAllUsersCharacters(ListUsersCharactersRequest request, Action<ListUsersCharactersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ListUsersCharactersResult>("/Client/GetAllUsersCharacters", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCatalogItems(GetCatalogItemsRequest request, Action<GetCatalogItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCatalogItemsResult>("/Client/GetCatalogItems", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterData(GetCharacterDataRequest request, Action<GetCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterDataResult>("/Client/GetCharacterData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterInventory(GetCharacterInventoryRequest request, Action<GetCharacterInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterInventoryResult>("/Client/GetCharacterInventory", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterLeaderboard(GetCharacterLeaderboardRequest request, Action<GetCharacterLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterLeaderboardResult>("/Client/GetCharacterLeaderboard", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterReadOnlyData(GetCharacterDataRequest request, Action<GetCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterDataResult>("/Client/GetCharacterReadOnlyData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCharacterStatistics(GetCharacterStatisticsRequest request, Action<GetCharacterStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetCharacterStatisticsResult>("/Client/GetCharacterStatistics", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetContentDownloadUrl(GetContentDownloadUrlRequest request, Action<GetContentDownloadUrlResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetContentDownloadUrlResult>("/Client/GetContentDownloadUrl", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetCurrentGames(CurrentGamesRequest request, Action<CurrentGamesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<CurrentGamesResult>("/Client/GetCurrentGames", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFriendLeaderboard(GetFriendLeaderboardRequest request, Action<GetLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardResult>("/Client/GetFriendLeaderboard", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFriendLeaderboardAroundPlayer(GetFriendLeaderboardAroundPlayerRequest request, Action<GetFriendLeaderboardAroundPlayerResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetFriendLeaderboardAroundPlayerResult>("/Client/GetFriendLeaderboardAroundPlayer", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetFriendsList(GetFriendsListRequest request, Action<GetFriendsListResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetFriendsListResult>("/Client/GetFriendsList", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetGameServerRegions(GameServerRegionsRequest request, Action<GameServerRegionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GameServerRegionsResult>("/Client/GetGameServerRegions", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboard(GetLeaderboardRequest request, Action<GetLeaderboardResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardResult>("/Client/GetLeaderboard", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardAroundCharacter(GetLeaderboardAroundCharacterRequest request, Action<GetLeaderboardAroundCharacterResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardAroundCharacterResult>("/Client/GetLeaderboardAroundCharacter", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardAroundPlayer(GetLeaderboardAroundPlayerRequest request, Action<GetLeaderboardAroundPlayerResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardAroundPlayerResult>("/Client/GetLeaderboardAroundPlayer", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetLeaderboardForUserCharacters(GetLeaderboardForUsersCharactersRequest request, Action<GetLeaderboardForUsersCharactersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetLeaderboardForUsersCharactersResult>("/Client/GetLeaderboardForUserCharacters", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPaymentToken(GetPaymentTokenRequest request, Action<GetPaymentTokenResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPaymentTokenResult>("/Client/GetPaymentToken", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPhotonAuthenticationToken(GetPhotonAuthenticationTokenRequest request, Action<GetPhotonAuthenticationTokenResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPhotonAuthenticationTokenResult>("/Client/GetPhotonAuthenticationToken", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerCombinedInfo(GetPlayerCombinedInfoRequest request, Action<GetPlayerCombinedInfoResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerCombinedInfoResult>("/Client/GetPlayerCombinedInfo", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerProfile(GetPlayerProfileRequest request, Action<GetPlayerProfileResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerProfileResult>("/Client/GetPlayerProfile", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerSegments(GetPlayerSegmentsRequest request, Action<GetPlayerSegmentsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerSegmentsResult>("/Client/GetPlayerSegments", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatistics(GetPlayerStatisticsRequest request, Action<GetPlayerStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticsResult>("/Client/GetPlayerStatistics", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerStatisticVersions(GetPlayerStatisticVersionsRequest request, Action<GetPlayerStatisticVersionsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerStatisticVersionsResult>("/Client/GetPlayerStatisticVersions", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerTags(GetPlayerTagsRequest request, Action<GetPlayerTagsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerTagsResult>("/Client/GetPlayerTags", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayerTrades(GetPlayerTradesRequest request, Action<GetPlayerTradesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayerTradesResponse>("/Client/GetPlayerTrades", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromFacebookIDs(GetPlayFabIDsFromFacebookIDsRequest request, Action<GetPlayFabIDsFromFacebookIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromFacebookIDsResult>("/Client/GetPlayFabIDsFromFacebookIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromFacebookInstantGamesIds(GetPlayFabIDsFromFacebookInstantGamesIdsRequest request, Action<GetPlayFabIDsFromFacebookInstantGamesIdsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromFacebookInstantGamesIdsResult>("/Client/GetPlayFabIDsFromFacebookInstantGamesIds", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromGameCenterIDs(GetPlayFabIDsFromGameCenterIDsRequest request, Action<GetPlayFabIDsFromGameCenterIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromGameCenterIDsResult>("/Client/GetPlayFabIDsFromGameCenterIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromGenericIDs(GetPlayFabIDsFromGenericIDsRequest request, Action<GetPlayFabIDsFromGenericIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromGenericIDsResult>("/Client/GetPlayFabIDsFromGenericIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromGoogleIDs(GetPlayFabIDsFromGoogleIDsRequest request, Action<GetPlayFabIDsFromGoogleIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromGoogleIDsResult>("/Client/GetPlayFabIDsFromGoogleIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromKongregateIDs(GetPlayFabIDsFromKongregateIDsRequest request, Action<GetPlayFabIDsFromKongregateIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromKongregateIDsResult>("/Client/GetPlayFabIDsFromKongregateIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromNintendoSwitchDeviceIds(GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest request, Action<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>("/Client/GetPlayFabIDsFromNintendoSwitchDeviceIds", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromPSNAccountIDs(GetPlayFabIDsFromPSNAccountIDsRequest request, Action<GetPlayFabIDsFromPSNAccountIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromPSNAccountIDsResult>("/Client/GetPlayFabIDsFromPSNAccountIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromSteamIDs(GetPlayFabIDsFromSteamIDsRequest request, Action<GetPlayFabIDsFromSteamIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromSteamIDsResult>("/Client/GetPlayFabIDsFromSteamIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromTwitchIDs(GetPlayFabIDsFromTwitchIDsRequest request, Action<GetPlayFabIDsFromTwitchIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromTwitchIDsResult>("/Client/GetPlayFabIDsFromTwitchIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPlayFabIDsFromXboxLiveIDs(GetPlayFabIDsFromXboxLiveIDsRequest request, Action<GetPlayFabIDsFromXboxLiveIDsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPlayFabIDsFromXboxLiveIDsResult>("/Client/GetPlayFabIDsFromXboxLiveIDs", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPublisherData(GetPublisherDataRequest request, Action<GetPublisherDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPublisherDataResult>("/Client/GetPublisherData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetPurchase(GetPurchaseRequest request, Action<GetPurchaseResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetPurchaseResult>("/Client/GetPurchase", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetSharedGroupData(GetSharedGroupDataRequest request, Action<GetSharedGroupDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetSharedGroupDataResult>("/Client/GetSharedGroupData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetStoreItems(GetStoreItemsRequest request, Action<GetStoreItemsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetStoreItemsResult>("/Client/GetStoreItems", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTime(GetTimeRequest request, Action<GetTimeResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTimeResult>("/Client/GetTime", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleData(GetTitleDataRequest request, Action<GetTitleDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleDataResult>("/Client/GetTitleData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitleNews(GetTitleNewsRequest request, Action<GetTitleNewsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTitleNewsResult>("/Client/GetTitleNews", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTitlePublicKey(GetTitlePublicKeyRequest request, Action<GetTitlePublicKeyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetTitlePublicKeyResult>("/Client/GetTitlePublicKey", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetTradeStatus(GetTradeStatusRequest request, Action<GetTradeStatusResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetTradeStatusResponse>("/Client/GetTradeStatus", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Client/GetUserData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserInventory(GetUserInventoryRequest request, Action<GetUserInventoryResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserInventoryResult>("/Client/GetUserInventory", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Client/GetUserPublisherData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserPublisherReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Client/GetUserPublisherReadOnlyData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetUserReadOnlyData(GetUserDataRequest request, Action<GetUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GetUserDataResult>("/Client/GetUserReadOnlyData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetWindowsHelloChallenge(GetWindowsHelloChallengeRequest request, Action<GetWindowsHelloChallengeResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetWindowsHelloChallengeResponse>("/Client/GetWindowsHelloChallenge", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GrantCharacterToUser(GrantCharacterToUserRequest request, Action<GrantCharacterToUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<GrantCharacterToUserResult>("/Client/GrantCharacterToUser", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkAndroidDeviceID(LinkAndroidDeviceIDRequest request, Action<LinkAndroidDeviceIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkAndroidDeviceIDResult>("/Client/LinkAndroidDeviceID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkCustomID(LinkCustomIDRequest request, Action<LinkCustomIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkCustomIDResult>("/Client/LinkCustomID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkFacebookAccount(LinkFacebookAccountRequest request, Action<LinkFacebookAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkFacebookAccountResult>("/Client/LinkFacebookAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkFacebookInstantGamesId(LinkFacebookInstantGamesIdRequest request, Action<LinkFacebookInstantGamesIdResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkFacebookInstantGamesIdResult>("/Client/LinkFacebookInstantGamesId", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkGameCenterAccount(LinkGameCenterAccountRequest request, Action<LinkGameCenterAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkGameCenterAccountResult>("/Client/LinkGameCenterAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkGoogleAccount(LinkGoogleAccountRequest request, Action<LinkGoogleAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkGoogleAccountResult>("/Client/LinkGoogleAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkIOSDeviceID(LinkIOSDeviceIDRequest request, Action<LinkIOSDeviceIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkIOSDeviceIDResult>("/Client/LinkIOSDeviceID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkKongregate(LinkKongregateAccountRequest request, Action<LinkKongregateAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkKongregateAccountResult>("/Client/LinkKongregate", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkNintendoSwitchDeviceId(LinkNintendoSwitchDeviceIdRequest request, Action<LinkNintendoSwitchDeviceIdResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkNintendoSwitchDeviceIdResult>("/Client/LinkNintendoSwitchDeviceId", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkOpenIdConnect(LinkOpenIdConnectRequest request, Action<EmptyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResult>("/Client/LinkOpenIdConnect", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkPSNAccount(LinkPSNAccountRequest request, Action<LinkPSNAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkPSNAccountResult>("/Client/LinkPSNAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkSteamAccount(LinkSteamAccountRequest request, Action<LinkSteamAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkSteamAccountResult>("/Client/LinkSteamAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkTwitch(LinkTwitchAccountRequest request, Action<LinkTwitchAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkTwitchAccountResult>("/Client/LinkTwitch", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkWindowsHello(LinkWindowsHelloAccountRequest request, Action<LinkWindowsHelloAccountResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkWindowsHelloAccountResponse>("/Client/LinkWindowsHello", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LinkXboxAccount(LinkXboxAccountRequest request, Action<LinkXboxAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<LinkXboxAccountResult>("/Client/LinkXboxAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithAndroidDeviceID(LoginWithAndroidDeviceIDRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithAndroidDeviceID", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithCustomID(LoginWithCustomIDRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithCustomID", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithEmailAddress(LoginWithEmailAddressRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithEmailAddress", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithFacebook(LoginWithFacebookRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithFacebook", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithFacebookInstantGamesId(LoginWithFacebookInstantGamesIdRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithFacebookInstantGamesId", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithGameCenter(LoginWithGameCenterRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithGameCenter", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithGoogleAccount(LoginWithGoogleAccountRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithGoogleAccount", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithIOSDeviceID(LoginWithIOSDeviceIDRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithIOSDeviceID", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithKongregate(LoginWithKongregateRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithKongregate", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithNintendoSwitchDeviceId(LoginWithNintendoSwitchDeviceIdRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithNintendoSwitchDeviceId", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithOpenIdConnect(LoginWithOpenIdConnectRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithOpenIdConnect", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithPlayFab(LoginWithPlayFabRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithPlayFab", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithPSN(LoginWithPSNRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithPSN", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithSteam(LoginWithSteamRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithSteam", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithTwitch(LoginWithTwitchRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithTwitch", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithWindowsHello(LoginWithWindowsHelloRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithWindowsHello", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void LoginWithXbox(LoginWithXboxRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/LoginWithXbox", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void Matchmake(MatchmakeRequest request, Action<MatchmakeResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<MatchmakeResult>("/Client/Matchmake", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void OpenTrade(OpenTradeRequest request, Action<OpenTradeResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<OpenTradeResponse>("/Client/OpenTrade", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void PayForPurchase(PayForPurchaseRequest request, Action<PayForPurchaseResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<PayForPurchaseResult>("/Client/PayForPurchase", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void PurchaseItem(PurchaseItemRequest request, Action<PurchaseItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<PurchaseItemResult>("/Client/PurchaseItem", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RedeemCoupon(RedeemCouponRequest request, Action<RedeemCouponResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RedeemCouponResult>("/Client/RedeemCoupon", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RefreshPSNAuthToken(RefreshPSNAuthTokenRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Client/RefreshPSNAuthToken", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RegisterForIOSPushNotification(RegisterForIOSPushNotificationRequest request, Action<RegisterForIOSPushNotificationResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RegisterForIOSPushNotificationResult>("/Client/RegisterForIOSPushNotification", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RegisterPlayFabUser(RegisterPlayFabUserRequest request, Action<RegisterPlayFabUserResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<RegisterPlayFabUserResult>("/Client/RegisterPlayFabUser", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RegisterWithWindowsHello(RegisterWithWindowsHelloRequest request, Action<LoginResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			request.TitleId = (request.TitleId ?? PlayFabSettings.TitleId);
			PlayFabHttp.MakeApiCall<LoginResult>("/Client/RegisterWithWindowsHello", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveContactEmail(RemoveContactEmailRequest request, Action<RemoveContactEmailResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveContactEmailResult>("/Client/RemoveContactEmail", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveFriend(RemoveFriendRequest request, Action<RemoveFriendResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveFriendResult>("/Client/RemoveFriend", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveGenericID(RemoveGenericIDRequest request, Action<RemoveGenericIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveGenericIDResult>("/Client/RemoveGenericID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveSharedGroupMembers(RemoveSharedGroupMembersRequest request, Action<RemoveSharedGroupMembersResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RemoveSharedGroupMembersResult>("/Client/RemoveSharedGroupMembers", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ReportDeviceInfo(DeviceInfoRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Client/ReportDeviceInfo", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ReportPlayer(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ReportPlayerClientResult>("/Client/ReportPlayer", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RestoreIOSPurchases(RestoreIOSPurchasesRequest request, Action<RestoreIOSPurchasesResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<RestoreIOSPurchasesResult>("/Client/RestoreIOSPurchases", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SendAccountRecoveryEmail(SendAccountRecoveryEmailRequest request, Action<SendAccountRecoveryEmailResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<SendAccountRecoveryEmailResult>("/Client/SendAccountRecoveryEmail", request, AuthType.None, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetFriendTags(SetFriendTagsRequest request, Action<SetFriendTagsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<SetFriendTagsResult>("/Client/SetFriendTags", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SetPlayerSecret(SetPlayerSecretRequest request, Action<SetPlayerSecretResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<SetPlayerSecretResult>("/Client/SetPlayerSecret", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void StartGame(StartGameRequest request, Action<StartGameResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<StartGameResult>("/Client/StartGame", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void StartPurchase(StartPurchaseRequest request, Action<StartPurchaseResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<StartPurchaseResult>("/Client/StartPurchase", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void SubtractUserVirtualCurrency(SubtractUserVirtualCurrencyRequest request, Action<ModifyUserVirtualCurrencyResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ModifyUserVirtualCurrencyResult>("/Client/SubtractUserVirtualCurrency", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkAndroidDeviceID(UnlinkAndroidDeviceIDRequest request, Action<UnlinkAndroidDeviceIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkAndroidDeviceIDResult>("/Client/UnlinkAndroidDeviceID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkCustomID(UnlinkCustomIDRequest request, Action<UnlinkCustomIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkCustomIDResult>("/Client/UnlinkCustomID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkFacebookAccount(UnlinkFacebookAccountRequest request, Action<UnlinkFacebookAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkFacebookAccountResult>("/Client/UnlinkFacebookAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkFacebookInstantGamesId(UnlinkFacebookInstantGamesIdRequest request, Action<UnlinkFacebookInstantGamesIdResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkFacebookInstantGamesIdResult>("/Client/UnlinkFacebookInstantGamesId", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkGameCenterAccount(UnlinkGameCenterAccountRequest request, Action<UnlinkGameCenterAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkGameCenterAccountResult>("/Client/UnlinkGameCenterAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkGoogleAccount(UnlinkGoogleAccountRequest request, Action<UnlinkGoogleAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkGoogleAccountResult>("/Client/UnlinkGoogleAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkIOSDeviceID(UnlinkIOSDeviceIDRequest request, Action<UnlinkIOSDeviceIDResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkIOSDeviceIDResult>("/Client/UnlinkIOSDeviceID", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkKongregate(UnlinkKongregateAccountRequest request, Action<UnlinkKongregateAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkKongregateAccountResult>("/Client/UnlinkKongregate", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkNintendoSwitchDeviceId(UnlinkNintendoSwitchDeviceIdRequest request, Action<UnlinkNintendoSwitchDeviceIdResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkNintendoSwitchDeviceIdResult>("/Client/UnlinkNintendoSwitchDeviceId", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkOpenIdConnect(UninkOpenIdConnectRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Client/UnlinkOpenIdConnect", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkPSNAccount(UnlinkPSNAccountRequest request, Action<UnlinkPSNAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkPSNAccountResult>("/Client/UnlinkPSNAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkSteamAccount(UnlinkSteamAccountRequest request, Action<UnlinkSteamAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkSteamAccountResult>("/Client/UnlinkSteamAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkTwitch(UnlinkTwitchAccountRequest request, Action<UnlinkTwitchAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkTwitchAccountResult>("/Client/UnlinkTwitch", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkWindowsHello(UnlinkWindowsHelloAccountRequest request, Action<UnlinkWindowsHelloAccountResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkWindowsHelloAccountResponse>("/Client/UnlinkWindowsHello", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlinkXboxAccount(UnlinkXboxAccountRequest request, Action<UnlinkXboxAccountResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlinkXboxAccountResult>("/Client/UnlinkXboxAccount", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlockContainerInstance(UnlockContainerInstanceRequest request, Action<UnlockContainerItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlockContainerItemResult>("/Client/UnlockContainerInstance", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnlockContainerItem(UnlockContainerItemRequest request, Action<UnlockContainerItemResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UnlockContainerItemResult>("/Client/UnlockContainerItem", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateAvatarUrl(UpdateAvatarUrlRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Client/UpdateAvatarUrl", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterData(UpdateCharacterDataRequest request, Action<UpdateCharacterDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterDataResult>("/Client/UpdateCharacterData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateCharacterStatistics(UpdateCharacterStatisticsRequest request, Action<UpdateCharacterStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateCharacterStatisticsResult>("/Client/UpdateCharacterStatistics", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdatePlayerStatistics(UpdatePlayerStatisticsRequest request, Action<UpdatePlayerStatisticsResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdatePlayerStatisticsResult>("/Client/UpdatePlayerStatistics", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateSharedGroupData(UpdateSharedGroupDataRequest request, Action<UpdateSharedGroupDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateSharedGroupDataResult>("/Client/UpdateSharedGroupData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Client/UpdateUserData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserPublisherData(UpdateUserDataRequest request, Action<UpdateUserDataResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserDataResult>("/Client/UpdateUserPublisherData", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateUserTitleDisplayName(UpdateUserTitleDisplayNameRequest request, Action<UpdateUserTitleDisplayNameResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<UpdateUserTitleDisplayNameResult>("/Client/UpdateUserTitleDisplayName", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ValidateAmazonIAPReceipt(ValidateAmazonReceiptRequest request, Action<ValidateAmazonReceiptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ValidateAmazonReceiptResult>("/Client/ValidateAmazonIAPReceipt", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ValidateGooglePlayPurchase(ValidateGooglePlayPurchaseRequest request, Action<ValidateGooglePlayPurchaseResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ValidateGooglePlayPurchaseResult>("/Client/ValidateGooglePlayPurchase", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ValidateIOSReceipt(ValidateIOSReceiptRequest request, Action<ValidateIOSReceiptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ValidateIOSReceiptResult>("/Client/ValidateIOSReceipt", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ValidateWindowsStoreReceipt(ValidateWindowsReceiptRequest request, Action<ValidateWindowsReceiptResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<ValidateWindowsReceiptResult>("/Client/ValidateWindowsStoreReceipt", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WriteCharacterEvent(WriteClientCharacterEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Client/WriteCharacterEvent", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WritePlayerEvent(WriteClientPlayerEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Client/WritePlayerEvent", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void WriteTitleEvent(WriteTitleEventRequest request, Action<WriteEventResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			if (!PlayFabClientAPI.IsClientLoggedIn())
			{
				throw new PlayFabException(PlayFabExceptionCode.NotLoggedIn, "Must be logged in to call this method");
			}
			PlayFabHttp.MakeApiCall<WriteEventResponse>("/Client/WriteTitleEvent", request, AuthType.LoginSession, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
