using System;
using System.Collections.Generic;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.UserNotifications;
using UnityEngine;

public class NotificationExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		ISN_LocalNotificationsController.OnLocalNotificationReceived += this.HandleOnLocalNotificationReceived;
		NotificationCenter.OnWillPresentNotification += delegate(NotificationRequest obj)
		{
			UnityEngine.Debug.Log("OnWillPresentNotification: " + obj.Content);
		};
		NotificationRequest launchNotification = NotificationCenter.LaunchNotification;
		if (launchNotification.Content != null)
		{
			IOSMessage.Create("Launch Notification", string.Concat(new object[]
			{
				"Messgae: ",
				launchNotification.Content,
				"\nNotification ID: ",
				launchNotification.Id
			}));
		}
		if (Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_LocalNotification launchNotification2 = Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Notification", "Messgae: " + launchNotification2.Message + "\nNotification Data: " + launchNotification2.Data);
		}
		if (Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_RemoteNotification launchNotification3 = Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Remote Notification", "Body: " + launchNotification3.Body);
		}
	}

	private void OnGUI()
	{
		base.UpdateToStartPos();
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Local and Push Notifications", this.style);
		this.StartY += this.YLableStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Request Permissions"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.RequestNotificationPermissions();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Print Notification Settings"))
		{
			this.CheckNotificationSettings();
		}
		this.StartY += this.YButtonStep;
		this.StartX = this.XStartPos;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Schedule Notification Silent"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += this.OnNotificationScheduleResult;
			ISN_LocalNotification isn_LocalNotification = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text No Sound", false);
			isn_LocalNotification.SetData("some_test_data");
			isn_LocalNotification.Schedule();
			this.lastNotificationId = isn_LocalNotification.Id;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Schedule Notification"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += this.OnNotificationScheduleResult;
			ISN_LocalNotification isn_LocalNotification2 = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text", true);
			isn_LocalNotification2.SetData("some_test_data");
			isn_LocalNotification2.SetSoundName("purchase_ok.wav");
			isn_LocalNotification2.SetBadgesNumber(1);
			isn_LocalNotification2.Schedule();
			this.lastNotificationId = isn_LocalNotification2.Id;
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Cancel All Notifications"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelAllLocalNotifications();
			IOSNativeUtility.SetApplicationBagesNumber(0);
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Cansel Last Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelLocalNotificationById(this.lastNotificationId);
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "Local and Push Notifications", this.style);
		this.StartY += this.YLableStep;
		this.StartX = this.XStartPos;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Reg Device For Push Notif. "))
		{
			Singleton<ISN_RemoteNotificationsController>.Instance.RegisterForRemoteNotifications(delegate(ISN_RemoteNotificationsRegistrationResult res)
			{
				UnityEngine.Debug.Log("ISN_RemoteNotificationsRegistrationResult: " + res.IsSucceeded);
				if (!res.IsSucceeded)
				{
					UnityEngine.Debug.Log(res.Error.Code + " / " + res.Error.Message);
				}
				else
				{
					UnityEngine.Debug.Log(res.Token.DeviceId);
				}
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Show Game Kit Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.ShowGmaeKitNotification("Title", "Message");
		}
		this.StartX = this.XStartPos;
		this.StartY += this.YButtonStep;
		this.StartY += this.YLableStep;
		GUI.Label(new Rect(this.StartX, this.StartY, (float)Screen.width, 40f), "User Notifications", this.style);
		this.StartY += this.YLableStep;
		this.StartX = this.XStartPos;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Reg Device For User Notif. "))
		{
			NotificationCenter.RequestPermissions(delegate(Result result)
			{
				ISN_Logger.Log("RequestPermissions callback" + result.ToString(), LogType.Log);
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Schedule for 5 sec"))
		{
			NotificationContent notificationContent = new NotificationContent();
			notificationContent.Title = "Title_";
			notificationContent.Subtitle = "Subtitle_";
			notificationContent.Body = "Body_";
			notificationContent.Badge = 1;
			notificationContent.Sound = "beep.mp3";
			notificationContent.UserInfo["404"] = "test User Info";
			TimeIntervalTrigger trigger = new TimeIntervalTrigger(5);
			NotificationRequest request = new NotificationRequest("some0id0", notificationContent, trigger);
			ISN_Logger.Log("request Schedule for 5 sec", LogType.Log);
			NotificationCenter.AddNotificationRequest(request, delegate(Result result)
			{
				ISN_Logger.Log("request callback", LogType.Log);
				ISN_Logger.Log(result.ToString(), LogType.Log);
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Schedule by Calendar - Date Components"))
		{
			NotificationContent notificationContent2 = new NotificationContent();
			notificationContent2.Title = "Calendar - Date Components";
			notificationContent2.Subtitle = "Subtitle_";
			notificationContent2.Body = "Body_";
			notificationContent2.Badge = 1;
			notificationContent2.UserInfo["404"] = "test User Info";
			DateComponents dateComponents = new DateComponents
			{
				Second = new int?(32)
			};
			CalendarTrigger calendarTrigger = new CalendarTrigger(dateComponents);
			calendarTrigger.SetRepeat(true);
			NotificationRequest request2 = new NotificationRequest("some0id1", notificationContent2, calendarTrigger);
			NotificationCenter.AddNotificationRequest(request2, delegate(Result result)
			{
				ISN_Logger.Log("request callback", LogType.Log);
				ISN_Logger.Log(result.ToString(), LogType.Log);
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Schedule by Calendar - Date"))
		{
			NotificationContent notificationContent3 = new NotificationContent();
			notificationContent3.Title = "Calendar - Date";
			notificationContent3.Subtitle = "Subtitle_";
			notificationContent3.Body = "Body_";
			notificationContent3.Badge = 1;
			notificationContent3.UserInfo["404"] = 1;
			DateComponents dateComponents2 = new DateComponents
			{
				Second = new int?(32),
				Year = new int?(2017),
				Month = new int?(6),
				Day = new int?(6),
				Hour = new int?(13),
				Minute = new int?(1)
			};
			CalendarTrigger trigger2 = new CalendarTrigger(dateComponents2);
			NotificationRequest request3 = new NotificationRequest("some0id2", notificationContent3, trigger2);
			NotificationCenter.AddNotificationRequest(request3, delegate(Result result)
			{
				ISN_Logger.Log("request callback", LogType.Log);
				ISN_Logger.Log(result.ToString(), LogType.Log);
			});
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Cancel All User Notifications"))
		{
			NotificationCenter.CancelAllNotifications();
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Cancel some0id2"))
		{
			NotificationCenter.CancelUserNotificationById("some0id2");
		}
		this.StartX += this.XButtonStep;
		if (GUI.Button(new Rect(this.StartX, this.StartY, (float)this.buttonWidth, (float)this.buttonHeight), "Get Pending UserNotifications"))
		{
			NotificationCenter.GetPendingNotificationRequests(delegate(List<NotificationRequest> requests)
			{
				for (int i = 0; i < requests.Count; i++)
				{
					ISN_Logger.Log(requests[i].Content.Title, LogType.Log);
				}
			});
		}
	}

	public void CheckNotificationSettings()
	{
		int allowedNotificationsType = ISN_LocalNotificationsController.AllowedNotificationsType;
		UnityEngine.Debug.Log("AllowedNotificationsType: " + allowedNotificationsType);
		if ((allowedNotificationsType & 2) != 0)
		{
			UnityEngine.Debug.Log("Sound avaliable");
		}
		if ((allowedNotificationsType & 1) != 0)
		{
			UnityEngine.Debug.Log("Badge avaliable");
		}
		if ((allowedNotificationsType & 4) != 0)
		{
			UnityEngine.Debug.Log("Alert avaliable");
		}
	}

	private void HandleOnLocalNotificationReceived(ISN_LocalNotification notification)
	{
		IOSMessage.Create("Notification Received", "Messgae: " + notification.Message + "\nNotification Data: " + notification.Data);
	}

	private void OnNotificationScheduleResult(Result res)
	{
		ISN_LocalNotificationsController.OnNotificationScheduleResult -= this.OnNotificationScheduleResult;
		string text = string.Empty;
		if (res.IsSucceeded)
		{
			text += "Notification was successfully scheduled\n allowed notifications types: \n";
		}
		else
		{
			text += "Notification scheduling failed";
		}
		IOSMessage.Create("On Notification Schedule Result", text);
	}

	private int lastNotificationId;
}
