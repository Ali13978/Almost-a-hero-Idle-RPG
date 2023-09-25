using System;
using SA.Common.Models;
using UnityEngine;

public class GameCenterExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		if (!GameCenterExample.IsInitialized)
		{
			GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_1_ID);
			GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_2_ID);
			GameCenterManager.OnAchievementsProgress += this.HandleOnAchievementsProgress;
			GameCenterManager.OnAchievementsReset += this.HandleOnAchievementsReset;
			GameCenterManager.OnAchievementsLoaded += this.OnAchievementsLoaded;
			GameCenterManager.OnScoreSubmitted += this.OnScoreSubmitted;
			GameCenterManager.OnLeadrboardInfoLoaded += this.OnLeadrboardInfoLoaded;
			GameCenterManager.OnAuthFinished += this.OnAuthFinished;
			GameCenterManager.Init();
			GameCenterExample.IsInitialized = true;
		}
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		if (GameCenterManager.Player != null)
		{
			GUI.Label(new Rect(100f, 10f, (float)Screen.width, 40f), "ID: " + GameCenterManager.Player.Id);
			GUI.Label(new Rect(100f, 20f, (float)Screen.width, 40f), "Name: " + GameCenterManager.Player.DisplayName);
			GUI.Label(new Rect(100f, 30f, (float)Screen.width, 40f), "Alias: " + GameCenterManager.Player.Alias);
			if (GameCenterManager.Player.SmallPhoto != null)
			{
				GUI.DrawTexture(new Rect(10f, 10f, 75f, 75f), GameCenterManager.Player.SmallPhoto);
			}
		}
		this.StartY += this.YLableStep;
		this.StartY += this.YLableStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Game Center Leaderboards", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Leaderboards"))
		{
			GameCenterManager.ShowLeaderboards();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Sets Info"))
		{
			GameCenterManager.OnLeaderboardSetsInfoLoaded += this.OnLeaderboardSetsInfoLoaded;
			GameCenterManager.LoadLeaderboardSetInfo();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Leaderboard 1"))
		{
			GameCenterManager.ShowLeaderboard(this.TEST_LEADERBOARD_1);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Report Score LB 1"))
		{
			this.hiScore++;
			GameCenterManager.ReportScore((long)this.hiScore, this.TEST_LEADERBOARD_1, 17L);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Score LB 1"))
		{
			GameCenterManager.LoadLeaderboardInfo(this.TEST_LEADERBOARD_1);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Leaderboard #2, user best score: " + GameCenterExample.LB2BestScores.ToString(), this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Leader Board2"))
		{
			GameCenterManager.ShowLeaderboard(this.TEST_LEADERBOARD_2);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Leaderboard 2 Today"))
		{
			GameCenterManager.ShowLeaderboard(this.TEST_LEADERBOARD_2, GK_TimeSpan.TODAY);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Report Score LB2"))
		{
			this.hiScore++;
			GameCenterManager.OnScoreSubmitted += this.OnScoreSubmitted;
			GameCenterManager.ReportScore((double)this.hiScore, this.TEST_LEADERBOARD_2);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Score LB 2"))
		{
			GameCenterManager.LoadLeaderboardInfo(this.TEST_LEADERBOARD_2);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Send Challenge"))
		{
			GameCenterManager.IssueLeaderboardChallenge(this.TEST_LEADERBOARD_2, "Here's a tiny challenge for you");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Game Center Achievements", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Achievements"))
		{
			GameCenterManager.ShowAchievements();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Reset Achievements"))
		{
			GameCenterManager.ResetAchievements();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Submit Achievements1"))
		{
			GameCenterManager.SubmitAchievement(GameCenterManager.GetAchievementProgress(this.TEST_ACHIEVEMENT_1_ID) + 2.432f, this.TEST_ACHIEVEMENT_1_ID);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Submit Achievements2"))
		{
			GameCenterManager.SubmitAchievement(88.66f, this.TEST_ACHIEVEMENT_2_ID, false);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Send Challenge"))
		{
			GameCenterManager.IssueAchievementChallenge(this.TEST_ACHIEVEMENT_1_ID, "Here's a tiny challenge for you");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "More", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Retrieve Signature"))
		{
			GameCenterManager.RetrievePlayerSignature();
			GameCenterManager.OnPlayerSignatureRetrieveResult += this.OnPlayerSignatureRetrieveResult;
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

	private void OnLeaderboardSetsInfoLoaded(Result res)
	{
		ISN_Logger.Log("OnLeaderboardSetsInfoLoaded", LogType.Log);
		GameCenterManager.OnLeaderboardSetsInfoLoaded -= this.OnLeaderboardSetsInfoLoaded;
		if (res.IsSucceeded)
		{
			foreach (GK_LeaderboardSet gk_LeaderboardSet in GameCenterManager.LeaderboardSets)
			{
				ISN_Logger.Log(gk_LeaderboardSet.Title, LogType.Log);
				ISN_Logger.Log(gk_LeaderboardSet.Identifier, LogType.Log);
				ISN_Logger.Log(gk_LeaderboardSet.GroupIdentifier, LogType.Log);
			}
		}
		if (GameCenterManager.LeaderboardSets.Count == 0)
		{
			return;
		}
		GK_LeaderboardSet gk_LeaderboardSet2 = GameCenterManager.LeaderboardSets[0];
		gk_LeaderboardSet2.OnLoaderboardsInfoLoaded += this.OnLoaderboardsInfoLoaded;
		gk_LeaderboardSet2.LoadLeaderBoardsInfo();
	}

	private void OnLoaderboardsInfoLoaded(ISN_LoadSetLeaderboardsInfoResult res)
	{
		res.LeaderBoardsSet.OnLoaderboardsInfoLoaded -= this.OnLoaderboardsInfoLoaded;
		if (res.IsSucceeded)
		{
			foreach (GK_LeaderBoardInfo gk_LeaderBoardInfo in res.LeaderBoardsSet.BoardsInfo)
			{
				ISN_Logger.Log(gk_LeaderBoardInfo.Title, LogType.Log);
				ISN_Logger.Log(gk_LeaderBoardInfo.Description, LogType.Log);
				ISN_Logger.Log(gk_LeaderBoardInfo.Identifier, LogType.Log);
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

	private void OnLeadrboardInfoLoaded(GK_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			GK_Score currentPlayerScore = result.Leaderboard.GetCurrentPlayerScore(GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
			IOSNativePopUpManager.showMessage("Leaderboard " + currentPlayerScore.LeaderboardId, string.Concat(new object[]
			{
				"Score: ",
				currentPlayerScore.LongScore,
				"\nRank:",
				currentPlayerScore.Rank
			}));
			ISN_Logger.Log("double score representation: " + currentPlayerScore.DecimalFloat_2, LogType.Log);
			ISN_Logger.Log("long score representation: " + currentPlayerScore.LongScore, LogType.Log);
			if (currentPlayerScore.LeaderboardId.Equals(this.TEST_LEADERBOARD_2))
			{
				ISN_Logger.Log("Updating leaderboard 2 score", LogType.Log);
				GameCenterExample.LB2BestScores = currentPlayerScore.LongScore;
			}
		}
	}

	private void OnScoreSubmitted(Result result)
	{
		GameCenterManager.OnScoreSubmitted -= this.OnScoreSubmitted;
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Score Submitted", LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Score Submit Failed", LogType.Log);
		}
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nAlias: " + GameCenterManager.Player.Alias);
			GameCenterManager.LoadLeaderboardInfo(this.TEST_LEADERBOARD_1);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void OnPlayerSignatureRetrieveResult(GK_PlayerSignatureResult result)
	{
		ISN_Logger.Log("OnPlayerSignatureRetrieveResult", LogType.Log);
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("PublicKeyUrl: " + result.PublicKeyUrl, LogType.Log);
			ISN_Logger.Log("Signature: " + result.Signature, LogType.Log);
			ISN_Logger.Log("Salt: " + result.Salt, LogType.Log);
			ISN_Logger.Log("Timestamp: " + result.Timestamp, LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Error code: " + result.Error.Code, LogType.Log);
			ISN_Logger.Log("Error description: " + result.Error.Message, LogType.Log);
		}
		GameCenterManager.OnPlayerSignatureRetrieveResult -= this.OnPlayerSignatureRetrieveResult;
	}

	private int hiScore = 200;

	private string TEST_LEADERBOARD_1 = "your.ios.leaderbord1.id";

	private string TEST_LEADERBOARD_2 = "combined.board.1";

	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";

	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";

	private static bool IsInitialized;

	private static long LB2BestScores;
}
