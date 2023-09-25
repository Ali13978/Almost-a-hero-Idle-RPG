using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UnlinkNintendoSwitchDeviceIdRequest : PlayFabRequestCommon
	{
		public string NintendoSwitchDeviceId;
	}
}
