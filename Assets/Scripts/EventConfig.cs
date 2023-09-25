using System;
using System.Collections.Generic;

public class EventConfig
{
	public string startDateSerialized;

	public double durationHours;

	public Dictionary<string, EventConfig.InternalEvent> events;

	public DateTime startDate;

	public DateTime endDate;

	public class InternalEvent
	{
		public string startDateSerialized;

		public double waitFromStartHours;

		public double durationHours;

		public DateTime startDate;

		public DateTime endDate;
	}
}
