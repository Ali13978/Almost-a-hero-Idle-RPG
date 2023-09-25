using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupMembersResponse : PlayFabResultCommon
	{
		public List<EntityMemberRole> Members;
	}
}
