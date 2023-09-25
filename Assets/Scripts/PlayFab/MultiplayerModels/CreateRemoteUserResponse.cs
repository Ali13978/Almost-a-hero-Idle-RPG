using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class CreateRemoteUserResponse : PlayFabResultCommon
	{
		public DateTime? ExpirationTime;

		public string Password;

		public string Username;
	}
}
