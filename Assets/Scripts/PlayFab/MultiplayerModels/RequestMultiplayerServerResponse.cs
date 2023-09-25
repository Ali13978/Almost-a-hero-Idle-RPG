using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class RequestMultiplayerServerResponse : PlayFabResultCommon
	{
		public List<ConnectedPlayer> ConnectedPlayers;

		public string FQDN;

		public string IPV4Address;

		public DateTime? LastStateTransitionTime;

		public List<Port> Ports;

		public AzureRegion? Region;

		public string ServerId;

		public string SessionId;

		public string State;

		public string VmId;
	}
}
