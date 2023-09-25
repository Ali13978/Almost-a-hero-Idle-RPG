using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.EventsModels
{
	[Serializable]
	public class WriteEventsRequest : PlayFabRequestCommon
	{
		public List<EventContents> Events;
	}
}
