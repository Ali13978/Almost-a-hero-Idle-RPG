using System;
using UnityEngine;

namespace DynamicLoading
{
	public class LoadAllAssetsRequest : AssetRequest
	{
		public LoadAllAssetsRequest(AssetBundleWrapper assetBundleWrapper, Action<UnityEngine.Object[]> onCompleted, bool canBeCancelled) : base(assetBundleWrapper, canBeCancelled)
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
			if (this.onCompleted != null)
			{
				this.onCompleted(this.assetBundleWrapper.AllAssetsRequest.allAssets);
			}
			return true;
		}

		private Action<UnityEngine.Object[]> onCompleted;
	}
}
