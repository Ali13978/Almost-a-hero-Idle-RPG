using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetRemoteLoginEndpointResponse : PlayFabResultCommon
	{
		public string IPV4Address;

		public int Port;
	}
}
