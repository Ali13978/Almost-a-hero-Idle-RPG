using System;
using System.Diagnostics;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class SK_CloudService : Singleton<SK_CloudService>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<SK_AuthorizationResult> OnAuthorizationFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<SK_RequestCapabilitieResult> OnCapabilitiesRequestFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<SK_RequestStorefrontIdentifierResult> OnStorefrontIdentifierRequestFinished;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RequestAuthorization()
	{
		BillingNativeBridge.CloudService_RequestAuthorization();
	}

	public void RequestCapabilities()
	{
		BillingNativeBridge.CloudService_RequestCapabilities();
	}

	public void RequestStorefrontIdentifier()
	{
		BillingNativeBridge.CloudService_RequestStorefrontIdentifier();
	}

	public static int AuthorizationStatus
	{
		get
		{
			return BillingNativeBridge.CloudService_AuthorizationStatus();
		}
	}

	private void Event_AuthorizationFinished(string data)
	{
		int status = Convert.ToInt32(data);
		SK_AuthorizationResult obj = new SK_AuthorizationResult((SK_CloudServiceAuthorizationStatus)status);
		SK_CloudService.OnAuthorizationFinished(obj);
	}

	private void Event_RequestCapabilitieSsuccess(string data)
	{
		int capability = Convert.ToInt32(data);
		SK_RequestCapabilitieResult obj = new SK_RequestCapabilitieResult((SK_CloudServiceCapability)capability);
		SK_CloudService.OnCapabilitiesRequestFinished(obj);
	}

	private void Event_RequestCapabilitiesFailed(string errorData)
	{
		SK_RequestCapabilitieResult obj = new SK_RequestCapabilitieResult(errorData);
		SK_CloudService.OnCapabilitiesRequestFinished(obj);
	}

	private void Event_RequestStorefrontIdentifierSsuccess(string storefrontIdentifier)
	{
		SK_RequestStorefrontIdentifierResult sk_RequestStorefrontIdentifierResult = new SK_RequestStorefrontIdentifierResult();
		sk_RequestStorefrontIdentifierResult.StorefrontIdentifier = storefrontIdentifier;
		SK_CloudService.OnStorefrontIdentifierRequestFinished(sk_RequestStorefrontIdentifierResult);
	}

	private void Event_RequestStorefrontIdentifierFailed(string errorData)
	{
		SK_RequestStorefrontIdentifierResult obj = new SK_RequestStorefrontIdentifierResult(errorData);
		SK_CloudService.OnStorefrontIdentifierRequestFinished(obj);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static SK_CloudService()
	{
		SK_CloudService.OnAuthorizationFinished = delegate(SK_AuthorizationResult A_0)
		{
		};
		SK_CloudService.OnCapabilitiesRequestFinished = delegate(SK_RequestCapabilitieResult A_0)
		{
		};
		SK_CloudService.OnStorefrontIdentifierRequestFinished = delegate(SK_RequestStorefrontIdentifierResult A_0)
		{
		};
	}
}
