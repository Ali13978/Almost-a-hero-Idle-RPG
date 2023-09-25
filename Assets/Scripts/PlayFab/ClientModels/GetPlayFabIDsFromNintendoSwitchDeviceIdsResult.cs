using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromNintendoSwitchDeviceIdsResult : PlayFabResultCommon
	{
		public List<NintendoSwitchPlayFabIdPair> Data;
	}
}
