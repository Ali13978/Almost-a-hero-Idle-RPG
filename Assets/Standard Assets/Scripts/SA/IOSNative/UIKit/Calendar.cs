using System;
using SA.Common.Pattern;

namespace SA.IOSNative.UIKit
{
	internal static class Calendar
	{
		static Calendar()
		{
			Singleton<NativeReceiver>.Instance.Init();
		}

		public static void PickDate(Action<DateTime> callback, int startFromYear = 0)
		{
			Calendar.OnCalendarClosed = callback;
		}

		internal static void DatePicked(string time)
		{
			DateTime obj = DateTime.Parse(time);
			Calendar.OnCalendarClosed(obj);
		}

		private static Action<DateTime> OnCalendarClosed;
	}
}
