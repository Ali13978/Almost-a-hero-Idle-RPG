using System;
using System.Collections.Generic;
using SA.Common.Util;

public class CK_RecordID
{
	public CK_RecordID(string recordName)
	{
		this._internalId = IdFactory.NextId;
		this._Name = recordName;
		ISN_CloudKit.CreateRecordId_Object(this._internalId, this._Name);
		CK_RecordID._Ids.Add(this._internalId, this);
	}

	public string Name
	{
		get
		{
			return this._Name;
		}
	}

	public int Internal_Id
	{
		get
		{
			return this._internalId;
		}
	}

	public static CK_RecordID GetRecordIdByInternalId(int id)
	{
		return CK_RecordID._Ids[id];
	}

	private int _internalId;

	private string _Name;

	private static Dictionary<int, CK_RecordID> _Ids = new Dictionary<int, CK_RecordID>();
}
