using System;
using System.Collections.Generic;
using SA.Common.Models;

public class GK_TBM_LoadMatchesResult : Result
{
	public GK_TBM_LoadMatchesResult(bool IsResultSucceeded)
	{
	}

	public GK_TBM_LoadMatchesResult(string errorData) : base(new Error(errorData))
	{
	}

	public Dictionary<string, GK_TBM_Match> LoadedMatches = new Dictionary<string, GK_TBM_Match>();
}
