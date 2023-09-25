using System;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.UserNotifications
{
	public class NativeReceiver : Singleton<NativeReceiver>
	{
		public void Init()
		{
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		private void RequestPermissionsCallbackEvent(string data)
		{
			NotificationCenter.RequestPermissionsResponse(data);
		}

		private void AddNotificationRequestEvent(string data)
		{
			NotificationCenter.AddNotificationRequestResponse(data);
		}

		private void WillPresentNotification(string data)
		{
			NotificationCenter.WillPresentNotification(data);
		}

		private void PendingNotificationsRequest(string data)
		{
			NotificationCenter.PendingNotificationsRequestResponse(data);
		}

		private void LaunchNotification(string data)
		{
			NotificationCenter.SetLastNotifification(data);
		}
	}
}
