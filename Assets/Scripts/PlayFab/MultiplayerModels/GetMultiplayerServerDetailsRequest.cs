using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetMultiplayerServerDetailsRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public AzureRegion Region;

		public string SessionId;
	}
}
