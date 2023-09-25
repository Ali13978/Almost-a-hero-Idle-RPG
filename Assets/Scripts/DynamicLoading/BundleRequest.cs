using System;

namespace DynamicLoading
{
	public class BundleRequest : AsyncRequest
	{
		public BundleRequest(AssetBundleWrapper assetBundleWrapper, Action onCompleted, bool canBeCancelled) : base(assetBundleWrapper, canBeCancelled)
		{
			assetBundleWrapper.Load();
			this.onCompleted = onCompleted;
		}

		public override bool Update()
		{
			if (this.assetBundleWrapper.IsLoading)
			{
				this.assetBundleWrapper.UpdateBundleRequest();
				return false;
			}
			if (!this.assetBundleWrapper.IsMainBundleNotLoaded && this.onCompleted != null)
			{
				this.onCompleted();
			}
			return true;
		}

		private Action onCompleted;
	}
}
