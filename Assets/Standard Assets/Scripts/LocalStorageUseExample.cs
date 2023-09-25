using System;
using SA.IOSNative.Storage;
using UnityEngine;

public class LocalStorageUseExample : MonoBehaviour
{
	private void Start()
	{
		AppCache.Save("TEST_KEY", "Some String");
		string @string = AppCache.GetString("TEST_KEY");
		UnityEngine.Debug.Log(@string);
		AppCache.Remove("TEST_KEY");
		Texture2D texture = new Texture2D(1, 1);
		AppCache.Save("TEST_IMAGE_KEY", texture);
		Texture2D texture2 = AppCache.GetTexture("TEST_IMAGE_KEY");
		UnityEngine.Debug.Log(texture2);
		byte[] data = null;
		AppCache.Save("TEST_DATA_KEY", data);
		byte[] data2 = AppCache.GetData("TEST_DATA_KEY");
		UnityEngine.Debug.Log(data2);
	}
}
