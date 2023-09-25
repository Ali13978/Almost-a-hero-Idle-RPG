using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithNintendoSwitchDeviceIdRequest : PlayFabRequestCommon
	{
		public bool? CreateAccount;

		public string EncryptedRequest;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		[Obsolete("No longer available", true)]
		public bool? LoginTitlePlayerAccountEntity;

		public string NintendoSwitchDeviceId;

		public string PlayerSecret;

		public string TitleId;
	}
}
