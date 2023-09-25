using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_MediaController : Singleton<ISN_MediaController>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<MP_MediaPickerResult> ActionMediaPickerResult;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<MP_MediaPickerResult> ActionQueueUpdated;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<MP_MediaItem> ActionNowPlayingItemChanged;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<MP_MusicPlaybackState> ActionPlaybackStateChanged;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetRepeatMode(MP_MusicRepeatMode mode)
	{
	}

	public void SetShuffleMode(MP_MusicShuffleMode mode)
	{
	}

	public void Play()
	{
	}

	public void Pause()
	{
	}

	public void SkipToNextItem()
	{
	}

	public void SkipToBeginning()
	{
	}

	public void SkipToPreviousItem()
	{
	}

	public void ShowMediaPicker()
	{
	}

	public void SetCollection(params MP_MediaItem[] items)
	{
		List<string> list = new List<string>();
		foreach (MP_MediaItem mp_MediaItem in items)
		{
			list.Add(mp_MediaItem.Id);
		}
		this.SetCollection(list.ToArray());
	}

	public void AddItemWithProductID(string productID)
	{
	}

	public void SetCollection(params string[] itemIds)
	{
	}

	public MP_MediaItem NowPlayingItem
	{
		get
		{
			return this._NowPlayingItem;
		}
	}

	public List<MP_MediaItem> CurrentQueue
	{
		get
		{
			return this._CurrentQueue;
		}
	}

	public MP_MusicPlaybackState State
	{
		get
		{
			return this._State;
		}
	}

	private List<MP_MediaItem> ParseMediaItemsList(string[] data, int index = 0)
	{
		List<MP_MediaItem> list = new List<MP_MediaItem>();
		for (int i = index; i < data.Length; i += 8)
		{
			if (data[i] == "endofline")
			{
				break;
			}
			MP_MediaItem item = this.ParseMediaItemData(data, i);
			list.Add(item);
		}
		return list;
	}

	private MP_MediaItem ParseMediaItemData(string[] data, int index)
	{
		return new MP_MediaItem(data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5], data[index + 6], data[index + 7]);
	}

	private void OnQueueUpdate(string data)
	{
		string[] data2 = data.Split(new char[]
		{
			'|'
		});
		this._CurrentQueue = this.ParseMediaItemsList(data2, 0);
		MP_MediaPickerResult obj = new MP_MediaPickerResult(this._CurrentQueue);
		ISN_MediaController.ActionQueueUpdated(obj);
	}

	private void OnQueueUpdateFailed(string errorData)
	{
		MP_MediaPickerResult obj = new MP_MediaPickerResult(errorData);
		ISN_MediaController.ActionQueueUpdated(obj);
	}

	private void OnMediaPickerResult(string data)
	{
		string[] data2 = data.Split(new char[]
		{
			'|'
		});
		this._CurrentQueue = this.ParseMediaItemsList(data2, 0);
		MP_MediaPickerResult obj = new MP_MediaPickerResult(this._CurrentQueue);
		ISN_MediaController.ActionMediaPickerResult(obj);
		ISN_MediaController.ActionQueueUpdated(obj);
	}

	private void OnMediaPickerFailed(string errorData)
	{
		MP_MediaPickerResult obj = new MP_MediaPickerResult(errorData);
		ISN_MediaController.ActionMediaPickerResult(obj);
	}

	private void OnNowPlayingItemchanged(string data)
	{
		string[] data2 = data.Split(new char[]
		{
			'|'
		});
		this._NowPlayingItem = this.ParseMediaItemData(data2, 0);
		ISN_MediaController.ActionNowPlayingItemChanged(this._NowPlayingItem);
	}

	private void OnPlaybackStateChanged(string state)
	{
		int state2 = Convert.ToInt32(state);
		this._State = (MP_MusicPlaybackState)state2;
		ISN_MediaController.ActionPlaybackStateChanged(this._State);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_MediaController()
	{
		ISN_MediaController.ActionMediaPickerResult = delegate(MP_MediaPickerResult A_0)
		{
		};
		ISN_MediaController.ActionQueueUpdated = delegate(MP_MediaPickerResult A_0)
		{
		};
		ISN_MediaController.ActionNowPlayingItemChanged = delegate(MP_MediaItem A_0)
		{
		};
		ISN_MediaController.ActionPlaybackStateChanged = delegate(MP_MusicPlaybackState A_0)
		{
		};
	}

	private MP_MediaItem _NowPlayingItem;

	private MP_MusicPlaybackState _State;

	private List<MP_MediaItem> _CurrentQueue = new List<MP_MediaItem>();
}
