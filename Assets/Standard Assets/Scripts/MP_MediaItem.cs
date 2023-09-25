using System;

public class MP_MediaItem
{
	public MP_MediaItem(string id, string title, string artist, string albumTitle, string albumArtist, string genre, string playbackDuration, string composer)
	{
		this._Id = id;
		this._Title = title;
		this._Artist = artist;
		this._AlbumTitle = albumTitle;
		this._AlbumArtist = albumArtist;
		this._Genre = genre;
		this._PlaybackDuration = playbackDuration;
		this._Composer = composer;
	}

	public string Id
	{
		get
		{
			return this._Id;
		}
	}

	public string Title
	{
		get
		{
			return this._Title;
		}
	}

	public string Artist
	{
		get
		{
			return this._Artist;
		}
	}

	public string AlbumTitle
	{
		get
		{
			return this._AlbumTitle;
		}
	}

	public string AlbumArtist
	{
		get
		{
			return this._AlbumArtist;
		}
	}

	public string PlaybackDuration
	{
		get
		{
			return this._PlaybackDuration;
		}
	}

	public string Genre
	{
		get
		{
			return this._Genre;
		}
	}

	public string Composer
	{
		get
		{
			return this._Composer;
		}
	}

	private string _Id;

	private string _Title;

	private string _Artist;

	private string _AlbumTitle;

	private string _AlbumArtist;

	private string _Genre;

	private string _PlaybackDuration;

	private string _Composer;
}
