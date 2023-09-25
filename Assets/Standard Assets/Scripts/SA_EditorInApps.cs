using System;

public class SA_EditorInApps
{
	public static void ShowInAppPopup(string title, string describtion, string price, Action<bool> OnComplete = null)
	{
		SA_EditorInApps.EditorUI.ShowInAppPopup(title, describtion, price, OnComplete);
	}

	private static SA_InApps_EditorUIController EditorUI
	{
		get
		{
			return SA_EditorInApps._EditorUI;
		}
	}

	private static SA_InApps_EditorUIController _EditorUI;
}
