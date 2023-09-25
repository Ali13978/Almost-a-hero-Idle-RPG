using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class CreateGroupRoleRequest : PlayFabRequestCommon
	{
		public EntityKey Group;

		public string RoleId;

		public string RoleName;
	}
}
