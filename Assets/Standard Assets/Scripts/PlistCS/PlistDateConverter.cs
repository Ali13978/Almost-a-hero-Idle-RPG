using System;

namespace PlistCS
{
	public static class PlistDateConverter
	{
		public static long GetAppleTime(long unixTime)
		{
			return unixTime - PlistDateConverter.timeDifference;
		}

		public static long GetUnixTime(long appleTime)
		{
			return appleTime + PlistDateConverter.timeDifference;
		}

		public static DateTime ConvertFromAppleTimeStamp(double timestamp)
		{
			DateTime dateTime = new DateTime(2001, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds(timestamp);
		}

		public static double ConvertToAppleTimeStamp(DateTime date)
		{
			DateTime d = new DateTime(2001, 1, 1, 0, 0, 0, 0);
			return Math.Floor((date - d).TotalSeconds);
		}

		public static long timeDifference = 978307200L;
	}
}
