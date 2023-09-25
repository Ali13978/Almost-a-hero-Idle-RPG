using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithPlayFabRequest : PlayFabRequestCommon
	{
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		[Obsolete("No longer available", true)]
		public bool? LoginTitlePlayerAccountEntity;

		public string Password;

		public string TitleId;

		public string Username;
	}
}
