using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DynamicLoading
{
	public class AssetBundleWrapper
	{
		public AssetBundleWrapper(string name)
		{
			this.BundleName = name;
			this.assetBundle = null;
			this.bundleRequest = null;
			this.AllAssetsRequest = null;
			this.AssetRequests = new Dictionary<string, AssetBundleRequest>();
			this.References = 0;
		}

		public bool IsMainBundleNotLoaded
		{
			get
			{
				return this.assetBundle == null;
			}
		}

		public bool IsLoading
		{
			get
			{
				return this.assetBundle == null && this.bundleRequest != null;
			}
		}

		public AssetBundle GetBundle()
		{
			if (this.assetBundle == null)
			{
				return null;
			}
			foreach (AssetBundleWrapper assetBundleWrapper in this.Dependencies)
			{
				if (assetBundleWrapper.GetBundle() == null)
				{
					return null;
				}
			}
			return this.assetBundle;
		}

		public void Load()
		{
			if (this.IsMainBundleNotLoaded && !this.IsLoading)
			{
				this.bundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, this.BundleName));
			}
		}

		public void UpdateBundleRequest()
		{
			if (this.bundleRequest.isDone)
			{
				this.assetBundle = this.bundleRequest.assetBundle;
				if (this.assetBundle == null)
				{
					UnityEngine.Debug.LogError("Error while loading Asset Bundle " + this.BundleName);
				}
				this.bundleRequest = null;
			}
		}

		public bool UnloadIfUnused()
		{
			if (this.NeverUnload)
			{
				return false;
			}
			if (this.assetBundle != null && this.References == 0)
			{
				this.UnloadAssets();
				return true;
			}
			return false;
		}

		public void UnloadAssets()
		{
			this.AllAssetsRequest = null;
			this.AssetRequests.Clear();
			if (this.assetBundle != null)
			{
				this.assetBundle.Unload(true);
				this.assetBundle = null;
			}
		}

		public List<AssetBundleWrapper> Dependencies;

		public int References;

		public string BundleName;

		public bool NeverUnload;

		public AssetBundleRequest AllAssetsRequest;

		public Dictionary<string, AssetBundleRequest> AssetRequests;

		private AssetBundle assetBundle;

		private AssetBundleCreateRequest bundleRequest;
	}
}
