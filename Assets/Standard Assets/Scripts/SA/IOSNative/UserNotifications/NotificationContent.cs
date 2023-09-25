using System;
using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class NotificationContent
	{
		public NotificationContent()
		{
		}

		public NotificationContent(Dictionary<string, object> contentDictionary)
		{
			this.Title = (string)contentDictionary["title"];
			this.Subtitle = (string)contentDictionary["subtitle"];
			this.Body = (string)contentDictionary["body"];
			this.Sound = (string)contentDictionary["sound"];
			this.LaunchImageName = (string)contentDictionary["launchImageName"];
			this.Badge = int.Parse(contentDictionary["badge"].ToString());
			this.UserInfo = (Dictionary<string, object>)Json.Deserialize(contentDictionary["userInfo"].ToString());
		}

		public override string ToString()
		{
			string text = Json.Serialize(this.UserInfo);
			return "{" + string.Format("\"title\" : \"{0}\", \"subtitle\" : \"{1}\", \"body\" : \"{2}\", \"sound\" : \"{3}\", \"badge\" : {4}, \"launchImageName\" : \"{5}\", \"userInfo\" : {6}", new object[]
			{
				this.Title,
				this.Subtitle,
				this.Body,
				this.Sound,
				this.Badge,
				this.LaunchImageName,
				text
			}) + "}";
		}

		public string Title = string.Empty;

		public string Subtitle = string.Empty;

		public string Body = string.Empty;

		public string Sound = string.Empty;

		public int Badge;

		public string LaunchImageName = string.Empty;

		public Dictionary<string, object> UserInfo = new Dictionary<string, object>();
	}
}
