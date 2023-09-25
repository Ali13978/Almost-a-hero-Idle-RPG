using System;
using System.Collections;
using UnityEngine;

namespace Tapdaq
{
	public class SpriteLoader : MonoBehaviour
	{
		public static SpriteLoader Instance
		{
			get
			{
				if (SpriteLoader.instance == null)
				{
					SpriteLoader[] array = UnityEngine.Object.FindObjectsOfType<SpriteLoader>();
					if (array.Length > 0)
					{
						SpriteLoader.instance = array[0];
					}
					else
					{
						SpriteLoader.instance = new GameObject("SpriteLoader").AddComponent<SpriteLoader>();
					}
				}
				return SpriteLoader.instance;
			}
		}

		public void LoadTextureAsync(string url, Action<Texture2D> action)
		{
			base.StartCoroutine(this.LoadTexture(url, action));
		}

		private IEnumerator LoadTexture(string url, Action<Texture2D> action)
		{
			WWW www = new WWW(url);
			yield return www;
			if (action != null)
			{
				action(www.texture);
			}
			yield break;
		}

		private static SpriteLoader instance;
	}
}
