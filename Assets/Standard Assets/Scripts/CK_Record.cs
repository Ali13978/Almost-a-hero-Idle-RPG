using System;
using System.Collections.Generic;
using SA.Common.Data;
using SA.Common.Util;

public class CK_Record
{
	public CK_Record(CK_RecordID id, string type)
	{
		this._Id = id;
		this._Type = type;
		this.IndexRecord();
	}

	public CK_Record(string name, string template)
	{
		this._Id = new CK_RecordID(name);
		string[] array = template.Split(new char[]
		{
			'|'
		});
		this._Type = array[0];
		for (int i = 1; i < array.Length; i += 2)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			this.SetObject(array[i], array[i + 1]);
		}
		this.IndexRecord();
	}

	public void SetObject(string key, string value)
	{
		if (this._Data.ContainsKey(key))
		{
			this._Data[key] = value;
		}
		else
		{
			this._Data.Add(key, value);
		}
	}

	public string GetObject(string key)
	{
		if (this._Data.ContainsKey(key))
		{
			return this._Data[key];
		}
		return string.Empty;
	}

	public CK_RecordID Id
	{
		get
		{
			return this._Id;
		}
	}

	public string Type
	{
		get
		{
			return this._Type;
		}
	}

	private void IndexRecord()
	{
		this._internalId = IdFactory.NextId;
		CK_Record._Records.Add(this._internalId, this);
	}

	public void UpdateRecord()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, string> keyValuePair in this._Data)
		{
			list.Add(keyValuePair.Key);
			list2.Add(keyValuePair.Value);
		}
		ISN_CloudKit.UpdateRecord_Object(this.Internal_Id, this.Type, Converter.SerializeArray(list.ToArray(), "%%%"), Converter.SerializeArray(list2.ToArray(), "%%%"), this.Id.Internal_Id);
	}

	public int Internal_Id
	{
		get
		{
			return this._internalId;
		}
	}

	public static CK_Record GetRecordByInternalId(int id)
	{
		return CK_Record._Records[id];
	}

	private static Dictionary<int, CK_Record> _Records = new Dictionary<int, CK_Record>();

	private CK_RecordID _Id;

	private string _Type = string.Empty;

	private Dictionary<string, string> _Data = new Dictionary<string, string>();

	private int _internalId;
}
