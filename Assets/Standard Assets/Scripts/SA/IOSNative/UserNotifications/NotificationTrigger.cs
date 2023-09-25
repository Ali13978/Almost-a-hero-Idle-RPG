using System;
using System.Collections.Generic;

namespace SA.IOSNative.UserNotifications
{
	public abstract class NotificationTrigger
	{
		public static NotificationTrigger triggerFromDictionary(Dictionary<string, object> triggerDictionary)
		{
			NotificationTrigger notificationTrigger;
			if (triggerDictionary.ContainsKey("intervalToFire"))
			{
				notificationTrigger = new TimeIntervalTrigger(int.Parse(triggerDictionary["intervalToFire"].ToString()));
			}
			else
			{
				DateComponents dateComponents = new DateComponents();
				if (triggerDictionary.ContainsKey("year"))
				{
					dateComponents.Year = new int?(int.Parse(triggerDictionary["year"].ToString()));
				}
				if (triggerDictionary.ContainsKey("month"))
				{
					dateComponents.Month = new int?(int.Parse(triggerDictionary["month"].ToString()));
				}
				if (triggerDictionary.ContainsKey("day"))
				{
					dateComponents.Day = new int?(int.Parse(triggerDictionary["day"].ToString()));
				}
				if (triggerDictionary.ContainsKey("hour"))
				{
					dateComponents.Hour = new int?(int.Parse(triggerDictionary["hour"].ToString()));
				}
				if (triggerDictionary.ContainsKey("minute"))
				{
					dateComponents.Minute = new int?(int.Parse(triggerDictionary["minute"].ToString()));
				}
				if (triggerDictionary.ContainsKey("second"))
				{
					dateComponents.Second = new int?(int.Parse(triggerDictionary["second"].ToString()));
				}
				if (triggerDictionary.ContainsKey("weekday"))
				{
					dateComponents.Weekday = new int?(int.Parse(triggerDictionary["weekday"].ToString()));
				}
				if (triggerDictionary.ContainsKey("quarter"))
				{
					dateComponents.Quarter = new int?(int.Parse(triggerDictionary["quarter"].ToString()));
				}
				notificationTrigger = new CalendarTrigger(dateComponents);
			}
			bool repeat = int.Parse(triggerDictionary["repeats"].ToString()) == 1;
			notificationTrigger.SetRepeat(repeat);
			return notificationTrigger;
		}

		public void SetRepeat(bool repeats)
		{
			this.repeated = repeats;
		}

		public string Type
		{
			get
			{
				return base.GetType().Name;
			}
		}

		public bool repeated;
	}
}
