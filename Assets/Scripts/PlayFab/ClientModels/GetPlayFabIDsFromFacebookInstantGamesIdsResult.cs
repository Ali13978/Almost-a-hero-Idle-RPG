using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromFacebookInstantGamesIdsResult : PlayFabResultCommon
	{
		public List<FacebookInstantGamesPlayFabIdPair> Data;
	}
}
