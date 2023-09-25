using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ui;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IapManager : IStoreListener
{
	public static void Init()
	{
		IapManager.inst = new IapManager();
		IapManager.inst.InitInst();
	}

	private void InitInst()
	{
		if (IapManager.m_StoreController == null)
		{
			this.InitializePurchasing(false);
		}
	}

	private void InitializePurchasing(bool force = false)
	{
		IapManager.xiaomiTransactions = new List<IapManager.XiaomiReceipt>();
		IapManager.LoadXiaomiTransactions();
		if (IapManager.IsInitialized() && !force)
		{
			return;
		}
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		IapManager.store = StandardPurchasingModule.Instance().appStore;
		foreach (string id in IapManager.productIds)
		{
			configurationBuilder.AddProduct(id, ProductType.Consumable);
		}
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	public static bool IsInitialized()
	{
		return IapManager.m_StoreController != null && IapManager.m_StoreExtensionProvider != null;
	}

	public void Buy(int index)
	{
		this.BuyProductID(IapManager.productIds[index]);
	}

	public static void RemoveXiaomiTransaction(string tranactionId)
	{
		IapManager.XiaomiReceipt xiaomiReceipt = IapManager.xiaomiTransactions.Find((IapManager.XiaomiReceipt x) => x.transactionId == tranactionId);
		if (xiaomiReceipt != null)
		{
			IapManager.xiaomiTransactions.Remove(xiaomiReceipt);
		}
		IapManager.SaveXiaomiTransactionLocal();
	}

	private void BuyProductID(string productId)
	{
		if (IapManager.IsInitialized())
		{
			Product product = IapManager.m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("IAP: Purchasing product asychronously: '{0}'", product.definition.id));
				IapManager.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("IAP: BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				UiManager.isPurchasing = false;
			}
		}
		else
		{
			UnityEngine.Debug.Log("IAP: BuyProductID FAIL. Not initialized.");
			UiManager.isPurchasing = false;
		}
	}

	public void RestorePurchases()
	{
		if (!IapManager.IsInitialized())
		{
			UnityEngine.Debug.Log("IAP: RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			UnityEngine.Debug.Log("IAP: RestorePurchases started ...");
			IAppleExtensions extension = IapManager.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			extension.RestoreTransactions(delegate(bool result)
			{
				UnityEngine.Debug.Log("IAP: RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			UnityEngine.Debug.Log("IAP: RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		UnityEngine.Debug.Log("IAP: OnInitialized: PASS");
		IapManager.m_StoreController = controller;
		IapManager.m_StoreExtensionProvider = extensions;
		IapManager.productPrices = new double[IapManager.productIds.Length];
		for (int i = 0; i < IapManager.productIds.Length; i++)
		{
			string id = IapManager.productIds[i];
			Product product = IapManager.m_StoreController.products.WithID(id);
			IapManager.storeCurrency = product.metadata.isoCurrencyCode;
			IapManager.productPriceStringsLocal[i] = product.metadata.localizedPriceString;
			IapManager.productPrices[i] = (double)product.metadata.localizedPrice;
		}
	}

	
	

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("IAP: OnInitializeFailed InitializationFailureReason:" + error);
	}

	public void RemoveFromPending(Product product)
	{
		if (IapManager.m_StoreController != null)
		{
			IapManager.m_StoreController.ConfirmPendingPurchase(product);
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		UnityEngine.Debug.Log(string.Format("IAP: ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		UnityEngine.Debug.Log("STORE: " + StandardPurchasingModule.Instance().appStore);
		Product purchasedProduct = args.purchasedProduct;
		this.VaidateReceipt(purchasedProduct);
		return PurchaseProcessingResult.Pending;
	}

	private void VaidateReceipt(Product receivedProduct)
	{
		if (receivedProduct == null || string.IsNullOrEmpty(receivedProduct.receipt))
		{
			UnityEngine.Debug.Log("IAP: receipt validation failed.");
			UiManager.isPurchasing = false;
			return;
		}
		IapManager.UnityIapReceipt unityIapReceipt = JsonConvert.DeserializeObject<IapManager.UnityIapReceipt>(receivedProduct.receipt);
		IapManager.UnityIapGooglePlayPayload unityIapGooglePlayPayload = JsonConvert.DeserializeObject<IapManager.UnityIapGooglePlayPayload>(unityIapReceipt.Payload);
		PlayfabManager.ValidateReceiptGoogle(receivedProduct, unityIapGooglePlayPayload.json, unityIapGooglePlayPayload.signature, receivedProduct.metadata.isoCurrencyCode, (uint)receivedProduct.metadata.localizedPrice * 100u, delegate(PlayfabManager.IapValidationResult success)
		{
			UiManager.isPurchasing = false;
			if (success == PlayfabManager.IapValidationResult.Success || success == PlayfabManager.IapValidationResult.ReceiptAlreadyUsed)
			{
				this.OnSuccess(receivedProduct);
			}
			else
			{
				UnityEngine.Debug.Log("IAP: receipt validation failed.");
			}
		});
	}

	private void OnSuccess(Product receivedProduct)
	{
		string id = receivedProduct.definition.id;
		int num = 0;
		foreach (string b in IapManager.productIds)
		{
			if (string.Equals(id, b, StringComparison.Ordinal))
			{
				break;
			}
			num++;
		}
		if (num == IapManager.productIds.Length)
		{
			throw new NotImplementedException();
		}
		this.boughtProductIndex = num;
		PlayfabManager.SendPlayerValidatedPurchase(receivedProduct);
		AdjustTracker.TrackIAPEventData(receivedProduct);
		this.RemoveFromPending(receivedProduct);
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		UnityEngine.Debug.Log(string.Format("IAP: OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
		if (failureReason == PurchaseFailureReason.DuplicateTransaction)
		{
			UnityEngine.Debug.Log("Trying to validate duplicated transaction");
			this.VaidateReceipt(product);
		}
		UiManager.isPurchasing = false;
	}

	private static void SaveXiaomiTransactionLocal()
	{
		string value = SaveLoadManager.EncodeString(JsonConvert.SerializeObject(IapManager.xiaomiTransactions));
		PlayerPrefs.SetString(IapManager.transactionSave, value);
		PlayerPrefs.Save();
	}

	private static void LoadXiaomiTransactions()
	{
		string @string = PlayerPrefs.GetString(IapManager.transactionSave, "NON");
		if (@string != "NON")
		{
			string value = SaveLoadManager.DecodeString(@string);
			IapManager.xiaomiTransactions = JsonConvert.DeserializeObject<List<IapManager.XiaomiReceipt>>(value);
		}
	}

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("Initializing IAP Failed with error: " + error + "; and message: " + message);
    }

    private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	public static AppStore store;

	public static string[] productIds = new string[]
	{
		"com.beesquare.almostahero.gem_pack_01",
		"com.beesquare.almostahero.gem_pack_02",
		"com.beesquare.almostahero.gem_pack_03",
		"com.beesquare.almostahero.gem_pack_04",
		"com.beesquare.almostahero.gem_pack_05",
		"com.beesquare.almostahero.gem_pack_06",
		"com.beesquare.almostahero.starter_pack",
		"com.beesquare.almostahero.currency_pack",
		"com.beesquare.almostahero.xmas_pack",
		"com.beesquare.almostahero.mid_gem_offer",
		"com.beesquare.almostahero.late_gem_offer",
		"com.beesquare.almostahero.rift_pack_01",
		"com.beesquare.almostahero.rift_pack_02",
		"com.beesquare.almostahero.rift_pack_03",
		"com.beesquare.almostahero.gem_pack_04_discounted",
		"com.beesquare.almostahero.stage_100_offer",
		"com.beesquare.almostahero.rift_pack_04",
		"com.beesquare.almostahero.halloween_pack_01",
		"com.beesquare.almostahero.candies_pack_01",
		"com.beesquare.almostahero.candies_pack_01",
		"com.beesquare.almostahero.xmas_pack_18_01",
		"com.beesquare.almostahero.xmas_pack_18_02",
		"com.beesquare.almostahero.stage_200",
		"com.beesquare.almostahero.stage_500",
		"com.beesquare.almostahero.bday_gems_19_01",
		"com.beesquare.almostahero.bday_currency_19_01",
		"com.beesquare.almostahero.bday_gems_19_02",
		"com.beesquare.almostahero.bday_currency_19_02"
	};

	public static string[] productPriceStringsLocal = new string[]
	{
		"$ 0.99",
		"$ 1.99",
		"$ 4.99",
		"$ 19.99",
		"$ 49.99",
		"$ 99.99",
		"$ 3.99",
		"$ 9.99",
		"$ 14.99",
		"$ 3.99",
		"$ 5.99",
		"$ 8.99",
		"$ 6.99",
		"$ 7.99",
		"$ 7.99",
		"$ 5.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 14.99",
		"$ 4.99",
		"$ 19.99",
		"$ 9.99",
		"$ 39.99"
	};

	public static double[] productPrices;

	public static List<IapManager.XiaomiReceipt> xiaomiTransactions;

	private static string transactionSave = "XIAOMI_TRANSACTIONS";

	public int boughtProductIndex = -1;

	public static IapManager inst;

	public static string storeCurrency;

	public class UnityIapReceipt
	{
		public string Store;

		public string TransactionID;

		public string Payload;
	}

	private class UnityIapGooglePlayPayload
	{
		public string json;

		public string signature;
	}

	public class XiaomiReceipt
	{
		public string receipt;

		public string transactionId;

		public string id;
	}
}
