using System;
using SA.Common.Models;

public class GK_RTM_MatchStartedResult : Result
{
	public GK_RTM_MatchStartedResult(GK_RTM_Match match)
	{
		this._Match = match;
	}

	public GK_RTM_MatchStartedResult(string errorData) : base(new Error(errorData))
	{
	}

	public GK_RTM_Match Match
	{
		get
		{
			return this._Match;
		}
	}

	private GK_RTM_Match _Match;
}
