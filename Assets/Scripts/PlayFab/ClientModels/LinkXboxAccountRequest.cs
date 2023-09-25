using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LinkXboxAccountRequest : PlayFabRequestCommon
	{
		public bool? ForceLink;

		public string XboxToken;
	}
}
