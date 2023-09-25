using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GameCenter_TBM : Singleton<GameCenter_TBM>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_LoadMatchResult> ActionMatchInfoLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_LoadMatchesResult> ActionMatchesInfoLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchDataUpdateResult> ActionMatchDataUpdated;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchInitResult> ActionMatchFound;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchQuitResult> ActionMatchQuit;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_EndTrunResult> ActionTrunEnded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchEndResult> ActionMacthEnded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_RematchResult> ActionRematched;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchRemovedResult> ActionMatchRemoved;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchInitResult> ActionMatchInvitationAccepted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchRemovedResult> ActionMatchInvitationDeclined;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_Match> ActionPlayerQuitForMatch;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<GK_TBM_MatchTurnResult> ActionTrunReceived;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadMatchesInfo()
	{
	}

	public void LoadMatch(string matchId)
	{
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

	public void SaveCurrentTurn(string matchId, byte[] matchData)
	{
	}

	public void EndTurn(string matchId, byte[] matchData, string nextPlayerId)
	{
	}

	public void QuitInTurn(string matchId, GK_TurnBasedMatchOutcome outcome, string nextPlayerId, byte[] matchData)
	{
	}

	public void QuitOutOfTurn(string matchId, GK_TurnBasedMatchOutcome outcome)
	{
	}

	public void EndMatch(string matchId, byte[] matchData)
	{
	}

	public void Rematch(string matchId)
	{
	}

	public void RemoveMatch(string matchId)
	{
	}

	public void AcceptInvite(string matchId)
	{
	}

	public void DeclineInvite(string matchId)
	{
	}

	public void UpdateParticipantOutcome(string matchId, int outcome, string playerId)
	{
	}

	public GK_TBM_Match GetMatchById(string matchId)
	{
		if (this._Matches.ContainsKey(matchId))
		{
			return this._Matches[matchId];
		}
		return null;
	}

	public static void PrintMatchInfo(GK_TBM_Match match)
	{
		string text = string.Empty;
		text += "----------------------------------------\n";
		text += "Printing basic match info, for \n";
		text = text + "Match ID: " + match.Id + "\n";
		text = string.Concat(new object[]
		{
			text,
			"Status:",
			match.Status,
			"\n"
		});
		if (match.CurrentParticipant != null)
		{
			if (match.CurrentParticipant.Player != null)
			{
				text = text + "CurrentPlayerID: " + match.CurrentParticipant.Player.Id + "\n";
			}
			else
			{
				text += "CurrentPlayerID: ---- \n";
			}
		}
		else
		{
			text += "CurrentPlayerID: ---- \n";
		}
		text = text + "Data: " + match.UTF8StringData + "\n";
		text += "*******Participants*******\n";
		foreach (GK_TBM_Participant gk_TBM_Participant in match.Participants)
		{
			if (gk_TBM_Participant.Player != null)
			{
				text = text + "PlayerId: " + gk_TBM_Participant.Player.Id + "\n";
			}
			else
			{
				text += "PlayerId: ---  \n";
			}
			text = string.Concat(new object[]
			{
				text,
				"Status: ",
				gk_TBM_Participant.Status,
				"\n"
			});
			text = string.Concat(new object[]
			{
				text,
				"MatchOutcome: ",
				gk_TBM_Participant.MatchOutcome,
				"\n"
			});
			text = text + "TimeoutDate: " + gk_TBM_Participant.TimeoutDate.ToString("DD MMM YYYY HH:mm:ss") + "\n";
			text = text + "LastTurnDate: " + gk_TBM_Participant.LastTurnDate.ToString("DD MMM YYYY HH:mm:ss") + "\n";
			text += "**********************\n";
		}
		text += "----------------------------------------\n";
		ISN_Logger.Log(text, LogType.Log);
	}

	public Dictionary<string, GK_TBM_Match> Matches
	{
		get
		{
			return this._Matches;
		}
	}

	public List<GK_TBM_Match> MatchesList
	{
		get
		{
			List<GK_TBM_Match> list = new List<GK_TBM_Match>();
			foreach (KeyValuePair<string, GK_TBM_Match> keyValuePair in this._Matches)
			{
				list.Add(keyValuePair.Value);
			}
			return list;
		}
	}

	public void OnLoadMatchesResult(string data)
	{
		ISN_Logger.Log("TBM::OnLoadMatchesResult: " + data, LogType.Log);
		GK_TBM_LoadMatchesResult gk_TBM_LoadMatchesResult = new GK_TBM_LoadMatchesResult(true);
		this._Matches = new Dictionary<string, GK_TBM_Match>();
		if (data.Length == 0)
		{
			GameCenter_TBM.ActionMatchesInfoLoaded(gk_TBM_LoadMatchesResult);
			return;
		}
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		if (array.Length > 0)
		{
			gk_TBM_LoadMatchesResult.LoadedMatches = new Dictionary<string, GK_TBM_Match>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == "endofline")
				{
					break;
				}
				GK_TBM_Match gk_TBM_Match = GameCenter_TBM.ParceMatchInfo(array[i]);
				this.UpdateMatchInfo(gk_TBM_Match);
				gk_TBM_LoadMatchesResult.LoadedMatches.Add(gk_TBM_Match.Id, gk_TBM_Match);
			}
		}
		GameCenter_TBM.ActionMatchesInfoLoaded(gk_TBM_LoadMatchesResult);
	}

	private void OnLoadMatchesResultFailed(string errorData)
	{
		GK_TBM_LoadMatchesResult obj = new GK_TBM_LoadMatchesResult(errorData);
		GameCenter_TBM.ActionMatchesInfoLoaded(obj);
	}

	private void OnLoadMatchResult(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		GK_TBM_LoadMatchResult obj = new GK_TBM_LoadMatchResult(match);
		GameCenter_TBM.ActionMatchInfoLoaded(obj);
	}

	private void OnLoadMatchResultFailed(string errorData)
	{
		GK_TBM_LoadMatchResult obj = new GK_TBM_LoadMatchResult(errorData);
		GameCenter_TBM.ActionMatchInfoLoaded(obj);
	}

	private void OnUpdateMatchResult(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		string text = array[0];
		GK_TBM_Match matchById = this.GetMatchById(text);
		GK_TBM_MatchDataUpdateResult obj;
		if (matchById == null)
		{
			Error error = new Error(0, "Match with id: " + text + " not found");
			obj = new GK_TBM_MatchDataUpdateResult(error);
		}
		else
		{
			matchById.SetData(array[1]);
			obj = new GK_TBM_MatchDataUpdateResult(matchById);
		}
		GameCenter_TBM.ActionMatchDataUpdated(obj);
	}

	private void OnUpdateMatchResultFailed(string errorData)
	{
		GK_TBM_MatchDataUpdateResult obj = new GK_TBM_MatchDataUpdateResult(errorData);
		GameCenter_TBM.ActionMatchDataUpdated(obj);
	}

	private void OnMatchFoundResult(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(match);
		GameCenter_TBM.ActionMatchFound(obj);
	}

	private void OnMatchFoundResultFailed(string errorData)
	{
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(errorData);
		GameCenter_TBM.ActionMatchFound(obj);
	}

	private void OnPlayerQuitForMatch(string data)
	{
		GK_TBM_Match gk_TBM_Match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(gk_TBM_Match);
		GameCenter_TBM.ActionPlayerQuitForMatch(gk_TBM_Match);
	}

	private void OnMatchQuitResult(string matchId)
	{
		GK_TBM_MatchQuitResult obj = new GK_TBM_MatchQuitResult(matchId);
		GameCenter_TBM.ActionMatchQuit(obj);
	}

	private void OnMatchQuitResultFailed(string errorData)
	{
		GK_TBM_MatchQuitResult obj = new GK_TBM_MatchQuitResult(errorData);
		GameCenter_TBM.ActionMatchQuit(obj);
	}

	private void OnEndTurnResult(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_EndTrunResult obj = new GK_TBM_EndTrunResult(match);
		GameCenter_TBM.ActionTrunEnded(obj);
	}

	private void OnEndTurnResultFailed(string errorData)
	{
		GK_TBM_EndTrunResult obj = new GK_TBM_EndTrunResult(errorData);
		GameCenter_TBM.ActionTrunEnded(obj);
	}

	private void OnEndMatch(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_MatchEndResult obj = new GK_TBM_MatchEndResult(match);
		GameCenter_TBM.ActionMacthEnded(obj);
	}

	private void OnEndMatchResult(string errorData)
	{
		GK_TBM_MatchEndResult obj = new GK_TBM_MatchEndResult(errorData);
		GameCenter_TBM.ActionMacthEnded(obj);
	}

	private void OnRematchResult(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_RematchResult obj = new GK_TBM_RematchResult(match);
		GameCenter_TBM.ActionRematched(obj);
	}

	private void OnRematchFailed(string errorData)
	{
		GK_TBM_RematchResult obj = new GK_TBM_RematchResult(errorData);
		GameCenter_TBM.ActionRematched(obj);
	}

	private void OnMatchRemoved(string matchId)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(matchId);
		if (this._Matches.ContainsKey(matchId))
		{
			this._Matches.Remove(matchId);
		}
		GameCenter_TBM.ActionMatchRemoved(obj);
	}

	private void OnMatchRemoveFailed(string errorData)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(errorData);
		GameCenter_TBM.ActionMatchRemoved(obj);
	}

	private void OnMatchInvitationAccepted(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(match);
		GameCenter_TBM.ActionMatchInvitationAccepted(obj);
	}

	private void OnMatchInvitationAcceptedFailed(string errorData)
	{
		GK_TBM_MatchInitResult obj = new GK_TBM_MatchInitResult(errorData);
		GameCenter_TBM.ActionMatchInvitationAccepted(obj);
	}

	private void OnMatchInvitationDeclined(string matchId)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(matchId);
		if (this._Matches.ContainsKey(matchId))
		{
			this._Matches.Remove(matchId);
		}
		GameCenter_TBM.ActionMatchInvitationDeclined(obj);
	}

	private void OnMatchInvitationDeclineFailed(string errorData)
	{
		GK_TBM_MatchRemovedResult obj = new GK_TBM_MatchRemovedResult(errorData);
		GameCenter_TBM.ActionMatchInvitationDeclined(obj);
	}

	private void OnTrunReceived(string data)
	{
		GK_TBM_Match match = GameCenter_TBM.ParceMatchInfo(data);
		this.UpdateMatchInfo(match);
		GK_TBM_MatchTurnResult obj = new GK_TBM_MatchTurnResult(match);
		GameCenter_TBM.ActionTrunReceived(obj);
	}

	private void UpdateMatchInfo(GK_TBM_Match match)
	{
		if (this._Matches.ContainsKey(match.Id))
		{
			this._Matches[match.Id] = match;
		}
		else
		{
			this._Matches.Add(match.Id, match);
		}
	}

	private static GK_TBM_Match ParceMatchInfo(string data)
	{
		string[] matchData = data.Split(new char[]
		{
			'|'
		});
		return GameCenter_TBM.ParceMatchInfo(matchData, 0);
	}

	public static GK_TBM_Match ParceMatchInfo(string[] MatchData, int index)
	{
		GK_TBM_Match gk_TBM_Match = new GK_TBM_Match();
		gk_TBM_Match.Id = MatchData[index];
		gk_TBM_Match.Status = (GK_TurnBasedMatchStatus)Convert.ToInt64(MatchData[index + 1]);
		gk_TBM_Match.Message = MatchData[index + 2];
		gk_TBM_Match.CreationTimestamp = DateTime.Parse(MatchData[index + 3]);
		gk_TBM_Match.SetData(MatchData[index + 4]);
		string playerId = MatchData[index + 5];
		gk_TBM_Match.Participants = GameCenterManager.ParseParticipantsData(MatchData, index + 6);
		foreach (GK_TBM_Participant gk_TBM_Participant in gk_TBM_Match.Participants)
		{
			gk_TBM_Participant.SetMatchId(gk_TBM_Match.Id);
		}
		gk_TBM_Match.CurrentParticipant = gk_TBM_Match.GetParticipantByPlayerId(playerId);
		return gk_TBM_Match;
	}

	// Note: this type is marked as 'beforefieldinit'.
	static GameCenter_TBM()
	{
		GameCenter_TBM.ActionMatchInfoLoaded = delegate(GK_TBM_LoadMatchResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchesInfoLoaded = delegate(GK_TBM_LoadMatchesResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchDataUpdated = delegate(GK_TBM_MatchDataUpdateResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchFound = delegate(GK_TBM_MatchInitResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchQuit = delegate(GK_TBM_MatchQuitResult A_0)
		{
		};
		GameCenter_TBM.ActionTrunEnded = delegate(GK_TBM_EndTrunResult A_0)
		{
		};
		GameCenter_TBM.ActionMacthEnded = delegate(GK_TBM_MatchEndResult A_0)
		{
		};
		GameCenter_TBM.ActionRematched = delegate(GK_TBM_RematchResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchRemoved = delegate(GK_TBM_MatchRemovedResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchInvitationAccepted = delegate(GK_TBM_MatchInitResult A_0)
		{
		};
		GameCenter_TBM.ActionMatchInvitationDeclined = delegate(GK_TBM_MatchRemovedResult A_0)
		{
		};
		GameCenter_TBM.ActionPlayerQuitForMatch = delegate(GK_TBM_Match A_0)
		{
		};
		GameCenter_TBM.ActionTrunReceived = delegate(GK_TBM_MatchTurnResult A_0)
		{
		};
	}

	private Dictionary<string, GK_TBM_Match> _Matches = new Dictionary<string, GK_TBM_Match>();
}
