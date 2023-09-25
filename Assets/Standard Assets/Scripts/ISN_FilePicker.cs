using System;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_FilePicker : Singleton<ISN_FilePicker>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<ISN_FilePickerResult> MediaPickFinished;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void PickFromCameraRoll(int maxItemsCount = 0)
	{
	}

	private void OnSelectImagesComplete(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		ISN_FilePickerResult isn_FilePickerResult = new ISN_FilePickerResult();
		if (data.Equals(string.Empty))
		{
			ISN_FilePicker.MediaPickFinished(isn_FilePickerResult);
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			string s = array[i];
			byte[] data2 = Convert.FromBase64String(s);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(data2);
			texture2D.hideFlags = HideFlags.DontSave;
			isn_FilePickerResult.PickedImages.Add(texture2D);
		}
		ISN_FilePicker.MediaPickFinished(isn_FilePickerResult);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_FilePicker()
	{
		ISN_FilePicker.MediaPickFinished = delegate(ISN_FilePickerResult A_0)
		{
		};
	}
}
