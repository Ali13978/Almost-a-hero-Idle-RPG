using System;

namespace PlayFab.PlayStreamModels
{
	public class GroupRoleCreatedEventData : PlayStreamEventBase
	{
		public string CreatorEntityId;

		public string CreatorEntityType;

		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public string RoleId;

		public string RoleName;
	}
}
