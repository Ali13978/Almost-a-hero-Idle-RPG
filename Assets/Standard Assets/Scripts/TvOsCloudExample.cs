using System;
using SA.Common.Pattern;
using UnityEngine;

public class TvOsCloudExample : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("iCloudManager.Instance.init()");
		iCloudManager.OnCloudDataReceivedAction += this.OnCloudDataReceivedAction;
		Singleton<iCloudManager>.Instance.SetString("Test", "test");
		Singleton<iCloudManager>.Instance.RequestDataForKey("Test", delegate(iCloudData data)
		{
			UnityEngine.Debug.Log("Internal callback");
			if (data.IsEmpty)
			{
				UnityEngine.Debug.Log(data.Key + " / data is empty");
			}
			else
			{
				UnityEngine.Debug.Log(data.Key + " / " + data.StringValue);
			}
		});
	}

	private void OnCloudDataReceivedAction(iCloudData data)
	{
		UnityEngine.Debug.Log("OnCloudDataReceivedAction");
		if (data.IsEmpty)
		{
			UnityEngine.Debug.Log(data.Key + " / data is empty");
		}
		else
		{
			UnityEngine.Debug.Log(data.Key + " / " + data.StringValue);
		}
	}
}
