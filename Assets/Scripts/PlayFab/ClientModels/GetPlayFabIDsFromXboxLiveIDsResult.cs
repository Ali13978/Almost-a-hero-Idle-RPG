using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromXboxLiveIDsResult : PlayFabResultCommon
	{
		public List<XboxLiveAccountPlayFabIdPair> Data;
	}
}
