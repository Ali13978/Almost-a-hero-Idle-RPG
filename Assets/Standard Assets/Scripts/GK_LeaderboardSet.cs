using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;

public class GK_LeaderboardSet
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<ISN_LoadSetLeaderboardsInfoResult> OnLoaderboardsInfoLoaded;



	public void LoadLeaderBoardsInfo()
	{
		GameCenterManager.LoadLeaderboardsForSet(this.Identifier);
	}

	public void AddBoardInfo(GK_LeaderBoardInfo info)
	{
		this._BoardsInfo.Add(info);
	}

	public void SendFailLoadEvent()
	{
		ISN_LoadSetLeaderboardsInfoResult obj = new ISN_LoadSetLeaderboardsInfoResult(this, new Error());
		this.OnLoaderboardsInfoLoaded(obj);
	}

	public void SendSuccessLoadEvent()
	{
		ISN_LoadSetLeaderboardsInfoResult obj = new ISN_LoadSetLeaderboardsInfoResult(this);
		this.OnLoaderboardsInfoLoaded(obj);
	}

	public List<GK_LeaderBoardInfo> BoardsInfo
	{
		get
		{
			return this._BoardsInfo;
		}
	}

	public string Title;

	public string Identifier;

	public string GroupIdentifier;

	public List<GK_LeaderBoardInfo> _BoardsInfo = new List<GK_LeaderBoardInfo>();
}
