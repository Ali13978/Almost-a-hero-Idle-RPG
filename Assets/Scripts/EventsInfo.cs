using System;
using System.Collections.Generic;

public class EventsInfo
{
	public void Init()
	{
		foreach (EventConfig eventConfig in this.events.Values)
		{
			eventConfig.startDate = DateTime.Parse(eventConfig.startDateSerialized);
			eventConfig.endDate = eventConfig.startDate.AddHours(eventConfig.durationHours);
			foreach (EventConfig.InternalEvent internalEvent in eventConfig.events.Values)
			{
				internalEvent.startDate = ((!string.IsNullOrEmpty(internalEvent.startDateSerialized)) ? DateTime.Parse(internalEvent.startDateSerialized) : eventConfig.startDate.AddHours(internalEvent.waitFromStartHours));
				internalEvent.endDate = ((internalEvent.durationHours <= 0.0) ? eventConfig.endDate : internalEvent.startDate.AddHours(internalEvent.durationHours));
			}
		}
	}

	public EventConfig GetEventConfig(string eventId)
	{
		EventConfig result = null;
		this.events.TryGetValue(eventId, out result);
		return result;
	}

	public EventConfig.InternalEvent GetInternalEventConfig(string eventId, string internalEventId)
	{
		EventConfig.InternalEvent result = null;
		EventConfig eventConfig = this.GetEventConfig(eventId);
		if (eventConfig != null)
		{
			eventConfig.events.TryGetValue(internalEventId, out result);
		}
		return result;
	}

	public Dictionary<string, EventConfig> events;
}
