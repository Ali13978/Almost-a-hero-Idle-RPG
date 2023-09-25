using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class UpdateGroupRoleRequest : PlayFabRequestCommon
	{
		public int? ExpectedProfileVersion;

		public EntityKey Group;

		public string RoleId;

		public string RoleName;
	}
}
