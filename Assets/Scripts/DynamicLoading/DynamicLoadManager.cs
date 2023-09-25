using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DynamicLoading
{
	public static class DynamicLoadManager
	{
		public static void Init(string mainBundleName)
		{

            DynamicLoadManager.assetBundles = new Dictionary<string, AssetBundleWrapper>();
            DynamicLoadManager.requests = new List<AsyncRequest>();
   
            AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, mainBundleName));
            AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            foreach (string text in assetBundleManifest.GetAllAssetBundles())
            {
               
                AssetBundleWrapper wrapper = DynamicLoadManager.GetWrapper(text);
                string[] allDependencies = assetBundleManifest.GetAllDependencies(text);
                List<AssetBundleWrapper> list = new List<AssetBundleWrapper>();
                foreach (string bundleName in allDependencies)
                {
                    list.Add(DynamicLoadManager.GetWrapper(bundleName));
                }
                wrapper.Dependencies = list;
            }
        }

        public static bool IsLoadingAssets()
        {
            if (DynamicLoadManager.requests.Count > 0)
            {
                return true;
            }
            foreach (AssetBundleWrapper assetBundleWrapper in DynamicLoadManager.assetBundles.Values)
            {
                if (assetBundleWrapper.IsLoading)
                {
                    return true;
                }
            }
            return false;
        }

        public static void UpdatePendingRequests()
        {
            if (DynamicLoadManager.requests.Count > 0)
            {
                for (int i = DynamicLoadManager.requests.Count - 1; i >= 0; i--)
                {
                    if (DynamicLoadManager.requests[i].Update())
                    {
                        DynamicLoadManager.requests.RemoveFastAt(i);
                    }
                }
            }
        }

        public static void CancelPendingRequests()
        {
            if (DynamicLoadManager.requests.Count > 0)
            {
                for (int i = DynamicLoadManager.requests.Count - 1; i >= 0; i--)
                {
                    if (DynamicLoadManager.requests[i].CanBeCancelled)
                    {
                        DynamicLoadManager.requests.RemoveFastAt(i);
                    }
                }
            }
        }

        public static void LoadAsset<T>(string bundleName, string assetPath, Action<T> onCompleted, bool canBeCancelled) where T : UnityEngine.Object
        {
            DynamicLoadManager.requests.Add(new LoadAssetRequest<T>(DynamicLoadManager.assetBundles[bundleName], assetPath, onCompleted, canBeCancelled));
        }

        public static void LoadAllAssets(string bundleName, bool canBeCancelled, Action<UnityEngine.Object[]> onCompleted = null)
        {
            DynamicLoadManager.requests.Add(new LoadAllAssetsRequest(DynamicLoadManager.assetBundles[bundleName], onCompleted, canBeCancelled));
        }

        public static void LoadAllAndGetAssetOfType<T>(string bundleName, Action<T> onCompleted, bool canBeCancelled) where T : UnityEngine.Object
        {
            DynamicLoadManager.requests.Add(new LoadAllAssetsAndGetFromTypeRequest<T>(DynamicLoadManager.assetBundles[bundleName], onCompleted, canBeCancelled));
        }

        public static void GetPermanentReferenceToBundle(string name, Action onBundleLoaded, bool neverUnload = false)
        {
            AssetBundleWrapper assetBundleWrapper;
            if (DynamicLoadManager.assetBundles.TryGetValue(name, out assetBundleWrapper))
            {

                assetBundleWrapper.NeverUnload = neverUnload;
                assetBundleWrapper.References++;
                if (assetBundleWrapper.IsMainBundleNotLoaded)
                {
                    DynamicLoadManager.LoadBundle(assetBundleWrapper, onBundleLoaded, !neverUnload);
                }
                else if (onBundleLoaded != null)
                {
                    Debug.Log("GetPermanentReferenceToBundle4");
                    onBundleLoaded();
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Bundle not found " + name);
            }
        }

        public static void RemovePermanentReferenceToBundle(string name)
        {
            AssetBundleWrapper assetBundleWrapper;
            if (DynamicLoadManager.assetBundles.TryGetValue(name, out assetBundleWrapper) && assetBundleWrapper.References > 0)
            {
                assetBundleWrapper.References--;
            }
            else
            {
                UnityEngine.Debug.LogWarning("Bundle not found: " + name);
            }
        }

        public static void UnloadUnusedBundles()
        {
            foreach (KeyValuePair<string, AssetBundleWrapper> keyValuePair in DynamicLoadManager.assetBundles)
            {
                AssetBundleWrapper value = keyValuePair.Value;
                if (value.UnloadIfUnused())
                {
                    DynamicLoadManager.RemovePendingRequestsFromBundle(value.BundleName);
                    foreach (AssetBundleWrapper assetBundleWrapper in value.Dependencies)
                    {
                        DynamicLoadManager.RemovePermanentReferenceToBundle(assetBundleWrapper.BundleName);
                    }
                }
            }
        }

        public static void UnloadAssetsFromBundle(string bundleName)
        {
            if (DynamicLoadManager.assetBundles.ContainsKey(bundleName))
            {
                DynamicLoadManager.assetBundles[bundleName].UnloadAssets();
            }
            DynamicLoadManager.RemovePendingRequestsFromBundle(bundleName);
        }

        private static void RemovePendingRequestsFromBundle(string bundleName)
        {
            for (int i = DynamicLoadManager.requests.Count - 1; i >= 0; i--)
            {
                if (DynamicLoadManager.requests[i].BelongsToBundle(bundleName))
                {
                    DynamicLoadManager.requests.RemoveFastAt(i);
                }
            }
        }

        public static string GetDebugInfo()
        {
            string text = string.Empty;
            foreach (AssetBundleWrapper assetBundleWrapper in DynamicLoadManager.assetBundles.Values)
            {
                text = text + "* " + assetBundleWrapper.BundleName;
                text = text + "\n\tIs Loaded: " + !assetBundleWrapper.IsMainBundleNotLoaded;
                text = text + "\n\tReferences: " + assetBundleWrapper.References;
                if (assetBundleWrapper.Dependencies.Count > 0)
                {
                    text += "\n\tDependencies: ";
                    foreach (AssetBundleWrapper assetBundleWrapper2 in assetBundleWrapper.Dependencies)
                    {
                        text = text + assetBundleWrapper2.BundleName + ", ";
                    }
                }
                text += '\n';
            }
            return text;
        }

        private static AssetBundleWrapper GetWrapper(string bundleName)
        {
            if (!DynamicLoadManager.assetBundles.ContainsKey(bundleName))
            {
                DynamicLoadManager.assetBundles.Add(bundleName, new AssetBundleWrapper(bundleName));
            }
            return DynamicLoadManager.assetBundles[bundleName];
        }

        private static void LoadBundle(AssetBundleWrapper bundleWrapper, Action onBundleLoaded, bool canBeCancelled)
        {
            if (!bundleWrapper.IsLoading)
            {
                foreach (AssetBundleWrapper assetBundleWrapper in bundleWrapper.Dependencies)
                {
                    DynamicLoadManager.GetPermanentReferenceToBundle(assetBundleWrapper.BundleName, (Action)null, false);
                }
            }
            DynamicLoadManager.requests.Add(new BundleRequest(bundleWrapper, onBundleLoaded, canBeCancelled));
        }

        private static Dictionary<string, AssetBundleWrapper> assetBundles;

        private static MonoBehaviour coroutiner;

        private static List<AsyncRequest> requests= new List<AsyncRequest>();

    }
}
