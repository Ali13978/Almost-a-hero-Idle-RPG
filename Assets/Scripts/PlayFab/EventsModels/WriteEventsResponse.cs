using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.EventsModels
{
	[Serializable]
	public class WriteEventsResponse : PlayFabResultCommon
	{
		public List<string> AssignedEventIds;
	}
}
