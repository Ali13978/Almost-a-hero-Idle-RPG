using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListAssetSummariesRequest : PlayFabRequestCommon
	{
		public int? PageSize;

		public string SkipToken;
	}
}
