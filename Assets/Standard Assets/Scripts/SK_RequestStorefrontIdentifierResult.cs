using System;
using SA.Common.Models;

public class SK_RequestStorefrontIdentifierResult : Result
{
	public SK_RequestStorefrontIdentifierResult()
	{
	}

	public SK_RequestStorefrontIdentifierResult(string errorData) : base(new Error(errorData))
	{
	}

	public string StorefrontIdentifier
	{
		get
		{
			return this._StorefrontIdentifier;
		}
		set
		{
			this._StorefrontIdentifier = value;
		}
	}

	private string _StorefrontIdentifier = string.Empty;
}
