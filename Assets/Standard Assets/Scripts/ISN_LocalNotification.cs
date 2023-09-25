using System;
using System.Text;
using SA.Common.Pattern;
using SA.Common.Util;
using UnityEngine;

public class ISN_LocalNotification
{
	public ISN_LocalNotification(DateTime time, string message, bool useSound = true)
	{
		this._Id = IdFactory.NextId;
		this._Date = time;
		this._Message = message;
		this._UseSound = useSound;
	}

	public ISN_LocalNotification(string serializaedNotificationData)
	{
		try
		{
			string[] array = serializaedNotificationData.Split(new string[]
			{
				"|||"
			}, StringSplitOptions.None);
			this._Id = Convert.ToInt32(array[0]);
			this._UseSound = Convert.ToBoolean(array[1]);
			this._Badges = Convert.ToInt32(array[2]);
			this._Data = array[3];
			this._SoundName = array[4];
			this._Date = new DateTime(Convert.ToInt64(array[5]));
		}
		catch (Exception ex)
		{
			ISN_Logger.Log("Failed to deserialize the ISN_LocalNotification object", LogType.Log);
			ISN_Logger.Log(ex.Message, LogType.Log);
		}
	}

	public void SetId(int id)
	{
		this._Id = id;
	}

	public void SetData(string data)
	{
		this._Data = data;
	}

	public void SetSoundName(string soundName)
	{
		this._SoundName = soundName;
	}

	public void SetBadgesNumber(int badges)
	{
		this._Badges = badges;
	}

	public void Schedule()
	{
		Singleton<ISN_LocalNotificationsController>.Instance.ScheduleNotification(this);
	}

	public int Id
	{
		get
		{
			return this._Id;
		}
	}

	public DateTime Date
	{
		get
		{
			return this._Date;
		}
	}

	public bool IsFired
	{
		get
		{
			return DateTime.Now.Ticks > this.Date.Ticks;
		}
	}

	public string Message
	{
		get
		{
			return this._Message;
		}
	}

	public bool UseSound
	{
		get
		{
			return this._UseSound;
		}
	}

	public int Badges
	{
		get
		{
			return this._Badges;
		}
	}

	public string Data
	{
		get
		{
			return this._Data;
		}
	}

	public string SoundName
	{
		get
		{
			return this._SoundName;
		}
	}

	public string SerializedString
	{
		get
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Concat(new string[]
			{
				this.Id.ToString(),
				"|||",
				this.UseSound.ToString(),
				"|||",
				this.Badges.ToString(),
				"|||",
				this.Data,
				"|||",
				this.SoundName,
				"|||",
				this.Date.Ticks.ToString()
			})));
		}
	}

	private int _Id;

	private DateTime _Date;

	private string _Message = string.Empty;

	private bool _UseSound = true;

	private int _Badges;

	private string _Data = string.Empty;

	private string _SoundName = string.Empty;

	private const string DATA_SPLITTER = "|||";
}
