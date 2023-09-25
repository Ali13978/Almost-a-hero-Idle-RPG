using System;

public class GK_Score
{
	public GK_Score(long vScore, int vRank, long vContext, GK_TimeSpan vTimeSpan, GK_CollectionType sCollection, string lid, string pid)
	{
		this._Score = vScore;
		this._Rank = vRank;
		this._Context = vContext;
		this._PlayerId = pid;
		this._LeaderboardId = lid;
		this._TimeSpan = vTimeSpan;
		this._Collection = sCollection;
	}

	public int Rank
	{
		get
		{
			return this._Rank;
		}
	}

	public long LongScore
	{
		get
		{
			return this._Score;
		}
	}

	public float CurrencyScore
	{
		get
		{
			return (float)this._Score / 100f;
		}
	}

	public float DecimalFloat_1
	{
		get
		{
			return (float)this._Score / 10f;
		}
	}

	public float DecimalFloat_2
	{
		get
		{
			return (float)this._Score / 100f;
		}
	}

	public float DecimalFloat_3
	{
		get
		{
			return (float)this._Score / 100f;
		}
	}

	public long Context
	{
		get
		{
			return this._Context;
		}
	}

	public TimeSpan Minutes
	{
		get
		{
			return System.TimeSpan.FromMinutes((double)this._Score);
		}
	}

	public TimeSpan Seconds
	{
		get
		{
			return System.TimeSpan.FromSeconds((double)this._Score);
		}
	}

	public TimeSpan Milliseconds
	{
		get
		{
			return System.TimeSpan.FromMilliseconds((double)this._Score);
		}
	}

	public string PlayerId
	{
		get
		{
			return this._PlayerId;
		}
	}

	public GK_Player Player
	{
		get
		{
			return GameCenterManager.GetPlayerById(this.PlayerId);
		}
	}

	public string LeaderboardId
	{
		get
		{
			return this._LeaderboardId;
		}
	}

	public GK_Leaderboard Leaderboard
	{
		get
		{
			return GameCenterManager.GetLeaderboard(this.LeaderboardId);
		}
	}

	public GK_CollectionType Collection
	{
		get
		{
			return this._Collection;
		}
	}

	public GK_TimeSpan TimeSpan
	{
		get
		{
			return this._TimeSpan;
		}
	}

	[Obsolete("rank is deprecated, plase use Rank instead")]
	public int rank
	{
		get
		{
			return this._Rank;
		}
	}

	[Obsolete("score is deprecated, plase use LongScore instead")]
	public long score
	{
		get
		{
			return this._Score;
		}
	}

	[Obsolete("playerId is deprecated, plase use PlayerId instead")]
	public string playerId
	{
		get
		{
			return this._PlayerId;
		}
	}

	[Obsolete("leaderboardId is deprecated, plase use LeaderboardId instead")]
	public string leaderboardId
	{
		get
		{
			return this._LeaderboardId;
		}
	}

	[Obsolete("timeSpan is deprecated, plase use TimeSpan instead")]
	public GK_TimeSpan timeSpan
	{
		get
		{
			return this._TimeSpan;
		}
	}

	[Obsolete("collection is deprecated, plase use Collection instead")]
	public GK_CollectionType collection
	{
		get
		{
			return this._Collection;
		}
	}

	private int _Rank;

	private long _Score;

	private long _Context;

	private string _PlayerId;

	private string _LeaderboardId;

	private GK_CollectionType _Collection;

	private GK_TimeSpan _TimeSpan;
}
