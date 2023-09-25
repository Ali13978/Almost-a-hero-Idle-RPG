using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class RegisterPlayFabUserResult : PlayFabResultCommon
	{
		public EntityTokenResponse EntityToken;

		public string PlayFabId;

		public string SessionTicket;

		public UserSettings SettingsForUser;

		public string Username;
	}
}
