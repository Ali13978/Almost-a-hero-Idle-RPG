using System;
using SA.Common.Models;

public class GK_UserInfoLoadResult : Result
{
	public GK_UserInfoLoadResult(GK_Player tpl)
	{
		this._tpl = tpl;
	}

	public GK_UserInfoLoadResult(string id) : base(new Error(0, "unknown erro"))
	{
		this._playerId = id;
	}

	public string playerId
	{
		get
		{
			return this._playerId;
		}
	}

	public GK_Player playerTemplate
	{
		get
		{
			return this._tpl;
		}
	}

	private string _playerId;

	private GK_Player _tpl;
}
