using System;
using System.Collections.Generic;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UserAccountInfo
	{
		public UserAndroidDeviceInfo AndroidDeviceInfo;

		public DateTime Created;

		public UserCustomIdInfo CustomIdInfo;

		public UserFacebookInfo FacebookInfo;

		public UserFacebookInstantGamesIdInfo FacebookInstantGamesIdInfo;

		public UserGameCenterInfo GameCenterInfo;

		public UserGoogleInfo GoogleInfo;

		public UserIosDeviceInfo IosDeviceInfo;

		public UserKongregateInfo KongregateInfo;

		public UserNintendoSwitchDeviceIdInfo NintendoSwitchDeviceIdInfo;

		public List<UserOpenIdInfo> OpenIdInfo;

		public string PlayFabId;

		public UserPrivateAccountInfo PrivateInfo;

		public UserPsnInfo PsnInfo;

		public UserSteamInfo SteamInfo;

		public UserTitleInfo TitleInfo;

		public UserTwitchInfo TwitchInfo;

		public string Username;

		public UserWindowsHelloInfo WindowsHelloInfo;

		public UserXboxInfo XboxInfo;
	}
}
