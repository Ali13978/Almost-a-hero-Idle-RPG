using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

public static class AdjustTracker
{
	public static void TrackAppLaunch(bool isFirstLaunch)
	{
		
	}

	public static void TrackIAPEventData(Product receivedProduct)
	{
		
	}

	public static void TrackAdWatchedEventData(bool isCapped, string state)
	{
		
	}

	public static void TrackLevelProgress(int prestige, int maxStageReached)
	{
		
	}

	public static void TrackTutorialCompleted()
	{
		
	}

	public static void TrackStageReachedEvent(int stage)
	{
		
	}

	public static void TrackCustomEvent(Dictionary<string, string> keyValuePairs)
	{
		
	}

	private static string ad_event = "";

	private static string app_launch = "";

	private static string custom_event = "";

	private static string iap_buy = "";

	private static string level_progress = "";

	private static string tutorial_complete = "";

	private static string stage_reached = "";
}
