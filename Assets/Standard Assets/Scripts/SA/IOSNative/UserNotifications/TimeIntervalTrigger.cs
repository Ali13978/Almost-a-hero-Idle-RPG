using System;
using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class TimeIntervalTrigger : NotificationTrigger
	{
		public TimeIntervalTrigger(int secondsInterval)
		{
			this.intervalToFire = secondsInterval;
		}

		public override string ToString()
		{
			return Json.Serialize(new Dictionary<string, object>
			{
				{
					"intervalToFire",
					this.intervalToFire
				},
				{
					"repeats",
					this.repeated
				}
			});
		}

		public int intervalToFire;
	}
}
