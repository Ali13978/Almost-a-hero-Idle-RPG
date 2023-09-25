using System;
using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class CalendarTrigger : NotificationTrigger
	{
		public CalendarTrigger(DateComponents dateComponents)
		{
			this.ComponentsOfDateToFire = dateComponents;
		}

		public override string ToString()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int? year = this.ComponentsOfDateToFire.Year;
			if (year != null)
			{
				dictionary.Add("year", this.ComponentsOfDateToFire.Year);
			}
			int? month = this.ComponentsOfDateToFire.Month;
			if (month != null)
			{
				dictionary.Add("month", this.ComponentsOfDateToFire.Month);
			}
			int? day = this.ComponentsOfDateToFire.Day;
			if (day != null)
			{
				dictionary.Add("day", this.ComponentsOfDateToFire.Day);
			}
			int? hour = this.ComponentsOfDateToFire.Hour;
			if (hour != null)
			{
				dictionary.Add("hour", this.ComponentsOfDateToFire.Hour);
			}
			int? minute = this.ComponentsOfDateToFire.Minute;
			if (minute != null)
			{
				dictionary.Add("minute", this.ComponentsOfDateToFire.Minute);
			}
			int? second = this.ComponentsOfDateToFire.Second;
			if (second != null)
			{
				dictionary.Add("second", this.ComponentsOfDateToFire.Second);
			}
			int? weekday = this.ComponentsOfDateToFire.Weekday;
			if (weekday != null)
			{
				dictionary.Add("weekday", this.ComponentsOfDateToFire.Weekday);
			}
			int? quarter = this.ComponentsOfDateToFire.Quarter;
			if (quarter != null)
			{
				dictionary.Add("quarter", this.ComponentsOfDateToFire.Quarter);
			}
			dictionary.Add("repeats", this.repeated);
			return Json.Serialize(dictionary);
		}

		private DateComponents ComponentsOfDateToFire;
	}
}
