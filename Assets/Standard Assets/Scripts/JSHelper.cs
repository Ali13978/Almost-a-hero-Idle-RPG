using System;
using SA.Common.Models;
using UnityEngine;

public class JSHelper : MonoBehaviour
{
	[Obsolete("InitGameCenter is deprecated, please use InitGameCenter() instead")]
	private void InitGameCneter()
	{
		this.InitGameCenter();
	}

	public void InitGameCenter()
	{
		GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_1_ID);
		GameCenterManager.RegisterAchievement(this.TEST_ACHIEVEMENT_2_ID);
		GameCenterManager.OnAchievementsLoaded += this.HandleOnAchievementsLoaded;
		GameCenterManager.OnAchievementsProgress += this.HandleOnAchievementsProgress;
		GameCenterManager.OnAchievementsReset += this.HandleOnAchievementsReset;
		GameCenterManager.OnScoreSubmitted += this.OnScoreSubmitted;
		GameCenterManager.OnAuthFinished += this.HandleOnAuthFinished;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		GameCenterManager.Init();
		ISN_Logger.Log("InitGameCenter", LogType.Log);
	}

	private void SubmitScore(int val)
	{
		ISN_Logger.Log("SubmitScore", LogType.Log);
		GameCenterManager.ReportScore((long)val, this.leaderboardId, 0L);
	}

	private void SubmitAchievement(string data)
	{
		string[] array = data.Split(new char[]
		{
			"|"[0]
		});
		float percent = Convert.ToSingle(array[0]);
		string text = array[1];
		ISN_Logger.Log("SubmitAchievement: " + text + "  " + percent.ToString(), LogType.Log);
		GameCenterManager.SubmitAchievement(percent, text);
	}

	private void HandleOnAchievementsLoaded(Result res)
	{
		ISN_Logger.Log("Achievements loaded from iOS Game Center", LogType.Log);
		foreach (GK_AchievementTemplate gk_AchievementTemplate in GameCenterManager.Achievements)
		{
			ISN_Logger.Log(gk_AchievementTemplate.Id + ":  " + gk_AchievementTemplate.Progress, LogType.Log);
		}
	}

	private void HandleOnAchievementsProgress(GK_AchievementProgressResult progress)
	{
		ISN_Logger.Log("OnAchievementProgress", LogType.Log);
		GK_AchievementTemplate achievement = progress.Achievement;
		ISN_Logger.Log(achievement.Id + ":  " + achievement.Progress.ToString(), LogType.Log);
	}

	private void HandleOnAchievementsReset(Result res)
	{
		ISN_Logger.Log("All Achievements were reset", LogType.Log);
	}

	private void OnScoreSubmitted(GK_LeaderboardResult result)
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
		}
	}

	private void HandleOnAuthFinished(Result r)
	{
		if (r.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authenticated", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
		}
	}

	private string leaderboardId = "your.leaderboard1.id.here";

	private string TEST_ACHIEVEMENT_1_ID = "your.achievement.id1.here";

	private string TEST_ACHIEVEMENT_2_ID = "your.achievement.id2.here";
}
