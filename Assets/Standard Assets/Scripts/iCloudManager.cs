using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class iCloudManager : Singleton<iCloudManager>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnCloudInitAction;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<iCloudData> OnCloudDataReceivedAction;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<List<iCloudData>> OnStoreDidChangeExternally;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	[Obsolete("use SetString instead")]
	public void setString(string key, string val)
	{
		this.SetString(key, val);
	}

	public void SetString(string key, string val)
	{
	}

	[Obsolete("use SetFloat instead")]
	public void setFloat(string key, float val)
	{
		this.SetFloat(key, val);
	}

	public void SetFloat(string key, float val)
	{
	}

	[Obsolete("use SetData instead")]
	public void setData(string key, byte[] val)
	{
		this.SetData(key, val);
	}

	public void SetData(string key, byte[] val)
	{
	}

	public void SetObject(string key, object val)
	{
	}

	public void SetInt(string key, int val)
	{
		string val2 = Convert.ToString(val);
		this.SetString(key, val2);
	}

	public void SetLong(string key, long val)
	{
		string val2 = Convert.ToString(val);
		this.SetString(key, val2);
	}

	public void SetUlong(string key, ulong val)
	{
		string val2 = Convert.ToString(val);
		this.SetString(key, val2);
	}

	public void SetArray(string key, List<object> val)
	{
		string val2 = Json.Serialize(val);
		this.SetString(key, val2);
	}

	public void SetDictionary(string key, Dictionary<object, object> val)
	{
		string val2 = Json.Serialize(val);
		this.SetString(key, val2);
	}

	[Obsolete("use RequestDataForKey instead")]
	public void requestDataForKey(string key)
	{
		this.RequestDataForKey(key);
	}

	public void RequestDataForKey(string key)
	{
		this.RequestDataForKey(key, null);
	}

	public void RequestDataForKey(string key, Action<iCloudData> callback)
	{
		if (callback != null)
		{
			if (this.s_requestDataCallbacks.ContainsKey(key))
			{
				this.s_requestDataCallbacks[key].Add(callback);
				return;
			}
			this.s_requestDataCallbacks.Add(key, new List<Action<iCloudData>>
			{
				callback
			});
		}
	}

	private void OnCloudInit()
	{
		Result obj = new Result();
		iCloudManager.OnCloudInitAction(obj);
	}

	private void OnCloudInitFail()
	{
		Result obj = new Result(new Error());
		iCloudManager.OnCloudInitAction(obj);
	}

	private void OnCloudDataChanged(string data)
	{
		List<iCloudData> list = new List<iCloudData>();
		string[] array = data.Split(new char[]
		{
			'|'
		});
		for (int i = 0; i < array.Length; i += 2)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			iCloudData item = new iCloudData(array[i], array[i + 1]);
			list.Add(item);
		}
		iCloudManager.OnStoreDidChangeExternally(list);
	}

	private void OnCloudData(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		iCloudData iCloudData = new iCloudData(array2[0], array2[1]);
		if (this.s_requestDataCallbacks.ContainsKey(iCloudData.Key))
		{
			List<Action<iCloudData>> list = this.s_requestDataCallbacks[iCloudData.Key];
			this.s_requestDataCallbacks.Remove(iCloudData.Key);
			foreach (Action<iCloudData> action in list)
			{
				action(iCloudData);
			}
		}
		iCloudManager.OnCloudDataReceivedAction(iCloudData);
	}

	private void OnCloudDataEmpty(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			'|'
		});
		iCloudData obj = new iCloudData(array2[0], "null");
		iCloudManager.OnCloudDataReceivedAction(obj);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static iCloudManager()
	{
		iCloudManager.OnCloudInitAction = delegate(Result A_0)
		{
		};
		iCloudManager.OnCloudDataReceivedAction = delegate(iCloudData A_0)
		{
		};
		iCloudManager.OnStoreDidChangeExternally = delegate(List<iCloudData> A_0)
		{
		};
	}

	private Dictionary<string, List<Action<iCloudData>>> s_requestDataCallbacks = new Dictionary<string, List<Action<iCloudData>>>();
}
