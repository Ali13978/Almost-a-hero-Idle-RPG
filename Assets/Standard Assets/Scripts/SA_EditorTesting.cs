using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class SA_EditorTesting
{
	public static bool IsInsideEditor
	{
		get
		{
			bool result = false;
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				result = true;
			}
			return result;
		}
	}

	public static bool HasFill(float fillRate)
	{
		int num = UnityEngine.Random.Range(1, 100);
		return (float)num <= fillRate;
	}

	public static void CheckForEventSystem()
	{
		EventSystem x = (EventSystem)UnityEngine.Object.FindObjectOfType(typeof(EventSystem));
		if (x == null)
		{
		}
	}

	public const int DEFAULT_SORT_ORDER = 10000;
}
