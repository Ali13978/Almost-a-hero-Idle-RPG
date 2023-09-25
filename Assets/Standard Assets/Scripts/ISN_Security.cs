using System;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_Security : Singleton<ISN_Security>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<ISN_LocalReceiptResult> OnReceiptLoaded;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnReceiptRefreshComplete;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RetrieveLocalReceipt()
	{
	}

	public void StartReceiptRefreshRequest()
	{
	}

	private void Event_ReceiptLoaded(string data)
	{
		ISN_LocalReceiptResult obj = new ISN_LocalReceiptResult(data);
		ISN_Security.OnReceiptLoaded(obj);
	}

	private void Event_ReceiptRefreshRequestReceived(string data)
	{
		Result obj;
		if (data.Equals("1"))
		{
			obj = new Result();
		}
		else
		{
			obj = new Result(new Error());
		}
		ISN_Security.OnReceiptRefreshComplete(obj);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_Security()
	{
		ISN_Security.OnReceiptLoaded = delegate(ISN_LocalReceiptResult A_0)
		{
		};
		ISN_Security.OnReceiptRefreshComplete = delegate(Result A_0)
		{
		};
	}
}
