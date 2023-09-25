using System;
using SA.Common.Models;
using UnityEngine;

public class GameCenterFriendLoadExample : MonoBehaviour
{
	private void Awake()
	{
		GameCenterManager.OnAuthFinished += this.HandleOnAuthFinished;
		GameCenterManager.Init();
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 20f, 400f, 40f), "Friend List Load Example", this.headerStyle);
		if (GUI.Button(new Rect(300f, 10f, 150f, 50f), "Load Friends"))
		{
			GameCenterManager.OnFriendsListLoaded += this.OnFriendsListLoaded;
			GameCenterManager.RetrieveFriends();
		}
		if (GUI.Button(new Rect(500f, 10f, 150f, 50f), "Invite Friends"))
		{
			GK_FriendRequest gk_FriendRequest = new GK_FriendRequest();
			gk_FriendRequest.Send();
		}
		if (GUI.Button(new Rect(700f, 10f, 150f, 50f), "Invite with Emails"))
		{
			GK_FriendRequest gk_FriendRequest2 = new GK_FriendRequest();
			gk_FriendRequest2.addRecipientsWithEmailAddresses(new string[]
			{
				"support@stansassets.com",
				"test@test.com"
			});
			gk_FriendRequest2.Send();
		}
		if (!this.renderFriendsList)
		{
			return;
		}
		if (GUI.Button(new Rect(500f, 10f, 180f, 50f), "Leaberboard Challenge All"))
		{
			GameCenterManager.IssueLeaderboardChallenge(this.ChallengeLeaderboard, "Your message here", GameCenterManager.FriendsList.ToArray());
		}
		if (GUI.Button(new Rect(730f, 10f, 180f, 50f), "Achievement Challenge All"))
		{
			GameCenterManager.IssueAchievementChallenge(this.ChallengeAchievement, "Your message here", GameCenterManager.FriendsList.ToArray());
		}
		GUI.Label(new Rect(10f, 90f, 100f, 40f), "id", this.boardStyle);
		GUI.Label(new Rect(150f, 90f, 100f, 40f), "name", this.boardStyle);
		GUI.Label(new Rect(300f, 90f, 100f, 40f), "avatar ", this.boardStyle);
		int num = 1;
		foreach (string text in GameCenterManager.FriendsList)
		{
			GK_Player playerById = GameCenterManager.GetPlayerById(text);
			if (playerById != null)
			{
				GUI.Label(new Rect(10f, (float)(90 + 70 * num), 100f, 40f), playerById.Id, this.boardStyle);
				GUI.Label(new Rect(150f, (float)(90 + 70 * num), 100f, 40f), playerById.Alias, this.boardStyle);
				if (playerById.SmallPhoto != null)
				{
					GUI.DrawTexture(new Rect(300f, (float)(75 + 70 * num), 50f, 50f), playerById.SmallPhoto);
				}
				else
				{
					GUI.Label(new Rect(300f, (float)(90 + 70 * num), 100f, 40f), "no photo ", this.boardStyle);
				}
				if (GUI.Button(new Rect(450f, (float)(90 + 70 * num), 150f, 30f), "Challenge Leaderboard"))
				{
					GameCenterManager.IssueLeaderboardChallenge(this.ChallengeLeaderboard, "Your message here", text);
				}
				if (GUI.Button(new Rect(650f, (float)(90 + 70 * num), 150f, 30f), "Challenge Achievement"))
				{
					GameCenterManager.IssueAchievementChallenge(this.ChallengeAchievement, "Your message here", text);
				}
				num++;
			}
		}
	}

	private void HandleOnAuthFinished(Result result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Player Authed", LogType.Log);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void OnFriendsListLoaded(Result result)
	{
		GameCenterManager.OnFriendsListLoaded -= this.OnFriendsListLoaded;
		if (result.IsSucceeded)
		{
			this.renderFriendsList = true;
		}
	}

	private string ChallengeLeaderboard = "your.leaderboard2.id.here";

	private string ChallengeAchievement = "your.achievement.id.here ";

	public GUIStyle headerStyle;

	public GUIStyle boardStyle;

	private bool renderFriendsList;
}
