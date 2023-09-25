using System;
using System.Diagnostics;
using UnityEngine;

public class IOSMessage : BaseIOSPopup
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action OnComplete;



	public static IOSMessage Create(string title, string message)
	{
		return IOSMessage.Create(title, message, "Ok");
	}

	public static IOSMessage Create(string title, string message, string ok)
	{
		IOSMessage iosmessage = new GameObject("IOSPopUp").AddComponent<IOSMessage>();
		iosmessage.title = title;
		iosmessage.message = message;
		iosmessage.ok = ok;
		iosmessage.init();
		return iosmessage;
	}

	public void init()
	{
		IOSNativePopUpManager.showMessage(this.title, this.message, this.ok);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		this.OnComplete();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public string ok;
}
