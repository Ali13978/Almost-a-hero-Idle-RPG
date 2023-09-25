using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class CreateGroupRoleResponse : PlayFabResultCommon
	{
		public int ProfileVersion;

		public string RoleId;

		public string RoleName;
	}
}
