using System;
using SA.Common.Models;

public class ISN_LoadSetLeaderboardsInfoResult : Result
{
	public ISN_LoadSetLeaderboardsInfoResult(GK_LeaderboardSet lbset)
	{
		this._LeaderBoardsSet = lbset;
	}

	public ISN_LoadSetLeaderboardsInfoResult(GK_LeaderboardSet lbset, Error error) : base(error)
	{
		this._LeaderBoardsSet = lbset;
	}

	public GK_LeaderboardSet LeaderBoardsSet
	{
		get
		{
			return this._LeaderBoardsSet;
		}
	}

	public GK_LeaderboardSet _LeaderBoardsSet;
}
