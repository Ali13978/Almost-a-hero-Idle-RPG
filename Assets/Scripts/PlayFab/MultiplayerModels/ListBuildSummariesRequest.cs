using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListBuildSummariesRequest : PlayFabRequestCommon
	{
		public int? PageSize;

		public string SkipToken;
	}
}
