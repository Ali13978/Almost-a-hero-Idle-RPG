using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class TBM_Multiplayer_Example : BaseIOSFeaturePreview
{
	private void Awake()
	{
		if (!TBM_Multiplayer_Example.IsInitialized)
		{
			GameCenterManager.Init();
			GameCenterManager.OnAuthFinished += this.OnAuthFinished;
			TBM_Multiplayer_Example.IsInitialized = true;
		}
		int playerAttributes = 4;
		Singleton<GameCenter_RTM>.Instance.SetPlayerAttributes(playerAttributes);
		int minPlayers = 2;
		int maxPlayers = 2;
		string msg = "Come play with me, bro.";
		string[] playersToInvite = new string[]
		{
			GameCenterManager.FriendsList[0]
		};
		Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(minPlayers, maxPlayers, msg, playersToInvite);
		GK_Player player = GameCenterManager.Player;
		player.OnPlayerPhotoLoaded += this.HandleOnPlayerPhotoLoaded;
		player.LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
		GameCenter_RTM.ActionMatchStarted += this.HandleActionMatchStarted;
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients += this.HandleActionPlayerRequestedMatchWithRecipients;
		GameCenterInvitations.ActionPlayerAcceptedInvitation += this.HandleActionPlayerAcceptedInvitation;
		GameCenter_RTM.ActionNearbyPlayerStateUpdated += this.HandleActionNearbyPlayerStateUpdated;
		Singleton<GameCenter_RTM>.Instance.StartBrowsingForNearbyPlayers();
	}

	private void HandleActionNearbyPlayerStateUpdated(GK_Player player, bool IsAvaliable)
	{
		ISN_Logger.Log(string.Concat(new object[]
		{
			"Player: ",
			player.DisplayName,
			"IsAvaliable: ",
			IsAvaliable
		}), LogType.Log);
		ISN_Logger.Log("Nearby Players Count: " + Singleton<GameCenter_RTM>.Instance.NearbyPlayers.Count, LogType.Log);
	}

	private void HandleActionPlayerAcceptedInvitation(GK_MatchType matchType, GK_Invite invite)
	{
		if (matchType == GK_MatchType.RealTime)
		{
			bool useNativeUI = true;
			Singleton<GameCenter_RTM>.Instance.StartMatchWithInvite(invite, useNativeUI);
		}
	}

	private void HandleActionPlayerRequestedMatchWithRecipients(GK_MatchType matchType, string[] recepientIds, GK_Player[] recepients)
	{
		if (matchType == GK_MatchType.RealTime)
		{
			string msg = "Come play with me, bro.";
			Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(recepientIds.Length, recepientIds.Length, msg, recepientIds);
		}
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Match is successfully created", LogType.Log);
			if (result.Match.ExpectedPlayerCount == 0)
			{
			}
		}
		else
		{
			ISN_Logger.Log("Match is creation failed with error: " + result.Error.Message, LogType.Log);
		}
	}

	private void HandleOnPlayerPhotoLoaded(GK_UserPhotoLoadResult result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log(result.Photo, LogType.Log);
			ISN_Logger.Log(GameCenterManager.Player.BigPhoto, LogType.Log);
		}
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		if (!GameCenterManager.IsPlayerAuthenticated)
		{
			GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "TBM Multiplayer Example Scene, Authentication....", this.style);
			GUI.enabled = false;
		}
		else
		{
			GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "TBM Multiplayer Example Scene.", this.style);
			GUI.enabled = true;
		}
		GUI.enabled = true;
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Matches Info"))
		{
			Singleton<GameCenter_TBM>.Instance.LoadMatchesInfo();
			GameCenter_TBM.ActionMatchesInfoLoaded += this.ActionMatchesResultLoaded;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Create Match"))
		{
			Singleton<GameCenter_TBM>.Instance.FindMatch(2, 2, string.Empty, null);
			GameCenter_TBM.ActionMatchFound += this.ActionMatchFound;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Create Match With Native UI"))
		{
			Singleton<GameCenter_TBM>.Instance.FindMatchWithNativeUI(2, 2, string.Empty, null);
			GameCenter_TBM.ActionMatchFound += this.ActionMatchFound;
		}
		if (this.CurrentMatch == null)
		{
			GUI.enabled = false;
		}
		else
		{
			GUI.enabled = true;
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Update Match Data"))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(this.CurrentMatch.UTF8StringData + "X");
			Singleton<GameCenter_TBM>.Instance.SaveCurrentTurn(this.CurrentMatch.Id, bytes);
			GameCenter_TBM.ActionMatchDataUpdated += this.ActionMatchDataUpdated;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Make A Trunn"))
		{
			byte[] bytes2 = Encoding.UTF8.GetBytes("Some trun data");
			this.CurrentMatch.CurrentParticipant.SetOutcome(GK_TurnBasedMatchOutcome.First);
			foreach (GK_TBM_Participant gk_TBM_Participant in this.CurrentMatch.Participants)
			{
				if (!gk_TBM_Participant.PlayerId.Equals(this.CurrentMatch.CurrentParticipant.PlayerId))
				{
					Singleton<GameCenter_TBM>.Instance.EndTurn(this.CurrentMatch.Id, bytes2, gk_TBM_Participant.PlayerId);
					GameCenter_TBM.ActionTrunEnded += this.ActionTrunEnded;
					return;
				}
			}
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "End Match"))
		{
			byte[] bytes3 = Encoding.UTF8.GetBytes("End match data");
			this.CurrentMatch.Participants[0].SetOutcome(GK_TurnBasedMatchOutcome.Won);
			this.CurrentMatch.Participants[1].SetOutcome(GK_TurnBasedMatchOutcome.Lost);
			Singleton<GameCenter_TBM>.Instance.EndMatch(this.CurrentMatch.Id, bytes3);
			GameCenter_TBM.ActionMacthEnded += this.ActionMacthEnded;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Remove Match"))
		{
			Singleton<GameCenter_TBM>.Instance.RemoveMatch(this.CurrentMatch.Id);
			GameCenter_TBM.ActionMatchRemoved += this.ActionMacthRemoved;
		}
	}

	private void OnAuthFinished(Result res)
	{
		ISN_Logger.Log("Auth IsSucceeded: " + res.IsSucceeded.ToString(), LogType.Log);
	}

	public void ActionMatchesResultLoaded(GK_TBM_LoadMatchesResult res)
	{
		GameCenter_TBM.ActionMatchesInfoLoaded -= this.ActionMatchesResultLoaded;
		ISN_Logger.Log("ActionMatchesResultLoaded: " + res.IsSucceeded, LogType.Log);
		if (res.IsFailed)
		{
			return;
		}
		if (res.LoadedMatches.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<string, GK_TBM_Match> keyValuePair in res.LoadedMatches)
		{
			GK_TBM_Match value = keyValuePair.Value;
			GameCenter_TBM.PrintMatchInfo(value);
		}
	}

	private void ActionMatchDataUpdated(GK_TBM_MatchDataUpdateResult res)
	{
		GameCenter_TBM.ActionMatchDataUpdated -= this.ActionMatchDataUpdated;
		ISN_Logger.Log("ActionMatchDataUpdated: " + res.IsSucceeded, LogType.Log);
		if (res.IsFailed)
		{
			ISN_Logger.Log(res.Error.Message, LogType.Log);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(res.Match);
		}
	}

	private void ActionTrunEnded(GK_TBM_EndTrunResult result)
	{
		GameCenter_TBM.ActionTrunEnded -= this.ActionTrunEnded;
		ISN_Logger.Log("ActionTrunEnded IsSucceeded: " + result.IsSucceeded, LogType.Log);
		if (result.IsFailed)
		{
			IOSMessage.Create("ActionTrunEnded", result.Error.Message);
			ISN_Logger.Log(result.Error.Message, LogType.Log);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}

	private void ActionMacthEnded(GK_TBM_MatchEndResult result)
	{
		GameCenter_TBM.ActionMacthEnded -= this.ActionMacthEnded;
		ISN_Logger.Log("ActionMacthEnded IsSucceeded: " + result.IsSucceeded, LogType.Log);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message, LogType.Log);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}

	private void ActionMacthRemoved(GK_TBM_MatchRemovedResult result)
	{
		GameCenter_TBM.ActionMatchRemoved -= this.ActionMacthRemoved;
		ISN_Logger.Log("ActionMacthRemoved IsSucceeded: " + result.IsSucceeded, LogType.Log);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message, LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Match Id: " + result.MatchId, LogType.Log);
		}
	}

	public GK_TBM_Match CurrentMatch
	{
		get
		{
			if (Singleton<GameCenter_TBM>.Instance.Matches.Count > 0)
			{
				return Singleton<GameCenter_TBM>.Instance.MatchesList[0];
			}
			return null;
		}
	}

	private void ActionMatchFound(GK_TBM_MatchInitResult result)
	{
		GameCenter_TBM.ActionMatchFound -= this.ActionMatchFound;
		ISN_Logger.Log("ActionMatchFound IsSucceeded: " + result.IsSucceeded, LogType.Log);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message, LogType.Log);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}

	private static bool IsInitialized;
}
