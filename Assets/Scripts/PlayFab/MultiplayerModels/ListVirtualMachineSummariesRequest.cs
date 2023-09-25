using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListVirtualMachineSummariesRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public int? PageSize;

		public AzureRegion Region;

		public string SkipToken;
	}
}
