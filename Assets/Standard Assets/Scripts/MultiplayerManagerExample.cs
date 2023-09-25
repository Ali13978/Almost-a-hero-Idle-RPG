using System;
using SA.Common.Pattern;
using UnityEngine;

public class MultiplayerManagerExample : MonoBehaviour
{
	private void Awake()
	{
		GameCenterManager.Init();
		GameCenter_RTM.ActionMatchStarted += this.HandleActionMatchStarted;
		GameCenter_RTM.ActionPlayerStateChanged += this.HandleActionPlayerStateChanged;
		GameCenter_RTM.ActionDataReceived += this.HandleActionDataReceived;
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients += this.HandleActionPlayerRequestedMatchWithRecipients;
		GameCenterInvitations.ActionPlayerAcceptedInvitation += this.HandleActionPlayerAcceptedInvitation;
	}

	private void HandleActionPlayerAcceptedInvitation(GK_MatchType math, GK_Invite invite)
	{
		Singleton<GameCenter_RTM>.Instance.StartMatchWithInvite(invite, true);
	}

	private void HandleActionPlayerRequestedMatchWithRecipients(GK_MatchType matchType, string[] recepientIds, GK_Player[] recepients)
	{
		ISN_Logger.Log("inictation received", LogType.Log);
		if (matchType == GK_MatchType.RealTime)
		{
			string msg = "Come play with me, bro.";
			Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(recepientIds.Length, recepientIds.Length, msg, recepientIds);
		}
	}

	private void OnGUI()
	{
	}

	private void HandleActionPlayerStateChanged(GK_Player player, GK_PlayerConnectionState state, GK_RTM_Match match)
	{
		ISN_Logger.Log(string.Concat(new object[]
		{
			"Player State Changed ",
			player.Alias,
			" state: ",
			state.ToString(),
			"\n  ExpectedPlayerCount: ",
			match.ExpectedPlayerCount
		}), LogType.Log);
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
		if (result.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Match Started", "let's play now\n  Others players count: " + result.Match.Players.Count);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Match Started Error", result.Error.Message);
		}
	}

	private void HandleActionDataReceived(GK_Player player, byte[] data)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
	}
}
