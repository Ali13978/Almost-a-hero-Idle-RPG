using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GK_Leaderboard
{
	public GK_Leaderboard(string leaderboardId)
	{
		this._info = new GK_LeaderBoardInfo();
		this._info.Identifier = leaderboardId;
	}

	public void Refresh()
	{
		this.SocsialCollection = new GK_ScoreCollection();
		this.GlobalCollection = new GK_ScoreCollection();
		this.CurrentPlayerScore = new List<GK_Score>();
		this.ScoreUpdateListners = new Dictionary<int, GK_LocalPlayerScoreUpdateListener>();
	}

	public GK_Score GetCurrentPlayerScore(GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		foreach (GK_Score gk_Score in this.CurrentPlayerScore)
		{
			if (gk_Score.TimeSpan == timeSpan && gk_Score.Collection == collection)
			{
				return gk_Score;
			}
		}
		return null;
	}

	public GK_Score GetScoreByPlayerId(string playerId, GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		if (playerId.Equals(GameCenterManager.Player.Id))
		{
			return this.GetCurrentPlayerScore(timeSpan, collection);
		}
		List<GK_Score> scoresList = this.GetScoresList(timeSpan, collection);
		foreach (GK_Score gk_Score in scoresList)
		{
			if (gk_Score.PlayerId.Equals(playerId))
			{
				return gk_Score;
			}
		}
		return null;
	}

	public List<GK_Score> GetScoresList(GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		GK_ScoreCollection gk_ScoreCollection = this.GlobalCollection;
		if (collection != GK_CollectionType.GLOBAL)
		{
			if (collection == GK_CollectionType.FRIENDS)
			{
				gk_ScoreCollection = this.SocsialCollection;
			}
		}
		else
		{
			gk_ScoreCollection = this.GlobalCollection;
		}
		Dictionary<int, GK_Score> dictionary = gk_ScoreCollection.AllTimeScores;
		if (timeSpan != GK_TimeSpan.ALL_TIME)
		{
			if (timeSpan != GK_TimeSpan.TODAY)
			{
				if (timeSpan == GK_TimeSpan.WEEK)
				{
					dictionary = gk_ScoreCollection.WeekScores;
				}
			}
			else
			{
				dictionary = gk_ScoreCollection.TodayScores;
			}
		}
		else
		{
			dictionary = gk_ScoreCollection.AllTimeScores;
		}
		List<GK_Score> list = new List<GK_Score>();
		list.AddRange(dictionary.Values);
		return list;
	}

	public GK_Score GetScore(int rank, GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		GK_ScoreCollection gk_ScoreCollection = this.GlobalCollection;
		if (collection != GK_CollectionType.GLOBAL)
		{
			if (collection == GK_CollectionType.FRIENDS)
			{
				gk_ScoreCollection = this.SocsialCollection;
			}
		}
		else
		{
			gk_ScoreCollection = this.GlobalCollection;
		}
		Dictionary<int, GK_Score> dictionary = gk_ScoreCollection.AllTimeScores;
		if (timeSpan != GK_TimeSpan.ALL_TIME)
		{
			if (timeSpan != GK_TimeSpan.TODAY)
			{
				if (timeSpan == GK_TimeSpan.WEEK)
				{
					dictionary = gk_ScoreCollection.WeekScores;
				}
			}
			else
			{
				dictionary = gk_ScoreCollection.TodayScores;
			}
		}
		else
		{
			dictionary = gk_ScoreCollection.AllTimeScores;
		}
		if (dictionary.ContainsKey(rank))
		{
			return dictionary[rank];
		}
		return null;
	}

	public GK_LeaderBoardInfo Info
	{
		get
		{
			return this._info;
		}
	}

	public string Id
	{
		get
		{
			return this._info.Identifier;
		}
	}

	public bool CurrentPlayerScoreLoaded
	{
		get
		{
			return this._CurrentPlayerScoreLoaded;
		}
	}

	public void CreateScoreListener(int requestId, bool isInternal)
	{
		GK_LocalPlayerScoreUpdateListener gk_LocalPlayerScoreUpdateListener = new GK_LocalPlayerScoreUpdateListener(requestId, this.Id, isInternal);
		this.ScoreUpdateListners.Add(gk_LocalPlayerScoreUpdateListener.RequestId, gk_LocalPlayerScoreUpdateListener);
	}

	public void ReportLocalPlayerScoreUpdate(GK_Score score, int requestId)
	{
		GK_LocalPlayerScoreUpdateListener gk_LocalPlayerScoreUpdateListener = this.ScoreUpdateListners[requestId];
		gk_LocalPlayerScoreUpdateListener.ReportScoreUpdate(score);
	}

	public void UpdateCurrentPlayerScore(List<GK_Score> newScores)
	{
		this.CurrentPlayerScore.Clear();
		foreach (GK_Score item in newScores)
		{
			this.CurrentPlayerScore.Add(item);
		}
		this._CurrentPlayerScoreLoaded = true;
	}

	public void UpdateCurrentPlayerScore(GK_Score score)
	{
		GK_Score currentPlayerScore = this.GetCurrentPlayerScore(score.TimeSpan, score.Collection);
		if (currentPlayerScore != null)
		{
			this.CurrentPlayerScore.Remove(currentPlayerScore);
		}
		this.CurrentPlayerScore.Add(score);
		this._CurrentPlayerScoreLoaded = true;
	}

	public void ReportLocalPlayerScoreUpdateFail(string errorData, int requestId)
	{
		GK_LocalPlayerScoreUpdateListener gk_LocalPlayerScoreUpdateListener = this.ScoreUpdateListners[requestId];
		gk_LocalPlayerScoreUpdateListener.ReportScoreUpdateFail(errorData);
	}

	public void UpdateScore(GK_Score s)
	{
		GK_ScoreCollection gk_ScoreCollection = this.GlobalCollection;
		GK_CollectionType collection = s.Collection;
		if (collection != GK_CollectionType.GLOBAL)
		{
			if (collection == GK_CollectionType.FRIENDS)
			{
				gk_ScoreCollection = this.SocsialCollection;
			}
		}
		else
		{
			gk_ScoreCollection = this.GlobalCollection;
		}
		Dictionary<int, GK_Score> dictionary = gk_ScoreCollection.AllTimeScores;
		GK_TimeSpan timeSpan = s.TimeSpan;
		if (timeSpan != GK_TimeSpan.ALL_TIME)
		{
			if (timeSpan != GK_TimeSpan.TODAY)
			{
				if (timeSpan == GK_TimeSpan.WEEK)
				{
					dictionary = gk_ScoreCollection.WeekScores;
				}
			}
			else
			{
				dictionary = gk_ScoreCollection.TodayScores;
			}
		}
		else
		{
			dictionary = gk_ScoreCollection.AllTimeScores;
		}
		if (dictionary.ContainsKey(s.Rank))
		{
			dictionary[s.Rank] = s;
		}
		else
		{
			dictionary.Add(s.Rank, s);
		}
	}

	[Obsolete("id is depreciated, plase use Id instead")]
	public string id
	{
		get
		{
			return this._info.Identifier;
		}
	}

	public void UpdateMaxRange(int MR)
	{
		this._info.MaxRange = MR;
	}

	public bool IsOpen = true;

	private bool _CurrentPlayerScoreLoaded;

	public GK_ScoreCollection SocsialCollection = new GK_ScoreCollection();

	public GK_ScoreCollection GlobalCollection = new GK_ScoreCollection();

	private List<GK_Score> CurrentPlayerScore = new List<GK_Score>();

	private Dictionary<int, GK_LocalPlayerScoreUpdateListener> ScoreUpdateListners = new Dictionary<int, GK_LocalPlayerScoreUpdateListener>();

	[SerializeField]
	private GK_LeaderBoardInfo _info;
}
