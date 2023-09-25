using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class SetEntityProfilePolicyRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public List<EntityPermissionStatement> Statements;
	}
}
