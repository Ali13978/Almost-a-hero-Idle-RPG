using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LinkPSNAccountRequest : PlayFabRequestCommon
	{
		public string AuthCode;

		public bool? ForceLink;

		public int? IssuerId;

		public string RedirectUri;
	}
}
