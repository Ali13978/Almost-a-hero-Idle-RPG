using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListAssetSummariesResponse : PlayFabResultCommon
	{
		public List<AssetSummary> AssetSummaries;

		public int PageSize;

		public string SkipToken;
	}
}
