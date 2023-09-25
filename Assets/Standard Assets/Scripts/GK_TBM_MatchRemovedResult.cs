using System;
using SA.Common.Models;

public class GK_TBM_MatchRemovedResult : Result
{
	public GK_TBM_MatchRemovedResult(string matchId)
	{
		this._MatchId = matchId;
	}

	public GK_TBM_MatchRemovedResult() : base(new Error())
	{
	}

	public string MatchId
	{
		get
		{
			return this._MatchId;
		}
	}

	private string _MatchId;
}
