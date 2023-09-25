using System;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class BuildRegion
	{
		public int MaxServers;

		public AzureRegion? Region;

		public int StandbyServers;

		public string Status;
	}
}
