using System;
using System.Collections.Generic;

namespace PlayFab.PlayStreamModels
{
	public class GroupMembersRemovedEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public List<Member> Members;
	}
}
