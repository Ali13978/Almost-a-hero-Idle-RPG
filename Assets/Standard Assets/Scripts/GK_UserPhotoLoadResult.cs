using System;
using SA.Common.Models;
using UnityEngine;

public class GK_UserPhotoLoadResult : Result
{
	public GK_UserPhotoLoadResult(GK_PhotoSize size, Texture2D photo)
	{
		this._Size = size;
		this._Photo = photo;
	}

	public GK_UserPhotoLoadResult(GK_PhotoSize size, Error error) : base(error)
	{
		this._Size = size;
	}

	public Texture2D Photo
	{
		get
		{
			return this._Photo;
		}
	}

	public GK_PhotoSize Size
	{
		get
		{
			return this._Size;
		}
	}

	private Texture2D _Photo;

	private GK_PhotoSize _Size;
}
