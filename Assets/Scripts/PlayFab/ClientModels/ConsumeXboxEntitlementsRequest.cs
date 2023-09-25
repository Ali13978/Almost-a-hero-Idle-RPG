using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class ConsumeXboxEntitlementsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public string XboxToken;
	}
}
