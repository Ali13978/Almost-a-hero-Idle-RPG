using System;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UserOpenIdInfo
	{
		public string ConnectionId;

		public string Issuer;

		public string Subject;
	}
}
