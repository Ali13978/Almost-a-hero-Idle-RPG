using System;
using SA.Common.Models;
using UnityEngine;

public class LeaderboardCustomGUIExample : MonoBehaviour
{
	private void Awake()
	{
		GameCenterManager.OnAuthFinished += this.OnAuthFinished;
		GameCenterManager.OnScoresListLoaded += new Action<GK_LeaderboardResult>(this.OnScoresListLoaded);
		GameCenterManager.Init();
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 20f, 400f, 40f), "Custom Leaderboard GUI Example", this.headerStyle);
		if (GUI.Button(new Rect(400f, 10f, 150f, 50f), "Load Friends Scores"))
		{
			GameCenterManager.LoadScore(this.leaderboardId_1, 1, 10, GK_TimeSpan.ALL_TIME, GK_CollectionType.FRIENDS);
		}
		if (GUI.Button(new Rect(600f, 10f, 150f, 50f), "Load Global Scores"))
		{
			GameCenterManager.LoadScore(this.leaderboardId_1, 1, 20, GK_TimeSpan.ALL_TIME, GK_CollectionType.GLOBAL);
		}
		Color color = GUI.color;
		if (this.displayCollection == GK_CollectionType.GLOBAL)
		{
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(800f, 10f, 170f, 50f), "Displaying Global Scores"))
		{
			this.displayCollection = GK_CollectionType.GLOBAL;
		}
		GUI.color = color;
		if (this.displayCollection == GK_CollectionType.FRIENDS)
		{
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(800f, 70f, 170f, 50f), "Displaying Friends Scores"))
		{
			this.displayCollection = GK_CollectionType.FRIENDS;
		}
		GUI.color = color;
		GUI.Label(new Rect(10f, 90f, 100f, 40f), "rank", this.boardStyle);
		GUI.Label(new Rect(100f, 90f, 100f, 40f), "score", this.boardStyle);
		GUI.Label(new Rect(200f, 90f, 100f, 40f), "playerId", this.boardStyle);
		GUI.Label(new Rect(400f, 90f, 100f, 40f), "name ", this.boardStyle);
		GUI.Label(new Rect(550f, 90f, 100f, 40f), "avatar ", this.boardStyle);
		if (this.loadedLeaderboard != null)
		{
			for (int i = 1; i < 10; i++)
			{
				GK_Score score = this.loadedLeaderboard.GetScore(i, GK_TimeSpan.ALL_TIME, this.displayCollection);
				if (score != null)
				{
					GUI.Label(new Rect(10f, (float)(90 + 70 * i), 100f, 40f), i.ToString(), this.boardStyle);
					GUI.Label(new Rect(100f, (float)(90 + 70 * i), 100f, 40f), score.LongScore.ToString(), this.boardStyle);
					GUI.Label(new Rect(200f, (float)(90 + 70 * i), 100f, 40f), score.PlayerId, this.boardStyle);
					GK_Player playerById = GameCenterManager.GetPlayerById(score.PlayerId);
					if (playerById != null)
					{
						GUI.Label(new Rect(400f, (float)(90 + 70 * i), 100f, 40f), playerById.Alias, this.boardStyle);
						if (playerById.SmallPhoto != null)
						{
							GUI.DrawTexture(new Rect(550f, (float)(75 + 70 * i), 50f, 50f), playerById.SmallPhoto);
						}
						else
						{
							GUI.Label(new Rect(550f, (float)(90 + 70 * i), 100f, 40f), "no photo ", this.boardStyle);
						}
					}
					if (GUI.Button(new Rect(650f, (float)(90 + 70 * i), 100f, 30f), "Challenge"))
					{
						GameCenterManager.IssueLeaderboardChallenge(this.leaderboardId_1, "Your message here", score.PlayerId);
					}
				}
			}
		}
	}

	private void OnScoresListLoaded(Result res)
	{
		if (res.IsSucceeded)
		{
			this.loadedLeaderboard = GameCenterManager.GetLeaderboard(this.leaderboardId_1);
			IOSMessage.Create("Success", "Scores loaded");
		}
		else
		{
			IOSMessage.Create("Fail", "Failed to load scores");
		}
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private string leaderboardId_1 = "your.ios.leaderbord1.id";

	public int hiScore = 100;

	public GUIStyle headerStyle;

	public GUIStyle boardStyle;

	private GK_Leaderboard loadedLeaderboard;

	private GK_CollectionType displayCollection;
}
