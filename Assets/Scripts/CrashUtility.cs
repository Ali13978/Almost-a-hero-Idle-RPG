using System;
using UnityEngine;

public static class CrashUtility
{
	public static void CopyAllCrashLogsToClipboard()
	{
		CrashReport[] reports = CrashReport.reports;
		string text = string.Empty;
		text = CrashUtility.GetLogs(reports, text);
		if (text.Length > 0)
		{
			GUIUtility.systemCopyBuffer = text;
		}
	}

	public static void LogAllCrashLogsToClipboard()
	{
		CrashReport[] reports = CrashReport.reports;
		string text = string.Empty;
		text = CrashUtility.GetLogs(reports, text);
		if (text.Length > 0)
		{
			UnityEngine.Debug.Log(text);
		}
	}

	private static string GetLogs(CrashReport[] reports, string logs)
	{
		if (CrashReport.lastReport != null)
		{
			logs = CrashUtility.AddLog(logs, CrashReport.lastReport);
		}
		if (reports.Length > 0)
		{
			foreach (CrashReport item in reports)
			{
				logs = CrashUtility.AddLog(logs, item);
			}
		}
		return logs;
	}

	private static string AddLog(string logs, CrashReport item)
	{
		string str = item.time.ToString();
		string text = item.text;
		if (logs.Length > 0)
		{
			logs += Environment.NewLine;
			logs += Environment.NewLine;
		}
		logs = "## Crash time: " + str;
		logs += Environment.NewLine;
		logs += Environment.NewLine;
		logs += text;
		return logs;
	}
}
