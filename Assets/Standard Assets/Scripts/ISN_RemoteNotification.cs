using System;

public class ISN_RemoteNotification
{
	public ISN_RemoteNotification(string body)
	{
		this._Body = body;
	}

	public string Body
	{
		get
		{
			return this._Body;
		}
	}

	private string _Body = "{}";
}
