using System;

public static class TrustedTime
{
	public static bool IsReady()
	{
		return TrustedTime.time != DateTime.MaxValue;
	}

	public static void CancelReady()
	{
		TrustedTime.time = DateTime.MaxValue;
	}

	public static void Refresh()
	{
		TrustedTime.CancelReady();
		PlayfabManager.GetTime(delegate(bool isSuccess, DateTime time)
		{
			if (isSuccess)
			{
				TrustedTime.Init(time);
			}
		});
	}

	public static void Init(DateTime newTime)
	{
		TrustedTime.time = newTime;
	}

	public static void Increment(double dt)
	{
		if (TrustedTime.IsReady())
		{
			TrustedTime.time = TrustedTime.time.AddSeconds(dt);
		}
	}

	public static DateTime Get()
	{
		if (!TrustedTime.IsReady())
		{
			throw new Exception("Check yoself befo yo reck yoself!");
		}
		return TrustedTime.time;
	}

	public static bool IsCalenderEventAvailable(DateTime lastBoughtTime, int numDays = 1)
	{
		return TrustedTime.IsReady() && TrustedTime.IsCalenderEventAvailable(lastBoughtTime, TrustedTime.time, numDays);
	}

	public static bool IsCalenderEventAvailable(DateTime lastBoughtTime, DateTime now, int numDays = 1)
	{
		DateTime dateTime = now.AddDays((double)(-(double)(numDays - 1)));
		return lastBoughtTime < new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
	}

	private static DateTime time = DateTime.MaxValue;
}
