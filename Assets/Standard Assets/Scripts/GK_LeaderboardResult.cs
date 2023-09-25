using System;
using SA.Common.Models;

public class GK_LeaderboardResult : Result
{
	public GK_LeaderboardResult(GK_Leaderboard leaderboard)
	{
		this.Setinfo(leaderboard);
	}

	public GK_LeaderboardResult(GK_Leaderboard leaderboard, Error error) : base(error)
	{
		this.Setinfo(leaderboard);
	}

	private void Setinfo(GK_Leaderboard leaderboard)
	{
		this._Leaderboard = leaderboard;
	}

	public GK_Leaderboard Leaderboard
	{
		get
		{
			return this._Leaderboard;
		}
	}

	private GK_Leaderboard _Leaderboard;
}
