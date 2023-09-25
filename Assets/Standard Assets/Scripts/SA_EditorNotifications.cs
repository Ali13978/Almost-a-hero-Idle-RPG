using System;

public class SA_EditorNotifications
{
	public static void ShowNotification(string title, string message, SA_EditorNotificationType type)
	{
		if (SA_EditorTesting.IsInsideEditor)
		{
			SA_EditorNotifications.EditorUI.ShowNotification(title, message, type);
		}
	}

	private static SA_Notifications_EditorUIController EditorUI
	{
		get
		{
			return SA_EditorNotifications._EditorUI;
		}
	}

	private static SA_Notifications_EditorUIController _EditorUI;
}
