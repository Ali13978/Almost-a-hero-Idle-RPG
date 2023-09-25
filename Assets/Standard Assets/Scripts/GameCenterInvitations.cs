using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Data;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenterInvitations : Singleton<GameCenterInvitations>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_Player, GK_InviteRecipientResponse> ActionInviteeResponse;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_MatchType, GK_Invite> ActionPlayerAcceptedInvitation;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_MatchType, string[], GK_Player[]> ActionPlayerRequestedMatchWithRecipients;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
	}

	private void OnInviteeResponse(string data)
	{
		ISN_Logger.Log("OnInviteeResponse", LogType.Log);
		string[] array = data.Split(new char[]
		{
			'|'
		});
		GK_Player playerById = GameCenterManager.GetPlayerById(array[0]);
		GK_InviteRecipientResponse arg = (GK_InviteRecipientResponse)Convert.ToInt32(array[1]);
		GameCenterInvitations.ActionInviteeResponse(playerById, arg);
	}

	private void OnPlayerAcceptedInvitation_RTM(string data)
	{
		ISN_Logger.Log("OnPlayerAcceptedInvitation_RTM", LogType.Log);
		GK_Invite arg = new GK_Invite(data);
		GameCenterInvitations.ActionPlayerAcceptedInvitation(GK_MatchType.RealTime, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_RTM(string data)
	{
		ISN_Logger.Log("OnPlayerRequestedMatchWithRecipients_RTM", LogType.Log);
		string[] array = Converter.ParseArray(data, "%%%");
		List<GK_Player> list = new List<GK_Player>();
		foreach (string playerID in array)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients(GK_MatchType.RealTime, array, list.ToArray());
	}

	private void OnPlayerAcceptedInvitation_TBM(string data)
	{
		ISN_Logger.Log("OnPlayerAcceptedInvitation_TBM", LogType.Log);
		GK_Invite arg = new GK_Invite(data);
		GameCenterInvitations.ActionPlayerAcceptedInvitation(GK_MatchType.TurnBased, arg);
	}

	private void OnPlayerRequestedMatchWithRecipients_TBM(string data)
	{
		ISN_Logger.Log("OnPlayerRequestedMatchWithRecipients_TBM", LogType.Log);
		string[] array = Converter.ParseArray(data, "%%%");
		List<GK_Player> list = new List<GK_Player>();
		foreach (string playerID in array)
		{
			list.Add(GameCenterManager.GetPlayerById(playerID));
		}
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients(GK_MatchType.RealTime, array, list.ToArray());
	}

	// Note: this type is marked as 'beforefieldinit'.
	static GameCenterInvitations()
	{
		GameCenterInvitations.ActionInviteeResponse = delegate(GK_Player A_0, GK_InviteRecipientResponse A_1)
		{
		};
		GameCenterInvitations.ActionPlayerAcceptedInvitation = delegate(GK_MatchType A_0, GK_Invite A_1)
		{
		};
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients = delegate(GK_MatchType A_0, string[] A_1, GK_Player[] A_2)
		{
		};
	}
}
