using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SA.Common.Models
{
	public class WWWTextureLoader : MonoBehaviour
	{
		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Texture2D> OnLoad;



		public static WWWTextureLoader Create()
		{
			return new GameObject("WWWTextureLoader").AddComponent<WWWTextureLoader>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadTexture(string url)
		{
			this._url = url;
			if (WWWTextureLoader.LocalCache.ContainsKey(this._url))
			{
				this.OnLoad(WWWTextureLoader.LocalCache[this._url]);
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			base.StartCoroutine(this.LoadCoroutin());
		}

		private IEnumerator LoadCoroutin()
		{
			WWW www = new WWW(this._url);
			yield return www;
			if (www.error == null)
			{
				WWWTextureLoader.UpdateLocalCache(this._url, www.texture);
				this.OnLoad(www.texture);
			}
			else
			{
				this.OnLoad(null);
			}
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}

		private static void UpdateLocalCache(string url, Texture2D image)
		{
			if (!WWWTextureLoader.LocalCache.ContainsKey(url))
			{
				WWWTextureLoader.LocalCache.Add(url, image);
			}
		}

		public static Dictionary<string, Texture2D> LocalCache = new Dictionary<string, Texture2D>();

		private string _url;
	}
}
