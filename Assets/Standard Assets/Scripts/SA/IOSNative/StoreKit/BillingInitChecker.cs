using System;
using SA.Common.Models;
using SA.Common.Pattern;

namespace SA.IOSNative.StoreKit
{
	public class BillingInitChecker
	{
		public BillingInitChecker(BillingInitChecker.BillingInitListener listener)
		{
			this._listener = listener;
			if (Singleton<PaymentManager>.Instance.IsStoreLoaded)
			{
				this._listener();
			}
			else
			{
				PaymentManager.OnStoreKitInitComplete += this.HandleOnStoreKitInitComplete;
				if (!Singleton<PaymentManager>.Instance.IsWaitingLoadResult)
				{
					Singleton<PaymentManager>.Instance.LoadStore(false);
				}
			}
		}

		private void HandleOnStoreKitInitComplete(Result obj)
		{
			PaymentManager.OnStoreKitInitComplete -= this.HandleOnStoreKitInitComplete;
			this._listener();
		}

		private BillingInitChecker.BillingInitListener _listener;

		public delegate void BillingInitListener();
	}
}
