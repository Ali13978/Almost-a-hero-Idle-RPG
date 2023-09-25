using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class GetGlobalPolicyResponse : PlayFabResultCommon
	{
		public List<EntityPermissionStatement> Permissions;
	}
}
