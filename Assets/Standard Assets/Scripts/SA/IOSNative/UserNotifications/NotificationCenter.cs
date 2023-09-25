using System;
using System.Collections.Generic;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.Core;

namespace SA.IOSNative.UserNotifications
{
	public static class NotificationCenter
	{
		static NotificationCenter()
		{
			NotificationCenter.OnWillPresentNotification = delegate(NotificationRequest A_0)
			{
			};
			Singleton<NativeReceiver>.Instance.Init();
			NotificationCenter.OnCallbackDictionary = new Dictionary<string, Action<Result>>();
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<NotificationRequest> OnWillPresentNotification;

		public static void RequestPermissions(Action<Result> callback)
		{
			NotificationCenter.RequestPermissionsCallback = callback;
		}

		public static void AddNotificationRequest(NotificationRequest request, Action<Result> callback)
		{
			string id = request.Id;
			NotificationContent content = request.Content;
			NotificationCenter.OnCallbackDictionary[id] = callback;
			string notificationJSONData = "{" + string.Format("\"id\" : \"{0}\", \"content\" : {1}, \"trigger\" : {2}", id, request.Content.ToString(), request.Trigger.ToString()) + "}";
			NotificationCenter.ScheduleUserNotification(notificationJSONData);
		}

		private static void ScheduleUserNotification(string notificationJSONData)
		{
		}

		public static void CancelAllNotifications()
		{
		}

		public static void CancelUserNotificationById(string nId)
		{
		}

		public static void GetPendingNotificationRequests(Action<List<NotificationRequest>> callback)
		{
			NotificationCenter.OnPendingNotificationsCallback = callback;
		}

		public static NotificationRequest LaunchNotification
		{
			get
			{
				return AppController.LaunchNotification;
			}
		}

		internal static void RequestPermissionsResponse(string dataString)
		{
			Result obj;
			if (dataString.Equals("success"))
			{
				obj = new Result();
			}
			else
			{
				obj = new Result(new Error());
			}
			NotificationCenter.RequestPermissionsCallback(obj);
		}

		internal static void AddNotificationRequestResponse(string dataString)
		{
			string[] array = dataString.Split(new string[]
			{
				"|%|"
			}, StringSplitOptions.None);
			string key = array[0];
			string text = array[1];
			Result obj;
			if (text.Equals("success"))
			{
				obj = new Result();
			}
			else
			{
				obj = new Result(new Error(text));
			}
			Action<Result> action = NotificationCenter.OnCallbackDictionary[key];
			if (action != null)
			{
				action(obj);
			}
		}

		internal static void WillPresentNotification(string data)
		{
			NotificationRequest obj = new NotificationRequest(data);
			NotificationCenter.OnWillPresentNotification(obj);
		}

		internal static void PendingNotificationsRequestResponse(string data)
		{
			if (data.Length > 0)
			{
				string[] array = data.Split(new string[]
				{
					"|%|"
				}, StringSplitOptions.None);
				List<NotificationRequest> list = new List<NotificationRequest>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == "endofline")
					{
						break;
					}
					NotificationRequest item = new NotificationRequest(data);
					list.Add(item);
				}
				NotificationCenter.OnPendingNotificationsCallback(list);
			}
		}

		internal static void SetLastNotifification(string data)
		{
			NotificationRequest lastNotificationRequest = new NotificationRequest(data);
			NotificationCenter.LastNotificationRequest = lastNotificationRequest;
		}

		private static Dictionary<string, Action<Result>> OnCallbackDictionary;

		private static Action<List<NotificationRequest>> OnPendingNotificationsCallback;

		private static Action<Result> RequestPermissionsCallback;

		public static NotificationRequest LastNotificationRequest;
	}
}
