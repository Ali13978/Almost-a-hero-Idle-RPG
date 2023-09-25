using System;

public struct CustomTimeSpan
{
	public CustomTimeSpan(TimeSpan timeSpan)
	{
		this.Years = timeSpan.Days / 365;
		this.Days = timeSpan.Days % 365;
		this.Hours = timeSpan.Hours;
		this.Minutes = timeSpan.Minutes;
		this.Seconds = timeSpan.Seconds;
	}

	public int Years;

	public int Days;

	public int Hours;

	public int Minutes;

	public int Seconds;
}
