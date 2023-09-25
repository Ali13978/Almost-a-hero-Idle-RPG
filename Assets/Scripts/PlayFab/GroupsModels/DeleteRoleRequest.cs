using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class DeleteRoleRequest : PlayFabRequestCommon
	{
		public EntityKey Group;

		public string RoleId;
	}
}
