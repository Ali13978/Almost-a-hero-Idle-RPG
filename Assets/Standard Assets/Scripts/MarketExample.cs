using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class MarketExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "In-App Purchases", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Init"))
		{
			PaymentManagerExample.init();
		}
		if (Singleton<PaymentManager>.Instance.IsStoreLoaded)
		{
			GUI.enabled = true;
		}
		else
		{
			GUI.enabled = false;
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Perform Buy #1"))
		{
			PaymentManagerExample.buyItem("your.product.id1.here");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Perform Buy #2"))
		{
			PaymentManagerExample.buyItem("your.product.id2.here");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Restore Purchases"))
		{
			Singleton<PaymentManager>.Instance.RestorePurchases();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Verify Last Purchases"))
		{
			Singleton<PaymentManager>.Instance.VerifyLastPurchase("https://sandbox.itunes.apple.com/verifyReceipt");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Load Product View"))
		{
			StoreProductView storeProductView = new StoreProductView(new string[]
			{
				"333700869"
			});
			storeProductView.Dismissed += this.StoreProductViewDisnissed;
			storeProductView.Load();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Is Payments Enabled On device"))
		{
			IOSNativePopUpManager.showMessage("Payments Settings State", "Is Payments Enabled: " + Singleton<PaymentManager>.Instance.IsInAppPurchasesEnabled);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.enabled = true;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Local Receipt Validation", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)(this.buttonWidth + 10), (float)this.buttonHeight), "Load Receipt"))
		{
			ISN_Security.OnReceiptLoaded += this.OnReceiptLoaded;
			Singleton<ISN_Security>.Instance.RetrieveLocalReceipt();
		}
	}

	private void StoreProductViewDisnissed()
	{
		ISN_Logger.Log("Store Product View was dismissed", LogType.Log);
	}

	private void OnReceiptLoaded(ISN_LocalReceiptResult result)
	{
		ISN_Logger.Log("OnReceiptLoaded", LogType.Log);
		ISN_Security.OnReceiptLoaded -= this.OnReceiptLoaded;
		if (result.Receipt != null)
		{
			this.ReceiptData = result.Receipt;
			IOSDialog iosdialog = IOSDialog.Create("Success", "Receipt loaded, byte array length: " + result.Receipt.Length + " Would you like to veriday it with Apple Sandbox server?");
			iosdialog.OnComplete += this.OnVerifayComplete;
		}
		else
		{
			IOSDialog iosdialog2 = IOSDialog.Create("Failed", "No Receipt found on the device. Would you like to refresh local Receipt?");
			iosdialog2.OnComplete += this.OnComplete;
		}
	}

	private void OnVerifayComplete(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			base.StartCoroutine(this.SendRequest());
		}
	}

	private IEnumerator SendRequest()
	{
		string base64string = Convert.ToBase64String(this.ReceiptData);
		string data = Json.Serialize(new Dictionary<string, object>
		{
			{
				"receipt-data",
				base64string
			}
		});
		byte[] binaryData = Encoding.UTF8.GetBytes(data);
		WWW www = new WWW("https://sandbox.itunes.apple.com/verifyReceipt", binaryData);
		yield return www;
		if (www.error == null)
		{
			UnityEngine.Debug.Log(www.text);
		}
		else
		{
			UnityEngine.Debug.Log(www.error);
		}
		yield break;
	}

	private void OnComplete(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			ISN_Security.OnReceiptRefreshComplete += this.OnReceiptRefreshComplete;
			Singleton<ISN_Security>.Instance.StartReceiptRefreshRequest();
		}
	}

	private void OnReceiptRefreshComplete(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSDialog iosdialog = IOSDialog.Create("Success", "Receipt Refreshed, would you like to check it again?");
			iosdialog.OnComplete += this.Dialog_RetrieveLocalReceipt;
		}
		else
		{
			IOSNativePopUpManager.showMessage("Fail", "Receipt Refresh Failed");
		}
	}

	private void Dialog_RetrieveLocalReceipt(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			ISN_Security.OnReceiptLoaded += this.OnReceiptLoaded;
			Singleton<ISN_Security>.Instance.RetrieveLocalReceipt();
		}
	}

	private byte[] ReceiptData;
}
