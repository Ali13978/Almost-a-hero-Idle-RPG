using System;
using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class NotificationRequest
	{
		public NotificationRequest()
		{
		}

		public NotificationRequest(string id, NotificationContent content, NotificationTrigger trigger)
		{
			this._Id = id;
			this._Content = content;
			this._Trigger = trigger;
		}

		public NotificationRequest(string data)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(data);
			this._Id = (string)dictionary["id"];
			Dictionary<string, object> contentDictionary = (Dictionary<string, object>)dictionary["content"];
			Dictionary<string, object> triggerDictionary = (Dictionary<string, object>)dictionary["trigger"];
			this._Content = new NotificationContent(contentDictionary);
			this._Trigger = NotificationTrigger.triggerFromDictionary(triggerDictionary);
		}

		public string Id
		{
			get
			{
				return this._Id;
			}
		}

		public NotificationContent Content
		{
			get
			{
				return this._Content;
			}
		}

		public NotificationTrigger Trigger
		{
			get
			{
				return this._Trigger;
			}
		}

		private string _Id = string.Empty;

		private NotificationContent _Content;

		private NotificationTrigger _Trigger;
	}
}
