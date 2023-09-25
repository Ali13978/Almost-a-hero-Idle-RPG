using System;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class ISN_SoomlaGrow : Singleton<ISN_SoomlaGrow>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action ActionInitialized;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action ActionConnected;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action ActionDisconnected;

	public void CreateObject()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static void Init()
	{
	}

	private static void HandleOnVerificationComplete(VerificationResponse res)
	{
		if (res.Status != 0)
		{
			ISN_SoomlaGrow.VerificationFailed();
		}
	}

	private static void HandleOnRestoreComplete(RestoreResult res)
	{
		ISN_SoomlaGrow.RestoreFinished(res.IsSucceeded);
	}

	private static void HandleOnRestoreStarted()
	{
		ISN_SoomlaGrow.RestoreStarted();
	}

	private static void HandleOnTransactionStarted(string prodcutId)
	{
		ISN_SoomlaGrow.PurchaseStarted(prodcutId);
	}

	private static void HandleOnTransactionComplete(PurchaseResult res)
	{
		PurchaseState state = res.State;
		if (state != PurchaseState.Purchased)
		{
			if (state == PurchaseState.Failed)
			{
				if (res.Error.Code == 2)
				{
					ISN_SoomlaGrow.PurchaseCanceled(res.ProductIdentifier);
				}
				else
				{
					ISN_SoomlaGrow.PurchaseError();
				}
			}
		}
		else
		{
			Product productById = Singleton<PaymentManager>.Instance.GetProductById(res.ProductIdentifier);
			if (productById != null)
			{
				ISN_SoomlaGrow.PurchaseFinished(productById.Id, productById.PriceInMicros.ToString(), productById.CurrencyCode);
			}
		}
	}

	public static void SocialAction(ISN_SoomlaEvent soomlaEvent, ISN_SoomlaAction action, ISN_SoomlaProvider provider)
	{
	}

	private static void PurchaseStarted(string prodcutId)
	{
	}

	private static void PurchaseFinished(string prodcutId, string priceInMicros, string currency)
	{
	}

	private static void PurchaseCanceled(string prodcutId)
	{
	}

	public static void SetPurchasesSupportedState(bool isSupported)
	{
	}

	private static void PurchaseError()
	{
	}

	private static void VerificationFailed()
	{
	}

	private static void RestoreStarted()
	{
	}

	private static void RestoreFinished(bool state)
	{
	}

	public static bool IsInitialized
	{
		get
		{
			return ISN_SoomlaGrow._IsInitialized;
		}
	}

	private void OnHighWayInitialized()
	{
		ISN_SoomlaGrow.ActionInitialized();
	}

	private void OnHihgWayConnected()
	{
		ISN_SoomlaGrow.ActionConnected();
	}

	private void OnHihgWayDisconnected()
	{
		ISN_SoomlaGrow.ActionDisconnected();
	}

	private static void HandleOnInstagramPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
		else
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
		}
	}

	private static void HandleOnTwitterPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
		else
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.FAILED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
		}
	}

	private static void HandleOnInstagramPostStart()
	{
		ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.INSTAGRAM);
	}

	private static void HandleOnTwitterPostStart()
	{
		ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.TWITTER);
	}

	private static void HandleOnFacebookPostStart()
	{
		ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.STARTED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
	}

	private static void HandleOnFacebookPostResult(Result res)
	{
		if (res.IsSucceeded)
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.FINISHED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
		else
		{
			ISN_SoomlaGrow.SocialAction(ISN_SoomlaEvent.CANCELLED, ISN_SoomlaAction.UPDATE_STORY, ISN_SoomlaProvider.FACEBOOK);
		}
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_SoomlaGrow()
	{
		ISN_SoomlaGrow.ActionInitialized = delegate()
		{
		};
		ISN_SoomlaGrow.ActionConnected = delegate()
		{
		};
		ISN_SoomlaGrow.ActionDisconnected = delegate()
		{
		};
		ISN_SoomlaGrow._IsInitialized = false;
	}

	private static bool _IsInitialized;
}
