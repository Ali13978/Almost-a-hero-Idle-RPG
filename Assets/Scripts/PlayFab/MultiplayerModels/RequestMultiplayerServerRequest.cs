using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class RequestMultiplayerServerRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public List<string> InitialPlayers;

		public List<AzureRegion> PreferredRegions;

		public string SessionCookie;

		public string SessionId;
	}
}
