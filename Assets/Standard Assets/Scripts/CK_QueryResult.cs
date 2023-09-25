using System;
using System.Collections.Generic;
using SA.Common.Models;

public class CK_QueryResult : Result
{
	public CK_QueryResult(List<CK_Record> records)
	{
		this._Records = records;
	}

	public CK_QueryResult(string errorData) : base(new Error(errorData))
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

	public List<CK_Record> Records
	{
		get
		{
			return this._Records;
		}
	}

	private CK_Database _Database;

	private List<CK_Record> _Records = new List<CK_Record>();
}
