using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class DeleteRemoteUserRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public AzureRegion Region;

		public string Username;

		public string VmId;
	}
}
