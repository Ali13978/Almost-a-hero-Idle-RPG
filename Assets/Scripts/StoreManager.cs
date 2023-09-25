using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.SimpleAndroidNotifications;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms;

public static class StoreManager
{
	public static bool IsAuthed()
	{
		return StoreManager.authState == AuthState.COMPLETED_SUCCESS;
	}

	public static void OnAuthFailed()
	{
		StoreManager.authState = AuthState.COMPLETED_FAIL;
	}

	private static IEnumerator PollForAuthResult(Action callback)
	{
		while (StoreManager.authState == AuthState.WAIT_SERVER_RESP)
		{
			yield return null;
		}
		callback();
		yield break;
	}

	public static void Authenticate(bool forceAuthenticate, Action callback)
	{
		if (StoreManager.authState == AuthState.WAIT_SERVER_RESP)
		{
			Main.coroutineObject.StartCoroutine(StoreManager.PollForAuthResult(callback));
			return;
		}
		if (!forceAuthenticate && StoreManager.authState != AuthState.INIT)
		{
			callback();
			return;
		}
		StoreManager.authState = AuthState.WAIT_SERVER_RESP;
		//PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().AddOauthScope("profile").RequestServerAuthCode(false).Build();
		//PlayGamesPlatform.InitializeInstance(configuration);
		//PlayGamesPlatform.Activate();
		UnityEngine.Debug.Log("Loggining in to google play");
		Social.localUser.Authenticate(delegate(bool res, string errorString)
		{
			if (!res && errorString == "Authentication failed")
			{
				Main.dontTryAuth = true;
			}
			UnityEngine.Debug.Log("On auth response");
			UnityEngine.Debug.Log("Did login: " + res);
			UnityEngine.Debug.Log("Error: " + errorString);
			//StoreManager.authState = ((!res) ? AuthState.COMPLETED_FAIL : AuthState.COMPLETED_SUCCESS);
            StoreManager.authState = AuthState.COMPLETED_SUCCESS;
            UnityEngine.Debug.Log(StoreManager.authState);
			callback();
		});
	}

	public static void LogOut()
	{
		//if (PlayGamesPlatform.Instance != null)
		//{
		//	PlayGamesPlatform.Instance.SignOut();
		//}
		StoreManager.authState = AuthState.COMPLETED_FAIL;
	}

	public static void ReportAchievement(string achievementId)
	{
		if (StoreManager.authState != AuthState.COMPLETED_SUCCESS)
		{
			UnityEngine.Debug.Log("You no auth, get out with your Achievements!");
			return;
		}
		Social.ReportProgress(achievementId, 100.0, delegate(bool res)
		{
		});
	}

	public static void CheckAchievements(Dictionary<string, bool> localAchievements)
	{
		if (StoreManager.isFetchingAchievements)
		{
			return;
		}
		StoreManager.isFetchingAchievements = true;
		Main.coroutineObject.StartCoroutine(StoreManager.CheckAchievementsWithDelay(0.5f, localAchievements));
	}

	public static IEnumerator CheckAchievementsWithDelay(float delay, Dictionary<string, bool> localAchievements)
	{
		yield return new WaitForSeconds(delay);
		if (StoreManager.authState != AuthState.COMPLETED_SUCCESS)
		{
			UnityEngine.Debug.Log("You no auth, get out with your Achievements!");
		}
		else
		{
			StoreManager.localAchievements = localAchievements;
			
			Social.LoadAchievements(new Action<IAchievement[]>(StoreManager.OnAchivementsLoaded));
		}
		yield break;
	}

	[Preserve]
	public static void OnAchivementsLoaded(IAchievement[] storeAchievements)
	{
		StoreManager.OnAchivementsLoaded(storeAchievements, StoreManager.localAchievements);
	}

	[Preserve]
	public static void OnAchivementsLoaded(IAchievement[] storeAchievements, Dictionary<string, bool> localAchievements)
	{
		try
		{
			StoreManager.isFetchingAchievements = false;
			if (storeAchievements == null)
			{
				UnityEngine.Debug.Log("Achivements on load: Store achivements are null skipping report");
			}
			else if (localAchievements == null)
			{
				UnityEngine.Debug.Log("Achivements on load: Local achivements are null skipping report");
			}
			else if (storeAchievements.Length > 0)
			{
				foreach (IAchievement achievement in storeAchievements)
				{
					if (achievement != null)
					{
						if (localAchievements.ContainsKey(achievement.id) && localAchievements[achievement.id] && (achievement.percentCompleted < 100.0 || achievement.completed))
						{
							string id = achievement.id;
							double progress = 100.0;
							
							Social.ReportProgress(id, progress, new Action<bool>(StoreManager.OnAcievementProgressReported));
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error on load achievements: " + ex.Message);
		}
	}

	[Preserve]
	private static void OnAcievementProgressReported(bool result)
	{
	}

	public static void ReportMaxStage(int stagePrestiged)
	{
		if (StoreManager.authState != AuthState.COMPLETED_SUCCESS)
		{
			UnityEngine.Debug.Log("You no auth, get out with your Max Stage!");
			return;
		}
		Social.ReportScore((long)stagePrestiged, LeaderboardId.MAX_STAGE, delegate(bool res)
		{
		});
	}

	public static void RegisterForNotifications()
	{
	}

	public static void SetNotifications(double freeChestInSeconds, double timeUntilShopPackOffer, double minesDuration, double sideQuestDuration, double flashOfferDuration, double secondsTillDustRestBonusFull, List<StoreManager.NotificationInfo> notifications)
	{
		StoreManager.CancelNotifications();
		if (!StoreManager.areNotificationsAllowed)
		{
			return;
		}
		if (StoreManager.freeChestsNotifications && freeChestInSeconds > 0.0)
		{
			StoreManager.SetNotification(freeChestInSeconds, LM.Get("NOTIF_FREE_CHEST"));
		}
		if (StoreManager.specialOffersNotifications && timeUntilShopPackOffer > 0.0)
		{
			StoreManager.SetNotification(timeUntilShopPackOffer, LM.Get("NOTIF_SHOP_OFFER"));
		}
		if (StoreManager.mineNotifications && minesDuration > 0.0)
		{
			StoreManager.SetNotification(minesDuration, LM.Get("NOTIF_MINE_COLLECT"));
		}
		if (StoreManager.sideQuestNotifications && sideQuestDuration > 0.0)
		{
			StoreManager.SetNotification(sideQuestDuration, LM.Get("NOTIF_DAILY_QUEST"));
		}
		if (StoreManager.flashOffersNotifications && flashOfferDuration > 0.0)
		{
			StoreManager.SetNotification(flashOfferDuration, LM.Get("NOTIF_FLASH_OFFER"));
		}
		if (StoreManager.dustRestBonusFullNotifications && secondsTillDustRestBonusFull > 0.0)
		{
			StoreManager.SetNotification(secondsTillDustRestBonusFull, LM.Get("NOTIF_REST_BONUS"));
		}
		for (int i = notifications.Count - 1; i >= 0; i--)
		{
			StoreManager.SetNotification(notifications[i].notificationTime, LM.Get(notifications[i].locKey));
		}
	}

	private static void SetNotification(double duration, string message)
	{
		NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(duration), message, string.Empty, new Color(0f, 0.6f, 1f), NotificationIcon.Bell, false);
	}

	public static void CancelNotifications()
	{
		NotificationManager.CancelAll();
	}

	public static AuthState authState;

	public static bool areNotificationsAllowed;

	public static bool mineNotifications;

	public static bool specialOffersNotifications;

	public static bool freeChestsNotifications;

	public static bool sideQuestNotifications;

	public static bool flashOffersNotifications;

	public static bool dustRestBonusFullNotifications;

	public static bool christmasEventNotifications;

	public static bool eventsNotifications;

	public static bool askedToAllowNotifications;

	public static bool isFetchingAchievements;

	private static Dictionary<string, bool> localAchievements;

	
	public struct NotificationInfo
	{
		public double notificationTime;

		public string locKey;
	}

	
	
}
