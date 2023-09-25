using System;
using UnityEngine;

public class GooglePlayObbDownloadManager
{
	public static GooglePlayObbDownloader GetGooglePlayObbDownloader()
	{
		if (GooglePlayObbDownloadManager.m_Instance != null)
		{
			return GooglePlayObbDownloadManager.m_Instance;
		}
		if (!GooglePlayObbDownloadManager.IsDownloaderAvailable())
		{
			UnityEngine.Debug.Log("Downloader is not available");
			return null;
		}
		GooglePlayObbDownloadManager.m_Instance = new GooglePlayObbDownloader();
		return GooglePlayObbDownloadManager.m_Instance;
	}

	public static bool IsDownloaderAvailable()
	{
		return GooglePlayObbDownloadManager.m_AndroidOSBuildClass.GetRawClass() != IntPtr.Zero;
	}

	private static AndroidJavaClass m_AndroidOSBuildClass = new AndroidJavaClass("android.os.Build");

	private static GooglePlayObbDownloader m_Instance;
}
