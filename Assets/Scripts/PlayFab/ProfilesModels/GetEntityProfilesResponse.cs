using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class GetEntityProfilesResponse : PlayFabResultCommon
	{
		public List<EntityProfileBody> Profiles;
	}
}
