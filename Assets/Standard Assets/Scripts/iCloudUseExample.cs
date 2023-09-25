using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class iCloudUseExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		iCloudManager.OnCloudInitAction += this.OnCloundInitAction;
		iCloudManager.OnCloudDataReceivedAction += this.OnCloudDataReceivedAction;
		iCloudManager.OnStoreDidChangeExternally += this.HandleOnStoreDidChangeExternally;
	}

	private void HandleOnStoreDidChangeExternally(List<iCloudData> changedData)
	{
		foreach (iCloudData iCloudData in changedData)
		{
			ISN_Logger.Log("Cloud data with key:  " + iCloudData.Key + " was chnaged", LogType.Log);
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(170f, 70f, 150f, 50f), "Set String"))
		{
			Singleton<iCloudManager>.Instance.SetString("TestStringKey", "Hello World");
		}
		if (GUI.Button(new Rect(170f, 130f, 150f, 50f), "Get String"))
		{
			Singleton<iCloudManager>.Instance.RequestDataForKey("TestStringKey");
		}
		if (GUI.Button(new Rect(330f, 70f, 150f, 50f), "Set Float"))
		{
			this.v += 1.1f;
			Singleton<iCloudManager>.Instance.SetFloat("TestFloatKey", this.v);
		}
		if (GUI.Button(new Rect(330f, 130f, 150f, 50f), "Get Float"))
		{
			Singleton<iCloudManager>.Instance.RequestDataForKey("TestFloatKey", delegate(iCloudData obj)
			{
				UnityEngine.Debug.Log(obj.FloatValue);
			});
		}
		if (GUI.Button(new Rect(490f, 70f, 150f, 50f), "Set Bytes"))
		{
			string s = "hello world";
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] bytes = utf8Encoding.GetBytes(s);
			Singleton<iCloudManager>.Instance.SetData("TestByteKey", bytes);
		}
		if (GUI.Button(new Rect(490f, 130f, 150f, 50f), "Get Bytes"))
		{
			Singleton<iCloudManager>.Instance.RequestDataForKey("TestByteKey");
		}
		if (GUI.Button(new Rect(170f, 500f, 150f, 50f), "TestConnection"))
		{
			base.LoadLevel("Peer-To-PeerGameExample");
		}
	}

	private void OnCloundInitAction(Result result)
	{
		if (result.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("iCloud", "Initialization Success!");
		}
		else
		{
			IOSNativePopUpManager.showMessage("iCloud", "Initialization Failed!");
		}
	}

	private void OnCloudDataReceivedAction(iCloudData data)
	{
		if (data.IsEmpty)
		{
			IOSNativePopUpManager.showMessage(data.Key, "data is empty");
		}
		else
		{
			IOSNativePopUpManager.showMessage(data.Key, data.StringValue);
		}
	}

	private void OnDestroy()
	{
		iCloudManager.OnCloudInitAction -= this.OnCloundInitAction;
		iCloudManager.OnCloudDataReceivedAction -= this.OnCloudDataReceivedAction;
	}

	private float v = 1.1f;
}
