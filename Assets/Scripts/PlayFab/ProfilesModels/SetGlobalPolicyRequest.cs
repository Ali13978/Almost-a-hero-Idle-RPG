using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class SetGlobalPolicyRequest : PlayFabRequestCommon
	{
		public List<EntityPermissionStatement> Permissions;
	}
}
