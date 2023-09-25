using System;
using UnityEngine;

namespace DynamicLoading
{
	public abstract class AssetRequest : AsyncRequest
	{
		public AssetRequest(AssetBundleWrapper assetBundleWrapper, bool canBeCancelled) : base(assetBundleWrapper, canBeCancelled)
		{
		}

		public override bool Update()
		{
			bool flag = true;
			if (this.assetBundleWrapper.Dependencies.Count > 0)
			{
				for (int i = this.assetBundleWrapper.Dependencies.Count - 1; i >= 0; i--)
				{
					AssetBundleWrapper assetBundleWrapper = this.assetBundleWrapper.Dependencies[i];
					if (assetBundleWrapper.AllAssetsRequest == null)
					{
						AssetBundle bundle = assetBundleWrapper.GetBundle();
						if (bundle != null)
						{
							assetBundleWrapper.AllAssetsRequest = bundle.LoadAllAssetsAsync();
						}
						flag = false;
					}
					else if (!assetBundleWrapper.AllAssetsRequest.isDone)
					{
						flag = false;
					}
				}
			}
			return flag && this.updateInternal();
		}

		protected abstract bool updateInternal();
	}
}
