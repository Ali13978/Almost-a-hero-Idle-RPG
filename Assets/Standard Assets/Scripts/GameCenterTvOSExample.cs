using System;
using SA.Common.Models;
using UnityEngine;

public class GameCenterTvOSExample : MonoBehaviour
{
	private void Start()
	{
		GameCenterManager.OnAuthFinished += this.OnAuthFinished;
		GameCenterManager.OnScoreSubmitted += this.OnScoreSubmitted;
		GameCenterManager.OnAchievementsProgress += this.HandleOnAchievementsProgress;
		GameCenterManager.OnAchievementsReset += this.HandleOnAchievementsReset;
		GameCenterManager.OnAchievementsLoaded += this.OnAchievementsLoaded;
		GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_1_ID);
		GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_2_ID);
		GameCenterManager.OnGameCenterViewDismissed += this.GameCenterManager_OnGameCenterViewDismissed;
		GameCenterManager.Init();
	}

	private void OnAuthFinished(Result res)
	{
		this._IsUILocked = true;
		IOSMessage iosmessage;
		if (res.IsSucceeded)
		{
			iosmessage = IOSMessage.Create("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nAlias: " + GameCenterManager.Player.Alias);
			GameCenterManager.LoadLeaderboardInfo(this.TEST_LEADERBOARD_1);
		}
		else
		{
			iosmessage = IOSMessage.Create("Game Center ", "Player authentication failed");
		}
		iosmessage.OnComplete += delegate()
		{
			this._IsUILocked = false;
		};
	}

	public void ShowAchivemnets()
	{
		UnityEngine.Debug.Log("ShowAchivemnets " + this._IsUILocked);
		if (this._IsUILocked)
		{
			return;
		}
		this._IsUILocked = true;
		GameCenterManager.ShowAchievements();
	}

	public void SubmitAchievement()
	{
		UnityEngine.Debug.Log("SubmitAchievement");
		GameCenterManager.SubmitAchievement(GameCenterManager.GetAchievementProgress(this.TEST_ACHIEVEMENT_1_ID) + 2.432f, this.TEST_ACHIEVEMENT_1_ID);
	}

	public void ResetAchievements()
	{
		UnityEngine.Debug.Log("ResetAchievements");
		GameCenterManager.ResetAchievements();
	}

	public void ShowLeaderboards()
	{
		UnityEngine.Debug.Log("ShowLeaderboards" + this._IsUILocked);
		if (this._IsUILocked)
		{
			return;
		}
		this._IsUILocked = true;
		GameCenterManager.ShowLeaderboards();
	}

	public void ShowLeaderboardByID()
	{
		UnityEngine.Debug.Log("ShowLeaderboardByID");
		GameCenterManager.OnFriendsListLoaded += delegate(Result obj)
		{
			UnityEngine.Debug.Log("Loaded: " + GameCenterManager.FriendsList.Count + " fiends");
		};
		GameCenterManager.RetrieveFriends();
	}

	public void ReportScore()
	{
		UnityEngine.Debug.Log("ReportScore");
		this.hiScore++;
		GameCenterManager.ReportScore((long)this.hiScore, this.TEST_LEADERBOARD_1, 17L);
	}

	private void OnScoreSubmitted(GK_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			GK_Score currentPlayerScore = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			IOSNativePopUpManager.showMessage("Leaderboard " + currentPlayerScore.LongScore, string.Concat(new object[]
			{
				"Score: ",
				currentPlayerScore.LongScore,
				"\nRank:",
				currentPlayerScore.Rank
			}));
		}
	}

	private void OnAchievementsLoaded(Result result)
	{
		ISN_Logger.Log("OnAchievementsLoaded", LogType.Log);
		ISN_Logger.Log(result.IsSucceeded, LogType.Log);
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Achievements were loaded from iOS Game Center", LogType.Log);
			foreach (GK_AchievementTemplate gk_AchievementTemplate in GameCenterManager.Achievements)
			{
				ISN_Logger.Log(gk_AchievementTemplate.Id + ":  " + gk_AchievementTemplate.Progress, LogType.Log);
			}
		}
	}

	private void HandleOnAchievementsReset(Result obj)
	{
		ISN_Logger.Log("All Achievements were reset", LogType.Log);
	}

	private void HandleOnAchievementsProgress(GK_AchievementProgressResult result)
	{
		if (result.IsSucceeded)
		{
			GK_AchievementTemplate achievement = result.Achievement;
			ISN_Logger.Log(achievement.Id + ":  " + achievement.Progress.ToString(), LogType.Log);
		}
	}

	private void GameCenterManager_OnGameCenterViewDismissed()
	{
		UnityEngine.Debug.Log("GameCenterManager ViewDismissed");
		this._IsUILocked = false;
	}

	private int hiScore = 200;

	private bool _IsUILocked;

	private string TEST_LEADERBOARD_1 = "your.ios.leaderbord1.id";

	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";

	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";
}
