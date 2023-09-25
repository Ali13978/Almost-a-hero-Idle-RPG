using System;
using SA.Common.Models;
using UnityEngine;

namespace SA.Common.Util
{
	public static class Loader
	{
		public static void LoadWebTexture(string url, Action<Texture2D> callback)
		{
			WWWTextureLoader wwwtextureLoader = WWWTextureLoader.Create();
			wwwtextureLoader.OnLoad += callback;
			wwwtextureLoader.LoadTexture(url);
		}

		public static void LoadPrefab(string localPath, Action<GameObject> callback)
		{
			PrefabAsyncLoader prefabAsyncLoader = PrefabAsyncLoader.Create();
			prefabAsyncLoader.ObjectLoadedAction += callback;
			prefabAsyncLoader.LoadAsync(localPath);
		}
	}
}
