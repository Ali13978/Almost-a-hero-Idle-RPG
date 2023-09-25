using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest : PlayFabRequestCommon
	{
		public List<string> NintendoSwitchDeviceIds;
	}
}
