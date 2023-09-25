using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithCustomIDRequest : PlayFabRequestCommon
	{
		public bool? CreateAccount;

		public string CustomId;

		public string EncryptedRequest;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		[Obsolete("No longer available", true)]
		public bool? LoginTitlePlayerAccountEntity;

		public string PlayerSecret;

		public string TitleId;
	}
}
