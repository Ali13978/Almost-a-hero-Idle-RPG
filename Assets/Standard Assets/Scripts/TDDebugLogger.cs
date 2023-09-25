using System;
using Tapdaq;
using UnityEngine;

public class TDDebugLogger
{
	public static void Log(object obj)
	{
		if (TDSettings.getInstance().isDebugMode)
		{
			UnityEngine.Debug.Log(obj);
		}
	}

	public static void LogWarning(object obj)
	{
		if (TDSettings.getInstance().isDebugMode)
		{
			UnityEngine.Debug.LogWarning(obj);
		}
	}

	public static void LogError(object obj)
	{
		if (TDSettings.getInstance().isDebugMode)
		{
			UnityEngine.Debug.LogError(obj);
		}
	}

	public static void LogException(Exception obj)
	{
		if (TDSettings.getInstance().isDebugMode)
		{
			UnityEngine.Debug.LogException(obj);
		}
	}
}
