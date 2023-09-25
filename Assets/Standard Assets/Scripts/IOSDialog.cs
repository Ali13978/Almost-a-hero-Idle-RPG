using System;
using System.Diagnostics;
using UnityEngine;

public class IOSDialog : BaseIOSPopup
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<IOSDialogResult> OnComplete;



	public static IOSDialog Create(string title, string message)
	{
		return IOSDialog.Create(title, message, "Yes", "No");
	}

	public static IOSDialog Create(string title, string message, string yes, string no)
	{
		IOSDialog iosdialog = new GameObject("IOSPopUp").AddComponent<IOSDialog>();
		iosdialog.title = title;
		iosdialog.message = message;
		iosdialog.yes = yes;
		iosdialog.no = no;
		iosdialog.init();
		return iosdialog;
	}

	public void init()
	{
		IOSNativePopUpManager.showDialog(this.title, this.message, this.yes, this.no);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		int num = (int)Convert.ToInt16(buttonIndex);
		if (num != 0)
		{
			if (num == 1)
			{
				this.OnComplete(IOSDialogResult.NO);
			}
		}
		else
		{
			this.OnComplete(IOSDialogResult.YES);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public string yes;

	public string no;
}
