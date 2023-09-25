using System;

namespace PlayFab.PlayStreamModels
{
	public class GroupRoleDeletedEventData : PlayStreamEventBase
	{
		public string DeleterEntityId;

		public string DeleterEntityType;

		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public string RoleId;

		public string RoleName;
	}
}
