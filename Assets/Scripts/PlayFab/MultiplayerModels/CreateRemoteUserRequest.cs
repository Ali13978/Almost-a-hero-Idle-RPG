using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class CreateRemoteUserRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public DateTime? ExpirationTime;

		public AzureRegion Region;

		public string Username;

		public string VmId;
	}
}
