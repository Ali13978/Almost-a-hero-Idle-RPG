using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class UpdateBuildRegionsRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public List<BuildRegionParams> BuildRegions;
	}
}
