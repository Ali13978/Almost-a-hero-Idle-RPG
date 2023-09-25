using System;
using UnityEngine;

namespace DynamicLoading
{
	public class LoadAssetRequest<T> : AssetRequest where T : UnityEngine.Object
	{
		public LoadAssetRequest(AssetBundleWrapper assetBundleWrapper, string assetPath, Action<T> onCompleted, bool canBeCancelled) : base(assetBundleWrapper, canBeCancelled)
		{
			this.onCompleted = onCompleted;
			this.assetPath = assetPath;
		}

		protected override bool updateInternal()
		{
			if (!this.assetBundleWrapper.AssetRequests.ContainsKey(this.assetPath))
			{
				AssetBundle bundle = this.assetBundleWrapper.GetBundle();
				if (bundle == null)
				{
					return false;
				}
				this.assetBundleWrapper.AssetRequests.Add(this.assetPath, bundle.LoadAssetAsync<T>(this.assetPath));
			}
			if (!this.assetBundleWrapper.AssetRequests[this.assetPath].isDone)
			{
				return false;
			}
			if (this.onCompleted != null)
			{
				this.onCompleted(this.assetBundleWrapper.AssetRequests[this.assetPath].asset as T);
			}
			return true;
		}

		private Action<T> onCompleted;

		private string assetPath;
	}
}
