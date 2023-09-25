using System;
using PlayFab;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayFabSharedSettings", menuName = "PlayFab/CreateSharedSettings", order = 1)]
public class PlayFabSharedSettings : ScriptableObject
{
	public string TitleId;

	public string VerticalName;

	public string ProductionEnvironmentUrl = string.Empty;

	public WebRequestType RequestType = WebRequestType.UnityWebRequest;

	public int RequestTimeout = 2000;

	public bool RequestKeepAlive = true;

	public bool CompressApiData = true;

	public PlayFabLogLevel LogLevel = PlayFabLogLevel.Warning | PlayFabLogLevel.Error;

	public string LoggerHost = string.Empty;

	public int LoggerPort;

	public bool EnableRealTimeLogging;

	public int LogCapLimit = 30;
}
