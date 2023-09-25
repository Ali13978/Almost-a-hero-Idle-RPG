using System;
using SA.Common.Models;

public class GK_SaveResult : Result
{
	public GK_SaveResult(GK_SavedGame save)
	{
		this._SavedGame = save;
	}

	public GK_SaveResult(string errorData) : base(new Error(errorData))
	{
	}

	public GK_SaveResult(Error error) : base(error)
	{
	}

	public GK_SavedGame SavedGame
	{
		get
		{
			return this._SavedGame;
		}
	}

	private GK_SavedGame _SavedGame;
}
