using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UninkOpenIdConnectRequest : PlayFabRequestCommon
	{
		public string ConnectionId;
	}
}
