using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleAndroidNotifications
{
	public class NotificationExample : MonoBehaviour
	{
		public void Awake()
		{
			this.Toggle.isOn = (NotificationManager.GetNotificationCallback() != null);
		}

		public void Rate()
		{
			Application.OpenURL("http://u3d.as/A6d");
		}

		public void OpenWiki()
		{
			Application.OpenURL("https://github.com/hippogamesunity/SimpleAndroidNotificationsPublic/wiki");
		}

		public void CancelAll()
		{
			NotificationManager.CancelAll();
		}

		public void ScheduleSimple(int seconds)
		{
			NotificationManager.Send(TimeSpan.FromSeconds((double)seconds), "Simple notification", "Please rate the asset on the Asset Store!", new Color(1f, 0.3f, 0.15f), NotificationIcon.Bell, false);
		}

		public void ScheduleNormal()
		{
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(5.0), "Notification", "Notification with app icon", new Color(0f, 0.6f, 1f), NotificationIcon.Message, false);
		}

		public void ScheduleRepeated()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Repeated notification",
				Message = "Please rate the asset on the Asset Store!",
				Ticker = "This is repeated message ticker!",
				Sound = true,
				Vibrate = true,
				Vibration = new int[]
				{
					500,
					500,
					500,
					500,
					500,
					500
				},
				Light = true,
				LightOnMs = 1000,
				LightOffMs = 1000,
				LightColor = Color.magenta,
				SmallIcon = NotificationIcon.Skull,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon",
				ExecuteMode = NotificationExecuteMode.Inexact,
				Repeat = true,
				RepeatInterval = TimeSpan.FromSeconds(30.0)
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public void ScheduleMultiline()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Multiline notification",
				Message = "Line#1\nLine#2\nLine#3\nLine#4",
				Ticker = "This is multiline message ticker!",
				Multiline = true
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public void ScheduleGrouped()
		{
			int notificationId = NotificationIdHandler.GetNotificationId();
			NotificationParams notificationParams = new NotificationParams
			{
				Id = notificationId,
				GroupName = "Group",
				GroupSummary = "{0} new messages",
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Grouped notification",
				Message = "Message " + notificationId,
				Ticker = "Please rate the asset on the Asset Store!"
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public void ScheduleCustom()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Notification with callback",
				Message = "Open app and check the checkbox!",
				Ticker = "Notification with callback",
				Sound = true,
				Vibrate = true,
				Vibration = new int[]
				{
					500,
					500,
					500,
					500,
					500,
					500
				},
				Light = true,
				LightOnMs = 1000,
				LightOffMs = 1000,
				LightColor = Color.red,
				SmallIcon = NotificationIcon.Sync,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon",
				ExecuteMode = NotificationExecuteMode.Inexact,
				CallbackData = "notification created at " + DateTime.Now
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public void ScheduleWithChannel()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = TimeSpan.FromSeconds(5.0),
				Title = "Notification with news channel",
				Message = "Check the channel in your app settings!",
				Ticker = "Notification with news channel",
				ChannelId = "com.company.app.news",
				ChannelName = "News"
			};
			NotificationManager.SendCustom(notificationParams);
		}

		public Toggle Toggle;
	}
}
