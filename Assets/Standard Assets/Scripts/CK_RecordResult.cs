using System;
using SA.Common.Models;

public class CK_RecordResult : Result
{
	public CK_RecordResult(int recordId)
	{
		this._Record = CK_Record.GetRecordByInternalId(recordId);
	}

	public CK_RecordResult(string errorData) : base(new Error(errorData))
	{
	}

	public void SetDatabase(CK_Database database)
	{
		this._Database = database;
	}

	public CK_Record Record
	{
		get
		{
			return this._Record;
		}
	}

	public CK_Database Database
	{
		get
		{
			return this._Database;
		}
	}

	private CK_Record _Record;

	private CK_Database _Database;
}
