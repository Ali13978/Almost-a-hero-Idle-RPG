using System;
using SA.Common.Models;
using UnityEngine;

public class IOSImagePickResult : Result
{
	public IOSImagePickResult(string ImageData)
	{
		if (ImageData.Length == 0)
		{
			this._Error = new Error(0, "No Image Data");
			return;
		}
		byte[] data = Convert.FromBase64String(ImageData);
		this._image = new Texture2D(1, 1);
		this._image.LoadImage(data);
		this._image.hideFlags = HideFlags.DontSave;
		if (!IOSNativeSettings.Instance.DisablePluginLogs)
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"IOSImagePickResult: w",
				this._image.width,
				" h: ",
				this._image.height
			}), LogType.Log);
		}
	}

	public Texture2D Image
	{
		get
		{
			return this._image;
		}
	}

	private Texture2D _image;
}
