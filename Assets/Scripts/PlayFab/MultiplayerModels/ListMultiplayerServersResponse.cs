using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListMultiplayerServersResponse : PlayFabResultCommon
	{
		public List<MultiplayerServerSummary> MultiplayerServerSummaries;

		public int PageSize;

		public string SkipToken;
	}
}
