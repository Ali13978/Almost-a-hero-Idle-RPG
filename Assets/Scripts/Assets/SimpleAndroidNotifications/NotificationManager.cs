using System;
using System.Linq;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public static class NotificationManager
	{
		public static int Send(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = NotificationIcon.Bell, bool silent = false)
		{
			return NotificationManager.SendCustom(new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = delay,
				Title = title,
				Message = message,
				Ticker = message,
				Sound = !silent,
				Vibrate = !silent,
				Light = true,
				SmallIcon = smallIcon,
				SmallIconColor = smallIconColor,
				LargeIcon = string.Empty,
				ExecuteMode = NotificationExecuteMode.Inexact
			});
		}

		public static int SendWithAppIcon(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = NotificationIcon.Bell, bool silent = false)
		{
			return NotificationManager.SendCustom(new NotificationParams
			{
				Id = NotificationIdHandler.GetNotificationId(),
				Delay = delay,
				Title = title,
				Message = message,
				Ticker = message,
				Sound = !silent,
				Vibrate = !silent,
				Light = true,
				SmallIcon = smallIcon,
				SmallIconColor = smallIconColor,
				LargeIcon = "app_icon",
				ExecuteMode = NotificationExecuteMode.Inexact
			});
		}

		public static int SendCustom(NotificationParams notificationParams)
		{
			long num = (long)notificationParams.Delay.TotalMilliseconds;
			long num2 = (!notificationParams.Repeat) ? 0L : ((long)notificationParams.RepeatInterval.TotalMilliseconds);
			string text = string.Join(",", (from i in notificationParams.Vibration
			select i.ToString()).ToArray<string>());
			new AndroidJavaClass("com.unity3d.plugin.downloader.NotificationController").CallStatic("SetNotification", new object[]
			{
				notificationParams.Id,
				notificationParams.GroupName ?? string.Empty,
				notificationParams.GroupSummary ?? string.Empty,
				notificationParams.ChannelId,
				notificationParams.ChannelName,
				num,
				Convert.ToInt32(notificationParams.Repeat),
				num2,
				notificationParams.Title,
				notificationParams.Message,
				notificationParams.Ticker,
				Convert.ToInt32(notificationParams.Multiline),
				Convert.ToInt32(notificationParams.Sound),
				notificationParams.CustomSound ?? string.Empty,
				Convert.ToInt32(notificationParams.Vibrate),
				text,
				Convert.ToInt32(notificationParams.Light),
				notificationParams.LightOnMs,
				notificationParams.LightOffMs,
				NotificationManager.ColotToInt(notificationParams.LightColor),
				notificationParams.LargeIcon ?? string.Empty,
				NotificationManager.GetSmallIconName(notificationParams.SmallIcon),
				NotificationManager.ColotToInt(notificationParams.SmallIconColor),
				(int)notificationParams.ExecuteMode,
				notificationParams.CallbackData,
				"com.unity3d.plugin.downloader.UnityObbDownloaderActivity"
			});
			NotificationIdHandler.AddScheduledNotificaion(notificationParams.Id);
			return notificationParams.Id;
		}

		public static void Cancel(int id)
		{
			new AndroidJavaClass("com.unity3d.plugin.downloader.NotificationController").CallStatic("CancelNotificationEx", new object[]
			{
				id
			});
			NotificationIdHandler.RemoveScheduledNotificaion(id);
		}

		public static void CancelAll()
		{
			new AndroidJavaClass("com.unity3d.plugin.downloader.NotificationController").CallStatic("CancelAllNotificationsEx", new object[0]);
			NotificationIdHandler.RemoveAllScheduledNotificaions();
		}

		public static NotificationCallback GetNotificationCallback()
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getIntent", new object[0]);
			bool flag = androidJavaObject.Call<bool>("hasExtra", new object[]
			{
				"Notification.Id"
			});
			if (flag)
			{
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getExtras", new object[0]);
				return new NotificationCallback
				{
					Id = androidJavaObject2.Call<int>("getInt", new object[]
					{
						"Notification.Id"
					}),
					Data = androidJavaObject2.Call<string>("getString", new object[]
					{
						"Notification.CallbackData"
					})
				};
			}
			return null;
		}

		private static int ColotToInt(Color color)
		{
			Color32 color2 = color;
			return (int)color2.r * 65536 + (int)color2.g * 256 + (int)color2.b;
		}

		private static string GetSmallIconName(NotificationIcon icon)
		{
			return "anp_" + icon.ToString().ToLower();
		}

		public const string FullClassName = "com.unity3d.plugin.downloader.NotificationController";

		public const string MainActivityClassName = "com.unity3d.plugin.downloader.UnityObbDownloaderActivity";
	}
}
