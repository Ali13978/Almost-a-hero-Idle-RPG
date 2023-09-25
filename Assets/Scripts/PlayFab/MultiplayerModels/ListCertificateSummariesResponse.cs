using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListCertificateSummariesResponse : PlayFabResultCommon
	{
		public List<CertificateSummary> CertificateSummaries;

		public int PageSize;

		public string SkipToken;
	}
}
