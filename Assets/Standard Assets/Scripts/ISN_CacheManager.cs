using System;
using UnityEngine;

public class ISN_CacheManager : MonoBehaviour
{
	public static void SaveAchievementRequest(string achievementId, float percent)
	{
		if (!IOSNativeSettings.Instance.UseGCRequestCaching)
		{
			return;
		}
		string text = ISN_CacheManager.SavedData;
		string text2 = achievementId + "&" + percent.ToString();
		if (text != string.Empty)
		{
			text = text + "|" + text2;
		}
		else
		{
			text = text2;
		}
		ISN_CacheManager.SavedData = text;
	}

	public static void SendAchievementCachedRequest()
	{
		string savedData = ISN_CacheManager.SavedData;
		if (savedData != string.Empty)
		{
			string[] array = savedData.Split(new char[]
			{
				"|"[0]
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					"&"[0]
				});
				GameCenterManager.SubmitAchievementNoCache(Convert.ToSingle(array3[1]), array3[0]);
			}
		}
		ISN_CacheManager.Clear();
	}

	public static void Clear()
	{
		PlayerPrefs.DeleteKey("ISN_Cache");
	}

	public static string SavedData
	{
		get
		{
			if (PlayerPrefs.HasKey("ISN_Cache"))
			{
				return PlayerPrefs.GetString("ISN_Cache");
			}
			return string.Empty;
		}
		set
		{
			PlayerPrefs.SetString("ISN_Cache", value);
		}
	}

	private const string DATA_SPLITTER = "|";

	private const string ACHIEVEMENT_SPLITTER = "&";

	private const string GA_DATA_CACHE_KEY = "ISN_Cache";
}
