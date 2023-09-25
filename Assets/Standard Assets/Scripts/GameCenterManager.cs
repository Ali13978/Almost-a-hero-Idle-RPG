using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenterManager : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnAuthFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_LeaderboardResult> OnScoreSubmitted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_LeaderboardResult> OnScoresListLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_LeaderboardResult> OnLeadrboardInfoLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnLeaderboardSetsInfoLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnAchievementsReset;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnAchievementsLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_AchievementProgressResult> OnAchievementsProgress;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action OnGameCenterViewDismissed;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnFriendsListLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_UserInfoLoadResult> OnUserInfoLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_PlayerSignatureResult> OnPlayerSignatureRetrieveResult;

	[Obsolete("init is deprecated, please use Init instead.")]
	public static void init()
	{
		GameCenterManager.Init();
	}

	public static void Init()
	{
		if (GameCenterManager._IsInitialized)
		{
			return;
		}
		GameCenterManager._IsInitialized = true;
		Singleton<GameCenterInvitations>.Instance.Init();
		GameObject gameObject = new GameObject("GameCenterManager");
		gameObject.AddComponent<GameCenterManager>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		foreach (GK_Leaderboard gk_Leaderboard in GameCenterManager.Leaderboards)
		{
			gk_Leaderboard.Refresh();
		}
	}

	public static void RetrievePlayerSignature()
	{
	}

	public static void ShowGmaeKitNotification(string title, string message)
	{
	}

	public static void RegisterAchievement(string achievementId)
	{
		GameCenterManager.RegisterAchievement(new GK_AchievementTemplate
		{
			Id = achievementId
		});
	}

	public static void RegisterAchievement(GK_AchievementTemplate achievement)
	{
		bool flag = false;
		int index = 0;
		foreach (GK_AchievementTemplate gk_AchievementTemplate in GameCenterManager.Achievements)
		{
			if (gk_AchievementTemplate.Id.Equals(achievement.Id))
			{
				flag = true;
				index = GameCenterManager.Achievements.IndexOf(gk_AchievementTemplate);
				break;
			}
		}
		if (flag)
		{
			GameCenterManager.Achievements[index] = achievement;
		}
		else
		{
			GameCenterManager.Achievements.Add(achievement);
		}
	}

	public static void ShowLeaderboard(string leaderboardId)
	{
		GameCenterManager.ShowLeaderboard(leaderboardId, GK_TimeSpan.ALL_TIME);
	}

	public static void ShowLeaderboard(string leaderboardId, GK_TimeSpan timeSpan)
	{
	}

	public static void ShowLeaderboards()
	{
	}

	public static void ReportScore(long score, string leaderboardId, long context = 0L)
	{
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("unity reportScore: " + leaderboardId, LogType.Log);
		}
	}

	public static void ReportScore(double score, string leaderboardId)
	{
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("unity reportScore double: " + leaderboardId, LogType.Log);
		}
	}

	public static void RetrieveFriends()
	{
	}

	[Obsolete("LoadUsersData is deprecated, please use LoadGKPlayerInfo instead.")]
	public static void LoadUsersData(string[] UIDs)
	{
		GameCenterManager.LoadGKPlayerInfo(UIDs[0]);
	}

	public static void LoadGKPlayerInfo(string playerId)
	{
	}

	public static void LoadGKPlayerPhoto(string playerId, GK_PhotoSize size)
	{
	}

	[Obsolete("LoadCurrentPlayerScore is deprecated, please use LoadLeaderboardInfo instead.")]
	public static void LoadCurrentPlayerScore(string leaderboardId, GK_TimeSpan timeSpan = GK_TimeSpan.ALL_TIME, GK_CollectionType collection = GK_CollectionType.GLOBAL)
	{
		GameCenterManager.LoadLeaderboardInfo(leaderboardId);
	}

	public static void LoadLeaderboardInfo(string leaderboardId)
	{
	}

	private IEnumerator LoadLeaderboardInfoLocal(string leaderboardId)
	{
		yield return new WaitForSeconds(4f);
		yield break;
	}

	public static void LoadScore(string leaderboardId, int startIndex, int length, GK_TimeSpan timeSpan = GK_TimeSpan.ALL_TIME, GK_CollectionType collection = GK_CollectionType.GLOBAL)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message, string playerId)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message, string[] playerIds)
	{
	}

	public static void IssueLeaderboardChallenge(string leaderboardId, string message)
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message, string playerId)
	{
	}

	public static void LoadLeaderboardSetInfo()
	{
	}

	public static void LoadLeaderboardsForSet(string setId)
	{
	}

	public static void LoadAchievements()
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message, string[] playerIds)
	{
	}

	public static void IssueAchievementChallenge(string achievementId, string message)
	{
	}

	public static void ShowAchievements()
	{
	}

	public static void ResetAchievements()
	{
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			GameCenterManager.ResetStoredProgress();
		}
	}

	public static void SubmitAchievement(float percent, string achievementId)
	{
		GameCenterManager.SubmitAchievement(percent, achievementId, true);
	}

	public static void SubmitAchievementNoCache(float percent, string achievementId)
	{
	}

	public static void SubmitAchievement(float percent, string achievementId, bool isCompleteNotification)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ISN_CacheManager.SaveAchievementRequest(achievementId, percent);
		}
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			GameCenterManager.SaveAchievementProgress(achievementId, percent);
		}
	}

	public static float GetAchievementProgress(string id)
	{
		float result;
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			result = GameCenterManager.GetStoredAchievementProgress(id);
		}
		else
		{
			GK_AchievementTemplate achievement = GameCenterManager.GetAchievement(id);
			result = achievement.Progress;
		}
		return result;
	}

	public static GK_AchievementTemplate GetAchievement(string achievementId)
	{
		foreach (GK_AchievementTemplate gk_AchievementTemplate in GameCenterManager.Achievements)
		{
			if (gk_AchievementTemplate.Id.Equals(achievementId))
			{
				return gk_AchievementTemplate;
			}
		}
		GK_AchievementTemplate gk_AchievementTemplate2 = new GK_AchievementTemplate();
		gk_AchievementTemplate2.Id = achievementId;
		GameCenterManager.Achievements.Add(gk_AchievementTemplate2);
		return gk_AchievementTemplate2;
	}

	public static GK_Leaderboard GetLeaderboard(string id)
	{
		foreach (GK_Leaderboard gk_Leaderboard in GameCenterManager.Leaderboards)
		{
			if (gk_Leaderboard.Id.Equals(id))
			{
				return gk_Leaderboard;
			}
		}
		GK_Leaderboard gk_Leaderboard2 = new GK_Leaderboard(id);
		GameCenterManager.Leaderboards.Add(gk_Leaderboard2);
		return gk_Leaderboard2;
	}

	public static GK_Player GetPlayerById(string playerID)
	{
		if (GameCenterManager._players.ContainsKey(playerID))
		{
			return GameCenterManager._players[playerID];
		}
		return null;
	}

	public static void SendFriendRequest(GK_FriendRequest request, List<string> emails, List<string> players)
	{
		GameCenterManager._FriendRequests.Add(request.Id, request);
	}

	public static List<GK_AchievementTemplate> Achievements
	{
		get
		{
			return IOSNativeSettings.Instance.Achievements;
		}
	}

	public static List<GK_Leaderboard> Leaderboards
	{
		get
		{
			return IOSNativeSettings.Instance.Leaderboards;
		}
	}

	public static Dictionary<string, GK_Player> Players
	{
		get
		{
			return GameCenterManager._players;
		}
	}

	public static GK_Player Player
	{
		get
		{
			return GameCenterManager._player;
		}
	}

	public static bool IsInitialized
	{
		get
		{
			return GameCenterManager._IsInitialized;
		}
	}

	public static List<GK_LeaderboardSet> LeaderboardSets
	{
		get
		{
			return GameCenterManager._LeaderboardSets;
		}
	}

	public static bool IsPlayerAuthenticated
	{
		get
		{
			return false;
		}
	}

	public static bool IsAchievementsInfoLoaded
	{
		get
		{
			return GameCenterManager._IsAchievementsInfoLoaded;
		}
	}

	public static List<string> FriendsList
	{
		get
		{
			return GameCenterManager._friendsList;
		}
	}

	public static bool IsPlayerUnderage
	{
		get
		{
			return false;
		}
	}

	private void OnLoaderBoardInfoRetrivedFail(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string id = array[0];
		int requestId = Convert.ToInt32(array[3]);
		string errorData = array[4];
		GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(id);
		leaderboard.ReportLocalPlayerScoreUpdateFail(errorData, requestId);
	}

	private void OnLoaderBoardInfoRetrived(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string text = array[0];
		GK_TimeSpan vTimeSpan = (GK_TimeSpan)Convert.ToInt32(array[1]);
		GK_CollectionType sCollection = (GK_CollectionType)Convert.ToInt32(array[2]);
		int requestId = Convert.ToInt32(array[3]);
		long vScore = Convert.ToInt64(array[4]);
		int vRank = Convert.ToInt32(array[5]);
		int num = Convert.ToInt32(array[6]);
		int mr = Convert.ToInt32(array[7]);
		string title = array[8];
		string description = array[9];
		GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(text);
		leaderboard.UpdateMaxRange(mr);
		leaderboard.Info.Title = title;
		leaderboard.Info.Description = description;
		GK_Score score = new GK_Score(vScore, vRank, (long)num, vTimeSpan, sCollection, text, GameCenterManager.Player.Id);
		leaderboard.ReportLocalPlayerScoreUpdate(score, requestId);
	}

	public void onScoreSubmittedEvent(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string leaderboardId = array[0];
		base.StartCoroutine(this.LoadLeaderboardInfoLocal(leaderboardId));
	}

	public void onScoreSubmittedFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string id = array[0];
		string errorData = array[2];
		GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(id);
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard, new Error(errorData));
		GameCenterManager.OnScoreSubmitted(obj);
	}

	private void OnLeaderboardScoreListLoaded(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string text = array[0];
		GK_TimeSpan vTimeSpan = (GK_TimeSpan)Convert.ToInt32(array[1]);
		GK_CollectionType sCollection = (GK_CollectionType)Convert.ToInt32(array[2]);
		GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(text);
		for (int i = 3; i < array.Length; i += 4)
		{
			string text2 = array[i];
			long vScore = Convert.ToInt64(array[i + 1]);
			int vRank = Convert.ToInt32(array[i + 2]);
			int num = Convert.ToInt32(array[i + 3]);
			GK_Score gk_Score = new GK_Score(vScore, vRank, (long)num, vTimeSpan, sCollection, text, text2);
			leaderboard.UpdateScore(gk_Score);
			if (GameCenterManager.Player != null && GameCenterManager.Player.Id.Equals(text2))
			{
				leaderboard.UpdateCurrentPlayerScore(gk_Score);
			}
		}
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard);
		GameCenterManager.OnScoresListLoaded(obj);
	}

	private void OnLeaderboardScoreListLoadFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string id = array[0];
		string errorData = array[3];
		GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(id);
		GK_LeaderboardResult obj = new GK_LeaderboardResult(leaderboard, new Error(errorData));
		GameCenterManager.OnScoresListLoaded(obj);
	}

	private void onAchievementsReset(string array)
	{
		Result obj = new Result();
		GameCenterManager.OnAchievementsReset(obj);
	}

	private void onAchievementsResetFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		GameCenterManager.OnAchievementsReset(obj);
	}

	private void onAchievementProgressChanged(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		GK_AchievementTemplate achievement = GameCenterManager.GetAchievement(array2[0]);
		achievement.Progress = Convert.ToSingle(array2[1]);
		GK_AchievementProgressResult obj = new GK_AchievementProgressResult(achievement);
		this.SaveLocalProgress(achievement);
		GameCenterManager.OnAchievementsProgress(obj);
	}

	private void onAchievementsLoaded(string array)
	{
		Result obj = new Result();
		if (array.Equals(string.Empty))
		{
			GameCenterManager.OnAchievementsLoaded(obj);
			return;
		}
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array2.Length; i += 3)
		{
			GK_AchievementTemplate achievement = GameCenterManager.GetAchievement(array2[i]);
			achievement.Description = array2[i + 1];
			achievement.Progress = Convert.ToSingle(array2[i + 2]);
			this.SaveLocalProgress(achievement);
		}
		GameCenterManager._IsAchievementsInfoLoaded = true;
		GameCenterManager.OnAchievementsLoaded(obj);
	}

	private void onAchievementsLoadedFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		GameCenterManager.OnAchievementsLoaded(obj);
	}

	private void onAuthenticateLocalPlayer(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		GameCenterManager._player = new GK_Player(array2[0], array2[1], array2[2]);
		ISN_CacheManager.SendAchievementCachedRequest();
		Result obj;
		if (GameCenterManager.IsPlayerAuthenticated)
		{
			obj = new Result();
		}
		else
		{
			obj = new Result(new Error());
		}
		GameCenterManager.OnAuthFinished(obj);
	}

	private void onAuthenticationFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		GameCenterManager.OnAuthFinished(obj);
	}

	private void OnUserInfoLoadedEvent(string array)
	{
		ISN_Logger.Log("OnUserInfoLoadedEvent", LogType.Log);
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		string text = array2[0];
		string pAlias = array2[1];
		string pName = array2[2];
		GK_Player gk_Player = new GK_Player(text, pName, pAlias);
		if (GameCenterManager._players.ContainsKey(text))
		{
			GameCenterManager._players[text] = gk_Player;
		}
		else
		{
			GameCenterManager._players.Add(text, gk_Player);
		}
		if (gk_Player.Id == GameCenterManager._player.Id)
		{
			GameCenterManager._player = gk_Player;
		}
		ISN_Logger.Log("Player Info loaded, for player with id: " + gk_Player.Id, LogType.Log);
		GK_UserInfoLoadResult obj = new GK_UserInfoLoadResult(gk_Player);
		GameCenterManager.OnUserInfoLoaded(obj);
	}

	private void OnUserInfoLoadFailedEvent(string playerId)
	{
		GK_UserInfoLoadResult obj = new GK_UserInfoLoadResult(playerId);
		GameCenterManager.OnUserInfoLoaded(obj);
	}

	private void OnUserPhotoLoadedEvent(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		string playerID = array2[0];
		GK_PhotoSize size = (GK_PhotoSize)Convert.ToInt32(array2[1]);
		string base64String = array2[2];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		if (playerById != null)
		{
			playerById.SetPhotoData(size, base64String);
		}
	}

	private void OnUserPhotoLoadFailedEvent(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		string playerID = array[0];
		GK_PhotoSize size = (GK_PhotoSize)Convert.ToInt32(array[1]);
		string errorData = array[2];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		if (playerById != null)
		{
			playerById.SetPhotoLoadFailedEventData(size, errorData);
		}
	}

	private void OnFriendListLoadedEvent(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		GameCenterManager._friendsList.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			GameCenterManager._friendsList.Add(array[i]);
		}
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log("Friends list loaded, total friends: " + GameCenterManager._friendsList.Count, LogType.Log);
		}
		Result obj = new Result();
		GameCenterManager.OnFriendsListLoaded(obj);
	}

	private void OnFriendListLoadFailEvent(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		GameCenterManager.OnFriendsListLoaded(obj);
	}

	private void OnGameCenterViewDismissedEvent(string data)
	{
		GameCenterManager.OnGameCenterViewDismissed();
	}

	private void VerificationSignatureRetrieveFailed(string array)
	{
		Error errro = new Error(array);
		GK_PlayerSignatureResult obj = new GK_PlayerSignatureResult(errro);
		GameCenterManager.OnPlayerSignatureRetrieveResult(obj);
	}

	private void VerificationSignatureRetrieved(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		GK_PlayerSignatureResult obj = new GK_PlayerSignatureResult(array2[0], array2[1], array2[2], array2[3]);
		GameCenterManager.OnPlayerSignatureRetrieveResult(obj);
	}

	private void SaveLocalProgress(GK_AchievementTemplate tpl)
	{
		if (IOSNativeSettings.Instance.UsePPForAchievements)
		{
			GameCenterManager.SaveAchievementProgress(tpl.Id, tpl.Progress);
		}
	}

	private static void ResetStoredProgress()
	{
		foreach (GK_AchievementTemplate gk_AchievementTemplate in GameCenterManager.Achievements)
		{
			PlayerPrefs.DeleteKey("ISN_GameCenterManager" + gk_AchievementTemplate.Id);
		}
	}

	private static void SaveAchievementProgress(string achievementId, float progress)
	{
		float storedAchievementProgress = GameCenterManager.GetStoredAchievementProgress(achievementId);
		if (progress > storedAchievementProgress)
		{
			PlayerPrefs.SetFloat("ISN_GameCenterManager" + achievementId, Mathf.Clamp(progress, 0f, 100f));
		}
	}

	private static float GetStoredAchievementProgress(string achievementId)
	{
		float result = 0f;
		if (PlayerPrefs.HasKey("ISN_GameCenterManager" + achievementId))
		{
			result = PlayerPrefs.GetFloat("ISN_GameCenterManager" + achievementId);
		}
		return result;
	}

	private void ISN_OnLBSetsLoaded(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		int num = 0;
		while (num + 2 < array2.Length)
		{
			GK_LeaderboardSet gk_LeaderboardSet = new GK_LeaderboardSet();
			gk_LeaderboardSet.Title = array2[num];
			gk_LeaderboardSet.Identifier = array2[num + 1];
			gk_LeaderboardSet.GroupIdentifier = array2[num + 2];
			GameCenterManager.LeaderboardSets.Add(gk_LeaderboardSet);
			num += 3;
		}
		Result obj = new Result();
		GameCenterManager.OnLeaderboardSetsInfoLoaded(obj);
	}

	private void ISN_OnLBSetsLoadFailed(string array)
	{
		Result obj = new Result(new Error());
		GameCenterManager.OnLeaderboardSetsInfoLoaded(obj);
	}

	private void ISN_OnLBSetsBoardsLoadFailed(string identifier)
	{
		foreach (GK_LeaderboardSet gk_LeaderboardSet in GameCenterManager.LeaderboardSets)
		{
			if (gk_LeaderboardSet.Identifier.Equals(identifier))
			{
				gk_LeaderboardSet.SendFailLoadEvent();
				break;
			}
		}
	}

	private void ISN_OnLBSetsBoardsLoaded(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		string value = array2[0];
		foreach (GK_LeaderboardSet gk_LeaderboardSet in GameCenterManager.LeaderboardSets)
		{
			if (gk_LeaderboardSet.Identifier.Equals(value))
			{
				for (int i = 1; i < array2.Length; i += 3)
				{
					gk_LeaderboardSet.AddBoardInfo(new GK_LeaderBoardInfo
					{
						Title = array2[i],
						Description = array2[i + 1],
						Identifier = array2[i + 2]
					});
				}
				gk_LeaderboardSet.SendSuccessLoadEvent();
				break;
			}
		}
	}

	public static void DispatchLeaderboardUpdateEvent(GK_LeaderboardResult result, bool isInternal)
	{
		if (isInternal)
		{
			GameCenterManager.OnScoreSubmitted(result);
		}
		else
		{
			GameCenterManager.OnLeadrboardInfoLoaded(result);
		}
	}

	public static List<GK_TBM_Participant> ParseParticipantsData(string[] data, int index)
	{
		List<GK_TBM_Participant> list = new List<GK_TBM_Participant>();
		for (int i = index; i < data.Length; i += 5)
		{
			if (data[i] == "endofline")
			{
				break;
			}
			GK_TBM_Participant item = GameCenterManager.ParseParticipanData(data, i);
			list.Add(item);
		}
		return list;
	}

	public static GK_TBM_Participant ParseParticipanData(string[] data, int index)
	{
		return new GK_TBM_Participant(data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4]);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static GameCenterManager()
	{
		GameCenterManager.OnAuthFinished = delegate(Result A_0)
		{
		};
		GameCenterManager.OnScoreSubmitted = delegate(GK_LeaderboardResult A_0)
		{
		};
		GameCenterManager.OnScoresListLoaded = delegate(GK_LeaderboardResult A_0)
		{
		};
		GameCenterManager.OnLeadrboardInfoLoaded = delegate(GK_LeaderboardResult A_0)
		{
		};
		GameCenterManager.OnLeaderboardSetsInfoLoaded = delegate(Result A_0)
		{
		};
		GameCenterManager.OnAchievementsReset = delegate(Result A_0)
		{
		};
		GameCenterManager.OnAchievementsLoaded = delegate(Result A_0)
		{
		};
		GameCenterManager.OnAchievementsProgress = delegate(GK_AchievementProgressResult A_0)
		{
		};
		GameCenterManager.OnGameCenterViewDismissed = delegate()
		{
		};
		GameCenterManager.OnFriendsListLoaded = delegate(Result A_0)
		{
		};
		GameCenterManager.OnUserInfoLoaded = delegate(GK_UserInfoLoadResult A_0)
		{
		};
		GameCenterManager.OnPlayerSignatureRetrieveResult = delegate(GK_PlayerSignatureResult A_0)
		{
		};
		GameCenterManager._IsInitialized = false;
		GameCenterManager._IsAchievementsInfoLoaded = false;
		GameCenterManager._players = new Dictionary<string, GK_Player>();
		GameCenterManager._friendsList = new List<string>();
		GameCenterManager._LeaderboardSets = new List<GK_LeaderboardSet>();
		GameCenterManager._FriendRequests = new Dictionary<int, GK_FriendRequest>();
		GameCenterManager._player = null;
	}

	private static bool _IsInitialized;

	private static bool _IsAchievementsInfoLoaded;

	private static Dictionary<string, GK_Player> _players;

	private static List<string> _friendsList;

	private static List<GK_LeaderboardSet> _LeaderboardSets;

	private static Dictionary<int, GK_FriendRequest> _FriendRequests;

	private static GK_Player _player;

	private const string ISN_GC_PP_KEY = "ISN_GameCenterManager";
}
