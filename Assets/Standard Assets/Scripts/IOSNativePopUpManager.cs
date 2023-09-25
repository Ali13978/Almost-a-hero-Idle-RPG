using System;

public class IOSNativePopUpManager
{
	public static void dismissCurrentAlert()
	{
	}

	public static void showRateUsPopUp(string title, string message, string rate, string remind, string declined)
	{
	}

	public static void showDialog(string title, string message)
	{
		IOSNativePopUpManager.showDialog(title, message, "Yes", "No");
	}

	public static void showDialog(string title, string message, string yes, string no)
	{
	}

	public static void showMessage(string title, string message)
	{
		IOSNativePopUpManager.showMessage(title, message, "Ok");
	}

	public static void showMessage(string title, string message, string ok)
	{
	}
}
