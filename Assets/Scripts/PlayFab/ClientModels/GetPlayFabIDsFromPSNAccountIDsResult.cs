using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromPSNAccountIDsResult : PlayFabResultCommon
	{
		public List<PSNAccountPlayFabIdPair> Data;
	}
}
