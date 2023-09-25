using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithOpenIdConnectRequest : PlayFabRequestCommon
	{
		public string ConnectionId;

		public bool? CreateAccount;

		public string EncryptedRequest;

		public string IdToken;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		[Obsolete("No longer available", true)]
		public bool? LoginTitlePlayerAccountEntity;

		public string PlayerSecret;

		public string TitleId;
	}
}
