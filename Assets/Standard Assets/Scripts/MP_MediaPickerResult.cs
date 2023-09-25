using System;
using System.Collections.Generic;
using SA.Common.Models;

public class MP_MediaPickerResult : Result
{
	public MP_MediaPickerResult(List<MP_MediaItem> selectedItems)
	{
		this._SelectedmediaItems = selectedItems;
	}

	public MP_MediaPickerResult(string errorData) : base(new Error(errorData))
	{
	}

	public List<MP_MediaItem> SelectedmediaItems
	{
		get
		{
			return this._SelectedmediaItems;
		}
	}

	public List<MP_MediaItem> Items
	{
		get
		{
			return this.SelectedmediaItems;
		}
	}

	private List<MP_MediaItem> _SelectedmediaItems;
}
