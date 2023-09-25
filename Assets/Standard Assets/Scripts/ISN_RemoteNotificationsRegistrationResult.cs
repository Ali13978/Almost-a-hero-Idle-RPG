using System;
using SA.Common.Models;

public class ISN_RemoteNotificationsRegistrationResult : Result
{
	public ISN_RemoteNotificationsRegistrationResult(ISN_DeviceToken token)
	{
		this._Token = token;
	}

	public ISN_RemoteNotificationsRegistrationResult(Error error) : base(error)
	{
	}

	public ISN_DeviceToken Token
	{
		get
		{
			return this._Token;
		}
	}

	private ISN_DeviceToken _Token;
}
