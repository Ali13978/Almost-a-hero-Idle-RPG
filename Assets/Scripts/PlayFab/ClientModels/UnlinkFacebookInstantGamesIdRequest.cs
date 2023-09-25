using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UnlinkFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		public string FacebookInstantGamesId;
	}
}
