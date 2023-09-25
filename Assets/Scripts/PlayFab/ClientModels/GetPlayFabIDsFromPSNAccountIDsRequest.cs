using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromPSNAccountIDsRequest : PlayFabRequestCommon
	{
		public int? IssuerId;

		public List<string> PSNAccountIDs;
	}
}
