using System;
using System.Collections.Generic;
using SA.Common.Data;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_CloudKit : Singleton<ISN_CloudKit>
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this._PublicDB = new CK_Database(-1);
		this._PrivateDB = new CK_Database(-2);
	}

	public CK_Database PrivateDB
	{
		get
		{
			return this._PrivateDB;
		}
	}

	public CK_Database PublicDB
	{
		get
		{
			return this._PublicDB;
		}
	}

	private void OnSaveRecordSuccess(string data)
	{
		string[] array = Converter.ParseArray(data, "%%%");
		int id = Convert.ToInt32(array[0]);
		int recordId = Convert.ToInt32(array[1]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		CK_RecordResult result = new CK_RecordResult(recordId);
		databaseByInternalId.FireSaveRecordResult(result);
	}

	private void OnSaveRecordFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		string errorData = array[1];
		CK_RecordResult result = new CK_RecordResult(errorData);
		databaseByInternalId.FireSaveRecordResult(result);
	}

	private void OnDeleteRecordSuccess(string data)
	{
		string[] array = Converter.ParseArray(data, "%%%");
		int id = Convert.ToInt32(array[0]);
		int recordId = Convert.ToInt32(array[1]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		CK_RecordDeleteResult result = new CK_RecordDeleteResult(recordId);
		databaseByInternalId.FireDeleteRecordResult(result);
	}

	private void OnDeleteRecordFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		string errorData = array[1];
		CK_RecordDeleteResult result = new CK_RecordDeleteResult(errorData);
		databaseByInternalId.FireDeleteRecordResult(result);
	}

	private void OnPerformQuerySuccess(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		List<CK_Record> list = new List<CK_Record>();
		for (int i = 1; i < array.Length; i += 2)
		{
			if (array[i] == "endofline")
			{
				break;
			}
			string name = array[i];
			string template = array[i + 1];
			CK_Record item = new CK_Record(name, template);
			list.Add(item);
		}
		CK_QueryResult result = new CK_QueryResult(list);
		databaseByInternalId.FireQueryCompleteResult(result);
	}

	private void OnPerformQueryFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		string errorData = array[1];
		CK_QueryResult result = new CK_QueryResult(errorData);
		databaseByInternalId.FireQueryCompleteResult(result);
	}

	private void OnFetchRecordSuccess(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		string name = array[1];
		string template = array[2];
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		CK_Record ck_Record = new CK_Record(name, template);
		CK_RecordResult result = new CK_RecordResult(ck_Record.Internal_Id);
		databaseByInternalId.FireFetchRecordResult(result);
	}

	private void OnFetchRecordFailed(string data)
	{
		string[] array = data.Split(new string[]
		{
			"|%|"
		}, StringSplitOptions.None);
		int id = Convert.ToInt32(array[0]);
		CK_Database databaseByInternalId = CK_Database.GetDatabaseByInternalId(id);
		string errorData = array[1];
		CK_RecordResult result = new CK_RecordResult(errorData);
		databaseByInternalId.FireFetchRecordResult(result);
	}

	public static void CreateRecordId_Object(int recordId, string name)
	{
	}

	public static void UpdateRecord_Object(int ID, string type, string keys, string values, int recordId)
	{
	}

	public static void SaveRecord(int dbId, int recordId)
	{
	}

	public static void DeleteRecord(int dbId, int recordId)
	{
	}

	public static void FetchRecord(int dbId, int recordId)
	{
	}

	public static void PerformQuery(int dbId, string query, string type)
	{
	}

	private CK_Database _PrivateDB;

	private CK_Database _PublicDB;

	private const int PUBLIC_DB_KEY = -1;

	private const int PRIVATE_DB_KEY = -2;
}
