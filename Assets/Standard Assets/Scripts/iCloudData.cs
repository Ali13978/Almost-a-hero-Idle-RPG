using System;
using System.Collections.Generic;
using SA.Common.Data;
using UnityEngine;

public class iCloudData
{
	public iCloudData(string k, string v)
	{
		this.m_key = k;
		this.m_val = v;
		if (this.m_val.Equals("null"))
		{
			if (!IOSNativeSettings.Instance.DisablePluginLogs)
			{
				ISN_Logger.Log("ISN iCloud Empty set", LogType.Log);
			}
			this.m_IsEmpty = true;
		}
	}

	public T GetObject<T>()
	{
		return default(T);
	}

	[Obsolete("use Key instead")]
	public string key
	{
		get
		{
			return this.Key;
		}
	}

	public string Key
	{
		get
		{
			return this.m_key;
		}
	}

	public bool IsEmpty
	{
		get
		{
			return this.m_IsEmpty;
		}
	}

	[Obsolete("use StringValue instead")]
	public string stringValue
	{
		get
		{
			return this.StringValue;
		}
	}

	public string StringValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return null;
			}
			return this.m_val;
		}
	}

	[Obsolete("use FloatValue instead")]
	public float floatValue
	{
		get
		{
			return this.FloatValue;
		}
	}

	public float FloatValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return 0f;
			}
			return Convert.ToSingle(this.m_val);
		}
	}

	[Obsolete("use BytesValue instead")]
	public byte[] bytesValue
	{
		get
		{
			return this.BytesValue;
		}
	}

	public byte[] BytesValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return null;
			}
			return Convert.FromBase64String(this.m_val);
		}
	}

	public List<object> ListValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return new List<object>();
			}
			return (List<object>)Json.Deserialize(this.m_val);
		}
	}

	public Dictionary<string, object> DictionaryValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return new Dictionary<string, object>();
			}
			return (Dictionary<string, object>)Json.Deserialize(this.m_val);
		}
	}

	public int IntValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return 0;
			}
			return Convert.ToInt32(this.m_val);
		}
	}

	public long LongValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return 0L;
			}
			return Convert.ToInt64(this.m_val);
		}
	}

	public ulong UlongValue
	{
		get
		{
			if (this.m_IsEmpty)
			{
				return 0UL;
			}
			return Convert.ToUInt64(this.m_val);
		}
	}

	private string m_key;

	private string m_val;

	private bool m_IsEmpty;
}
