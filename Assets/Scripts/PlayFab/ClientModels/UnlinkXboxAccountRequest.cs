using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UnlinkXboxAccountRequest : PlayFabRequestCommon
	{
		public string XboxToken;
	}
}
