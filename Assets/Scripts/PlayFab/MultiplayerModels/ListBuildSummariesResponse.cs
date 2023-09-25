using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListBuildSummariesResponse : PlayFabResultCommon
	{
		public List<BuildSummary> BuildSummaries;

		public int PageSize;

		public string SkipToken;
	}
}
