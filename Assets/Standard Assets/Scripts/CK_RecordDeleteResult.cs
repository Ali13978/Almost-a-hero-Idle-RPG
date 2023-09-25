using System;
using SA.Common.Models;

public class CK_RecordDeleteResult : Result
{
	public CK_RecordDeleteResult(int recordId)
	{
		this._RecordID = CK_RecordID.GetRecordIdByInternalId(recordId);
	}

	public CK_RecordDeleteResult(string errorData) : base(new Error(errorData))
	{
	}

	public void SetDatabase(CK_Database database)
	{
		this._Database = database;
	}

	public CK_Database Database
	{
		get
		{
			return this._Database;
		}
	}

	public CK_RecordID RecordID
	{
		get
		{
			return this._RecordID;
		}
	}

	private CK_RecordID _RecordID;

	private CK_Database _Database;
}
