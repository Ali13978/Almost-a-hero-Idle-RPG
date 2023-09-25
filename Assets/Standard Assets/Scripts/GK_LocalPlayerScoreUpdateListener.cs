using System;
using System.Collections.Generic;
using SA.Common.Models;
using UnityEngine;

public class GK_LocalPlayerScoreUpdateListener
{
	public GK_LocalPlayerScoreUpdateListener(int requestId, string leaderboardId, bool isInternal)
	{
		this._RequestId = requestId;
		this._leaderboardId = leaderboardId;
		this._IsInternal = isInternal;
	}

	public void ReportScoreUpdate(GK_Score score)
	{
		this.Scores.Add(score);
		this.DispatchUpdate();
	}

	public void ReportScoreUpdateFail(string errorData)
	{
		ISN_Logger.Log("ReportScoreUpdateFail", LogType.Log);
		this._ErrorData = errorData;
		this.Scores.Add(null);
		this.DispatchUpdate();
	}

	public int RequestId
	{
		get
		{
			return this._RequestId;
		}
	}

	private void DispatchUpdate()
	{
		if (this.Scores.Count == 6)
		{
			GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(this._leaderboardId);
			GK_LeaderboardResult result;
			if (this._ErrorData != null)
			{
				result = new GK_LeaderboardResult(leaderboard, new Error(this._ErrorData));
			}
			else
			{
				leaderboard.UpdateCurrentPlayerScore(this.Scores);
				result = new GK_LeaderboardResult(leaderboard);
			}
			GameCenterManager.DispatchLeaderboardUpdateEvent(result, this._IsInternal);
		}
	}

	private int _RequestId;

	private bool _IsInternal;

	private string _leaderboardId;

	private string _ErrorData;

	private List<GK_Score> Scores = new List<GK_Score>();
}
