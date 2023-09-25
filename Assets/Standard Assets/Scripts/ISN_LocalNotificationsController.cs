using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_LocalNotificationsController : Singleton<ISN_LocalNotificationsController>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<Result> OnNotificationScheduleResult;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<ISN_LocalNotification> OnLocalNotificationReceived;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RequestNotificationPermissions()
	{
		if (ISN_Device.CurrentDevice.MajorSystemVersion < 8)
		{
			return;
		}
	}

	public void ShowGmaeKitNotification(string title, string message)
	{
		GameCenterManager.ShowGmaeKitNotification(title, message);
	}

	public void CancelAllLocalNotifications()
	{
		this.SaveNotifications(new List<ISN_LocalNotification>());
	}

	public void CancelLocalNotification(ISN_LocalNotification notification)
	{
		this.CancelLocalNotificationById(notification.Id);
	}

	public void CancelLocalNotificationById(int notificationId)
	{
	}

	public void ScheduleNotification(ISN_LocalNotification notification)
	{
	}

	public List<ISN_LocalNotification> LoadPendingNotifications(bool includeAll = false)
	{
		return null;
	}

	public void ApplicationIconBadgeNumber(int badges)
	{
	}

	public static int AllowedNotificationsType
	{
		get
		{
			return 0;
		}
	}

	public ISN_LocalNotification LaunchNotification
	{
		get
		{
			return this._LaunchNotification;
		}
	}

	private void OnNotificationScheduleResultAction(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			"|"[0]
		});
		Result obj;
		if (array2[0].Equals("0"))
		{
			obj = new Result(new Error());
		}
		else
		{
			obj = new Result();
		}
		ISN_LocalNotificationsController.OnNotificationScheduleResult(obj);
	}

	private void OnLocalNotificationReceived_Event(string array)
	{
		string[] array2 = array.Split(new char[]
		{
			"|"[0]
		});
		string message = array2[0];
		int id = Convert.ToInt32(array2[1]);
		string data = array2[2];
		int badgesNumber = Convert.ToInt32(array2[3]);
		ISN_LocalNotification isn_LocalNotification = new ISN_LocalNotification(DateTime.Now, message, true);
		isn_LocalNotification.SetData(data);
		isn_LocalNotification.SetBadgesNumber(badgesNumber);
		isn_LocalNotification.SetId(id);
		ISN_LocalNotificationsController.OnLocalNotificationReceived(isn_LocalNotification);
	}

	private void SaveNotifications(List<ISN_LocalNotification> notifications)
	{
		if (notifications.Count == 0)
		{
			PlayerPrefs.DeleteKey("IOSNotificationControllerKey");
			return;
		}
		string text = string.Empty;
		int count = notifications.Count;
		for (int i = 0; i < count; i++)
		{
			if (i != 0)
			{
				text += '|';
			}
			text += notifications[i].SerializedString;
		}
		PlayerPrefs.SetString("IOSNotificationControllerKey", text);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_LocalNotificationsController()
	{
		ISN_LocalNotificationsController.OnNotificationScheduleResult = delegate(Result A_0)
		{
		};
		ISN_LocalNotificationsController.OnLocalNotificationReceived = delegate(ISN_LocalNotification A_0)
		{
		};
	}

	private const string PP_KEY = "IOSNotificationControllerKey";

	private const string PP_ID_KEY = "IOSNotificationControllerrKey_ID";

	private ISN_LocalNotification _LaunchNotification;
}
