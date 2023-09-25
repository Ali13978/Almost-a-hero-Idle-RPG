using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Pattern;

namespace SA.IOSNative.StoreKit
{
	public class StoreProductView
	{
		public StoreProductView()
		{
			foreach (string productId in IOSNativeSettings.Instance.DefaultStoreProductsView)
			{
				this.addProductId(productId);
			}
			Singleton<PaymentManager>.Instance.RegisterProductView(this);
		}

		public StoreProductView(params string[] ids)
		{
			foreach (string productId in ids)
			{
				this.addProductId(productId);
			}
			Singleton<PaymentManager>.Instance.RegisterProductView(this);
		}

		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action Loaded;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action LoadFailed;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action Appeared;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action Dismissed;



		public void addProductId(string productId)
		{
			if (this._ids.Contains(productId))
			{
				return;
			}
			this._ids.Add(productId);
		}

		public void Load()
		{
		}

		public void Show()
		{
		}

		public int Id
		{
			get
			{
				return this._id;
			}
		}

		public void OnProductViewAppeard()
		{
			this.Appeared();
		}

		public void OnProductViewDismissed()
		{
			this.Dismissed();
		}

		public void OnContentLoaded()
		{
			this.Show();
			this.Loaded();
		}

		public void OnContentLoadFailed()
		{
			this.LoadFailed();
		}

		public void SetId(int viewId)
		{
			this._id = viewId;
		}

		private int _id;

		private List<string> _ids = new List<string>();
	}
}
