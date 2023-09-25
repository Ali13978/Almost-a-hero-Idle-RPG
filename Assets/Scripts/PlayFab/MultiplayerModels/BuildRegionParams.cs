using System;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class BuildRegionParams
	{
		public int MaxServers;

		public AzureRegion Region;

		public int StandbyServers;
	}
}
