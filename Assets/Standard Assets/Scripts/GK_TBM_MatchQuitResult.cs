using System;
using SA.Common.Models;

public class GK_TBM_MatchQuitResult : Result
{
	public GK_TBM_MatchQuitResult(string matchId)
	{
		this._MatchId = matchId;
	}

	public GK_TBM_MatchQuitResult() : base(new Error())
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
