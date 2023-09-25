using System;
using System.Diagnostics;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_RemoteNotificationsController : Singleton<ISN_RemoteNotificationsController>
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action<ISN_RemoteNotification> OnRemoteNotificationReceived;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RegisterForRemoteNotifications(Action<ISN_RemoteNotificationsRegistrationResult> callback = null)
	{
		ISN_RemoteNotificationsController._RegistrationCallback = callback;
	}

	public ISN_RemoteNotification LaunchNotification
	{
		get
		{
			return this._LaunchNotification;
		}
	}

	private void DidFailToRegisterForRemoteNotifications(string errorData)
	{
		Error error = new Error(errorData);
		ISN_RemoteNotificationsRegistrationResult obj = new ISN_RemoteNotificationsRegistrationResult(error);
		if (ISN_RemoteNotificationsController._RegistrationCallback != null)
		{
			ISN_RemoteNotificationsController._RegistrationCallback(obj);
		}
	}

	private void DidRegisterForRemoteNotifications(string data)
	{
		string[] array = data.Split(new char[]
		{
			'|'
		});
		string token = array[0];
		string base64String = array[1];
		ISN_DeviceToken token2 = new ISN_DeviceToken(base64String, token);
		ISN_RemoteNotificationsRegistrationResult obj = new ISN_RemoteNotificationsRegistrationResult(token2);
		if (ISN_RemoteNotificationsController._RegistrationCallback != null)
		{
			ISN_RemoteNotificationsController._RegistrationCallback(obj);
		}
	}

	private void DidReceiveRemoteNotification(string notificationBody)
	{
		ISN_RemoteNotification obj = new ISN_RemoteNotification(notificationBody);
		ISN_RemoteNotificationsController.OnRemoteNotificationReceived(obj);
	}

	// Note: this type is marked as 'beforefieldinit'.
	static ISN_RemoteNotificationsController()
	{
		ISN_RemoteNotificationsController.OnRemoteNotificationReceived = delegate(ISN_RemoteNotification A_0)
		{
		};
	}

	private static Action<ISN_RemoteNotificationsRegistrationResult> _RegistrationCallback = null;

	private ISN_RemoteNotification _LaunchNotification;
}
