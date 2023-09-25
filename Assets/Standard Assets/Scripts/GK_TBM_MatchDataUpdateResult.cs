using System;
using SA.Common.Models;

public class GK_TBM_MatchDataUpdateResult : Result
{
	public GK_TBM_MatchDataUpdateResult(GK_TBM_Match updatedMatch)
	{
		this._Match = updatedMatch;
	}

	public GK_TBM_MatchDataUpdateResult(string errorData) : base(new Error(errorData))
	{
	}

	public GK_TBM_MatchDataUpdateResult(Error error) : base(error)
	{
	}

	public GK_TBM_Match Match
	{
		get
		{
			return this._Match;
		}
	}

	private GK_TBM_Match _Match;
}
