using System;
using SA.Common.Models;

public class GK_SaveDataLoaded : Result
{
	public GK_SaveDataLoaded(GK_SavedGame save)
	{
		this._SavedGame = save;
	}

	public GK_SaveDataLoaded(Error error) : base(error)
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
