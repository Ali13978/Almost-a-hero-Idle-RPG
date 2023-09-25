using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LinkFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		public string FacebookInstantGamesSignature;

		public bool? ForceLink;
	}
}
