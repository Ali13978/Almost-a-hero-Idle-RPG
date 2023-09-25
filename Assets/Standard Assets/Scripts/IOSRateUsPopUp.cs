using System;
using System.Diagnostics;
using UnityEngine;

public class IOSRateUsPopUp : BaseIOSPopup
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<IOSDialogResult> OnComplete;



	public static IOSRateUsPopUp Create()
	{
		return IOSRateUsPopUp.Create("Like the Game?", "Rate Us");
	}

	public static IOSRateUsPopUp Create(string title, string message)
	{
		return IOSRateUsPopUp.Create(title, message, "Rate Now", "Ask me later", "No, thanks");
	}

	public static IOSRateUsPopUp Create(string title, string message, string rate, string remind, string declined)
	{
		IOSRateUsPopUp iosrateUsPopUp = new GameObject("IOSRateUsPopUp").AddComponent<IOSRateUsPopUp>();
		iosrateUsPopUp.title = title;
		iosrateUsPopUp.message = message;
		iosrateUsPopUp.rate = rate;
		iosrateUsPopUp.remind = remind;
		iosrateUsPopUp.declined = declined;
		iosrateUsPopUp.init();
		return iosrateUsPopUp;
	}

	public void init()
	{
		IOSNativePopUpManager.showRateUsPopUp(this.title, this.message, this.rate, this.remind, this.declined);
	}

	public void onPopUpCallBack(string buttonIndex)
	{
		int num = (int)Convert.ToInt16(buttonIndex);
		if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					this.OnComplete(IOSDialogResult.DECLINED);
				}
			}
			else
			{
				this.OnComplete(IOSDialogResult.REMIND);
			}
		}
		else
		{
			IOSNativeUtility.RedirectToAppStoreRatingPage();
			this.OnComplete(IOSDialogResult.RATED);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public string rate;

	public string remind;

	public string declined;
}
