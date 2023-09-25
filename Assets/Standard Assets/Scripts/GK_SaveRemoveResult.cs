using System;
using SA.Common.Models;

public class GK_SaveRemoveResult : Result
{
	public GK_SaveRemoveResult(string name)
	{
		this._SaveName = name;
	}

	public GK_SaveRemoveResult(string name, string errorData) : base(new Error(errorData))
	{
		this._SaveName = name;
	}

	public string SaveName
	{
		get
		{
			return this._SaveName;
		}
	}

	private string _SaveName = string.Empty;
}
