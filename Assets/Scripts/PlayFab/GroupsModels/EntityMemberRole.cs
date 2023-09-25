using System;
using System.Collections.Generic;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class EntityMemberRole
	{
		public List<EntityWithLineage> Members;

		public string RoleId;

		public string RoleName;
	}
}
