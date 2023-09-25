using System;
using SA.Common.Models;

public class GK_RTM_QueryActivityResult : Result
{
	public GK_RTM_QueryActivityResult(int activity)
	{
		this._Activity = activity;
	}

	public GK_RTM_QueryActivityResult(string errorData) : base(new Error(errorData))
	{
	}

	public int Activity
	{
		get
		{
			return this._Activity;
		}
	}

	private int _Activity;
}
