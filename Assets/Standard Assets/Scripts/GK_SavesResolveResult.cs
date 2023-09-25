using System;
using System.Collections.Generic;
using SA.Common.Models;

public class GK_SavesResolveResult : Result
{
	public GK_SavesResolveResult(List<GK_SavedGame> saves)
	{
		this._ResolvedSaves = saves;
	}

	public GK_SavesResolveResult(Error error) : base(error)
	{
	}

	public GK_SavesResolveResult(string errorData) : base(new Error(errorData))
	{
	}

	public List<GK_SavedGame> SavedGames
	{
		get
		{
			return this._ResolvedSaves;
		}
	}

	private List<GK_SavedGame> _ResolvedSaves = new List<GK_SavedGame>();
}
