using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromXboxLiveIDsRequest : PlayFabRequestCommon
	{
		public string Sandbox;

		public List<string> XboxLiveAccountIDs;
	}
}
