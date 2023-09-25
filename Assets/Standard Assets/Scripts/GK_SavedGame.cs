using System;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class GK_SavedGame
{
	public GK_SavedGame(string id, string name, string device, string dateString)
	{
		this._Id = id;
		this._Name = name;
		this._DeviceName = device;
		this._OriginalDateString = dateString;
		this._ModificationDate = DateTime.Now;
		try
		{
			this._ModificationDate = DateTime.Parse(dateString);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("GK_SavedGame Date parsing issue, cnonsider to parce date byyourself using  _OriginalDateString property");
			UnityEngine.Debug.Log(ex.Message);
		}
	}

	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<GK_SaveDataLoaded> ActionDataLoaded;



	public void LoadData()
	{
		Singleton<ISN_GameSaves>.Instance.LoadSaveData(this);
	}

	public void GenerateDataLoadEvent(string base64Data)
	{
		this._Data = Convert.FromBase64String(base64Data);
		this._IsDataLoaded = true;
		GK_SaveDataLoaded obj = new GK_SaveDataLoaded(this);
		this.ActionDataLoaded(obj);
	}

	public void GenerateDataLoadFailedEvent(string erorrData)
	{
		Error error = new Error(erorrData);
		GK_SaveDataLoaded obj = new GK_SaveDataLoaded(error);
		this.ActionDataLoaded(obj);
	}

	public string Id
	{
		get
		{
			return this._Id;
		}
	}

	public string Name
	{
		get
		{
			return this._Name;
		}
	}

	public string DeviceName
	{
		get
		{
			return this._DeviceName;
		}
	}

	public DateTime ModificationDate
	{
		get
		{
			return this._ModificationDate;
		}
	}

	public string OriginalDateString
	{
		get
		{
			return this._OriginalDateString;
		}
	}

	public byte[] Data
	{
		get
		{
			return this._Data;
		}
	}

	public bool IsDataLoaded
	{
		get
		{
			return this._IsDataLoaded;
		}
	}

	private string _Id;

	private string _Name;

	private string _DeviceName;

	private DateTime _ModificationDate;

	private string _OriginalDateString;

	private byte[] _Data;

	private bool _IsDataLoaded;
}
