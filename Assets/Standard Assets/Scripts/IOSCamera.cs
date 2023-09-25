using System;
using System.Collections;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class IOSCamera : Singleton<IOSCamera>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<IOSImagePickResult> OnImagePicked;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnImageSaved;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<string> OnVideoPathPicked;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.Init();
	}

	public void Init()
	{
		if (this._IsInitialized)
		{
			return;
		}
		this._IsInitialized = true;
	}

	public void SaveTextureToCameraRoll(Texture2D texture)
	{
	}

	public void SaveScreenshotToCameraRoll()
	{
		base.StartCoroutine(this.SaveScreenshot());
	}

	public void GetVideoPathFromAlbum()
	{
	}

	[Obsolete("GetImageFromAlbum is deprecated, please use PickImage(ISN_ImageSource.Album) ")]
	public void GetImageFromAlbum()
	{
		this.PickImage(ISN_ImageSource.Album);
	}

	[Obsolete("GetImageFromCamera is deprecated, please use PickImage(ISN_ImageSource.Camera) ")]
	public void GetImageFromCamera()
	{
		this.PickImage(ISN_ImageSource.Camera);
	}

	public void PickImage(ISN_ImageSource source)
	{
		if (this._IsWaitngForResponce)
		{
			return;
		}
		this._IsWaitngForResponce = true;
	}

	private void OnImagePickedEvent(string data)
	{
		this._IsWaitngForResponce = false;
		IOSImagePickResult obj = new IOSImagePickResult(data);
		IOSCamera.OnImagePicked(obj);
	}

	private void OnImageSaveFailed()
	{
		Result obj = new Result(new Error());
		IOSCamera.OnImageSaved(obj);
	}

	private void OnImageSaveSuccess()
	{
		Result obj = new Result();
		IOSCamera.OnImageSaved(obj);
	}

	private void OnVideoPickedEvent(string path)
	{
		IOSCamera.OnVideoPathPicked(path);
	}

	private IEnumerator SaveScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		this.SaveTextureToCameraRoll(tex);
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	// Note: this type is marked as 'beforefieldinit'.
	static IOSCamera()
	{
		IOSCamera.OnImagePicked = delegate(IOSImagePickResult A_0)
		{
		};
		IOSCamera.OnImageSaved = delegate(Result A_0)
		{
		};
		IOSCamera.OnVideoPathPicked = delegate(string A_0)
		{
		};
	}

	private bool _IsWaitngForResponce;

	private bool _IsInitialized;
}
