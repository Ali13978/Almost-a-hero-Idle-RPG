using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class ConsumePSNEntitlementsRequest : PlayFabRequestCommon
	{
		public string CatalogVersion;

		public int ServiceLabel;
	}
}
