using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class GetPlayFabIDsFromGenericIDsResult : PlayFabResultCommon
	{
		public List<GenericPlayFabIdPair> Data;
	}
}
