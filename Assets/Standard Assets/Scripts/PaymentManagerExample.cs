using System;
using System.Runtime.CompilerServices;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class PaymentManagerExample
{
	public static void init()
	{
		if (!PaymentManagerExample.IsInitialized)
		{
			Singleton<PaymentManager>.Instance.AddProductId("your.product.id1.here");
			Singleton<PaymentManager>.Instance.AddProductId("your.product.id2.here");
			if (PaymentManagerExample._003C_003Ef__mg_0024cache0 == null)
			{
				PaymentManagerExample._003C_003Ef__mg_0024cache0 = new Action<VerificationResponse>(PaymentManagerExample.HandleOnVerificationComplete);
			}
			PaymentManager.OnVerificationComplete += PaymentManagerExample._003C_003Ef__mg_0024cache0;
			if (PaymentManagerExample._003C_003Ef__mg_0024cache1 == null)
			{
				PaymentManagerExample._003C_003Ef__mg_0024cache1 = new Action<Result>(PaymentManagerExample.OnStoreKitInitComplete);
			}
			PaymentManager.OnStoreKitInitComplete += PaymentManagerExample._003C_003Ef__mg_0024cache1;
			if (PaymentManagerExample._003C_003Ef__mg_0024cache2 == null)
			{
				PaymentManagerExample._003C_003Ef__mg_0024cache2 = new Action<PurchaseResult>(PaymentManagerExample.OnTransactionComplete);
			}
			PaymentManager.OnTransactionComplete += PaymentManagerExample._003C_003Ef__mg_0024cache2;
			if (PaymentManagerExample._003C_003Ef__mg_0024cache3 == null)
			{
				PaymentManagerExample._003C_003Ef__mg_0024cache3 = new Action<RestoreResult>(PaymentManagerExample.OnRestoreComplete);
			}
			PaymentManager.OnRestoreComplete += PaymentManagerExample._003C_003Ef__mg_0024cache3;
			PaymentManagerExample.IsInitialized = true;
			Singleton<PaymentManager>.Instance.LoadStore(false);
		}
	}

	public static void buyItem(string productId)
	{
		Singleton<PaymentManager>.Instance.BuyProduct(productId);
	}

	private static void UnlockProducts(string productIdentifier)
	{
		if (productIdentifier != null)
		{
			if (!(productIdentifier == "your.product.id1.here"))
			{
				if (!(productIdentifier == "your.product.id2.here"))
				{
				}
			}
		}
		Singleton<PaymentManager>.Instance.FinishTransaction(productIdentifier);
	}

	private static void OnTransactionComplete(PurchaseResult result)
	{
		ISN_Logger.Log("OnTransactionComplete: " + result.ProductIdentifier, LogType.Log);
		ISN_Logger.Log("OnTransactionComplete: state: " + result.State, LogType.Log);
		switch (result.State)
		{
		case PurchaseState.Purchased:
		case PurchaseState.Restored:
			PaymentManagerExample.UnlockProducts(result.ProductIdentifier);
			break;
		case PurchaseState.Failed:
			ISN_Logger.Log("Transaction failed with error, code: " + result.Error.Code, LogType.Log);
			ISN_Logger.Log("Transaction failed with error, description: " + result.Error.Message, LogType.Log);
			break;
		}
		if (result.State == PurchaseState.Failed)
		{
			IOSNativePopUpManager.showMessage("Transaction Failed", string.Concat(new object[]
			{
				"Error code: ",
				result.Error.Code,
				"\nError description:",
				result.Error.Message
			}));
		}
		else
		{
			IOSNativePopUpManager.showMessage("Store Kit Response", "product " + result.ProductIdentifier + " state: " + result.State.ToString());
		}
	}

	private static void OnRestoreComplete(RestoreResult res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Success", "Restore Compleated");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Error: " + res.Error.Code, res.Error.Message);
		}
	}

	private static void HandleOnVerificationComplete(VerificationResponse response)
	{
		IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + response.Status.ToString());
		ISN_Logger.Log("ORIGINAL JSON: " + response.OriginalJSON, LogType.Log);
	}

	private static void OnStoreKitInitComplete(Result result)
	{
		if (result.IsSucceeded)
		{
			int num = 0;
			foreach (Product product in Singleton<PaymentManager>.Instance.Products)
			{
				if (product.IsAvailable)
				{
					num++;
				}
			}
			IOSNativePopUpManager.showMessage("StoreKit Init Succeeded", "Available products count: " + num);
			ISN_Logger.Log("StoreKit Init Succeeded Available products count: " + num, LogType.Log);
		}
		else
		{
			IOSNativePopUpManager.showMessage("StoreKit Init Failed", string.Concat(new object[]
			{
				"Error code: ",
				result.Error.Code,
				"\nError description:",
				result.Error.Message
			}));
		}
	}

	public const string SMALL_PACK = "your.product.id1.here";

	public const string NC_PACK = "your.product.id2.here";

	public string lastTransactionProdudctId = string.Empty;

	private static bool IsInitialized;

	[CompilerGenerated]
	private static Action<VerificationResponse> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static Action<Result> _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static Action<PurchaseResult> _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static Action<RestoreResult> _003C_003Ef__mg_0024cache3;
}
