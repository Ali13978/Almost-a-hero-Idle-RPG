using System;
using System.Collections.Generic;
using SA.Common.Models;

public class GK_FetchResult : Result
{
	public GK_FetchResult(List<GK_SavedGame> saves)
	{
		this._SavedGames = saves;
	}

	public GK_FetchResult(string errorData) : base(new Error(errorData))
	{
	}

	public List<GK_SavedGame> SavedGames
	{
		get
		{
			return this._SavedGames;
		}
	}

	private List<GK_SavedGame> _SavedGames = new List<GK_SavedGame>();
}
