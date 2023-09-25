using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenter_RTM : Singleton<GameCenter_RTM>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_RTM_MatchStartedResult> ActionMatchStarted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Error> ActionMatchFailed;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_Player, bool> ActionNearbyPlayerStateUpdated;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_RTM_QueryActivityResult> ActionActivityResultReceived;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Error> ActionDataSendError;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_Player, byte[]> ActionDataReceived;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_Player, GK_PlayerConnectionState, GK_RTM_Match> ActionPlayerStateChanged;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_Player> ActionDiconnectedPlayerReinvited;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void FindMatch(int minPlayers, int maxPlayers, string msg = "", string[] playersToInvite = null)
	{
	}

	public void FindMatchWithNativeUI(int minPlayers, int maxPlayers, string msg = "", string[] playersToInvite = null)
	{
	}

	public void SetPlayerGroup(int group)
	{
	}

	public void SetPlayerAttributes(int attributes)
	{
	}

	public void StartMatchWithInvite(GK_Invite invite, bool useNativeUI)
	{
	}

	public void CancelPendingInviteToPlayer(GK_Player player)
	{
	}

	public void CancelMatchSearch()
	{
	}

	public void FinishMatchmaking()
	{
	}

	public void QueryActivity()
	{
	}

	public void QueryPlayerGroupActivity(int group)
	{
	}

	public void StartBrowsingForNearbyPlayers()
	{
	}

	public void StopBrowsingForNearbyPlayers()
	{
	}

	public void Rematch()
	{
	}

	public void Disconnect()
	{
		this._CurrentMatch = null;
	}

	public void SendDataToAll(byte[] data, GK_MatchSendDataMode dataMode)
	{
	}

	public void SendData(byte[] data, GK_MatchSendDataMode dataMode, params GK_Player[] players)
	{
	}

	public GK_RTM_Match CurrentMatch
	{
		get
		{
			return this._CurrentMatch;
		}
	}

	public List<GK_Player> NearbyPlayersList
	{
		get
		{
			List<GK_Player> list = new List<GK_Player>();
			foreach (KeyValuePair<string, GK_Player> keyValuePair in this._NearbyPlayers)
			{
				list.Add(keyValuePair.Value);
			}
			return list;
		}
	}

	public Dictionary<string, GK_Player> NearbyPlayers
	{
		get
		{
			return this._NearbyPlayers;
		}
	}

	private void OnMatchStartFailed(string errorData)
	{
		GK_RTM_MatchStartedResult obj = new GK_RTM_MatchStartedResult(errorData);
		GameCenter_RTM.ActionMatchStarted(obj);
	}

	private void OnMatchStarted(string matchData)
	{
		GK_RTM_Match match = this.ParseMatchData(matchData);
		GK_RTM_MatchStartedResult obj = new GK_RTM_MatchStartedResult(match);
		GameCenter_RTM.ActionMatchStarted(obj);
	}

	private void OnMatchFailed(string errorData)
	{
		this._CurrentMatch = null;
		Error obj = new Error(errorData);
		GameCenter_RTM.ActionMatchFailed(obj);
	}

	private void OnNearbyPlayerInfoReceived(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		string playerID = array[0];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		bool flag = Convert.ToBoolean(array[1]);
		if (flag)
		{
			if (!this._NearbyPlayers.ContainsKey(playerById.Id))
			{
				this._NearbyPlayers.Add(playerById.Id, playerById);
			}
		}
		else if (this._NearbyPlayers.ContainsKey(playerById.Id))
		{
			this._NearbyPlayers.Remove(playerById.Id);
		}
		GameCenter_RTM.ActionNearbyPlayerStateUpdated(playerById, flag);
	}

	private void OnQueryActivity(string data)
	{
		int activity = Convert.ToInt32(data);
		GK_RTM_QueryActivityResult obj = new GK_RTM_QueryActivityResult(activity);
		GameCenter_RTM.ActionActivityResultReceived(obj);
	}

	private void OnQueryActivityFailed(string errorData)
	{
		GK_RTM_QueryActivityResult obj = new GK_RTM_QueryActivityResult(errorData);
		GameCenter_RTM.ActionActivityResultReceived(obj);
	}

	private void OnMatchInfoUpdated(string matchData)
	{
		GK_RTM_Match gk_RTM_Match = this.ParseMatchData(matchData);
		if (gk_RTM_Match.Players.Count == 0 && gk_RTM_Match.ExpectedPlayerCount == 0)
		{
			this._CurrentMatch = null;
		}
	}

	private void OnMatchPlayerStateChanged(string data)
	{
		if (this._CurrentMatch == null)
		{
			return;
		}
		string[] array = data.Split(new char[]
		{
			'|'
		});
		string playerID = array[0];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		GK_PlayerConnectionState arg = (GK_PlayerConnectionState)Convert.ToInt32(array[1]);
		GameCenter_RTM.ActionPlayerStateChanged(playerById, arg, this.CurrentMatch);
	}

	private void OnDiconnectedPlayerReinvited(string playerId)
	{
		GK_Player playerById = GameCenterManager.GetPlayerById(playerId);
		GameCenter_RTM.ActionDiconnectedPlayerReinvited(playerById);
	}

	private void OnMatchDataReceived(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		string playerID = array[0];
		GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
		byte[] arg = Convert.FromBase64String(array[1]);
		GameCenter_RTM.ActionDataReceived(playerById, arg);
	}

	private void OnSendDataError(string errorData)
	{
		Error obj = new Error(errorData);
		GameCenter_RTM.ActionDataSendError(obj);
	}

	private GK_RTM_Match ParseMatchData(string matchData)
	{
		GK_RTM_Match gk_RTM_Match = new GK_RTM_Match(matchData);
		this._CurrentMatch = gk_RTM_Match;
		return gk_RTM_Match;
	}

	// Note: this type is marked as 'beforefieldinit'.
	static GameCenter_RTM()
	{
		GameCenter_RTM.ActionMatchStarted = delegate(GK_RTM_MatchStartedResult A_0)
		{
		};
		GameCenter_RTM.ActionMatchFailed = delegate(Error A_0)
		{
		};
		GameCenter_RTM.ActionNearbyPlayerStateUpdated = delegate(GK_Player A_0, bool A_1)
		{
		};
		GameCenter_RTM.ActionActivityResultReceived = delegate(GK_RTM_QueryActivityResult A_0)
		{
		};
		GameCenter_RTM.ActionDataSendError = delegate(Error A_0)
		{
		};
		GameCenter_RTM.ActionDataReceived = delegate(GK_Player A_0, byte[] A_1)
		{
		};
		GameCenter_RTM.ActionPlayerStateChanged = delegate(GK_Player A_0, GK_PlayerConnectionState A_1, GK_RTM_Match A_2)
		{
		};
		GameCenter_RTM.ActionDiconnectedPlayerReinvited = delegate(GK_Player A_0)
		{
		};
	}

	private GK_RTM_Match _CurrentMatch;

	private Dictionary<string, GK_Player> _NearbyPlayers = new Dictionary<string, GK_Player>();
}
