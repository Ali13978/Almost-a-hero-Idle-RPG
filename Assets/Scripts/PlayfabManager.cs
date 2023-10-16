using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GooglePlayGames;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using SaveLoad;
using Simulation;
using Static;
using Ui;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;

public static class PlayfabManager
{
	static PlayfabManager()
	{
		PlayFabSettings.TitleId = "2BC3";
		PlayfabManager.titleData = new PlayfabManager.TitleData();
		PlayfabManager.titleData.minVersionAllowed = Cheats.version;
		PlayfabManager.titleData.latestVersion = Cheats.version;
		PlayfabManager.titleData.adDragonProbWeightAd = 1f;
		PlayfabManager.titleData.adDragonProbWeightDirect = 3f;
		PlayfabManager.titleData.adDragonProbWeightAdGold = 6f;
		PlayfabManager.titleData.adDragonProbWeightAdToken = 1f;
		PlayfabManager.titleData.adDragonProbWeightAdCredits = 1f;
		PlayfabManager.titleData.adDragonProbWeightAdScrap = 1f;
		PlayfabManager.titleData.adDragonProbWeightAdMyth = 0f;
		PlayfabManager.titleData.adDragonProbWeightDirectGold = 20f;
		PlayfabManager.titleData.adDragonProbWeightDirectToken = 1f;
		PlayfabManager.titleData.adDragonProbWeightDirectCredits = 1f;
		PlayfabManager.titleData.adDragonProbWeightDirectScrap = 1f;
		PlayfabManager.titleData.adDragonProbWeightDirectMyth = 0f;
		PlayfabManager.titleData.adDragonRewardAdGoldFactor = 4500.0;
		PlayfabManager.titleData.adDragonRewardAdToken = 25.0;
		PlayfabManager.titleData.adDragonRewardAdCredits = 10.0;
		PlayfabManager.titleData.adDragonRewardAdScrap = 40.0;
		PlayfabManager.titleData.adDragonRewardAdMyth = 0.0;
		PlayfabManager.titleData.adDragonRewardDirectGoldFactor = 450.0;
		PlayfabManager.titleData.adDragonRewardDirectToken = 6.0;
		PlayfabManager.titleData.adDragonRewardDirectCredits = 3.0;
		PlayfabManager.titleData.adDragonRewardDirectScrap = 12.0;
		PlayfabManager.titleData.adDragonRewardDirectMyth = 0.0;
		PlayfabManager.titleData.freeCreditsAmount = 5.0;
		PlayfabManager.titleData.goblinChestAppear = 0.03f;
		PlayfabManager.titleData.offlineEarningsFactor = 0.25;
		PlayfabManager.titleData.gearLevelUpgradeChances = new float[]
		{
			0.2f,
			0.15f,
			0.1f,
			0.05f,
			0.01f
		};
		PlayfabManager.titleData.freeLootpackPeriod = 14400f;
		PlayfabManager.titleData.christmasCandyCapAmount = 1000.0;
		PlayfabManager.titleData.christmasAdCandiesAmount = 100.0;
		PlayfabManager.titleData.christmasFreeCandiesAmount = 500.0;
	}

	public static bool HaveLoggedIn()
	{
		return PlayfabManager.loginState == PlayfabManager.LoginState.COMPLETED_SUCCESS_CUSTOM || PlayfabManager.loginState == PlayfabManager.LoginState.COMPLETED_SUCCESS_VIA_STORE;
	}

	public static bool IsWaitingForLoginServerResponse()
	{
		return PlayfabManager.loginState == PlayfabManager.LoginState.WAIT_SERVER_RESP;
	}

	public static float GetChanceAd()
	{
		float adDragonProbWeightAd = PlayfabManager.titleData.adDragonProbWeightAd;
		float num = PlayfabManager.titleData.adDragonProbWeightAd + PlayfabManager.titleData.adDragonProbWeightDirect;
		return adDragonProbWeightAd / num;
	}

	public static CurrencyType GetRewardTypeAd()
	{
		float max = PlayfabManager.titleData.adDragonProbWeightAdGold + PlayfabManager.titleData.adDragonProbWeightAdToken + PlayfabManager.titleData.adDragonProbWeightAdCredits + PlayfabManager.titleData.adDragonProbWeightAdScrap + PlayfabManager.titleData.adDragonProbWeightAdMyth + PlayfabManager.titleData.adDragonProbWeightAdAeon;
		float num = GameMath.GetRandomFloat(0f, max, GameMath.RandType.NoSeed);
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdGold)
		{
			return CurrencyType.GOLD;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdGold;
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdToken)
		{
			return CurrencyType.TOKEN;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdToken;
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdCredits)
		{
			return CurrencyType.GEM;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdCredits;
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdScrap)
		{
			return CurrencyType.SCRAP;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdScrap;
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdMyth)
		{
			return CurrencyType.MYTHSTONE;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdMyth;
		if (num <= PlayfabManager.titleData.adDragonProbWeightAdAeon)
		{
			return CurrencyType.AEON;
		}
		num -= PlayfabManager.titleData.adDragonProbWeightAdAeon;
		return CurrencyType.GOLD;
	}

	public static CurrencyType GetRewardTypeDirect(GameMode gameMode)
	{
		float num = PlayfabManager.titleData.adDragonProbWeightDirectToken + PlayfabManager.titleData.adDragonProbWeightDirectCredits + PlayfabManager.titleData.adDragonProbWeightDirectScrap + PlayfabManager.titleData.adDragonProbWeightDirectMyth + PlayfabManager.titleData.adDragonProbWeightDirectAeon;
		if (gameMode == GameMode.STANDARD)
		{
			num += PlayfabManager.titleData.adDragonProbWeightDirectGold;
		}
		float num2 = GameMath.GetRandomFloat(0f, num, GameMath.RandType.NoSeed);
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectToken)
		{
			return CurrencyType.TOKEN;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectToken;
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectCredits)
		{
			return CurrencyType.GEM;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectCredits;
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectScrap)
		{
			return CurrencyType.SCRAP;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectScrap;
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectMyth)
		{
			return CurrencyType.MYTHSTONE;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectMyth;
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectGold)
		{
			return CurrencyType.GOLD;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectGold;
		if (num2 <= PlayfabManager.titleData.adDragonProbWeightDirectAeon)
		{
			return CurrencyType.AEON;
		}
		num2 -= PlayfabManager.titleData.adDragonProbWeightDirectAeon;
		return CurrencyType.GOLD;
	}

	private static void GetGoogleServerAuthCode(Action<string> callback)
	{
		try
		{
			if (PlayfabManager.didEverGetAutCode)
			{
				PlayGamesPlatform.Instance.GetAnotherServerAuthCode(false, delegate (string code)
				{
					UnityEngine.Debug.Log("Auth code: " + code);
					callback(code);
				});
			}
			else
			{
				string serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
				PlayfabManager.didEverGetAutCode = true;
				callback(serverAuthCode);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error getting Google Server Auth: " + ex.Message);
			callback(null);
		}
	}

	private static IEnumerator PollForLoginResult(Action callback)
	{
		while (PlayfabManager.IsWaitingForLoginServerResponse())
		{
			yield return null;
		}
		callback();
		yield break;
	}

	public static void Login(Action callback, bool showPlayfabCustomLoginWarning = true)
	{
		try
		{
			if (PlayfabManager.IsWaitingForLoginServerResponse())
			{
				Main.coroutineObject.StartCoroutine(PlayfabManager.PollForLoginResult(callback));
			}
			else
			{
				PlayfabManager.loginState = PlayfabManager.LoginState.WAIT_SERVER_RESP;
				if (StoreManager.IsAuthed())
				{
					PlayfabManager.GetGoogleServerAuthCode(delegate (string authCode)
					{
						if (string.IsNullOrEmpty(authCode))
						{
							StoreManager.OnAuthFailed();
							StoreManager.Authenticate(false, delegate
							{
								if (StoreManager.IsAuthed())
								{
									PlayfabManager.GetGoogleServerAuthCode(delegate (string authCodeRetried)
									{
										UnityEngine.Debug.Log("Auth code: " + authCodeRetried);
										if (string.IsNullOrEmpty(authCodeRetried))
										{
											PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
										}
										else
										{
											PlayfabManager.LoginGoogle(authCodeRetried, callback, showPlayfabCustomLoginWarning);
										}
									});
								}
								else
								{
									PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
								}
							});
						}
						else
						{
							PlayfabManager.LoginGoogle(authCode, callback, showPlayfabCustomLoginWarning);
						}
					});
				}
				else
				{
					PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
				}
			}
		}
		catch (Exception)
		{
			PlayfabManager.loginState = PlayfabManager.LoginState.COMPLETED_FAIL;
			callback();
		}
		PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
    }

	

	public static void Update()
	{
		for (int i = 0; i < PlayfabManager.pendingPlayerEvents.Count; i++)
		{
			PlayfabManager.PendingPlayerEvent pendingPlayerEvent = PlayfabManager.pendingPlayerEvents[i];
			if (pendingPlayerEvent.successCondition())
			{
				PlayfabManager.SendPlayerEvent(pendingPlayerEvent.eventName, pendingPlayerEvent.body, null, null, true);
				PlayfabManager.pendingPlayerEvents.RemoveAt(i);
			}
			else if (pendingPlayerEvent.DidExpire())
			{
				PlayfabManager.pendingPlayerEvents.RemoveAt(i);
			}
		}
	}

	private static void OnGodPlayerProfile(GetPlayerProfileResult profile)
	{
		PlayfabManager.countryCode = ((profile.PlayerProfile.Locations.Count <= 0) ? null : profile.PlayerProfile.Locations[0].CountryCode);
		PlayerStats.playfabCreationDate = new long?(profile.PlayerProfile.Created.Value.Ticks);
	}

	private static void LoginCustom(Action callback, bool showPlayfabCustomLoginWarning)
	{
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
		{
			TitleId = "2BC3",
			CreateAccount = new bool?(true),
			CustomId = PlayfabManager.GetDeviceId(),
			InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
			{
				GetPlayerProfile = true
			}
		};
		PlayFabClientAPI.LoginWithCustomID(request, delegate(LoginResult res)
		{
			PlayfabManager.loginState = PlayfabManager.LoginState.COMPLETED_SUCCESS_CUSTOM;
			if (showPlayfabCustomLoginWarning && !Main.dontStoreAuthenticate)
			{
				UiManager.showPlayfabCustomLoginWarning = true;
			}
			GetPlayerProfileRequest request2 = new GetPlayerProfileRequest
			{
				PlayFabId = res.PlayFabId,
				ProfileConstraints = new PlayerProfileViewConstraints
				{
					ShowLocations = true,
					ShowCreated = true
				}
			};
			PlayFabClientAPI.GetPlayerProfile(request2, delegate(GetPlayerProfileResult profile)
			{
				PlayerStats.LoadPlayerLocations(profile.PlayerProfile.Locations);
				PlayfabManager.OnGodPlayerProfile(profile);
			}, delegate(PlayFabError error)
			{
			}, null, null);
			PlayerStats.LoadPayerStatus(res.InfoResultPayload.PlayerProfile);
			PlayfabManager.OnLoginSuccess(res, callback);
		}, delegate(PlayFabError err)
		{
			PlayfabManager.loginState = PlayfabManager.LoginState.COMPLETED_FAIL;
			callback();
		}, null, null);
	}

	private static void LoginGameCenter(Action callback, bool showPlayfabCustomLoginWarning)
	{
		UnityEngine.Debug.Log("Playfab: Logging in via GameCenter... " + Social.localUser.id);
		LoginWithGameCenterRequest request = new LoginWithGameCenterRequest
		{
			TitleId = "2BC3",
			CreateAccount = new bool?(true),
			PlayerId = GameCenterManager.Player.Id,
			InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
			{
				GetPlayerProfile = true
			}
		};
		PlayFabClientAPI.LoginWithGameCenter(request, delegate(LoginResult res)
		{
			UnityEngine.Debug.Log("Playfab: Login via game center is succesful.");
			PlayfabManager.loginState = PlayfabManager.LoginState.COMPLETED_SUCCESS_VIA_STORE;
			GetPlayerProfileRequest request2 = new GetPlayerProfileRequest
			{
				PlayFabId = res.PlayFabId,
				ProfileConstraints = new PlayerProfileViewConstraints
				{
					ShowLocations = true,
					ShowCreated = true
				}
			};
			PlayFabClientAPI.GetPlayerProfile(request2, delegate(GetPlayerProfileResult profile)
			{
				PlayerStats.LoadPlayerLocations(profile.PlayerProfile.Locations);
				PlayfabManager.OnGodPlayerProfile(profile);
			}, delegate(PlayFabError error)
			{
			}, null, null);
			PlayerStats.LoadPayerStatus(res.InfoResultPayload.PlayerProfile);
			PlayfabManager.OnLoginSuccess(res, callback);
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log("Playfab: Error loging in via gameCenter " + error.ErrorMessage);
			PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
		}, null, null);
	}

	private static void LoginGoogle(string authCode, Action callback, bool showPlayfabCustomLoginWarning)
	{
		UnityEngine.Debug.Log("Playfab: Logging in via Google: " + authCode);
		LoginWithGoogleAccountRequest request = new LoginWithGoogleAccountRequest
		{
			TitleId = "2BC3",
			CreateAccount = new bool?(true),
			ServerAuthCode = authCode,
			InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
			{
				GetPlayerProfile = true
			}
		};
		PlayFabClientAPI.LoginWithGoogleAccount(request, delegate(LoginResult res)
		{
			UnityEngine.Debug.Log("Playfab: Login via google is successful.");
			PlayfabManager.loginState = PlayfabManager.LoginState.COMPLETED_SUCCESS_VIA_STORE;
			GetPlayerProfileRequest request2 = new GetPlayerProfileRequest
			{
				PlayFabId = res.PlayFabId,
				ProfileConstraints = new PlayerProfileViewConstraints
				{
					ShowLocations = true,
					ShowCreated = true
				}
			};
			PlayFabClientAPI.GetPlayerProfile(request2, delegate(GetPlayerProfileResult profile)
			{
				PlayerStats.LoadPlayerLocations(profile.PlayerProfile.Locations);
				PlayfabManager.OnGodPlayerProfile(profile);
			}, delegate(PlayFabError error)
			{
			}, null, null);
			PlayerStats.LoadPayerStatus(res.InfoResultPayload.PlayerProfile);
			PlayfabManager.OnLoginSuccess(res, callback);
		}, delegate(PlayFabError error)
		{
			UnityEngine.Debug.Log("Playfab: Error logging in via google: " + error.ErrorMessage);
			PlayfabManager.LoginCustom(callback, showPlayfabCustomLoginWarning);
		}, null, null);
	}

	private static void OnLoginSuccess(LoginResult res, Action callback)
	{
		PlayfabManager.playerId = res.PlayFabId;
		if (PlayfabManager.allPlayfabIds == null)
		{
			PlayfabManager.allPlayfabIds = new List<string>();
		}
		if (!PlayfabManager.allPlayfabIds.Contains(PlayfabManager.playerId))
		{
			PlayfabManager.allPlayfabIds.Add(PlayfabManager.playerId);
		}
		PlayfabManager.checkForPlayfabData = true;
		callback();
		if (!PlayfabManager.conversionDataSent)
		{
			PlayfabManager.SendConversionData();
		}
		if (!PlayfabManager.instantAppStatSent)
		{
			if (PlayerStats.acquiredTrhoughInstantGame)
			{
				PlayfabManager.SendPlayerEvent(PlayfabEventId.ACQUIRED_THROUGH_INSTANT, new Dictionary<string, object>(), null, null, true);
			}
			PlayfabManager.instantAppStatSent = true;
		}
	}

	public static void PrepareForIap(Action<bool> callback)
	{
		try
		{
			bool flag = false;
			if (!flag && PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.AskPlayerData(delegate(bool isSuccess, SaveData t1, PlayfabManager.RewardData t2)
				{
					callback(isSuccess);
				});
			}
			else
			{
				UnityEngine.Debug.Log("Store manager is Authenticating. Forced authentication = " + flag);
				StoreManager.Authenticate(flag, delegate
				{
					PlayfabManager.Login(delegate
					{
						callback(PlayfabManager.HaveLoggedIn());
					}, true);
				});
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error while PrepareForIap: " + ex.Message);
			callback(false);
		}
	}

	public static void AskTitleNews(Action<List<PlayfabManager.NewsData>> callback)
	{
		try
		{
			PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest(), delegate(GetTitleNewsResult result)
			{
				List<PlayfabManager.NewsData> list = new List<PlayfabManager.NewsData>();
				foreach (TitleNewsItem titleNewsItem in result.News)
				{
					list.Add(new PlayfabManager.NewsData
					{
						dateTime = titleNewsItem.Timestamp,
						body = JsonConvert.DeserializeObject<PlayfabManager.NewsBody>(titleNewsItem.Body)
					});
				}
				callback(list);
			}, delegate(PlayFabError error)
			{
				UnityEngine.Debug.Log("Error while Asking title news: " + error.ErrorMessage);
			}, null, null);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error while Asking title news: " + ex.Message);
		}
	}

	public static void AskPlayerData(Action<bool, SaveData, PlayfabManager.RewardData> callback)
	{
		try
		{
			if (PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.AskPlayerDataAfterLogin(callback);
			}
			else
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						if (PlayfabManager.HaveLoggedIn())
						{
							PlayfabManager.AskPlayerDataAfterLogin(callback);
						}
						else
						{
							callback(false, null, null);
						}
					}, true);
				});
			}
		}
		catch (Exception)
		{
			callback(false, null, null);
		}
	}

	private static void AskPlayerDataAfterLogin(Action<bool, SaveData, PlayfabManager.RewardData> callback)
	{
		GetUserDataRequest req = new GetUserDataRequest
		{
			Keys = new List<string>
			{
				"save",
				"reward"
			}
		};
		PlayFabClientAPI.GetUserData(req, delegate(GetUserDataResult res)
		{
			PlayfabManager.OnGotPlayerData(res, callback);
		}, delegate(PlayFabError err)
		{
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					if (PlayfabManager.loginState == PlayfabManager.LoginState.COMPLETED_FAIL)
					{
						callback(false, null, null);
					}
					else
					{
						PlayFabClientAPI.GetUserData(req, delegate(GetUserDataResult res)
						{
							PlayfabManager.OnGotPlayerData(res, callback);
						}, delegate(PlayFabError err2)
						{
							callback(false, null, null);
						}, null, null);
					}
				}, true);
			});
		}, null, null);
	}

	private static void OnGotPlayerData(GetUserDataResult receivedData, Action<bool, SaveData, PlayfabManager.RewardData> callback)
	{
		Dictionary<string, UserDataRecord> data = receivedData.Data;
		SaveData saveData = null;
		if (data.ContainsKey("save"))
		{
			string value = data["save"].Value;
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					saveData = JsonConvert.DeserializeObject<SaveData>(value);
					saveData = SaveDataMigrationUtility.Migrate(saveData);
					PlayfabManager.haveReceivedAnySaveData = true;
				}
				catch (Exception ex)
				{
					saveData = null;
					SaveLoadManager.loadingSaveFailed = true;
					UnityEngine.Debug.Log("Playfab: Error while deserializing saveData: " + ex.Message);
					UnityEngine.Debug.Log(ex.StackTrace);
				}
			}
		}
		else
		{
			UnityEngine.Debug.Log("Playfab: There was no save data.");
			PlayfabManager.haveReceivedAnySaveData = true;
		}
		PlayfabManager.RewardData arg = null;
		if (data.ContainsKey("reward"))
		{
			string value2 = data["reward"].Value;
			if (!string.IsNullOrEmpty(value2))
			{
				UnityEngine.Debug.Log("Playfab: Got Player Data: Reward.");
				try
				{
					PlayfabManager.RewardData rewardData = JsonConvert.DeserializeObject<PlayfabManager.RewardData>(value2);
					arg = rewardData;
				}
				catch (Exception ex2)
				{
					arg = null;
					UnityEngine.Debug.Log("Playfab: Error while deserializing reward: " + ex2.Message);
				}
			}
		}
		callback(true, saveData, arg);
	}

	private static bool DoesErrorImplyNotLoggedIn(PlayFabErrorCode error)
	{
		return error == PlayFabErrorCode.InvalidSessionTicket || error == PlayFabErrorCode.NotAuthenticated;
	}

	public static void Save(string json, Action onSuccess)
	{
		PlayfabManager.Save(json, "save", onSuccess);
	}

	public static void SaveAsOld(string json)
	{
		PlayfabManager.Save(json, "old_save", null);
	}

	private static void Save(string json, string playfabDataId, Action onSuccess)
	{
		if (!PlayfabManager.haveReceivedAnySaveData)
		{
			UnityEngine.Debug.LogWarning("SAVE ERROR: We didn't receive any save data from server.");
			PlayfabManager.checkForPlayfabData = true;
			return;
		}
		if (PlayfabManager.haveReceivedSaveDataFromFutureVersion)
		{
			return;
		}
		if (PlayfabManager.waitingForSaveConflictReslove)
		{
			return;
		}
		UpdateUserDataRequest req = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{
					playfabDataId,
					json
				}
			}
		};
		if (PlayfabManager.HaveLoggedIn())
		{
			PlayfabManager.SaveAfterLogin(req, onSuccess);
		}
		else
		{
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					if (PlayfabManager.HaveLoggedIn())
					{
						PlayfabManager.SaveAfterLogin(req, onSuccess);
					}
					else
					{
						UnityEngine.Debug.Log("Couldn't save! Cannot login to Playfab.");
					}
				}, true);
			});
		}
	}

	private static void SaveAfterLogin(UpdateUserDataRequest req, Action onSuccess)
	{
		PlayFabClientAPI.UpdateUserData(req, delegate(UpdateUserDataResult res)
		{
			if (onSuccess != null)
			{
				onSuccess();
			}
		}, delegate(PlayFabError err)
		{
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					PlayFabClientAPI.UpdateUserData(req, delegate(UpdateUserDataResult r)
					{
						if (onSuccess != null)
						{
							onSuccess();
						}
					}, delegate(PlayFabError e)
					{
					}, null, null);
				}, true);
			});
		}, null, null);
	}

	public static void EraseReward(Action<bool> callback)
	{
		try
		{
			if (PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.EraseRewardAfterLogin(callback);
			}
			else
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						if (PlayfabManager.HaveLoggedIn())
						{
							PlayfabManager.EraseRewardAfterLogin(callback);
						}
						else
						{
							callback(false);
						}
					}, true);
				});
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error while EraseReward: " + ex.Message);
			callback(false);
		}
	}

	private static void EraseRewardAfterLogin(Action<bool> callback)
	{
		UnityEngine.Debug.Log("Playfab: Erasing reward data...");
		UpdateUserDataRequest req = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{
					"reward",
					string.Empty
				}
			}
		};
		PlayFabClientAPI.UpdateUserData(req, delegate(UpdateUserDataResult res)
		{
			UnityEngine.Debug.Log("Playfab: Erased reward successfully.");
			callback(true);
		}, delegate(PlayFabError err)
		{
			UnityEngine.Debug.Log("Playfab: Error while erasing reward: " + err.ErrorMessage);
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					PlayFabClientAPI.UpdateUserData(req, delegate(UpdateUserDataResult r)
					{
						UnityEngine.Debug.Log("Playfab: Erase Reward Success at Try#2");
						callback(true);
					}, delegate(PlayFabError e)
					{
						UnityEngine.Debug.Log("Playfab: Error during Erase Reward Try#2: " + e.ErrorMessage);
						callback(false);
					}, null, null);
				}, true);
			});
		}, null, null);
	}

	public static void AskTitleData()
	{
		if (PlayfabManager.HaveLoggedIn())
		{
			PlayfabManager.AskTitleDataAfterLogin();
		}
		else
		{
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					if (PlayfabManager.HaveLoggedIn())
					{
						PlayfabManager.AskTitleDataAfterLogin();
					}
				}, true);
			});
		}
	}

	private static void AskTitleDataAfterLogin()
	{
		GetTitleDataRequest req = new GetTitleDataRequest
		{
			Keys = new List<string>
			{
				"config_v2",
				"eDamageTakenFromHeroes",
				"areRegionalOffersEnabled",
				"halloweenOfferConfig",
				"christmasEventConfig",
				"eventsInfo",
				"patchnotes"
			}
		};
		PlayFabClientAPI.GetTitleData(req, delegate(GetTitleDataResult res)
		{
			PlayfabManager.OnReceivedTitleData(res);
		}, delegate(PlayFabError err)
		{
			UnityEngine.Debug.Log("Playfab: Error while getting titleData: " + err.ErrorMessage);
			StoreManager.Authenticate(false, delegate
			{
				PlayfabManager.Login(delegate
				{
					PlayFabClientAPI.GetTitleData(req, delegate(GetTitleDataResult res)
					{
						UnityEngine.Debug.Log("Playfab: Got titleData at try#2");
						PlayfabManager.OnReceivedTitleData(res);
					}, delegate(PlayFabError e)
					{
						UnityEngine.Debug.Log("Playfab: Error while getting titleData at try#2: " + err.ErrorMessage);
					}, null, null);
				}, true);
			});
		}, null, null);
	}

	private static void OnReceivedTitleData(GetTitleDataResult receivedData)
	{
		if (!receivedData.Data.ContainsKey("config_v2"))
		{
			return;
		}
		string value = receivedData.Data["config_v2"];
		double num = 0.0;
		if (receivedData.Data.ContainsKey("eDamageTakenFromHeroes"))
		{
			string value2 = receivedData.Data["eDamageTakenFromHeroes"];
			num = Convert.ToDouble(value2);
			PlayerPrefs.SetString("titleTempString", value2);
		}
		if (receivedData.Data.ContainsKey("areRegionalOffersEnabled"))
		{
			string a = receivedData.Data["areRegionalOffersEnabled"];
			if (a == "true")
			{
				PlayfabManager.isRegionalOffersEnabled = true;
			}
			else
			{
				PlayfabManager.isRegionalOffersEnabled = false;
			}
			PlayfabManager.isRegionalOfferStatusLoaded = true;
		}
		if (receivedData.Data.ContainsKey("halloweenOfferConfig"))
		{
			PlayfabManager.halloweenOfferConfig = JsonConvert.DeserializeObject<PlayfabManager.OfferConfig>(receivedData.Data["halloweenOfferConfig"]);
			PlayfabManager.halloweenOfferConfig.startDateParsed = DateTime.Parse(PlayfabManager.halloweenOfferConfig.startDate);
			PlayfabManager.halloweenOfferConfig.endDateParsed = DateTime.Parse(PlayfabManager.halloweenOfferConfig.endDate);
			PlayfabManager.halloweenOfferConfigLoaded = true;
		}
		if (receivedData.Data.ContainsKey("christmasEventConfig"))
		{
			PlayfabManager.christmasOfferConfig = JsonConvert.DeserializeObject<PlayfabManager.ChristmasEventConfig>(receivedData.Data["christmasEventConfig"]);
			PlayfabManager.christmasOfferConfig.offerConfig.startDateParsed = DateTime.Parse(PlayfabManager.christmasOfferConfig.offerConfig.startDate);
			PlayfabManager.christmasOfferConfig.offerConfig.endDateParsed = DateTime.Parse(PlayfabManager.christmasOfferConfig.offerConfig.endDate);
			PlayfabManager.christmasOfferConfig.candyDropLimitDateParsed = DateTime.Parse(PlayfabManager.christmasOfferConfig.candyDropLimitDate);
			PlayfabManager.christmasOfferConfigLoaded = true;
		}
		if (receivedData.Data.ContainsKey("eventsInfo"))
		{
			PlayfabManager.eventsInfo = JsonConvert.DeserializeObject<EventsInfo>(receivedData.Data["eventsInfo"]);
			PlayfabManager.eventsInfo.Init();
		}
		if (receivedData.Data.ContainsKey("patchnotes"))
		{
			string value3 = receivedData.Data["patchnotes"];
			PlayerPrefs.SetString("localpatchnotes", value3);
			PatchNote[] notes = JsonConvert.DeserializeObject<PatchNote[]>(value3);
			PatchNote.InitPatchNotes(notes);
		}
		if (string.IsNullOrEmpty(value))
		{
			return;
		}
		PlayfabManager.TitleData titleData = null;
		try
		{
			titleData = JsonConvert.DeserializeObject<PlayfabManager.TitleData>(value);
		}
		catch (Exception ex)
		{
			titleData = null;
			UnityEngine.Debug.Log("Playfab: Error deserializing titleData: " + ex.Message);
		}
		PlayfabManager.extraDamageTakenFromHeroes = num;
		if (titleData != null)
		{
			GameMath.VersionComparisonResult versionComparisonResult = GameMath.CompareVersions(Cheats.version, titleData.minVersionAllowed);
			if (versionComparisonResult == GameMath.VersionComparisonResult.OLD || versionComparisonResult == GameMath.VersionComparisonResult.NON_COMPARABLE)
			{
				UnityEngine.Debug.Log("Playfab: CLIENT NEEDS AN UPDATE!");
				PlayfabManager.forceUpdate = true;
			}
			else
			{
				PlayfabManager.titleData = titleData;
				if (string.IsNullOrEmpty(PlayfabManager.titleData.latestVersion))
				{
					PlayfabManager.titleData.latestVersion = Cheats.version;
				}
				else
				{
					GameMath.VersionComparisonResult versionComparisonResult2 = GameMath.CompareVersions(Cheats.version, titleData.latestVersion);
					if (versionComparisonResult2 == GameMath.VersionComparisonResult.OLD || versionComparisonResult2 == GameMath.VersionComparisonResult.NON_COMPARABLE)
					{
						PlayfabManager.optionalUpdate = true;
					}
				}
			}
			PlayerPrefs.SetString("titleDataString", value);
		}
	}

	public static void AddPendingPlayerEvent(string eventName, Dictionary<string, object> body, Func<bool> successCondition, float timeoutDuration = 10f)
	{
		PlayfabManager.pendingPlayerEvents.Add(new PlayfabManager.PendingPlayerEvent(eventName, body, successCondition, (double)timeoutDuration));
	}

	public static void SendPlayerEvent(string eventName, Dictionary<string, object> body, Action<WriteEventResponse> resultCallback = null, Action<PlayFabError> errorCallback = null, bool fillCommonEventData = true)
	{
		if (PlayfabManager.HaveLoggedIn())
		{
			PlayfabManager.SendPlayerEventAfterLogin(eventName, body, PlayfabManager.sim, resultCallback, errorCallback, fillCommonEventData);
		}
	}

	private static void SendPlayerEventAfterLogin(string eventName, Dictionary<string, object> body, Simulator sim, Action<WriteEventResponse> resultCallback = null, Action<PlayFabError> errorCallback = null, bool fillCommonEventData = true)
	{
		if (fillCommonEventData && sim != null)
		{
			PlayfabManager.FillCommonEventData(body, sim);
		}
		WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest
		{
			EventName = eventName,
			Body = body
		};
		PlayFabClientAPI.WritePlayerEvent(request, delegate(WriteEventResponse res)
		{
			if (resultCallback != null)
			{
				resultCallback(res);
			}
		}, delegate(PlayFabError err)
		{
			UnityEngine.Debug.Log("Playfab: Error while sending player event: " + err.ErrorMessage);
			if (errorCallback != null)
			{
				errorCallback(err);
			}
		}, null, null);
	}

	public static void SendPlayerValidatedPurchase(Product receivedProduct)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("product", receivedProduct.definition.storeSpecificId);
		PlayfabManager.SendPlayerEvent(PlayfabEventId.IAP_VALIDATED, dictionary, null, null, true);
	}

	public static void SendCustomPuchaseEvent(string storeName, Product product, bool isSuccess, string status)
	{
		if (product == null)
		{
			return;
		}
		string eventName = storeName + "_realmoney_purchase" + ((!isSuccess) ? "_fail" : "_success");
		WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest
		{
			EventName = eventName,
			Body = new Dictionary<string, object>
			{
				{
					"TransactionID",
					product.transactionID
				},
				{
					"ProductID",
					product.definition.id
				},
				{
					"Currency",
					product.metadata.isoCurrencyCode
				},
				{
					"Value",
					product.metadata.localizedPrice.ToString()
				},
				{
					"ValueLocalized",
					product.metadata.localizedPriceString
				},
				{
					"Status",
					status
				}
			}
		};
		PlayFabClientAPI.WritePlayerEvent(request, delegate(WriteEventResponse res)
		{
			UnityEngine.Debug.Log("Playfab: Sent player puchase event successfully.");
		}, delegate(PlayFabError err)
		{
			UnityEngine.Debug.Log("Playfab: Error while sending player puchase event: " + err.ErrorMessage);
		}, null, null);
	}

	public static void FillCommonEventData(Dictionary<string, object> body, Simulator sim)
	{
		body.Add("version", Cheats.version);
		body.Add("platform", Application.platform);
		body.Add("lifetime", PlayerStats.lifeTimeInTicks / 10000000L);
		body.Add("country", (PlayfabManager.countryCode == null) ? "NotAvailable" : PlayfabManager.countryCode.Value.ToString());
		World world = sim.GetWorld(GameMode.STANDARD);
		body.Add("max_stage", (world == null) ? 0 : world.GetMaxStageReached());
		body.Add("gems_current", sim.GetCredits().GetAmount());
		body.Add("gems_spent", PlayerStats.spentCredits);
		body.Add("scraps_current", sim.GetScraps().GetAmount());
		body.Add("scraps_spent", PlayerStats.spentScraps);
		body.Add("tokens_current", sim.GetTokens().GetAmount());
		body.Add("tokens_spent", PlayerStats.spentTokens);
	}

	

	public static IEnumerator AskValidationToBee(WWWForm post, Action<PlayfabManager.IapValidationResult> callback, Product product, string receipt, string transactionID)
	{
		int maxTryCount = 3;
		while (maxTryCount > 0)
		{
			maxTryCount--;
			using (UnityWebRequest www = UnityWebRequest.Post("https://beesquare.hk/scripts/xvalidate2.php", post))
			{
				www.timeout = 10;
				yield return www.SendWebRequest();
				if (www.isNetworkError)
				{
					callback(PlayfabManager.IapValidationResult.LoginFail);
					maxTryCount = 0;
					UnityEngine.Debug.Log(www.error);
				}
				else
				{
					string text = www.downloadHandler.text;
					var anonymousTypeObject = new
					{
						status = string.Empty
					};
					var AnonymousType = JsonConvert.DeserializeAnonymousType(text, anonymousTypeObject);
					if (AnonymousType != null && AnonymousType.status == "SUCCESS")
					{
						callback(PlayfabManager.IapValidationResult.Success);
						IapManager.RemoveXiaomiTransaction(transactionID);
						maxTryCount = 0;
					}
					else if (AnonymousType != null && AnonymousType.status == "UNCONFIRMED")
					{
						UnityEngine.Debug.Log("IAP Response: " + AnonymousType.status + "Trying again");
					}
					else if (AnonymousType != null && AnonymousType.status == "FAILED")
					{
						IapManager.RemoveXiaomiTransaction(transactionID);
					}
					else
					{
						UnityEngine.Debug.Log("IAP Response: " + AnonymousType.status);
						callback(PlayfabManager.IapValidationResult.InvalidReceipt);
						maxTryCount = 0;
					}
				}
			}
		}
		yield break;
	}


	public static void ValidateReceiptApple(Product product, string receiptData, string currencyCode, int purchasePrice, Action<PlayfabManager.IapValidationResult> callback)
	{
		try
		{
			if (PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.ValidateReceiptAppleAfterLogin(product, receiptData, currencyCode, purchasePrice, callback);
			}
			else
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						if (PlayfabManager.HaveLoggedIn())
						{
							PlayfabManager.ValidateReceiptAppleAfterLogin(product, receiptData, currencyCode, purchasePrice, callback);
						}
						else
						{
							callback(PlayfabManager.IapValidationResult.LoginFail);
						}
					}, true);
				});
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error while ValidateReceiptApple: " + ex.Message);
			callback(PlayfabManager.IapValidationResult.Exception);
		}
	}

	private static void ValidateReceiptAppleAfterLogin(Product product, string receiptData, string currencyCode, int purchasePrice, Action<PlayfabManager.IapValidationResult> callback)
	{
		ValidateIOSReceiptRequest req = new ValidateIOSReceiptRequest
		{
			ReceiptData = receiptData,
			CurrencyCode = currencyCode,
			PurchasePrice = purchasePrice
		};
		PlayFabClientAPI.ValidateIOSReceipt(req, delegate(ValidateIOSReceiptResult res)
		{
			callback(PlayfabManager.IapValidationResult.Success);
		}, delegate(PlayFabError err)
		{
			if (err.Error == PlayFabErrorCode.Success)
			{
				callback(PlayfabManager.IapValidationResult.Success);
			}
			else if (err.Error == PlayFabErrorCode.ReceiptAlreadyUsed)
			{
				callback(PlayfabManager.IapValidationResult.ReceiptAlreadyUsed);
			}
			else if (PlayfabManager.DoesErrorImplyNotLoggedIn(err.Error))
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						PlayFabClientAPI.ValidateIOSReceipt(req, delegate(ValidateIOSReceiptResult r)
						{
							UnityEngine.Debug.Log("Playfab: Validated receipt at Try#2");
							callback(PlayfabManager.IapValidationResult.Success);
						}, delegate(PlayFabError e)
						{
							PlayfabManager.IapValidationErrorCallback(callback, e);
						}, null, null);
					}, true);
				});
			}
			else
			{
				callback(PlayfabManager.IapValidationResult.Exception);
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Playfab: Validation Fail:  Error:  ",
					err.Error,
					"Message: ",
					err.ErrorMessage
				}));
			}
		}, null, null);
	}

	private static void IapValidationErrorCallback(Action<PlayfabManager.IapValidationResult> callback, PlayFabError e)
	{
		if (e.Error == PlayFabErrorCode.Success)
		{
			callback(PlayfabManager.IapValidationResult.Success);
		}
		else if (e.Error == PlayFabErrorCode.ReceiptAlreadyUsed)
		{
			callback(PlayfabManager.IapValidationResult.ReceiptAlreadyUsed);
		}
		else if (e.Error == PlayFabErrorCode.ReceiptDoesNotContainInAppItems)
		{
			callback(PlayfabManager.IapValidationResult.LoginFail);
		}
		else
		{
			callback(PlayfabManager.IapValidationResult.Exception);
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Playfab: Error while validating receipt Try#2 Error:  ",
				e.Error,
				"Message: ",
				e.ErrorMessage
			}));
		}
	}

	public static void ValidateReceiptGoogle(Product product, string receiptJson, string signature, string currencyCode, uint purchasePrice, Action<PlayfabManager.IapValidationResult> callback)
	{
		try
		{
			if (PlayfabManager.HaveLoggedIn())
			{
				PlayfabManager.ValidateReceiptGoogleAfterLogin(product, receiptJson, signature, currencyCode, purchasePrice, callback);
			}
			else
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						if (PlayfabManager.HaveLoggedIn())
						{
							PlayfabManager.ValidateReceiptGoogleAfterLogin(product, receiptJson, signature, currencyCode, purchasePrice, callback);
						}
						else
						{
							callback(PlayfabManager.IapValidationResult.LoginFail);
						}
					}, true);
				});
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error while ValidateReceiptGoogle: " + ex.Message);
			callback(PlayfabManager.IapValidationResult.Exception);
		}
	}

	private static void ValidateReceiptGoogleAfterLogin(Product product, string receiptJson, string signature, string currencyCode, uint purchasePrice, Action<PlayfabManager.IapValidationResult> callback)
	{
		ValidateGooglePlayPurchaseRequest req = new ValidateGooglePlayPurchaseRequest
		{
			ReceiptJson = receiptJson,
			Signature = signature,
			CurrencyCode = currencyCode,
			PurchasePrice = new uint?(purchasePrice)
		};
		PlayFabClientAPI.ValidateGooglePlayPurchase(req, delegate(ValidateGooglePlayPurchaseResult res)
		{
			callback(PlayfabManager.IapValidationResult.Success);
		}, delegate(PlayFabError err)
		{
			if (err.Error == PlayFabErrorCode.Success)
			{
				callback(PlayfabManager.IapValidationResult.Success);
			}
			else if (err.Error == PlayFabErrorCode.ReceiptAlreadyUsed)
			{
				callback(PlayfabManager.IapValidationResult.ReceiptAlreadyUsed);
			}
			else if (PlayfabManager.DoesErrorImplyNotLoggedIn(err.Error))
			{
				StoreManager.Authenticate(false, delegate
				{
					PlayfabManager.Login(delegate
					{
						PlayFabClientAPI.ValidateGooglePlayPurchase(req, delegate(ValidateGooglePlayPurchaseResult r)
						{
							UnityEngine.Debug.Log("Playfab: Validated receipt at Try#2");
							callback(PlayfabManager.IapValidationResult.Success);
						}, delegate(PlayFabError e)
						{
							PlayfabManager.IapValidationErrorCallback(callback, e);
						}, null, null);
					}, true);
				});
			}
			else
			{
				callback(PlayfabManager.IapValidationResult.Exception);
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Playfab: Validation Fail:  Error:  ",
					err.Error,
					"Message: ",
					err.ErrorMessage
				}));
			}
		}, null, null);
	}

	public static void GetTime(Action<bool, DateTime> callback)
	{
		PlayfabManager.timeRequests.Add(callback);
		if (!PlayfabManager.isFetchingTime)
		{
			try
			{
				PlayfabManager.isFetchingTime = true;
				if (PlayfabManager.HaveLoggedIn())
				{
					PlayfabManager.GetTimeAfterLogin();
				}
				else
				{
					StoreManager.Authenticate(false, delegate
					{
						PlayfabManager.Login(delegate
						{
							if (PlayfabManager.HaveLoggedIn())
							{
								PlayfabManager.GetTimeAfterLogin();
							}
							else
							{
								PlayfabManager.TriggerTimeRequests(false, DateTime.MinValue);
							}
						}, true);
					});
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Error while GetTime: " + ex.Message);
				PlayfabManager.TriggerTimeRequests(false, DateTime.MinValue);
			}
		}
	}

	private static void TriggerTimeRequests(bool isSuccess, DateTime dateTime)
	{
		foreach (Action<bool, DateTime> action in PlayfabManager.timeRequests)
		{
			action(isSuccess, dateTime);
		}
		PlayfabManager.timeRequests.Clear();
		PlayfabManager.isFetchingTime = false;
	}

	private static void GetTimeAfterLogin()
	{
		GetTimeRequest request = new GetTimeRequest();
		PlayFabClientAPI.GetTime(request, delegate(GetTimeResult res)
		{
			DateTime time = res.Time;
			PlayfabManager.TriggerTimeRequests(true, time);
			PlayfabManager.isFetchingTime = false;
		}, delegate(PlayFabError err)
		{
			UnityEngine.Debug.Log("Playfab: Error while getting time Try#2: " + err.ErrorMessage);
			PlayfabManager.TriggerTimeRequests(false, DateTime.MinValue);
			PlayfabManager.isFetchingTime = false;
		}, null, null);
	}

	public static void BackupSaveData(Action<bool> OnResponse)
	{
		if (PlayfabManager.isBackupInProgress)
		{
			return;
		}
		PlayfabManager.isBackupInProgress = true;
		UnityEngine.Debug.Log("Starting save cloud save backup process");
		PlayfabManager.OnBackupResponse = OnResponse;
		ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
		{
			FunctionName = "BackupCurrentSaveData",
			GeneratePlayStreamEvent = new bool?(true),
			RevisionSelection = new CloudScriptRevisionOption?(CloudScriptRevisionOption.Live)
		};
		if (PlayfabManager._003C_003Ef__mg_0024cache0 == null)
		{
			PlayfabManager._003C_003Ef__mg_0024cache0 = new Action<ExecuteCloudScriptResult>(PlayfabManager.OnBackupSaveResponse);
		}
		Action<ExecuteCloudScriptResult> resultCallback = PlayfabManager._003C_003Ef__mg_0024cache0;
		if (PlayfabManager._003C_003Ef__mg_0024cache1 == null)
		{
			PlayfabManager._003C_003Ef__mg_0024cache1 = new Action<PlayFabError>(PlayfabManager.OnErrorShared);
		}
		PlayFabClientAPI.ExecuteCloudScript(request, resultCallback, PlayfabManager._003C_003Ef__mg_0024cache1, null, null);
	}

	public static void SendConversionData()
	{
		PlayfabManager.conversionDataSent = (PlayerPrefs.GetInt(PlayfabManager.conversionDataFlagKey, 0) == 1);
		if (PlayfabManager.conversionDataSent)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (!string.IsNullOrEmpty(PlayfabManager.appsflyer_conversion_data))
		{
			dictionary.Add("provider", "APPSFLYER");
			dictionary.Add("data", PlayfabManager.appsflyer_conversion_data);
		}
		else if (!string.IsNullOrEmpty(PlayfabManager.adjust_conversion_data))
		{
			dictionary.Add("provider", "ADJUST");
			dictionary.Add("data", PlayfabManager.adjust_conversion_data);
		}
		if (dictionary.Count > 0)
		{
			string attribution = PlayfabEventId.ATTRIBUTION;
			Dictionary<string, object> body = dictionary;
			if (PlayfabManager._003C_003Ef__mg_0024cache2 == null)
			{
				PlayfabManager._003C_003Ef__mg_0024cache2 = new Action<WriteEventResponse>(PlayfabManager.OnConversionDataSent);
			}
			Action<WriteEventResponse> resultCallback = PlayfabManager._003C_003Ef__mg_0024cache2;
			if (PlayfabManager._003C_003Ef__mg_0024cache3 == null)
			{
				PlayfabManager._003C_003Ef__mg_0024cache3 = new Action<PlayFabError>(PlayfabManager.OnConversionDataSendingFailed);
			}
			PlayfabManager.SendPlayerEvent(attribution, body, resultCallback, PlayfabManager._003C_003Ef__mg_0024cache3, true);
		}
	}

	private static void OnConversionDataSendingFailed(PlayFabError obj)
	{
		PlayfabManager.conversionDataSent = false;
		PlayerPrefs.SetInt(PlayfabManager.conversionDataFlagKey, 0);
	}

	private static void OnConversionDataSent(WriteEventResponse obj)
	{
		PlayfabManager.conversionDataSent = true;
		PlayerPrefs.SetInt(PlayfabManager.conversionDataFlagKey, 1);
	}

	private static void OnErrorShared(PlayFabError obj)
	{
		PlayfabManager.OnBackupResponse(false);
		PlayfabManager.isBackupInProgress = false;
		UnityEngine.Debug.LogWarning("An error occured during cloud save backup process error: " + obj.Error);
	}

	private static void OnBackupSaveResponse(ExecuteCloudScriptResult obj)
	{
		bool flag = false;
		foreach (LogStatement logStatement in obj.Logs)
		{
			if (logStatement.Level == "Error")
			{
				flag = true;
			}
		}
		if (obj.Error != null)
		{
			flag = true;
		}
		if (flag)
		{
			UnityEngine.Debug.LogWarning("An error occured during cloud save backup process error: " + obj.Error);
			PlayfabManager.OnBackupResponse(false);
		}
		else
		{
			PlayfabManager.OnBackupResponse(true);
			UnityEngine.Debug.Log("Cloud save backup stored succesfully");
		}
		PlayfabManager.isBackupInProgress = false;
	}

	public static string GetDeviceId()
	{
		//AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		//AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getContentResolver", new object[0]);
		//AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.provider.Settings$Secure");
		//return androidJavaClass2.CallStatic<string>("getString", new object[]
		//{
		//	androidJavaObject,
		//	"android_id"
		//});
        return string.Empty;
	}

	public static void RegisterForPushNotifications()
	{
		if (!PlayfabManager.HaveLoggedIn())
		{
			UnityEngine.Debug.Log("PUSH: pf not logged in.");
			return;
		}
	}

	public static bool dontSendPlayfabEvents;

	public const string TITLE_DATA_LOCAL_SAVE_ID = "titleDataString";

	public const string TITLE_DATA_TEMP_SAVE_ID = "titleTempString";

	private const string GOOGLE_CLIENT_ID = "862605511190-d6hao81o9198jo1l15jm2fbg0e0obfj2.apps.googleusercontent.com";

	private const string GOOGLE_CLIENT_SECRET = "OshWUV4BiXkpRHs5qMtJlW1_";

	private const string TITLE_ID = "2BC3";

	private const string TITLE_DATA_ID = "config_v2";

	private const string TITLE_DATA_EDFH = "eDamageTakenFromHeroes";

	private const string TITLE_DATA_REGIONAL_OFFER_STATUS = "areRegionalOffersEnabled";

	private const string TITLE_DATA_HALLOWEEN_OFFER_CONFIG = "halloweenOfferConfig";

	private const string TITLE_DATA_CHRISTMAS_OFFER_CONFIG = "christmasEventConfig";

	public const string SECOND_ANNIVERSARY_EVENT_ID = "secondAnniversary";

	private const string TITLE_DATA_EVENTS_INFO = "eventsInfo";

	private const string TITLE_DATA_PATCH_NOTES = "patchnotes";

	private const string SAVE_DATA_ID = "save";

	private const string REWARD_DATA_ID = "reward";

	private const string SAVE_DATA_OLD_ID = "old_save";

	public const string LOCAL_PATCH_NOTES = "localpatchnotes";

	private const string XIAOMI_IAP_SERVER = "https://beesquare.hk/scripts/xvalidate2.php";

	public static string appsflyer_conversion_data = null;

	public static string adjust_conversion_data = null;

	private static string conversionDataFlagKey = "conversion_data_flag";

	public static bool conversionDataSent = false;

	public static bool instantAppStatSent = false;

	public static List<PlayfabManager.PendingPlayerEvent> pendingPlayerEvents = new List<PlayfabManager.PendingPlayerEvent>();

	public static PlayfabManager.LoginState loginState = PlayfabManager.LoginState.START;

	public static string playerId;

	public static PlayfabManager.TitleData titleData;

	private static bool haveReceivedAnySaveData = false;

	public static bool haveReceivedSaveDataFromFutureVersion = false;

	public static bool waitingForSaveConflictReslove = false;

	public static bool forceUpdate = false;

	public static bool optionalUpdate = false;

	public static bool checkForPlayfabData;

	public static double extraDamageTakenFromHeroes = 2.0;

	public static List<string> allPlayfabIds;

	public static bool isRegionalOfferStatusLoaded;

	public static bool isRegionalOffersEnabled;

	public static bool halloweenOfferConfigLoaded;

	public static PlayfabManager.OfferConfig halloweenOfferConfig;

	public static bool christmasOfferConfigLoaded;

	public static PlayfabManager.ChristmasEventConfig christmasOfferConfig;

	public static EventsInfo eventsInfo = new EventsInfo
	{
		events = new Dictionary<string, EventConfig>()
	};

	public static Simulator sim;

	private static bool didEverGetAutCode = false;

	public static bool isFetchingTime = false;

	private static List<Action<bool, DateTime>> timeRequests = new List<Action<bool, DateTime>>();

	private static Action<bool> OnBackupResponse;

	private static bool isBackupInProgress;

	private static CountryCode? countryCode;

	[CompilerGenerated]
	private static Action<ExecuteCloudScriptResult> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Action<PlayFabError> _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static Action<WriteEventResponse> _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static Action<PlayFabError> _003C_003Ef__mg_0024cache3;

	public enum LoginState
	{
		START,
		WAIT_SERVER_RESP,
		COMPLETED_FAIL,
		COMPLETED_SUCCESS_CUSTOM,
		COMPLETED_SUCCESS_VIA_STORE
	}

	public class TitleData
	{
		public string minVersionAllowed;

		public string latestVersion;

		public float adDragonProbWeightAd;

		public float adDragonProbWeightDirect;

		public float adDragonProbWeightAdGold;

		public float adDragonProbWeightAdToken;

		public float adDragonProbWeightAdCredits;

		public float adDragonProbWeightAdScrap;

		public float adDragonProbWeightAdMyth;

		public float adDragonProbWeightAdAeon;

		public float adDragonProbWeightDirectGold;

		public float adDragonProbWeightDirectToken;

		public float adDragonProbWeightDirectCredits;

		public float adDragonProbWeightDirectScrap;

		public float adDragonProbWeightDirectMyth;

		public float adDragonProbWeightDirectAeon;

		public double adDragonRewardAdGoldFactor;

		public double adDragonRewardAdToken;

		public double adDragonRewardAdCredits;

		public double adDragonRewardAdScrap;

		public double adDragonRewardAdMyth;

		public double adDragonRewardAdAeon;

		public double adDragonRewardDirectGoldFactor;

		public double adDragonRewardDirectToken;

		public double adDragonRewardDirectCredits;

		public double adDragonRewardDirectScrap;

		public double adDragonRewardDirectMyth;

		public double adDragonRewardDirectAeon;

		public double freeCreditsAmount;

		public float goblinChestAppear;

		public double offlineEarningsFactor;

		public float[] gearLevelUpgradeChances;

		public float freeLootpackPeriod;

		public double christmasCandyCapAmount;

		public double christmasFreeCandiesAmount;

		public double christmasAdCandiesAmount;
	}

	[Serializable]
	public class RewardData
	{
		public bool HasReward()
		{
			return this.amountAeons > 0.0 || this.amountCredits > 0.0 || this.amountMythstones > 0.0 || this.amountScraps > 0.0 || this.amountToken > 0.0 || this.amountCandies > 0.0 || this.amountTrinketBoxes > 0 || (this.skinIds != null && this.skinIds.Count > 0);
		}

		public string title;

		public string desc;

		public double amountToken;

		public double amountScraps;

		public double amountCredits;

		public double amountMythstones;

		public double amountAeons;

		public double amountCandies;

		public int amountTrinketBoxes;

		public List<int> skinIds;
	}

	public class NewsData : IComparable
	{
		public int CompareTo(object obj)
		{
			PlayfabManager.NewsData newsData = obj as PlayfabManager.NewsData;
			if (newsData == null)
			{
				return 1;
			}
			return this.CompareTo(newsData);
		}

		public int CompareTo(PlayfabManager.NewsData newsData)
		{
			return this.dateTime.CompareTo(newsData.dateTime);
		}

		public bool CanBeShownReachingMinStage(int maxStageReached, DateTime now, DateTime lastNewsTime)
		{
			return (this.body.maxStage == 0 || maxStageReached <= this.body.maxStage) && new Version(this.body.minVersion).CompareTo(Cheats.versionObject) <= 0 && lastNewsTime < this.dateTime && now < this.dateTime.AddHours((double)this.body.durationInHours);
		}

		public bool HasReward()
		{
			return this.body.reward.HasReward();
		}

		public int id;

		public DateTime dateTime;

		public PlayfabManager.NewsBody body;
	}

	public class NewsBody
	{
		public int durationInHours;

		public PlayfabManager.RewardData reward;

		public string minVersion;

		public int minStage;

		public int maxStage;
	}

	public class OfferConfig
	{
		public string startDate;

		public string endDate;

		public DateTime startDateParsed;

		public DateTime endDateParsed;
	}

	public class ChristmasEventConfig
	{
		public PlayfabManager.OfferConfig offerConfig;

		public string candyDropLimitDate;

		public DateTime candyDropLimitDateParsed;
	}

	public class Payload
	{
		public string gameOrderId;

		public string productCode;

		public string orderQueryToken;

		public string transactionId;
	}

	public enum IapValidationResult
	{
		Success,
		ReceiptAlreadyUsed,
		InvalidReceipt,
		LoginFail,
		Exception
	}

	public class XiaomiIapRequest
	{
		public string userLoginToken;

		public string receipt;

		public string transactionID;

		public string playfabID;

		public List<string> otherIds;
	}

	public class PendingPlayerEvent
	{
		public PendingPlayerEvent(string eventName, Dictionary<string, object> body, Func<bool> successCondition, double timeoutDuration = 10.0)
		{
			this.eventName = eventName;
			this.body = body;
			this.successCondition = successCondition;
			this.timeoutDuration = timeoutDuration;
			this.timeStamp = DateTime.UtcNow;
		}

		public bool DidExpire()
		{
			return (DateTime.UtcNow - this.timeStamp).TotalSeconds >= this.timeoutDuration;
		}

		public string eventName;

		public Dictionary<string, object> body;

		public Func<bool> successCondition;

		private double timeoutDuration;

		private DateTime timeStamp;
	}
}
