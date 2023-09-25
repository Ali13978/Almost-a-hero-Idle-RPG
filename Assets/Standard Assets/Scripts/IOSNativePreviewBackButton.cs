using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IOSNativePreviewBackButton : BaseIOSFeaturePreview
{
	public static IOSNativePreviewBackButton Create()
	{
		return new GameObject("BackButton").AddComponent<IOSNativePreviewBackButton>();
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.initialSceneName = this.loadedLevelName;
	}

	private void OnGUI()
	{
		float num = 120f;
		float x = (float)Screen.width - num * 1.2f;
		float y = num * 0.2f;
		if (!this.loadedLevelName.Equals(this.initialSceneName))
		{
			Color color = GUI.color;
			GUI.color = Color.green;
			if (GUI.Button(new Rect(x, y, num, num * 0.4f), "Back"))
			{
				base.LoadLevel(this.initialSceneName);
			}
			GUI.color = color;
		}
	}

	public string loadedLevelName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	private string initialSceneName = "scene";
}
