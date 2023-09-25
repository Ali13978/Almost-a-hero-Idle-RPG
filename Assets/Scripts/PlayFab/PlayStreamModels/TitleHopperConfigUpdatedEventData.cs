using System;

namespace PlayFab.PlayStreamModels
{
	public class TitleHopperConfigUpdatedEventData : PlayStreamEventBase
	{
		public bool Deleted;

		public string DeveloperId;

		public string MatchHopperId;

		public string UserId;
	}
}
