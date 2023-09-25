using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		public bool? CreateAccount;

		public string EncryptedRequest;

		public string FacebookInstantGamesSignature;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		[Obsolete("No longer available", true)]
		public bool? LoginTitlePlayerAccountEntity;

		public string PlayerSecret;

		public string TitleId;
	}
}
