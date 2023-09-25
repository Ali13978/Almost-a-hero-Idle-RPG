using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetRemoteLoginEndpointRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public AzureRegion Region;

		public string VmId;
	}
}
