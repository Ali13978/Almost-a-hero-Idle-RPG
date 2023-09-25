using System;
using SA.Common.Models;

public class SK_RequestCapabilitieResult : Result
{
	public SK_RequestCapabilitieResult(SK_CloudServiceCapability capability)
	{
		this._Capability = capability;
	}

	public SK_RequestCapabilitieResult(string errorData) : base(new Error(errorData))
	{
	}

	public SK_CloudServiceCapability Capability
	{
		get
		{
			return this._Capability;
		}
	}

	private SK_CloudServiceCapability _Capability;
}
