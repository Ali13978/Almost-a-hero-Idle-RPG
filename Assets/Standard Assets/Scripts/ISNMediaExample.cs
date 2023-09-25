using System;
using SA.Common.Pattern;
using UnityEngine;

public class ISNMediaExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		ISN_MediaController.ActionQueueUpdated += this.HandleActionQueueUpdated;
		ISN_MediaController.ActionMediaPickerResult += this.HandleActionMediaPickerResult;
		ISN_MediaController.ActionPlaybackStateChanged += this.HandleActionPlaybackStateChanged;
		ISN_MediaController.ActionNowPlayingItemChanged += this.HandleActionNowPlayingItemChanged;
	}

	private void HandleActionNowPlayingItemChanged(MP_MediaItem item)
	{
		ISN_Logger.Log("Now Playing Item Changed: " + Singleton<ISN_MediaController>.Instance.NowPlayingItem.Title, LogType.Log);
	}

	private void HandleActionPlaybackStateChanged(MP_MusicPlaybackState state)
	{
		ISN_Logger.Log("Playback State Changed: " + Singleton<ISN_MediaController>.Instance.State.ToString(), LogType.Log);
	}

	private void HandleActionQueueUpdated(MP_MediaPickerResult res)
	{
		if (res.IsSucceeded)
		{
			foreach (MP_MediaItem mp_MediaItem in res.Items)
			{
				ISN_Logger.Log("Item: " + mp_MediaItem.Title + " / " + mp_MediaItem.Id, LogType.Log);
			}
		}
		else
		{
			ISN_Logger.Log("Queue Updated failed: " + res.Error.Message, LogType.Log);
		}
	}

	private void HandleActionMediaPickerResult(MP_MediaPickerResult res)
	{
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Media piacker Succeeded", LogType.Log);
		}
		else
		{
			ISN_Logger.Log("Media piacker failed: " + res.Error.Message, LogType.Log);
		}
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Selecting Songs", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Picker"))
		{
			Singleton<ISN_MediaController>.Instance.ShowMediaPicker();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Set Perviostly Picked Song"))
		{
			Singleton<ISN_MediaController>.Instance.Pause();
			ISN_Logger.Log(Singleton<ISN_MediaController>.Instance.CurrentQueue[0].Title, LogType.Log);
			Singleton<ISN_MediaController>.Instance.SetCollection(new MP_MediaItem[]
			{
				Singleton<ISN_MediaController>.Instance.CurrentQueue[0]
			});
			Singleton<ISN_MediaController>.Instance.Play();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Controling Playback", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Play"))
		{
			Singleton<ISN_MediaController>.Instance.Play();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Pause"))
		{
			Singleton<ISN_MediaController>.Instance.Pause();
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Next"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToNextItem();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Previous"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToPreviousItem();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Skip To Beginning"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToBeginning();
		}
	}
}
