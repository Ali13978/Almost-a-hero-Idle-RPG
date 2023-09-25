using System;
using UnityEngine;

namespace DynamicLoading
{
	[Serializable]
	public class AssetBundledObject<T> where T : UnityEngine.Object
	{
		public bool isReady { get; private set; }

		public bool isLoading { get; private set; }

		public void LoadObjectAsync(Action<T> onLoad)
		{
			if (this.isReady)
			{
				this.OnLoaded(this.obj);
				onLoad(this.obj);
				return;
			}
			if (this.isLoading)
			{
				return;
			}
			this.isLoading = true;
			DynamicLoadManager.GetPermanentReferenceToBundle(this.bundleName, delegate
			{
				DynamicLoadManager.LoadAllAndGetAssetOfType<T>(this.bundleName, delegate(T m)
				{
					this.OnLoaded(m);
					onLoad(m);
				}, false);
			}, false);
		}

		public void Unload()
		{
			this.isReady = false;
			this.isLoading = false;
			DynamicLoadManager.RemovePermanentReferenceToBundle(this.bundleName);
			DynamicLoadManager.UnloadUnusedBundles();
		}

		private void OnLoaded(T obj)
		{
			this.obj = obj;
			this.isReady = true;
			this.isLoading = false;
		}

		public T GetObject()
		{
			if (this.isReady)
			{
				return this.obj;
			}
			UnityEngine.Debug.LogError("The object is not ready returning null");
			return (T)((object)null);
		}

		public string bundleName;

		private T obj;
	}
}
