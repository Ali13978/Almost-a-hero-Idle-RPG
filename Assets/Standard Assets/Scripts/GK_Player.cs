using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using UnityEngine;

public class GK_Player
{
	public GK_Player(string pId, string pName, string pAlias)
	{
		this._PlayerId = pId;
		this._DisplayName = pName;
		this._Alias = pAlias;
		this._SmallPhoto = GK_Player.GetLocalCachedPhotoByKey(this.SmallPhotoCacheKey);
		this._BigPhoto = GK_Player.GetLocalCachedPhotoByKey(this.BigPhotoCacheKey);
		if (IOSNativeSettings.Instance.AutoLoadUsersBigImages)
		{
			this.LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
		}
		if (IOSNativeSettings.Instance.AutoLoadUsersSmallImages)
		{
			this.LoadPhoto(GK_PhotoSize.GKPhotoSizeSmall);
		}
	}

	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<GK_UserPhotoLoadResult> OnPlayerPhotoLoaded;



	public void LoadPhoto(GK_PhotoSize size)
	{
		if (size == GK_PhotoSize.GKPhotoSizeSmall)
		{
			if (this._SmallPhoto != null)
			{
				GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, this._SmallPhoto);
				this.OnPlayerPhotoLoaded(obj);
				return;
			}
		}
		else if (this._BigPhoto != null)
		{
			GK_UserPhotoLoadResult obj2 = new GK_UserPhotoLoadResult(size, this._BigPhoto);
			this.OnPlayerPhotoLoaded(obj2);
			return;
		}
		GameCenterManager.LoadGKPlayerPhoto(this.Id, size);
	}

	public void SetPhotoData(GK_PhotoSize size, string base64String)
	{
		if (base64String.Length == 0)
		{
			return;
		}
		byte[] data = Convert.FromBase64String(base64String);
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.LoadImage(data);
		if (size == GK_PhotoSize.GKPhotoSizeSmall)
		{
			this._SmallPhoto = texture2D;
			GK_Player.UpdatePhotosCache(this.SmallPhotoCacheKey, this._SmallPhoto);
		}
		else
		{
			this._BigPhoto = texture2D;
			GK_Player.UpdatePhotosCache(this.BigPhotoCacheKey, this._BigPhoto);
		}
		GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, texture2D);
		this.OnPlayerPhotoLoaded(obj);
	}

	public void SetPhotoLoadFailedEventData(GK_PhotoSize size, string errorData)
	{
		GK_UserPhotoLoadResult obj = new GK_UserPhotoLoadResult(size, new Error(errorData));
		this.OnPlayerPhotoLoaded(obj);
	}

	public string Id
	{
		get
		{
			return this._PlayerId;
		}
	}

	public string Alias
	{
		get
		{
			return this._Alias;
		}
	}

	public string DisplayName
	{
		get
		{
			return this._DisplayName;
		}
	}

	public Texture2D SmallPhoto
	{
		get
		{
			return this._SmallPhoto;
		}
	}

	public Texture2D BigPhoto
	{
		get
		{
			return this._BigPhoto;
		}
	}

	private string SmallPhotoCacheKey
	{
		get
		{
			return this.Id + GK_PhotoSize.GKPhotoSizeSmall.ToString();
		}
	}

	private string BigPhotoCacheKey
	{
		get
		{
			return this.Id + GK_PhotoSize.GKPhotoSizeNormal.ToString();
		}
	}

	public static void UpdatePhotosCache(string key, Texture2D photo)
	{
		if (GK_Player.LocalPhotosCache.ContainsKey(key))
		{
			GK_Player.LocalPhotosCache[key] = photo;
		}
		else
		{
			GK_Player.LocalPhotosCache.Add(key, photo);
		}
	}

	public static Texture2D GetLocalCachedPhotoByKey(string key)
	{
		if (GK_Player.LocalPhotosCache.ContainsKey(key))
		{
			return GK_Player.LocalPhotosCache[key];
		}
		return null;
	}

	private string _PlayerId;

	private string _DisplayName;

	private string _Alias;

	private Texture2D _SmallPhoto;

	private Texture2D _BigPhoto;

	private static Dictionary<string, Texture2D> LocalPhotosCache = new Dictionary<string, Texture2D>();
}
