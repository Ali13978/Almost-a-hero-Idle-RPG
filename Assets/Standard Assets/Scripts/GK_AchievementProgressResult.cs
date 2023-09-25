using System;
using SA.Common.Models;

public class GK_AchievementProgressResult : Result
{
	public GK_AchievementProgressResult(GK_AchievementTemplate tpl)
	{
		this._tpl = tpl;
	}

	public GK_AchievementTemplate info
	{
		get
		{
			return this._tpl;
		}
	}

	public GK_AchievementTemplate Achievement
	{
		get
		{
			return this._tpl;
		}
	}

	private GK_AchievementTemplate _tpl;
}
