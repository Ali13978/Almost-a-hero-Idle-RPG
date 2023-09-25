using System;

namespace PlayFab.EventsModels
{
	[Serializable]
	public class EventContents
	{
		public EntityKey Entity;

		public string EventNamespace;

		public string Name;

		public string OriginalId;

		public DateTime? OriginalTimestamp;

		public object Payload;

		public string PayloadJSON;
	}
}
