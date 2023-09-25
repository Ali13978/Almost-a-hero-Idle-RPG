using System;
using SA.Common.Models;

public class SK_AuthorizationResult : Result
{
	public SK_AuthorizationResult(SK_CloudServiceAuthorizationStatus status)
	{
		this._AuthorizationStatus = status;
	}

	public SK_CloudServiceAuthorizationStatus AuthorizationStatus
	{
		get
		{
			return this._AuthorizationStatus;
		}
	}

	private SK_CloudServiceAuthorizationStatus _AuthorizationStatus;
}
