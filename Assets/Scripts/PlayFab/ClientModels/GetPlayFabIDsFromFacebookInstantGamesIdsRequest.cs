using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromFacebookInstantGamesIdsRequest : PlayFabRequestCommon
	{
		public List<string> FacebookInstantGamesIds;
	}
}
