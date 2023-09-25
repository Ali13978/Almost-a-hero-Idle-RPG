using System;
using System.Collections.Generic;
using SA.Common.Data;

public class GK_RTM_Match
{
	public GK_RTM_Match(string matchData)
	{
		string[] array = matchData.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		this._ExpectedPlayerCount = Convert.ToInt32(array[0]);
		string[] array2 = Converter.ParseArray(array[1], "%%%");
		foreach (string playerID in array2)
		{
			GK_Player playerById = GameCenterManager.GetPlayerById(playerID);
			this._Players.Add(playerById);
		}
	}

	public int ExpectedPlayerCount
	{
		get
		{
			return this._ExpectedPlayerCount;
		}
	}

	public List<GK_Player> Players
	{
		get
		{
			return this._Players;
		}
	}

	private int _ExpectedPlayerCount;

	private List<GK_Player> _Players = new List<GK_Player>();
}
