using System;
using System.Collections.Generic;
using System.Diagnostics;

public class CK_Database
{
	public CK_Database(int internalId)
	{
		this._InternalId = internalId;
		CK_Database._Databases.Add(this._InternalId, this);
	}

	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<CK_RecordResult> ActionRecordSaved;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<CK_RecordResult> ActionRecordFetchComplete;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<CK_RecordDeleteResult> ActionRecordDeleted;



	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<CK_QueryResult> ActionQueryComplete;



	public void SaveRecrod(CK_Record record)
	{
		record.UpdateRecord();
		ISN_CloudKit.SaveRecord(this._InternalId, record.Internal_Id);
	}

	public void FetchRecordWithID(CK_RecordID recordId)
	{
		ISN_CloudKit.FetchRecord(this._InternalId, recordId.Internal_Id);
	}

	public void DeleteRecordWithID(CK_RecordID recordId)
	{
		ISN_CloudKit.DeleteRecord(this._InternalId, recordId.Internal_Id);
	}

	public void PerformQuery(CK_Query query)
	{
		ISN_CloudKit.PerformQuery(this._InternalId, query.Predicate, query.RecordType);
	}

	public int InternalId
	{
		get
		{
			return this._InternalId;
		}
	}

	public static CK_Database GetDatabaseByInternalId(int id)
	{
		return CK_Database._Databases[id];
	}

	public void FireSaveRecordResult(CK_RecordResult result)
	{
		result.SetDatabase(this);
		this.ActionRecordSaved(result);
	}

	public void FireFetchRecordResult(CK_RecordResult result)
	{
		result.SetDatabase(this);
		this.ActionRecordFetchComplete(result);
	}

	public void FireDeleteRecordResult(CK_RecordDeleteResult result)
	{
		result.SetDatabase(this);
		this.ActionRecordDeleted(result);
	}

	public void FireQueryCompleteResult(CK_QueryResult result)
	{
		result.SetDatabase(this);
		this.ActionQueryComplete(result);
	}

	private static Dictionary<int, CK_Database> _Databases = new Dictionary<int, CK_Database>();

	private int _InternalId;
}
