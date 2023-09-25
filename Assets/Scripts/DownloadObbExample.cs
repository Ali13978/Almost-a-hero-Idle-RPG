using System;
using UnityEngine;

public class DownloadObbExample : MonoBehaviour
{
	private void Start()
	{
		this.m_obbDownloader = GooglePlayObbDownloadManager.GetGooglePlayObbDownloader();
		this.m_obbDownloader.PublicKey = string.Empty;
	}

	private void OnGUI()
	{
		if (!GooglePlayObbDownloadManager.IsDownloaderAvailable())
		{
			GUI.Label(new Rect(10f, 10f, (float)(Screen.width - 10), 20f), "Use GooglePlayDownloader only on Android device!");
			return;
		}
		string expansionFilePath = this.m_obbDownloader.GetExpansionFilePath();
		if (expansionFilePath == null)
		{
			GUI.Label(new Rect(10f, 10f, (float)(Screen.width - 10), 20f), "External storage is not available!");
		}
		else
		{
			string mainOBBPath = this.m_obbDownloader.GetMainOBBPath();
			string patchOBBPath = this.m_obbDownloader.GetPatchOBBPath();
			GUI.Label(new Rect(10f, 10f, (float)(Screen.width - 10), 20f), "Main = ..." + ((mainOBBPath != null) ? mainOBBPath.Substring(expansionFilePath.Length) : " NOT AVAILABLE"));
			GUI.Label(new Rect(10f, 25f, (float)(Screen.width - 10), 20f), "Patch = ..." + ((patchOBBPath != null) ? patchOBBPath.Substring(expansionFilePath.Length) : " NOT AVAILABLE"));
			if ((mainOBBPath == null || patchOBBPath == null) && GUI.Button(new Rect(10f, 100f, 100f, 100f), "Fetch OBBs"))
			{
				this.m_obbDownloader.FetchOBB();
			}
		}
	}

	private IGooglePlayObbDownloader m_obbDownloader;
}
