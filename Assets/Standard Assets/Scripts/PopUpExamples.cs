using System;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class PopUpExamples : BaseIOSFeaturePreview
{
	private void Awake()
	{
		IOSNativePopUpManager.showMessage("Welcome", "Hey there, welcome to the Pop-ups testing scene!");
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Native Pop-ups", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Rate Pop-up with events"))
		{
			IOSRateUsPopUp iosrateUsPopUp = IOSRateUsPopUp.Create("Like this game?", "Please rate to support future updates!");
			iosrateUsPopUp.OnComplete += this.onRatePopUpClose;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Dialog Pop-up"))
		{
			IOSDialog iosdialog = IOSDialog.Create("Dialog Title", "Dialog message");
			iosdialog.OnComplete += this.onDialogClose;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Message Pop-up"))
		{
			IOSMessage iosmessage = IOSMessage.Create("Message Title", "Message body");
			iosmessage.OnComplete += this.onMessageClose;
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Dismissed Pop-up"))
		{
			base.Invoke("dismissAlert", 2f);
			IOSMessage.Create("Hello", "I will die in 2 sec");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Open App Store"))
		{
			IOSNativeUtility.RedirectToAppStoreRatingPage();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Preloader "))
		{
			IOSNativeUtility.ShowPreloader();
			base.Invoke("HidePreloader", 3f);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Hide Preloader"))
		{
			this.HidePreloader();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Locale"))
		{
			IOSNativeUtility.OnLocaleLoaded += this.GetLocale;
			Singleton<IOSNativeUtility>.Instance.GetLocale();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Store Review Popop"))
		{
			if (SK_StoreReviewController.IsAvaliable)
			{
				SK_StoreReviewController.RrequestReview();
			}
			else
			{
				UnityEngine.Debug.Log("StoreReviewController is not avaliable");
			}
		}
	}

	private void HidePreloader()
	{
		IOSNativeUtility.HidePreloader();
	}

	private void dismissAlert()
	{
		IOSNativePopUpManager.dismissCurrentAlert();
	}

	private void onRatePopUpClose(IOSDialogResult result)
	{
		if (result != IOSDialogResult.RATED)
		{
			if (result != IOSDialogResult.REMIND)
			{
				if (result == IOSDialogResult.DECLINED)
				{
					ISN_Logger.Log("Decline button pressed", LogType.Log);
				}
			}
			else
			{
				ISN_Logger.Log("Remind button pressed", LogType.Log);
			}
		}
		else
		{
			ISN_Logger.Log("Rate button pressed", LogType.Log);
		}
		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	private void onDialogClose(IOSDialogResult result)
	{
		if (result != IOSDialogResult.YES)
		{
			if (result == IOSDialogResult.NO)
			{
				ISN_Logger.Log("No button pressed", LogType.Log);
			}
		}
		else
		{
			ISN_Logger.Log("Yes button pressed", LogType.Log);
		}
		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	private void onMessageClose()
	{
		ISN_Logger.Log("Message was just closed", LogType.Log);
		IOSNativePopUpManager.showMessage("Result", "Message Closed");
	}

	private void GetLocale(ISN_Locale locale)
	{
		ISN_Logger.Log("GetLocale", LogType.Log);
		ISN_Logger.Log(locale.DisplayCountry, LogType.Log);
		IOSNativePopUpManager.showMessage("Locale Info:", string.Concat(new string[]
		{
			"Country:",
			locale.CountryCode,
			"/",
			locale.DisplayCountry,
			"  :   Language:",
			locale.LanguageCode,
			"/",
			locale.DisplayLanguage
		}));
		IOSNativeUtility.OnLocaleLoaded -= this.GetLocale;
	}
}
