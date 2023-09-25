using System;
using SA.Common.Pattern;

public class GK_TBM_Participant
{
	public GK_TBM_Participant(string playerId, string status, string outcome, string timeoutDate, string lastTurnDate)
	{
		this._PlayerId = playerId;
		this._TimeoutDate = DateTime.Parse(timeoutDate);
		this._LastTurnDate = DateTime.Parse(lastTurnDate);
		this._Status = (GK_TurnBasedParticipantStatus)Convert.ToInt32(status);
		this._MatchOutcome = (GK_TurnBasedMatchOutcome)Convert.ToInt32(outcome);
	}

	public void SetOutcome(GK_TurnBasedMatchOutcome outcome)
	{
		if (this.Player == null)
		{
			return;
		}
		this._MatchOutcome = outcome;
		Singleton<GameCenter_TBM>.Instance.UpdateParticipantOutcome(this.MatchId, (int)this._MatchOutcome, this._PlayerId);
	}

	public void SetMatchId(string matchId)
	{
		this._MatchId = matchId;
	}

	public string PlayerId
	{
		get
		{
			return this._PlayerId;
		}
	}

	public GK_Player Player
	{
		get
		{
			return GameCenterManager.GetPlayerById(this._PlayerId);
		}
	}

	public string MatchId
	{
		get
		{
			return this._MatchId;
		}
	}

	public DateTime TimeoutDate
	{
		get
		{
			return this._TimeoutDate;
		}
	}

	public DateTime LastTurnDate
	{
		get
		{
			return this._LastTurnDate;
		}
	}

	public GK_TurnBasedParticipantStatus Status
	{
		get
		{
			return this._Status;
		}
	}

	public GK_TurnBasedMatchOutcome MatchOutcome
	{
		get
		{
			return this._MatchOutcome;
		}
	}

	private string _PlayerId;

	private string _MatchId;

	private DateTime _TimeoutDate;

	private DateTime _LastTurnDate;

	private GK_TurnBasedParticipantStatus _Status;

	private GK_TurnBasedMatchOutcome _MatchOutcome;
}
