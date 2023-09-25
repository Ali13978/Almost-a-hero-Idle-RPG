using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListCertificateSummariesRequest : PlayFabRequestCommon
	{
		public int? PageSize;

		public string SkipToken;
	}
}
