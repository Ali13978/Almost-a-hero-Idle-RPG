using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LinkOpenIdConnectRequest : PlayFabRequestCommon
	{
		public string ConnectionId;

		public bool? ForceLink;

		public string IdToken;
	}
}
