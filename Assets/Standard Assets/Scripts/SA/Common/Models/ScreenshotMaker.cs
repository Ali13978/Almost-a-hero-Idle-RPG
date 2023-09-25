using System;
using System.Collections;
using UnityEngine;

namespace SA.Common.Models
{
	public class ScreenshotMaker : MonoBehaviour
	{
		public static ScreenshotMaker Create()
		{
			return new GameObject("ScreenshotMaker").AddComponent<ScreenshotMaker>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void GetScreenshot()
		{
			base.StartCoroutine(this.SaveScreenshot());
		}

		private IEnumerator SaveScreenshot()
		{
			yield return new WaitForEndOfFrame();
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			tex.Apply();
			if (this.OnScreenshotReady != null)
			{
				this.OnScreenshotReady(tex);
			}
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}

		public Action<Texture2D> OnScreenshotReady = delegate(Texture2D A_0)
		{
		};
	}
}
