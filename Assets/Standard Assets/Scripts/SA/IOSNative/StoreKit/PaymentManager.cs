using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.StoreKit
{
	public class PaymentManager : Singleton<PaymentManager>
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Result> OnStoreKitInitComplete;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnRestoreStarted;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<RestoreResult> OnRestoreComplete;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<string> OnTransactionStarted;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<PurchaseResult> OnTransactionComplete;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<string> OnProductPurchasedExternally;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<VerificationResponse> OnVerificationComplete;

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadStore(bool forceLoad = false)
		{
			if (this._IsStoreLoaded && !forceLoad)
			{
				base.Invoke("FireSuccessInitEvent", 1f);
				return;
			}
			if (this._IsWaitingLoadResult)
			{
				return;
			}
			this._IsWaitingLoadResult = true;
			string text = string.Empty;
			int count = this.Products.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					text += ",";
				}
				text += this.Products[i].Id;
			}
			ISN_SoomlaGrow.Init();
			if (!Application.isEditor)
			{
				BillingNativeBridge.LoadStore(text);
				if (IOSNativeSettings.Instance.TransactionsHandlingMode == TransactionsHandlingMode.Manual)
				{
					BillingNativeBridge.EnableManulaTransactionsMode();
				}
				if (!IOSNativeSettings.Instance.PromotedPurchaseSupport)
				{
					BillingNativeBridge.DisablePromotedPurchases();
				}
			}
			else if (IOSNativeSettings.Instance.InAppsEditorTesting)
			{
				base.Invoke("EditorFakeInitEvent", 1f);
			}
		}

		public void BuyProduct(string productId)
		{
			if (!Application.isEditor)
			{
				PaymentManager.OnTransactionStarted(productId);
				if (!this._IsStoreLoaded)
				{
					ISN_Logger.Log("buyProduct shouldn't be called before StoreKit is initialized", LogType.Log);
					Error error = new Error(4, "StoreKit not yet initialized");
					this.SendTransactionFailEvent(productId, error);
					return;
				}
				BillingNativeBridge.BuyProduct(productId);
			}
			else if (IOSNativeSettings.Instance.InAppsEditorTesting)
			{
				this.FireProductBoughtEvent(productId, string.Empty, string.Empty, string.Empty, false);
			}
		}

		public void FinishTransaction(string productId)
		{
			BillingNativeBridge.FinishTransaction(productId);
		}

		public void AddProductId(string productId)
		{
			this.AddProduct(new Product
			{
				Id = productId
			});
		}

		public void AddProduct(Product product)
		{
			bool flag = false;
			int index = 0;
			foreach (Product product2 in this.Products)
			{
				if (product2.Id.Equals(product.Id))
				{
					flag = true;
					index = this.Products.IndexOf(product2);
					break;
				}
			}
			if (flag)
			{
				this.Products[index] = product;
			}
			else
			{
				this.Products.Add(product);
			}
		}

		public Product GetProductById(string prodcutId)
		{
			foreach (Product product in this.Products)
			{
				if (product.Id.Equals(prodcutId))
				{
					return product;
				}
			}
			Product product2 = new Product();
			product2.Id = prodcutId;
			this.Products.Add(product2);
			return product2;
		}

		public void RestorePurchases()
		{
			if (!this._IsStoreLoaded)
			{
				Error e = new Error(7, "Store Kit Initilizations required");
				RestoreResult obj = new RestoreResult(e);
				PaymentManager.OnRestoreComplete(obj);
				return;
			}
			PaymentManager.OnRestoreStarted();
			if (!Application.isEditor)
			{
				BillingNativeBridge.RestorePurchases();
			}
			else if (IOSNativeSettings.Instance.InAppsEditorTesting)
			{
				foreach (Product product in this.Products)
				{
					if (product.Type == ProductType.NonConsumable)
					{
						ISN_Logger.Log("Restored: " + product.Id, LogType.Log);
						this.FireProductBoughtEvent(product.Id, string.Empty, string.Empty, string.Empty, true);
					}
				}
				this.FireRestoreCompleteEvent();
			}
		}

		public void VerifyLastPurchase(string url)
		{
			BillingNativeBridge.VerifyLastPurchase(url);
		}

		public void RegisterProductView(StoreProductView view)
		{
			view.SetId(PaymentManager.NextId);
			this._productsView.Add(view.Id, view);
		}

		public List<Product> Products
		{
			get
			{
				return IOSNativeSettings.Instance.InAppProducts;
			}
		}

		public bool IsStoreLoaded
		{
			get
			{
				return this._IsStoreLoaded;
			}
		}

		public bool IsInAppPurchasesEnabled
		{
			get
			{
				return BillingNativeBridge.ISN_InAppSettingState();
			}
		}

		public bool IsWaitingLoadResult
		{
			get
			{
				return this._IsWaitingLoadResult;
			}
		}

		private static int NextId
		{
			get
			{
				PaymentManager._nextId++;
				return PaymentManager._nextId;
			}
		}

		private void OnStoreKitInitFailed(string data)
		{
			Error error = new Error(data);
			this._IsStoreLoaded = false;
			this._IsWaitingLoadResult = false;
			Result obj = new Result(error);
			PaymentManager.OnStoreKitInitComplete(obj);
			if (!IOSNativeSettings.Instance.DisablePluginLogs)
			{
				ISN_Logger.Log("STORE_KIT_INIT_FAILED Error: " + error.Message, LogType.Log);
			}
		}

		private void onStoreDataReceived(string data)
		{
			if (data.Equals(string.Empty))
			{
				ISN_Logger.Log("InAppPurchaseManager, no products avaiable", LogType.Log);
				Result obj = new Result();
				PaymentManager.OnStoreKitInitComplete(obj);
				return;
			}
			string[] array = data.Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i += 7)
			{
				string prodcutId = array[i];
				Product productById = this.GetProductById(prodcutId);
				productById.DisplayName = array[i + 1];
				productById.Description = array[i + 2];
				productById.LocalizedPrice = array[i + 3];
				productById.Price = Convert.ToSingle(array[i + 4]);
				productById.CurrencyCode = array[i + 5];
				productById.CurrencySymbol = array[i + 6];
				productById.IsAvailable = true;
			}
			ISN_Logger.Log("InAppPurchaseManager, total products in settings: " + this.Products.Count.ToString(), LogType.Log);
			int num = 0;
			foreach (Product product in this.Products)
			{
				if (product.IsAvailable)
				{
					num++;
				}
			}
			ISN_Logger.Log("InAppPurchaseManager, total avaliable products" + num, LogType.Log);
			this.FireSuccessInitEvent();
		}

		private void onProductBought(string array)
		{
			string[] array2 = array.Split(new char[]
			{
				"|"[0]
			});
			bool isRestored = false;
			if (array2[1].Equals("0"))
			{
				isRestored = true;
			}
			string productIdentifier = array2[0];
			this.FireProductBoughtEvent(productIdentifier, array2[2], array2[3], array2[4], isRestored);
		}

		private void onProductPurchasedExternally(string productIdentifier)
		{
			PaymentManager.OnProductPurchasedExternally(productIdentifier);
		}

		private void onProductStateDeferred(string productIdentifier)
		{
			PurchaseResult obj = new PurchaseResult(productIdentifier, PurchaseState.Deferred, string.Empty, string.Empty, string.Empty);
			PaymentManager.OnTransactionComplete(obj);
		}

		private void onTransactionFailed(string data)
		{
			string[] array = data.Split(new string[]
			{
				"|%|".ToString()
			}, StringSplitOptions.None);
			string productIdentifier = array[0];
			Error error = new Error(array[1]);
			this.SendTransactionFailEvent(productIdentifier, error);
		}

		private void onVerificationResult(string data)
		{
			VerificationResponse obj = new VerificationResponse(PaymentManager.lastPurchasedProduct, data);
			PaymentManager.OnVerificationComplete(obj);
		}

		public void onRestoreTransactionFailed(string array)
		{
			Error e = new Error(array);
			RestoreResult obj = new RestoreResult(e);
			PaymentManager.OnRestoreComplete(obj);
		}

		public void onRestoreTransactionComplete(string array)
		{
			this.FireRestoreCompleteEvent();
		}

		private void OnProductViewLoaded(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (this._productsView.ContainsKey(key))
			{
				this._productsView[key].OnContentLoaded();
			}
		}

		private void OnProductViewLoadedFailed(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (this._productsView.ContainsKey(key))
			{
				this._productsView[key].OnContentLoadFailed();
			}
		}

		private void OnProductViewDismissed(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (this._productsView.ContainsKey(key))
			{
				this._productsView[key].OnProductViewDismissed();
			}
		}

		private void FireSuccessInitEvent()
		{
			this._IsStoreLoaded = true;
			this._IsWaitingLoadResult = false;
			Result obj = new Result();
			PaymentManager.OnStoreKitInitComplete(obj);
		}

		private void FireRestoreCompleteEvent()
		{
			RestoreResult obj = new RestoreResult();
			PaymentManager.OnRestoreComplete(obj);
		}

		private void FireProductBoughtEvent(string productIdentifier, string applicationUsername, string receipt, string transactionIdentifier, bool IsRestored)
		{
			PurchaseState state;
			if (IsRestored)
			{
				state = PurchaseState.Restored;
			}
			else
			{
				state = PurchaseState.Purchased;
			}
			PurchaseResult purchaseResult = new PurchaseResult(productIdentifier, state, applicationUsername, receipt, transactionIdentifier);
			PaymentManager.lastPurchasedProduct = purchaseResult.ProductIdentifier;
			PaymentManager.OnTransactionComplete(purchaseResult);
		}

		private void SendTransactionFailEvent(string productIdentifier, Error error)
		{
			PurchaseResult obj = new PurchaseResult(productIdentifier, error);
			PaymentManager.OnTransactionComplete(obj);
		}

		private void EditorFakeInitEvent()
		{
			this.FireSuccessInitEvent();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static PaymentManager()
		{
			PaymentManager.OnStoreKitInitComplete = delegate(Result A_0)
			{
			};
			PaymentManager.OnRestoreStarted = delegate()
			{
			};
			PaymentManager.OnRestoreComplete = delegate(RestoreResult A_0)
			{
			};
			PaymentManager.OnTransactionStarted = delegate(string A_0)
			{
			};
			PaymentManager.OnTransactionComplete = delegate(PurchaseResult A_0)
			{
			};
			PaymentManager.OnProductPurchasedExternally = delegate(string A_0)
			{
			};
			PaymentManager.OnVerificationComplete = delegate(VerificationResponse A_0)
			{
			};
			PaymentManager._nextId = 1;
		}

		public const string APPLE_VERIFICATION_SERVER = "https://buy.itunes.apple.com/verifyReceipt";

		public const string SANDBOX_VERIFICATION_SERVER = "https://sandbox.itunes.apple.com/verifyReceipt";

		private bool _IsStoreLoaded;

		private bool _IsWaitingLoadResult;

		private static int _nextId;

		private Dictionary<int, StoreProductView> _productsView = new Dictionary<int, StoreProductView>();

		private static string lastPurchasedProduct;
	}
}
