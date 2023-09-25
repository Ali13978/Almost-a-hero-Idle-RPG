using System;

namespace PlayFab.PlayStreamModels
{
	public class GroupRoleUpdatedEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public RolePropertyValues NewValues;

		public RolePropertyValues OldValues;

		public string RoleId;

		public string RoleName;

		public string UpdaterEntityId;

		public string UpdaterEntityType;
	}
}
