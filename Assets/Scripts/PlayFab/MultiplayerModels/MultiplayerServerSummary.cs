using System;
using System.Collections.Generic;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class MultiplayerServerSummary
	{
		public List<ConnectedPlayer> ConnectedPlayers;

		public DateTime? LastStateTransitionTime;

		public AzureRegion? Region;

		public string ServerId;

		public string SessionId;

		public string State;

		public string VmId;
	}
}
