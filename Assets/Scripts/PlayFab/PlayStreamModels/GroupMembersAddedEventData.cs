using System;
using System.Collections.Generic;

namespace PlayFab.PlayStreamModels
{
	public class GroupMembersAddedEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public List<Member> Members;

		public string RoleId;

		public string RoleName;
	}
}
