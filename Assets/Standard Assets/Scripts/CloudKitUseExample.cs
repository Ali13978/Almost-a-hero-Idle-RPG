using System;
using SA.Common.Pattern;
using UnityEngine;

public class CloudKitUseExample : BaseIOSFeaturePreview
{
	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Cloud Kit", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Create Record"))
		{
			CK_RecordID id = new CK_RecordID("1");
			CK_Record ck_Record = new CK_Record(id, "Post");
			ck_Record.SetObject("PostText", "Sample point of interest");
			ck_Record.SetObject("PostTitle", "My favorite point of interest");
			CK_Database privateDB = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB.SaveRecrod(ck_Record);
			privateDB.ActionRecordSaved += this.Database_ActionRecordSaved;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Delete Record"))
		{
			CK_RecordID recordId = new CK_RecordID("1");
			CK_Database privateDB2 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB2.DeleteRecordWithID(recordId);
			privateDB2.ActionRecordDeleted += this.Database_ActionRecordDeleted;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Fetch Record"))
		{
			CK_RecordID recordId2 = new CK_RecordID("1");
			CK_Database privateDB3 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB3.FetchRecordWithID(recordId2);
			privateDB3.ActionRecordFetchComplete += this.Database_ActionRecordFetchComplete;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Fetch And Update"))
		{
			CK_RecordID recordId3 = new CK_RecordID("1");
			CK_Database privateDB4 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			privateDB4.FetchRecordWithID(recordId3);
			privateDB4.ActionRecordFetchComplete += this.Database_ActionRecordFetchForUpdateComplete;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Perform Query"))
		{
			CK_Database privateDB5 = Singleton<ISN_CloudKit>.Instance.PrivateDB;
			CK_Query query = new CK_Query("TRUEPREDICATE", "Post");
			privateDB5.ActionQueryComplete += this.Database_ActionQueryComplete;
			privateDB5.PerformQuery(query);
		}
	}

	private void Database_ActionQueryComplete(CK_QueryResult res)
	{
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionQueryComplete, total records found: " + res.Records.Count, LogType.Log);
			foreach (CK_Record ck_Record in res.Records)
			{
				UnityEngine.Debug.Log(ck_Record.Id.Name);
				ISN_Logger.Log("Post Title: " + ck_Record.GetObject("PostTitle"), LogType.Log);
			}
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Database_ActionRecordFetchComplete, Error: ",
				res.Error.Code,
				" / ",
				res.Error.Message
			}), LogType.Log);
		}
	}

	private void Database_ActionRecordFetchComplete(CK_RecordResult res)
	{
		res.Database.ActionRecordFetchComplete -= this.Database_ActionRecordFetchComplete;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordFetchComplete:", LogType.Log);
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"), LogType.Log);
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Database_ActionRecordFetchComplete, Error: ",
				res.Error.Code,
				" / ",
				res.Error.Message
			}), LogType.Log);
		}
	}

	private void Database_ActionRecordFetchForUpdateComplete(CK_RecordResult res)
	{
		res.Database.ActionRecordFetchComplete -= this.Database_ActionRecordFetchForUpdateComplete;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordFetchComplete:", LogType.Log);
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"), LogType.Log);
			ISN_Logger.Log("Updatting Title: ", LogType.Log);
			CK_Record record = res.Record;
			record.SetObject("PostTitle", "My favorite point of interest - updated");
			Singleton<ISN_CloudKit>.Instance.PrivateDB.SaveRecrod(record);
			Singleton<ISN_CloudKit>.Instance.PrivateDB.ActionRecordSaved += this.Database_ActionRecordSaved;
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Database_ActionRecordFetchComplete, Error: ",
				res.Error.Code,
				" / ",
				res.Error.Message
			}), LogType.Log);
		}
	}

	private void Database_ActionRecordDeleted(CK_RecordDeleteResult res)
	{
		res.Database.ActionRecordDeleted -= this.Database_ActionRecordDeleted;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordDeleted, Success: ", LogType.Log);
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Database_ActionRecordDeleted, Error: ",
				res.Error.Code,
				" / ",
				res.Error.Message
			}), LogType.Log);
		}
	}

	private void Database_ActionRecordSaved(CK_RecordResult res)
	{
		res.Database.ActionRecordSaved -= this.Database_ActionRecordSaved;
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Database_ActionRecordSaved:", LogType.Log);
			ISN_Logger.Log("Post Title: " + res.Record.GetObject("PostTitle"), LogType.Log);
		}
		else
		{
			ISN_Logger.Log(string.Concat(new object[]
			{
				"Database_ActionRecordSaved, Error: ",
				res.Error.Code,
				" / ",
				res.Error.Message
			}), LogType.Log);
		}
	}
}
