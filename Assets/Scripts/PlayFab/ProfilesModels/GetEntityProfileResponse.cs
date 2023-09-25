using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class GetEntityProfileResponse : PlayFabResultCommon
	{
		public EntityProfileBody Profile;
	}
}
