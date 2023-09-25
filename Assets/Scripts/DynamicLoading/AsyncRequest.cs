using System;

namespace DynamicLoading
{
	public abstract class AsyncRequest
	{
		public AsyncRequest(AssetBundleWrapper assetBundleWrapper, bool canBeCancelled)
		{
			this.assetBundleWrapper = assetBundleWrapper;
			this.CanBeCancelled = canBeCancelled;
		}

		public bool CanBeCancelled { get; private set; }

		public bool BelongsToBundle(string bundleName)
		{
			return this.assetBundleWrapper.BundleName == bundleName;
		}

		public abstract bool Update();

		protected AssetBundleWrapper assetBundleWrapper;
	}
}
