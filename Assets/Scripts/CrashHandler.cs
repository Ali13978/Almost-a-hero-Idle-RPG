using System;
using System.Collections.Generic;
using UnityEngine.CrashReportHandler;

public static class CrashHandler
{
	public static void SetPlayfabId(List<string> allIds)
	{
		if (allIds != null && allIds.Count > 0)
		{
			CrashReportHandler.SetUserMetadata("playfab_ids", allIds[0]);
		}
		else
		{
			CrashReportHandler.SetUserMetadata("playfab_ids", "id_not_available");
		}
	}

	public static void SetUiState(string stateNameCurrent, string stateNameTarget)
	{
		CrashReportHandler.SetUserMetadata("ui_state_current", stateNameCurrent);
		CrashReportHandler.SetUserMetadata("ui_state_target", stateNameTarget);
	}
}
