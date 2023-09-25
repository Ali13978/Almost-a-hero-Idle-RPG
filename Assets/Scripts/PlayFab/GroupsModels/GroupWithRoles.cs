using System;
using System.Collections.Generic;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GroupWithRoles
	{
		public EntityKey Group;

		public string GroupName;

		public int ProfileVersion;

		public List<GroupRole> Roles;
	}
}
