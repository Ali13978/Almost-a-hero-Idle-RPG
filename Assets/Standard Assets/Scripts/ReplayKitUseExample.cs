using System;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ReplayKitUseExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		ISN_ReplayKit.ActionRecordStarted += this.HandleActionRecordStarted;
		ISN_ReplayKit.ActionRecordStoped += this.HandleActionRecordStoped;
		ISN_ReplayKit.ActionRecordInterrupted += this.HandleActionRecordInterrupted;
		ISN_ReplayKit.ActionShareDialogFinished += this.HandleActionShareDialogFinished;
		ISN_ReplayKit.ActionRecorderDidChangeAvailability += this.HandleActionRecorderDidChangeAvailability;
		IOSNativePopUpManager.showMessage("Welcome", "Hey there, welcome to the ReplayKit testing scene!");
		ISN_Logger.Log("ReplayKit Is Avaliable: " + Singleton<ISN_ReplayKit>.Instance.IsAvailable, LogType.Log);
	}

	private void OnDestroy()
	{
		ISN_ReplayKit.ActionRecordStarted -= this.HandleActionRecordStarted;
		ISN_ReplayKit.ActionRecordStoped -= this.HandleActionRecordStoped;
		ISN_ReplayKit.ActionRecordInterrupted -= this.HandleActionRecordInterrupted;
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Replay Kit", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Start Recording"))
		{
			Singleton<ISN_ReplayKit>.Instance.StartRecording(true);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Stop Recording"))
		{
			Singleton<ISN_ReplayKit>.Instance.StopRecording();
		}
	}

	private void HandleActionRecordInterrupted(Error error)
	{
		IOSNativePopUpManager.showMessage("Video was interrupted with error: ", " " + error.Message);
	}

	private void HandleActionRecordStoped(Result res)
	{
		if (res.IsSucceeded)
		{
			Singleton<ISN_ReplayKit>.Instance.ShowVideoShareDialog();
		}
		else
		{
			IOSNativePopUpManager.showMessage("Fail", "Error: " + res.Error.Message);
		}
	}

	private void HandleActionShareDialogFinished(ReplayKitVideoShareResult res)
	{
		if (res.Sources.Length > 0)
		{
			foreach (string str in res.Sources)
			{
				IOSNativePopUpManager.showMessage("Success", "User has shared the video to" + str);
			}
		}
		else
		{
			IOSNativePopUpManager.showMessage("Fail", "User declined video sharing!");
		}
	}

	private void HandleActionRecordStarted(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Success", "Record was successfully started!");
		}
		else
		{
			ISN_Logger.Log("Record start failed: " + res.Error.Message, LogType.Log);
			IOSNativePopUpManager.showMessage("Fail", "Error: " + res.Error.Message);
		}
		ISN_ReplayKit.ActionRecordStarted -= this.HandleActionRecordStarted;
	}

	private void HandleActionRecorderDidChangeAvailability(bool IsRecordingAvaliable)
	{
		ISN_Logger.Log("Is Recording Avaliable: " + IsRecordingAvaliable, LogType.Log);
		ISN_ReplayKit.ActionRecordDiscard += this.HandleActionRecordDiscard;
		Singleton<ISN_ReplayKit>.Instance.DiscardRecording();
	}

	private void HandleActionRecordDiscard()
	{
		ISN_Logger.Log("Record Discarded", LogType.Log);
	}
}
