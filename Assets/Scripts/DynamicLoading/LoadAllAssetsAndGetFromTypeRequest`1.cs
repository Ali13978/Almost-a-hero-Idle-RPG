using System;
using UnityEngine;

namespace DynamicLoading
{
	public class LoadAllAssetsAndGetFromTypeRequest<T> : AssetRequest where T : UnityEngine.Object
	{
		public LoadAllAssetsAndGetFromTypeRequest(AssetBundleWrapper assetBundleWrapper, Action<T> onCompleted, bool canBeCancelled) : base(assetBundleWrapper, canBeCancelled)
		{
			this.onCompleted = onCompleted;
		}

		protected override bool updateInternal()
		{
			if (this.assetBundleWrapper.AllAssetsRequest == null)
			{
				AssetBundle bundle = this.assetBundleWrapper.GetBundle();
				if (bundle == null)
				{
					return false;
				}
				this.assetBundleWrapper.AllAssetsRequest = bundle.LoadAllAssetsAsync();
			}
			if (!this.assetBundleWrapper.AllAssetsRequest.isDone)
			{
				return false;
			}
			int num = 0;
			T t = (T)((object)null);
			while (num < this.assetBundleWrapper.AllAssetsRequest.allAssets.Length && t == null)
			{
				t = (this.assetBundleWrapper.AllAssetsRequest.allAssets[num++] as T);
			}
			if (t == null)
			{
				UnityEngine.Debug.LogError("Asset of type " + typeof(T).Name + " not found in bundle " + this.assetBundleWrapper.BundleName);
				this.assetBundleWrapper.AllAssetsRequest = null;
				return false;
			}
			this.onCompleted(t);
			return true;
		}

		private Action<T> onCompleted;
	}
}
