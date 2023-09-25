using System;
using System.Diagnostics;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_ReplayKit : Singleton<ISN_ReplayKit>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> ActionRecordStarted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> ActionRecordStoped;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<ReplayKitVideoShareResult> ActionShareDialogFinished;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Error> ActionRecordInterrupted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<bool> ActionRecorderDidChangeAvailability;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action ActionRecordDiscard;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartRecording(bool microphoneEnabled = true)
	{
		this._IsRecodingAvailableToShare = false;
	}

	public void StopRecording()
	{
	}

	public void DiscardRecording()
	{
		this._IsRecodingAvailableToShare = false;
	}

	public void ShowVideoShareDialog()
	{
		this._IsRecodingAvailableToShare = false;
	}

	public bool IsRecording
	{
		get
		{
			return false;
		}
	}

	public bool IsRecodingAvailableToShare
	{
		get
		{
			return this._IsRecodingAvailableToShare;
		}
	}

	public bool IsAvailable
	{
		get
		{
			return false;
		}
	}

	public bool IsMicEnabled
	{
		get
		{
			return false;
		}
	}

	private void OnRecorStartSuccess(string data)
	{
		Result obj = new Result();
		ISN_ReplayKit.ActionRecordStarted(obj);
	}

	private void OnRecorStartFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		ISN_ReplayKit.ActionRecordStarted(obj);
	}

	private void OnRecorStopFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		ISN_ReplayKit.ActionRecordStoped(obj);
	}

	private void OnRecorStopSuccess()
	{
		this._IsRecodingAvailableToShare = true;
		Result obj = new Result();
		ISN_ReplayKit.ActionRecordStoped(obj);
	}

	private void OnRecordInterrupted(string errorData)
	{
		this._IsRecodingAvailableToShare = false;
		Error obj = new Error(errorData);
		ISN_ReplayKit.ActionRecordInterrupted(obj);
	}

	private void OnRecorderDidChangeAvailability(string data)
	{
		ISN_ReplayKit.ActionRecorderDidChangeAvailability(this.IsAvailable);
	}

	private void OnSaveResult(string sourcesData)
	{
		string[] sourcesArray = Converter.ParseArray(sourcesData, "%%%");
		ReplayKitVideoShareResult obj = new ReplayKitVideoShareResult(sourcesArray);
		ISN_ReplayKit.ActionShareDialogFinished(obj);
	}

	public void OnRecordDiscard(string data)
	{
		this._IsRecodingAvailableToShare = false;
		ISN_ReplayKit.ActionRecordDiscard();
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_ReplayKit()
	{
		ISN_ReplayKit.ActionRecordStarted = delegate(Result A_0)
		{
		};
		ISN_ReplayKit.ActionRecordStoped = delegate(Result A_0)
		{
		};
		ISN_ReplayKit.ActionShareDialogFinished = delegate(ReplayKitVideoShareResult A_0)
		{
		};
		ISN_ReplayKit.ActionRecordInterrupted = delegate(Error A_0)
		{
		};
		ISN_ReplayKit.ActionRecorderDidChangeAvailability = delegate(bool A_0)
		{
		};
		ISN_ReplayKit.ActionRecordDiscard = delegate()
		{
		};
	}

	private bool _IsRecodingAvailableToShare;
}
